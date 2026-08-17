namespace SupportBotAI.Models;

public sealed class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Neue Konversation";
    public string Category { get; set; } = "Allgemein";
    public ConversationStatus Status { get; set; } = ConversationStatus.Open;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public List<ChatMessage> Messages { get; set; } = [];
}

public enum ConversationStatus
{
    Open,
    Resolved,
    Escalated
}
