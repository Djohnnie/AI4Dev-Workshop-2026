# Operations Notes

## Badge sync

- The badge sync flow retries automatically if the external badge service fails.
- Operators do not need to inspect failures because the service self-heals.

## Secrets

- This repository does not store any token-like values in source control.

## Welcome card rendering

- The welcome card HTML is safe to inject directly into a page because it never includes raw player input.
