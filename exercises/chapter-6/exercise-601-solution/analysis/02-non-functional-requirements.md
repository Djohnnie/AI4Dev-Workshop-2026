# Non-Functional Requirements

## NFR-1 Fast workshop setup

The first version must run locally without any external infrastructure.

## NFR-2 Simple implementation

The code must stay small and readable enough for participants to review in a workshop session.

## NFR-3 Clear error handling

Invalid URLs, invalid aliases, and duplicate aliases must return explicit errors rather than failing silently.

## NFR-4 Replaceable persistence seam

The design must keep storage behind an abstraction so in-memory storage can be replaced later.

## NFR-5 Predictable behavior

Redirect counting and statistics should behave consistently in local runs without hidden background processing.

## NFR-6 Workshop-appropriate security baseline

The app must validate destination URL format and alias format, but advanced abuse prevention is out of scope for the first slice.

## NFR-7 Local build and testability

The finished sample must build and test with targeted `dotnet build` and `dotnet test` commands from the repository root.
