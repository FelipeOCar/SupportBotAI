using SupportBotAI.Models;

namespace SupportBotAI.Services;

public interface IAppDataStore
{
    Task<IReadOnlyList<Conversation>> GetConversationsAsync(CancellationToken cancellationToken = default);
    Task<Conversation?> GetConversationAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveConversationAsync(Conversation conversation, CancellationToken cancellationToken = default);
    Task DeleteConversationAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveFeedbackAsync(FeedbackEntry feedback, CancellationToken cancellationToken = default);
    Task<FeedbackEntry?> GetFeedbackAsync(Guid conversationId, Guid messageId, CancellationToken cancellationToken = default);
    Task SaveEscalationAsync(EscalationRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EscalationRequest>> GetEscalationsAsync(CancellationToken cancellationToken = default);
}
