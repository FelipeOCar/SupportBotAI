using SupportBotAI.Models;

namespace SupportBotAI.Services;

public interface IGeminiChatService
{
    IAsyncEnumerable<string> StreamReplyAsync(
        IReadOnlyList<ChatMessage> messages,
        bool simulateTimeout,
        CancellationToken cancellationToken = default);
}

public enum AiFailureKind
{
    Configuration,
    Timeout,
    Unavailable,
    Cancelled
}

public sealed class AiServiceException : Exception
{
    public AiServiceException(AiFailureKind kind, string userMessage, Exception? innerException = null)
        : base(userMessage, innerException)
    {
        Kind = kind;
        UserMessage = userMessage;
    }

    public AiFailureKind Kind { get; }
    public string UserMessage { get; }
}
