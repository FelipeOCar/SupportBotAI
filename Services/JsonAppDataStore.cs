using System.Text.Json;
using System.Text.Json.Serialization;
using SupportBotAI.Models;

namespace SupportBotAI.Services;

public sealed class JsonAppDataStore : IAppDataStore
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public JsonAppDataStore(IWebHostEnvironment environment)
    {
        var directory = Path.Combine(environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(directory);
        _filePath = Path.Combine(directory, "supportbot-data.json");
    }

    public async Task<IReadOnlyList<Conversation>> GetConversationsAsync(CancellationToken cancellationToken = default)
    {
        var data = await ReadAsync(cancellationToken);
        return data.Conversations.OrderByDescending(item => item.UpdatedAt).ToList();
    }

    public async Task<Conversation?> GetConversationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await ReadAsync(cancellationToken);
        return data.Conversations.FirstOrDefault(item => item.Id == id);
    }

    public Task SaveConversationAsync(Conversation conversation, CancellationToken cancellationToken = default) =>
        UpdateAsync(data =>
        {
            conversation.UpdatedAt = DateTimeOffset.Now;
            var index = data.Conversations.FindIndex(item => item.Id == conversation.Id);
            if (index >= 0)
            {
                data.Conversations[index] = conversation;
            }
            else
            {
                data.Conversations.Add(conversation);
            }
        }, cancellationToken);

    public Task DeleteConversationAsync(Guid id, CancellationToken cancellationToken = default) =>
        UpdateAsync(data => data.Conversations.RemoveAll(item => item.Id == id), cancellationToken);

    public Task SaveFeedbackAsync(FeedbackEntry feedback, CancellationToken cancellationToken = default) =>
        UpdateAsync(data =>
        {
            data.FeedbackEntries.RemoveAll(item =>
                item.ConversationId == feedback.ConversationId && item.MessageId == feedback.MessageId);
            data.FeedbackEntries.Add(feedback);
        }, cancellationToken);

    public async Task<FeedbackEntry?> GetFeedbackAsync(
        Guid conversationId,
        Guid messageId,
        CancellationToken cancellationToken = default)
    {
        var data = await ReadAsync(cancellationToken);
        return data.FeedbackEntries.FirstOrDefault(item =>
            item.ConversationId == conversationId && item.MessageId == messageId);
    }

    public Task SaveEscalationAsync(EscalationRequest request, CancellationToken cancellationToken = default) =>
        UpdateAsync(data =>
        {
            data.EscalationRequests.Add(request);
            var conversation = data.Conversations.FirstOrDefault(item => item.Id == request.ConversationId);
            if (conversation is not null)
            {
                conversation.Status = ConversationStatus.Escalated;
                conversation.UpdatedAt = DateTimeOffset.Now;
            }
        }, cancellationToken);

    public async Task<IReadOnlyList<EscalationRequest>> GetEscalationsAsync(CancellationToken cancellationToken = default)
    {
        var data = await ReadAsync(cancellationToken);
        return data.EscalationRequests.OrderByDescending(item => item.CreatedAt).ToList();
    }

    private async Task<AppDataDocument> ReadAsync(CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            return await ReadUnsafeAsync(cancellationToken);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task UpdateAsync(Action<AppDataDocument> update, CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            var data = await ReadUnsafeAsync(cancellationToken);
            update(data);
            await WriteUnsafeAsync(data, cancellationToken);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<AppDataDocument> ReadUnsafeAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_filePath))
        {
            var seed = SeedData.Create();
            await WriteUnsafeAsync(seed, cancellationToken);
            return seed;
        }

        try
        {
            await using var stream = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<AppDataDocument>(stream, _jsonOptions, cancellationToken)
                   ?? new AppDataDocument();
        }
        catch (JsonException)
        {
            var backup = $"{_filePath}.{DateTime.Now:yyyyMMddHHmmss}.invalid";
            File.Copy(_filePath, backup, overwrite: true);
            var reset = SeedData.Create();
            await WriteUnsafeAsync(reset, cancellationToken);
            return reset;
        }
    }

    private async Task WriteUnsafeAsync(AppDataDocument data, CancellationToken cancellationToken)
    {
        var temporaryPath = $"{_filePath}.tmp";
        await using (var stream = File.Create(temporaryPath))
        {
            await JsonSerializer.SerializeAsync(stream, data, _jsonOptions, cancellationToken);
        }

        File.Move(temporaryPath, _filePath, overwrite: true);
    }
}
