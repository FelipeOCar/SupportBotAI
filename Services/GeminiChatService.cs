using System.Runtime.CompilerServices;
using System.Text;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Options;
using SupportBotAI.Models;
using SupportBotAI.Options;

namespace SupportBotAI.Services;

public sealed class GeminiChatService : IGeminiChatService
{
    private readonly GeminiOptions _options;
    private readonly ILogger<GeminiChatService> _logger;

    public GeminiChatService(IOptions<GeminiOptions> options, ILogger<GeminiChatService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async IAsyncEnumerable<string> StreamReplyAsync(
        IReadOnlyList<ChatMessage> messages,
        bool simulateTimeout,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var apiKey = string.IsNullOrWhiteSpace(_options.ApiKey)
            ? Environment.GetEnvironmentVariable("GEMINI_API_KEY")
            : _options.ApiKey;

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new AiServiceException(
                AiFailureKind.Configuration,
                "Der Gemini API-Key fehlt. Richte ihn über Visual Studio User Secrets ein.");
        }

        var timeoutSeconds = Math.Clamp(_options.TimeoutSeconds, 5, 120);
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

        try
        {
            if (simulateTimeout)
            {
                await Task.Delay(TimeSpan.FromSeconds(timeoutSeconds + 5), timeoutSource.Token);
            }

            var client = new Client(apiKey: apiKey);
            var config = new GenerateContentConfig
            {
                SystemInstruction = new Content
                {
                    Parts = new List<Part> { new() { Text = TechShopContext.SystemPrompt } }
                },
                Temperature = 0.2,
                MaxOutputTokens = Math.Clamp(_options.MaxOutputTokens, 100, 2000)
            };

            var prompt = BuildConversationPrompt(messages);
            var receivedContent = false;

            var stream = client.Models.GenerateContentStreamAsync(
                model: _options.Model,
                contents: prompt,
                config: config);

            await using var enumerator = stream.GetAsyncEnumerator(timeoutSource.Token);
            while (await enumerator.MoveNextAsync().AsTask().WaitAsync(timeoutSource.Token))
            {
                var chunk = enumerator.Current;
                var text = chunk.Candidates?
                    .FirstOrDefault()?
                    .Content?
                    .Parts?
                    .FirstOrDefault()?
                    .Text;

                if (!string.IsNullOrEmpty(text))
                {
                    receivedContent = true;
                    yield return text;
                }
            }

            if (!receivedContent)
            {
                throw new AiServiceException(
                    AiFailureKind.Unavailable,
                    "Die KI hat keine Antwort geliefert. Bitte versuche es erneut oder übergib die Anfrage an den Support.");
            }
        }
        catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested)
        {
            throw new AiServiceException(AiFailureKind.Cancelled, "Die Antwort wurde abgebrochen.", exception);
        }
        catch (OperationCanceledException exception)
        {
            throw new AiServiceException(
                AiFailureKind.Timeout,
                "Die KI hat nicht rechtzeitig geantwortet. Deine Nachricht bleibt erhalten.",
                exception);
        }
        catch (AiServiceException)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Gemini-Anfrage fehlgeschlagen");
            throw new AiServiceException(
                AiFailureKind.Unavailable,
                "Der KI-Dienst ist momentan nicht erreichbar. Bitte versuche es erneut oder übergib die Anfrage an den Support.",
                exception);
        }
    }

    private static string BuildConversationPrompt(IReadOnlyList<ChatMessage> messages)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Bisheriger Chatverlauf:");
        foreach (var message in messages.TakeLast(12))
        {
            var role = message.Role == MessageRole.User ? "Kundin/Kunde" : "SupportBot AI";
            builder.Append(role).Append(": ").AppendLine(message.Content);
        }

        builder.AppendLine();
        builder.AppendLine("Antworte jetzt auf die letzte Nachricht der Kundin oder des Kunden.");
        return builder.ToString();
    }
}
