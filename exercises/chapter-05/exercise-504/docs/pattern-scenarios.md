# Prompt Pattern Playground Scenarios

Use the existing files to try different prompting styles without inventing new tasks from scratch.

## Comment-driven

File: `Services/ReleaseSummaryService.cs`

Ask Copilot what the leading comment implies about:

- tone
- ordering
- acceptable output length

## Test-first

Files:

- `Services/ReleaseWindowCalculator.cs`
- `PromptPatternsPlayground.Tests/ReleaseWindowCalculatorTests.cs`

Ask Copilot for missing edge cases before you read the implementation closely.

## Persona

File: `Services/SecurityHeadersPolicy.cs`

Use a security or performance reviewer lens and compare how the feedback changes.

## Stepwise

File: `Services/ImportWorkflowService.cs`

Ask Copilot to break a refactor or enhancement into small, safe steps instead of one big change.

## Diff-driven

Files:

- `Services/LegacyAuditLogger.cs`
- `Services/StructuredAuditLogger.cs`
- `Services/LegacyBillingLogger.cs`

Show the first two as the before/after pair, then ask Copilot to apply the same pattern to the third file.
