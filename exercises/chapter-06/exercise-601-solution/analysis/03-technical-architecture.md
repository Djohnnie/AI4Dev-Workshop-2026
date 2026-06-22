# Technical Architecture

## Recommended Workshop Architecture

Build the solution as a **single .NET 10 minimal API** with a small service layer and an in-memory repository.

## Components

### Endpoints

Minimal API endpoints own HTTP routing and response shaping.

### Service layer

The service layer owns:

- request validation
- short-code generation coordination
- duplicate handling
- redirect counting workflow

### Repository layer

The repository abstraction owns storage and retrieval of short links.

### Domain model

`ShortLink` holds:

- short code
- destination URL
- created timestamp
- redirect count
- last redirect timestamp

## Why this shape fits the exercise

1. It is fast to understand.
2. It preserves a clean analysis-to-code story.
3. It allows a later swap from in-memory storage to SQLite or another persistent store.

## Storage Decision

Use **in-memory storage** for the workshop sample.

### Reason

- no infrastructure setup
- minimal ceremony
- focus stays on analysis and first implementation

### Consequence

Data does not survive app restarts. This is acceptable for the exercise but should be revisited for production.

## Possible Production Evolution

If the scenario grows, the next step would be:

1. replace the repository with SQLite or PostgreSQL
2. add richer analytics
3. add authentication and authorization
4. add abuse prevention for redirects
