namespace AgentOrchestration.Model;

public class DebugChat
{
    public bool IsQuestion { get; set; }
    public string AgentName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
