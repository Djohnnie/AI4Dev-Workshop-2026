# Functional Requirements

## Goal

Provide a .NET 10 short-link service that can create short URLs, redirect visitors, and expose lightweight statistics for each link.

## Core Capabilities

### FR-1 Create a short link

The system must accept a destination URL and return a short code that can be shared.

### FR-2 Support valid web destinations

The system must accept absolute `http` and `https` URLs only.

### FR-3 Support optional custom aliases

The system should allow a caller to request a custom short code when it meets validation rules and is not already in use.

### FR-4 Redirect visitors

The system must resolve `GET /{code}` requests and redirect the visitor to the stored destination URL.

### FR-5 Handle unknown short codes

The system must return a clear not-found response when a short code does not exist.

### FR-6 Track total redirects

The system must increment a redirect counter whenever a known short code is resolved successfully.

### FR-7 Track last redirect time

The system must store the last time a short link was used.

### FR-8 Return per-link statistics

The system must expose a stats endpoint that returns:

- short code
- destination URL
- created timestamp
- redirect count
- last redirect timestamp

### FR-9 List created links for workshop inspection

The system should expose a list endpoint so facilitators can inspect all links created during the exercise.

## API Shape for the First Slice

- `POST /api/links`
- `GET /{code}`
- `GET /api/links/{code}/stats`
- `GET /api/links`
