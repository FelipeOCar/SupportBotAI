using SupportBotAI.Models;

namespace SupportBotAI.Services;

internal static class SeedData
{
    public static AppDataDocument Create()
    {
        var now = DateTimeOffset.Now;
        return new AppDataDocument
        {
            Conversations =
            [
                new Conversation
                {
                    Title = "Retoure SoundPulse Pro",
                    Category = "Retoure",
                    Status = ConversationStatus.Resolved,
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddDays(-1).AddMinutes(4),
                    Messages =
                    [
                        new ChatMessage
                        {
                            Role = MessageRole.User,
                            Content = "Kann ich die SoundPulse Pro Kopfhörer zurückgeben?",
                            CreatedAt = now.AddDays(-1)
                        },
                        new ChatMessage
                        {
                            Role = MessageRole.Assistant,
                            Content = "Ja. Die Standardretoure ist innerhalb von 30 Tagen möglich. Der Artikel muss vollständig und möglichst in der Originalverpackung sein.",
                            CreatedAt = now.AddDays(-1).AddMinutes(1),
                            ContextSources = ["TechShop-Retourenrichtlinie"]
                        }
                    ]
                },
                new Conversation
                {
                    Title = "Dock für NovaBook Air 14",
                    Category = "Produktfrage",
                    Status = ConversationStatus.Resolved,
                    CreatedAt = now.AddDays(-2),
                    UpdatedAt = now.AddDays(-2).AddMinutes(3),
                    Messages =
                    [
                        new ChatMessage
                        {
                            Role = MessageRole.User,
                            Content = "Welches Dock passt zum NovaBook Air 14?",
                            CreatedAt = now.AddDays(-2)
                        },
                        new ChatMessage
                        {
                            Role = MessageRole.Assistant,
                            Content = "Das VisionDock 4K ist kompatibel. Es bietet HDMI, Netzwerk, USB-A und USB-C-Power-Delivery bis 100 W.",
                            CreatedAt = now.AddDays(-2).AddMinutes(1),
                            ContextSources = ["TechShop-Produktkatalog"]
                        }
                    ]
                }
            ]
        };
    }
}
