# Context Variables Playground Questions

Use these prompts as a starting point while exploring the codebase.

## Broad questions for `@workspace`

- `@workspace map the project and tell me which services matter most.`
- `@workspace explain how repository data flows into the summary endpoint.`
- `@workspace are there any TODO comments or demo-only shortcuts in this app?`

## Narrow questions for `#file`

- `#file:Services/AuthGateway.cs explain the trust boundary in this file.`
- `#file:Services/SprintHealthService.cs tell me exactly how the risk band is calculated.`
- `#file:Endpoints/PlaygroundEndpoints.cs which endpoint should I call to inspect the incident digest?`

## Comparison exercise

Ask this once without variables and once with `@workspace`:

`Explain how the incident digest is assembled.`

Then compare:

1. how many concrete file names Copilot mentioned
2. whether it identified the repository and service relationship
3. whether it noticed the TODO comments
