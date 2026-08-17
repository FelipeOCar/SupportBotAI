namespace SupportBotAI.Models;

public sealed class AppDataDocument
{
    public List<Conversation> Conversations { get; set; } = [];
    public List<FeedbackEntry> FeedbackEntries { get; set; } = [];
    public List<EscalationRequest> EscalationRequests { get; set; } = [];
}
