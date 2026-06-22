# Exercise 601 — Short URL Discovery Sprint

> **Chapter:** Chapter 6, Exercise 1  
> **Skill focus:** Analysis-first greenfield planning with Copilot before implementation  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

You are starting from **nothing** except a short product brief.

Your team needs a .NET 10 application that can:

- create short URLs for long destinations
- redirect visitors from the short URL to the underlying URL
- keep simple statistics for each short link

The point of the exercise is **not** to jump straight into vibe coding. First, use Copilot to analyse the problem and turn the brief into a plan. Only after that analysis is explicit should you implement the first slice.

---

## ✅ What Participants Should Do

### Phase 1 — Analyse the brief

Start from the scenario below:

> Build a .NET 10 short-link service for workshop demos. A user should be able to submit a long URL, receive a short code, share the shortened link, and have visitors redirected to the original destination. The system should also keep lightweight statistics so facilitators can see how often a link was used.

Ask Copilot to produce:

1. functional requirements
2. non-functional requirements
3. technical architecture options
4. epics
5. user stories
6. assumptions, risks, and open questions

### Phase 2 — Review before coding

Before implementing anything, review the analysis and agree on:

- the smallest useful v1
- which endpoints are essential
- whether storage is in-memory or persistent for the workshop
- what statistics count as "good enough" for the first slice

### Phase 3 — Vibe code the approved plan

Once the analysis is clear, build the first version of the app.

Suggested minimum slice:

1. `POST /api/links` to create a short link
2. `GET /{code}` to redirect to the destination URL
3. `GET /api/links/{code}/stats` to show basic usage statistics

### Phase 4 — Compare with the sample solution

After participants finish, compare their result with the possible finished implementation in:

`..\exercise-601-solution\`

---

## 🤖 Prompt Starters

```text
Act as a staff product engineer. Turn this short-link brief into functional requirements, non-functional requirements, and open questions before we write code.
```

```text
Compare two realistic .NET 10 architecture options for this short URL service. Keep one option optimized for workshop speed and one optimized for production durability.
```

```text
Break this short-link product into epics and user stories, then identify the smallest vertical slice that still proves the workflow end to end.
```

```text
We are not coding yet. List the risks, assumptions, and decisions we should make before implementation starts.
```

---

## 🏁 Stretch Goals

1. Support custom aliases.
2. Support expiration dates.
3. Distinguish between workshop-friendly statistics and production-grade analytics.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-06/SLIDES.md)
