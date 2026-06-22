using AgentOrchestration.Base;
using Microsoft.Extensions.Configuration;

namespace AgentOrchestration.Agents;

internal class GeneralAgentFactory : AgentFactoryBase
{
    private readonly string _description = "An agent that can answer general questions";
    private readonly string _instructions = @"
You should answer general questions that are not related to power usage, solar energy and home battery, electric car, heating, sauna, and photos to the best of your ability.
Adhere to the following rules:
- Just use plain text, no markdown or any other formatting
- Separate every sentence with a [BR] as custom newline";

    protected override string AgentName => "GeneralAgent";
    public override string AgentDescription => _description;
    protected override string AgentInstruction => _instructions;

    public GeneralAgentFactory(IConfiguration configuration) : base(configuration) { }
}
