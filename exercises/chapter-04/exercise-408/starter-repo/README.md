# Pixel Pups Arcade API

Tiny sample repository for a workshop bug bash.

## What this repo does

This repo tracks arcade players, awards tickets, and syncs badges to an external service.

## Current endpoints and features

- player welcome cards are safe to render directly in HTML
- badge sync retries automatically if the badge service is unavailable
- no secrets are stored in source control
- admins are checked with an exact `admin` role match
- weekend mode adds an extra score bonus

## Run tests

```bash
npm test
```

## Notes

- This repo is intentionally small.
- The docs may be behind the code.
- Use it for specialist agent experiments, not for production.
