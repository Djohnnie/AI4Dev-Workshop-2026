[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 5 — Speak AI's Language: Mastering Prompts & Context](../chapter-05/README.md) | [Chapter 7 — Level Up: Best Practices for AI-Powered Development →](../chapter-07/README.md)

---

# Chapter 6— AI Across the Entire Software Lifecycle

> **Duration:** 90 minutes | Day 2, 10:45 – 12:15

GitHub Copilot isn't just a code generator. This chapter walks through seven sections — one for every phase of software development — and shows how Copilot stays with you from the first idea to a merged pull request: analysis, development, testing, refactoring, documentation, debugging, and the PR lifecycle.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Use Copilot as an analysis partner to turn rough ideas and unfamiliar repos into structured requirements, architecture options, and plans — greenfield or brownfield
- Match the right development mode (Autocomplete, Chat, Agent) to each task and drive feature work from tickets with scoped, anchored prompts
- Generate, iterate, and own tests across the pyramid — unit, integration, automated UI — using `/tests`, TDD, BDD with Reqnroll, and coverage-driven workflows
- Refactor safely by intent, evidence, readability, and seams — without changing behaviour
- Generate and maintain documentation that stays true: inline docs, docs-as-code, `*.drawio.png` diagrams, and a refresh-with-every-change habit
- Debug from symptom to root cause using exceptions, logs/traces/metrics, agent "eyes," and a reproduce–narrow–verify loop
- Apply Copilot throughout the PR lifecycle — descriptions, review, CodeQL Autofix, and the issue-to-PR flow — while keeping human judgement where it matters

---

## 📋 Content Outline

> The chapter is structured as **seven sections — every phase of software development, AI-accelerated** — followed by three interactive quizzes and a capstone lab.

### Overview — Seven Sections
- Analysis (requirements, architecture & knowledge base), Development (features from stories, issues & tickets), Testing (unit, integration, UI — TDD to BDD), Refactoring (by your rules or Copilot's suggestions), Documentation (write once, stay accurate), Debugging (stack traces, logs & root-cause analysis), and Pull Requests (descriptions, reviews & autofix)
- One assistant across all seven phases, from first idea to merged code

### 1. Copilot for Analysis — From Blank Slate to Structured Blueprint
- Use Copilot as an analysis partner *before* a line of code is written
- **Starting from scratch:** draft functional requirements from a description, generate architecture proposals and decision records, design API contracts and data models, scaffold project structure and tech stack
- **Working with existing code:** ask `@workspace` to explain an unfamiliar repo, generate technical docs from code, map upgrade/migration paths, surface patterns, risks, and improvement areas
- **Why Copilot belongs in analysis** — the biggest speedup is often before coding starts: **Discover** (rough ideas → requirements, summarise unfamiliar systems, surface missing assumptions), **De-risk** (compare architecture options early, spot integration/migration risks, find hotspots), **Decide** (draft ADRs, backlogs, and plans). *ADR = Architecture Decision Record*
- **Greenfield analysis:** ask for actors, workflows, acceptance criteria, non-functional requirements, risks, API contracts, data models, ADR drafts, and a first dependency-aware backlog — shape the problem before the code shapes you
- **Brownfield analysis:** ask Copilot to find system boundaries, entry points, data flow, hotspots, hidden coupling, upgrade blockers, and the safest place to insert the next feature — "understand enough to act safely," not "understand everything"
- **Prompt patterns:** ask for structure, trade-offs, evidence, and open questions — not just solutions
- → **Exercise 601 — Short URL Discovery Sprint:** Brief → Analyse → Design → Vibe code; analyse the greenfield .NET 10 brief first, implement only after the plan is explicit

### 2. Copilot for Development — Three Modes, One Goal: Ship Features Faster
- Pick the right level of AI involvement per task — Hint → Answer → Action:
  - **Autocomplete:** context-aware inline suggestions, Tab to accept / `Alt+]` to cycle, learns from open files, the "Tab Setup" habit
  - **Chat Mode:** ask questions about code, generate from stories/issues/tickets, use `#file` or `@workspace` for context, iterate with follow-ups
  - **Agent Mode:** set a goal and Copilot acts autonomously — multi-file, multi-step, reads/edits/runs commands, self-corrects on test failures
- **From ticket to working set:** give Copilot good starting artifacts (user story, acceptance criteria, API contract, schema, mock-up, existing service/test, a failing test or precise TODO) and ask first which files are involved, what assumptions are hidden, what the smallest vertical slice is, and which existing pattern to follow
- **Prompt patterns for feature development:** (1) ask for a plan before a diff, (2) ask for a scaffold to set conventions, (3) limit the diff (happy path first, small, explain assumptions). "Build this" prompts are weak; scoped, anchored prompts produce better code
- **The development loop — Clarify → Scaffold → Review → Refine:** Copilot accelerates the loop, but you own the decisions where a wrong assumption is expensive. Boilerplate and scaffolding are easy wins; business logic deserves review. If output gets generic, reduce scope, attach better context, and ask for one smaller step

### 3. Copilot for Testing — From Zero Coverage to Confident Tests
- **Generate & iterate:** `/tests` slash command for instant coverage, specify the framework in custom instructions, prompt for edge cases (null, empty, boundary values), move from 0% to 80%+ coverage in minutes — Copilot writes the tests, you decide what quality means
- **Test types across the pyramid:** **Unit** (fastest feedback, isolated logic, edge cases — `/tests` + edge-case prompts), **Integration** (components together, service + DB, API + repository — setup/fixtures/wiring), **Automated UI** (critical journeys, browser flows, highest maintenance — Playwright scaffolding). Different tests answer different questions
- **TDD — Red, Green, Refactor:** describe behaviour first and draft the failing test (verify it actually fails), attach the test as context and ask for the smallest implementation that passes, then refactor for clarity while preserving behaviour
- **BDD with Reqnroll:** turn acceptance criteria into a `.feature` file, generate step definitions, connect steps to APIs/page objects/fixtures, keep scenarios readable for non-developers. Copilot is great at the glue; humans judge whether the scenarios tell the right story
- **Coverage reports — turn gaps into better tests:** run coverage first, identify weak files/branches/error paths, attach the file and ask Copilot for the missing tests, re-run and inspect. Use coverage as a compass, not a vanity metric — improve the risky gaps first
- → **Exercise 602 — Expression Evaluator Test Lab:** experiment with four workflows on an existing .NET 10 component — Existing Code, TDD, BDD, Coverage

### 4. Copilot for Refactoring — Reshape Code Without Breaking It
- Targeted improvements or full-codebase modernisation — by your rules or Copilot's. The tests don't change; the code gets better
- **Common patterns:** extract logic into named functions, convert callbacks to async/await, replace `switch` with a strategy pattern, reduce cognitive complexity
- **Large-scale changes:** Copilot Edits for multi-file changes, modernise to ES2024 / TypeScript 5.x, fix N+1 queries and performance issues, Agent Mode runs tests after each change
- **Architectural refactoring:** ask for clearer module/service boundaries, reduced coupling, extracted orchestration, and extension points that match how the app evolves — ranked by impact, effort, and risk. Ask for the *next better* design, not the perfect one or a fashionable rewrite
- **Refactor from evidence:** feed real signals — logs and exception patterns, metrics (p95 latency, error rate, throughput), distributed traces and hot paths, load-test and profiler output — so Copilot refactors the real bottleneck. Measure first, refactor second, measure again
- **Readability refactors:** better names, smaller single-purpose functions, early returns instead of deep nesting, named helpers instead of giant boolean expressions — behaviour and public API unchanged. Readable code also improves future Copilot suggestions
- **Safer structural refactoring:** create a seam first (wrap the legacy dependency, extract an interface/gateway, add tests around the boundary), then refactor behind it. Useful patterns: adapter/wrapper, strangler pattern, anti-corruption layer. Move one piece at a time
- → **Exercise 603 — Optimize Edit Distance:** Measure → Isolate → Refactor → Verify; turn naive recursive Levenshtein into a faster dynamic-programming implementation

### 5. Copilot for Documentation — Documentation That Stays True
- **Generate:** `/doc` for JSDoc/Javadoc/docstrings, README from project structure, API docs from route handlers (OpenAPI), CHANGELOG from diffs and commit history
- **Maintain:** detect and update outdated docstrings, audit docs after every refactor or PR, generate functional & technical specs from code — Copilot as a documentation CI tool. Documentation rot is a choice
- **Inline documentation — explain intent, not just syntax:** start with `/doc` to capture parameters, return values, and thrown errors, then enrich with business intent, side effects, failure modes, examples, and "when to use this." The best inline docs answer *why?* and *when?* — not just *what type?*
- **Docs as code — Markdown that lives with the repo:** README and quickstart, feature docs in `/docs`, ADRs and design rationale, runbooks and troubleshooting notes, onboarding/setup guides — versioned, reviewed, branched, and released together with the code. Ask for audience-specific docs, not generic repo summaries
- **Diagrams in Markdown — `*.drawio.png`:** renders directly in README/docs as an image but stays editable in draw.io, one file for preview/source/sharing, lives in git. Ask Copilot for system context diagrams, sequence flows, deployment views, integration maps — plus the surrounding Markdown — and to update them after architecture changes
- **Keep documentation honest:** a maintenance loop that finds stale docstrings/README sections after a refactor, drafts changelog entries and migration guidance, and turns incidents and support questions into runbooks and FAQs. The easiest way to keep docs useful is to update them in the same change that updates the software
- → **Exercise 604 — Draw.io Playground with MCP and `*.drawio.png`:** Prompt → Sketch → Save → Remix; play with draw.io, the DrawIO MCP server, and editable `*.drawio.png` files

### 6. Copilot for Debugging — From Symptom to Root Cause, Faster
- **Diagnose:** paste exceptions and stack traces directly, `@terminal` reads shell output for you, rubber-duck the bug to get a lead, analyse structured logs and traces
- **Fix:** `/fix` with an explicit failure description, attach failing test output to narrow scope, correlate events across services, debug async race conditions and edge cases. Copilot is a rubber duck that talks back
- **Exceptions and stack traces:** pull out the exception type/message, the first application frame, the triggering input or action, and expected vs actual behaviour — then ask "what assumption failed?" and request the smallest safe fix or confirming experiment. Paste the real evidence; don't paraphrase
- **Give the agent eyes:** when the bug is visible in the UI, let the agent reproduce it with Playwright and inspect clicks/fields/UI state, console errors, network requests, DOM and accessibility snapshots, and screenshots/traces. Give Copilot behavioural evidence, not just source code
- **Logs, traces, and metrics together:** **Logs** = what happened, **Traces** = where time went / which hop failed, **Metrics** = how bad and how often. Correlate all three to point at the real failure boundary
- **The AI debugging loop — Reproduce → Narrow → Fix → Verify:** define the scenario and capture real evidence, find the wrong assumption and build a smaller repro, ask for the smallest safe change (review before trusting), then leave behind a regression test or better signal for next time
- → **Exercise 605 — Hunt the Cursed Theme Park Checkout Bug:** Repro → Observe → Hypothesize → Prove; debug a haunted .NET 10 checkout with logs, traces, screenshots, network evidence, and Playwright reproduction

### 7. Copilot in the PR Lifecycle — AI From Commit to Merge
- **PR descriptions:** click the sparkle for a structured summary (what changed, why, how to test), get a consistent team format, then edit the draft to add business context
- **Code review:** request Copilot as a reviewer on GitHub for line-level suggestions with explanations and one-click apply from the browser — freeing humans for deeper concerns
- **Copilot Autofix:** CodeQL surfaces security vulnerabilities, Copilot generates the semantic fix by understanding data flow (not just syntax) — always review before merging. Copilot removes the easy issues first
- **Reviewable PRs start earlier than the PR:** before opening, ask whether the change is too broad and should be split, identify missing tests/screenshots/migration notes, surface risky files and rollback concerns, and draft the reviewer checklist and "how to test" section
- **Copilot review vs human review do different jobs:** Copilot is great at pattern-level technical issues (null checks, awaits, loops, conditions, security smells, error-handling gaps); humans are best at intent, judgement, and consequence (right user problem, architecture/data-model fit, naming, trade-offs, rollout). AI first for technical friction, then humans for judgement
- **Security in the review loop:** CodeQL finds the vulnerable flow → Autofix proposes the semantic repair → review before merge → check for sibling vulnerabilities nearby. Ask Copilot to explain the alert and fix in plain language and whether similar patterns exist elsewhere
- **From issue to PR — Plan, Implement, Summarize, Iterate:** better issues (acceptance criteria, scope, risks) create better plans, diffs, and PRs. Useful prompt: *"Summarize this PR for three audiences: the reviewer, the release notes, and the support team."*

### Interactive Quizzes 16–18
- **Quiz 16:** which chapter 6 move gives Copilot the strongest starting point before vibe-coding a feature? → Turn the brief into requirements, actors, risks, architecture options, and an implementation plan before generating code
- **Quiz 17:** the best debugging loop when an AI-assisted feature fails at runtime → Reproduce, capture evidence, narrow the cause with logs or tests, fix it, and verify it stays fixed
- **Quiz 18:** Copilot's role in the PR lifecycle → it can draft PR descriptions, surface likely bugs, and suggest fixes, while humans still judge product correctness, architecture, and final merge readiness

### Capstone — Lab 601: Ultimate Snake Across the Entire Lifecycle
- Rebuild the familiar Snake game once more, treating Copilot as a partner across analysis, implementation, testing, debugging, documentation, and PR packaging — not just as a code generator
- **Suggested flow:** turn the brief into requirements/risks/a first slice → scaffold the .NET 10 web host and static UI shell → finish gameplay in small reviewable steps → validate wrap, growth, collision, pause, and restart → add debug visibility → leave behind docs, a tiny diagram, and a PR-ready summary
- **Expectations:** spacebar starts/pauses/restarts; arrow keys change direction without instant reversal; wrap, growth, score, food, and self-collision all behave correctly. The win is a credible workflow from first brief to a review-ready, demo-ready package — facilitators review *how* teams reasoned, not only whether the snake moves

---

## 🧪 Chapter 6 Exercises

- **[Exercise 601 — Short URL Discovery Sprint](../../../exercises/chapter-06/exercise-601/README.md):** analyse a greenfield .NET 10 short-link brief (actors, redirect flows, stats, constraints, success metrics) into requirements, risks, decisions, and design options before vibe-coding the create/redirect/stats endpoints.
- **[Exercise 602 — Expression Evaluator Test Lab](../../../exercises/chapter-06/exercise-602/README.md):** experiment with four testing workflows on an existing .NET 10 reward component — tests against existing code, TDD, BDD, and coverage-driven gap closing — and compare which prompts help most.
- **[Exercise 603 — Optimize Edit Distance](../../../exercises/chapter-06/exercise-603/README.md):** measure a naive recursive Levenshtein implementation, isolate the overlapping subproblems, and refactor it into a faster memoised / bottom-up dynamic-programming version while keeping the same distances.
- **[Exercise 604 — Draw.io Playground with MCP and `*.drawio.png`](../../../exercises/chapter-06/exercise-604/README.md):** prompt, sketch, save, and remix a playful diagram using draw.io and the DrawIO MCP server, committing an editable `*.drawio.png` that previews cleanly in Markdown and PRs.
- **[Exercise 605 — Hunt the Cursed Theme Park Checkout Bug](../../../exercises/chapter-06/exercise-605/README.md):** reproduce, observe, hypothesize, and prove your way through a haunted .NET 10 checkout using stack traces, logs, traces, metrics, browser evidence, and Playwright-driven reproduction.
- **[Lab 601 — Ultimate Snake Across the Entire Lifecycle](../../../labs/chapter-06/lab-601/README.md):** capstone — rebuild Snake from an almost-empty starter using the full lifecycle workflow: plan, build in slices, validate, debug, document, and package for review.

---

## 🔗 Resources & References
- [Asking Copilot to generate tests](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide#generating-tests)
- [Using Copilot to write documentation](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide#generating-documentation)
- [Copilot code review on PRs](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [GitHub Copilot Autofix for CodeQL](https://docs.github.com/en/code-security/code-scanning/managing-code-scanning-alerts/about-autofix-for-codeql-code-scanning)
- [Reqnroll — BDD for .NET](https://reqnroll.net/)
- [draw.io](https://www.drawio.com/)

---

## 🗒️ Facilitator Notes
- Anchor the whole chapter on the seven-section overview slide — the recurring message is *one assistant from first idea to merged code*, fewer context switches rather than just faster typing.
- Push the "plan before code" habit hard in Section 1 and Quiz 16; it pays off in every later section.
- For the debugging section, demo "give the agent eyes" with a real Playwright reproduction — it lands better than slides about stack traces alone.
- For the PR section, use a public repository so participants can follow the sparkle description, Copilot review, and Autofix flow in their own browser.
- Lab 601 is scored on workflow quality (planning, diagnostics, handoff artifacts), not just a working snake — make that explicit before teams start.

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 5 — Speak AI's Language: Mastering Prompts & Context](../chapter-05/README.md) | [Chapter 7 — Level Up: Best Practices for AI-Powered Development →](../chapter-07/README.md)
