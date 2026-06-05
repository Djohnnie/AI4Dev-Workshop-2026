using AgentOrchestration.Base;
using Microsoft.Extensions.Configuration;

namespace AgentOrchestration.Agents;

internal class OrchestratorAgentFactory : AgentFactoryBase
{
    private readonly string _description = "An agent that forwards questions and commands to the correct specialized agent";
    private readonly string _instructions = @"
You should forward questions to the correct agent to the best of your ability";

    protected override string AgentName => "OrchestratorAgent";
    public override string AgentDescription => _description;
    protected override string AgentInstruction => _instructions;

    public OrchestratorAgentFactory(IConfiguration configuration) : base(configuration) { }
}
