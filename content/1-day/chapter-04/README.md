[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 3 — Speak AI's Language: Mastering Prompts, Workflow & Best Practices](../chapter-03/README.md)

---

# Chapter 4 — Get Your Hands Dirty: Real-World AI in Action

> **Duration:** 90 minutes | Day 1, 15:00 – 16:30

The final session. Participants first see Copilot applied across every phase of the software lifecycle — testing, documentation, debugging, and code review — then put it all together in a focused build sprint, share what they created, and leave with a concrete plan for tomorrow.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Generate tests with `/tests`, extend them with edge cases, and apply a TDD workflow with Copilot
- Document code and projects using `/doc` and `@workspace`-grounded prompts
- Debug and refactor with `/fix`, error-paste workflows, and targeted inline Chat instructions
- Use Copilot throughout the PR lifecycle — descriptions, code review, and Autofix
- Complete a full feature development cycle (design → code → test → document → PR) using Copilot
- Articulate a personal action plan for adopting Copilot in their daily work starting tomorrow

---

## 📋 Content Outline

### 1. Copilot Across the SDLC — Power Moves (20 min)

#### Testing (8 min)
- Generating unit tests with `/tests`:
  - **How it works:** select a function or class, open inline Chat or Chat panel, run `/tests` — Copilot reads the selected code, infers expected behaviours from the implementation and its name, and generates a test file using your project's detected testing framework
  - **Specify the framework:** `/tests` alone may pick the wrong framework in projects that have migrated; attach the config file — "`#file:vitest.config.ts` `/tests`" — or set it permanently in `copilot-instructions.md`: "All tests use Vitest. Mock functions use `vi.fn()`. Component tests use `@testing-library/react`"
  - **The quality floor:** Copilot produces tests that run and pass the happy path — it will not proactively cover all failure modes; treat the first `/tests` output as a scaffold, not a finished product
  - **Iterating from the output:** follow up: "Add tests for error cases" or "Add a test for when `userId` is null" — the iterative loop is faster than trying to write a comprehensive prompt upfront

- Writing edge-case and boundary tests:
  - **Why you must ask explicitly:** Copilot's training data contains far more happy-path tests than edge-case tests — `/tests` alone will typically generate one or two "it works" tests and miss all failure modes; always add a follow-up for boundary and error cases
  - **The edge-case prompt:** "Add tests for: null input, empty string, empty array, negative numbers, values over `Number.MAX_SAFE_INTEGER`, and invalid date strings" — an explicit list produces more comprehensive output than asking generically for "edge cases"
  - **Error path tests:** "Add tests that verify the function throws a `ValidationError` for invalid inputs" — explicitly requesting assertions on exceptions covers the code paths most likely to contain bugs in production

- TDD with Copilot — the accelerated loop:
  1. **Write (or generate) the failing test:** describe desired behaviour as "should" statements in Chat — "Generate a failing test for `validateEmail`: it should return null for empty input, throw `ValidationError` for emails without `@`, and return the email lowercased for valid input" — the test *becomes* the specification
  2. **Ask Copilot to write the passing implementation:** "`#file:email.test.ts` Write an implementation of `validateEmail` that makes all these tests pass" — Copilot reads each assertion and reverse-engineers a compliant implementation
  3. **Refactor with tests as safety net:** "The tests pass. Refactor the implementation to eliminate the nested ternary and extract the validation logic into a named helper" — tests prevent regressions during the refactor

#### Documentation (5 min)
- Generating JSDoc/docstrings with `/doc`:
  - **How it works:** select a function, class, or method; open inline Chat; run `/doc` — Copilot infers parameter names and types, return type, purpose (from name, body logic, surrounding code), and any exceptions thrown
  - **The quality ceiling:** Copilot documents what the code *does*, not what the developer *intended* — if the function name is misleading or the logic is unclear, the generated doc faithfully describes the wrong thing; always review for semantic accuracy
  - **Prompting for richer docs:** `/doc` alone produces minimal correct docs; follow up: "Also document when callers should prefer this over `formatDateShort`, and include a usage example" — the follow-up transforms a skeleton into genuinely useful documentation

- Writing README files and API docs:
  - **The @workspace pattern:** "`@workspace` Create a README for this project. Include: a one-paragraph description, installation instructions, quickstart example, configuration options, and links to test and deployment docs" — `@workspace` gathers the project structure, `package.json`, entry point, and existing docs to ground the generation
  - **What Copilot gets right automatically:** project name, tech stack, file structure, environment variables, basic usage patterns — all inferred from the codebase without extra input from you
  - **What you still need to supply:** the "why" (motivation, problem it solves), known limitations, contribution guidelines, production deployment considerations — not derivable from code; add these in a follow-up or manually
  - **Keeping docs in sync:** when you refactor a function, select it + its docstring and use inline Chat: "This function has changed — it can now return null. Update the docstring to accurately describe the new behaviour"; use this as a routine step after every refactor, not an occasional cleanup

#### Debugging & Refactoring (4 min)
- Paste errors directly into Chat:
  - **The direct approach:** copy the full error (including stack trace) and paste into Chat with context — "I got this error calling `createUser` with an empty email string: [paste]" — Copilot identifies the error type, traces it to the likely source, and suggests a fix; "what is causing this?" and "how do I fix this?" often produce different, complementary answers — ask for root cause first
  - **For cryptic errors:** paste the full output verbatim without editing; Copilot handles compiler output, linker errors, Python tracebacks, JVM stack traces, and shell error codes with equal ability
  - **Rubber duck debugging:** describe the bug in plain English before pasting any code — "This function occasionally sends duplicate notifications when two requests arrive within 100ms" — Copilot will often identify the likely cause from the description alone; articulating the problem reveals the solution surprisingly often

- Using `/fix` and inline Chat for targeted refactoring:
  - **Make the failure mode explicit:** `/fix` alone gives Copilot no information about what's wrong — add a description: "`/fix` This function crashes on an empty array because it calls `.reduce()` without an initial value" — the description dramatically improves fix accuracy
  - **Common refactoring commands:** "Extract this block into a separate named function", "Convert this callback to async/await — preserve all error handling, do not change the public signature", "Rewrite this loop to avoid the N+1 query using a single batch `findMany` with an `IN` clause" — these are reliable, mechanical refactors Copilot handles consistently well
  - **Multi-file refactors:** when a change touches 3+ files, open Copilot Edits (Ctrl+Shift+I), add all relevant files to the working set, and describe the change — Copilot generates coordinated diffs for each file simultaneously

#### The Pull Request Lifecycle (3 min)
- Copilot-generated PR descriptions:
  - **How to trigger:** on GitHub.com when creating a PR, click the Copilot sparkle icon in the description field — Copilot reads the full diff and generates a structured summary: "What Changed", "Why", and "How to Test"
  - **Treat it as a first draft:** add the business rationale, screenshots, links to the issue, known limitations, and deployment notes — context that can't be inferred from the diff; remove anything obvious

- Copilot code review:
  - **Requesting it:** in the PR's Reviewers panel on GitHub.com, type "Copilot" and select it — Copilot begins the review immediately; it completes within 1–2 minutes for most PRs
  - **What it catches:** logic errors (off-by-one, missing null checks, incorrect boolean logic), security anti-patterns (SQL injection, XSS risk, missing auth checks), performance concerns (N+1, unbounded loops), error handling gaps; every comment explains *why* the change is suggested, not just *what* to change
  - **Copilot Autofix:** when GitHub Advanced Security (CodeQL) finds a security alert, Autofix generates a suggested fix grounded in semantic code analysis — review before applying; Copilot may fix the specific vulnerability while inadvertently changing surrounding behaviour

---

### 2. Capstone Build Sprint (40 min)

Participants work individually or in pairs. The goal: complete a full development cycle using Copilot at every step.

#### The Challenge Brief (3 min)
- **Suggested project: "Mini REST API with authentication, validation, and tests"** — 2–3 endpoints (`POST /register`, `POST /login`, `GET /me`), authentication middleware, input validation, error responses, at least one database interaction (in-memory is fine)
  - **Why this works:** it touches every Copilot skill from today — scaffolding (Phase 1), completions and Agent Mode (Phase 2), `/tests` with edge cases (Phase 3), `/doc` and PR description (Phase 4); it produces a portable artifact participants can share with colleagues
  - **Alternative:** if participants prefer a different domain (data processing CLI, front-end search component), they can self-scope to the same 4-phase structure
  - **Own project option:** participants may use their real project for maximum relevance — pick a well-scoped, self-contained feature that fits in 30 minutes with Copilot; the same 4-phase structure applies

- **Starter repos:** pre-provisioned with the runtime and framework installed; application code starts empty; available as GitHub Codespaces for zero-setup; minimum TypeScript/Express, Python/FastAPI variants

- **Requirements format:** written as a GitHub Issue with acceptance criteria and deliberate ambiguities — participants must clarify the ambiguities with Copilot Chat before writing code, modelling the "clarify before coding" habit from Chapter 3

#### Phase 1 — Design & Scaffold (8 min)
- Create a `copilot-instructions.md` for the project:
  - **What to write first:** tech stack one-liner, file naming convention, test framework and file location, error handling style ("throw typed errors, never return error objects") — four lines anchors every subsequent Copilot interaction to the correct context
  - **Do this before writing a single line of application code** — teams that skip the instructions file produce less consistent output and may not understand why

- Use Copilot Chat to turn the requirements into a file/function structure:
  - **The planning prompt:** paste the requirements and ask "Based on these requirements, what files and functions do I need? Output a folder structure and a brief description of each file's responsibility" — produces a concrete plan to react to rather than designing from scratch
  - **Evaluate critically:** does the proposed structure make sense for your framework? Does it separate concerns appropriately? Adjust before starting implementation — changing structure after writing 10 files is expensive

- Scaffold with Agent Mode:
  - **The ideal Agent Mode task:** "Create the folder structure above and generate boilerplate files for each. Include imports, empty function stubs, and JSDoc stubs — no implementation yet" — Agent Mode creates all files in one coordinated action
  - **What good scaffolding looks like:** each file exists, imports are correct, function signatures match the planned interface, placeholder comments indicate what each function should do — the scaffold compiles before implementation begins
  - **60-second sanity check:** open each generated file and verify it looks reasonable before starting Phase 2 — faster than debugging a structural mistake mid-implementation

#### Phase 2 — Implementation (18 min)
- Write code using completions and inline Chat:
  - **The flow state goal:** Copilot should be generating 60–80% of the code while the developer reviews, accepts, and occasionally corrects; position the cursor inside each stub function — the JSDoc stub from Phase 1 acts as a specification
  - **Inline Chat for targeted fixes:** when a completion misses the mark, select the generated code and use inline Chat — "This doesn't handle the case where the user already exists — add a check that throws a `UserAlreadyExistsError`"; targeted corrections are faster than dismissing and re-prompting from scratch
  - **Stay in the editor:** if a question arises about a library API, ask Chat before opening a browser — "In Express 5, how does error-handling middleware signature differ from regular middleware?" is faster via Chat and scoped to your framework version

- Use Agent Mode for multi-file generation:
  - **When to escalate:** if implementing a feature requires coordinated changes across 3+ files (route handler + service + validation schema + app mount), Agent Mode handles the coordination automatically
  - **The agent prompt:** "Implement `POST /register` end-to-end. Create the route handler in `routes/auth.ts`, business logic in `services/authService.ts`, Zod schema in `schemas/userSchema.ts`, and update `app.ts` to mount the router. Follow the patterns already established in the scaffold"
  - **Monitor the agent:** watch what files it creates or modifies — if it edits files outside the intended scope, use the pause/stop control to redirect it; Agent Mode has no implicit understanding of scope boundaries unless explicitly stated
  - **Fallback to Copilot Edits:** if Agent Mode feels too autonomous, Copilot Edits (Ctrl+Shift+I) achieves similar multi-file coordination with explicit per-file diff review

- Use `@workspace` to query what's already been built:
  - **Mid-sprint orientation:** "`@workspace` What authentication middleware have I already written?" or "`@workspace` Does my current implementation handle the case where the database is unavailable?" — treats Copilot as living documentation of the emerging codebase
  - **Gap check against requirements:** "`@workspace` Looking at the requirements and what I've built so far, which acceptance criteria have I not yet implemented?" — rapid gap analysis without manually reading both documents

#### Phase 3 — Testing (8 min)
- Generate tests with `/tests` for each non-trivial function:
  - **Starting point:** select each non-trivial function from Phase 2, run `/tests`; accept the initial output as a scaffold; expect 3–5 basic tests per function; scan the generated file before running — is it using the correct test runner syntax? Are imports correct?
  - **Run the tests:** `npm test`, `pytest`, `go test ./...` — tests should be runnable immediately; failures in Phase 3 are expected and valuable — they identify where the implementation had gaps

- Add at least 3 meaningful edge case tests:
  - **The edge case brainstorm prompt:** "Looking at the `register` function I just tested, what are 5 edge cases the happy-path tests don't cover? List them, then write the tests" — Copilot identifies its own blind spots when explicitly asked
  - **Mandatory edge cases for the REST API:** empty string for required fields; malformed email; password technically valid but extremely short; duplicate registration (same email twice); request body valid JSON but missing required fields entirely
  - **Make them meaningful:** an edge case test is only useful if it asserts on the *specific* error — not just that an error occurred, but the right HTTP status code (422 vs 400 vs 409) and the right error message

- Debug failing tests with Copilot:
  - **The debugging loop:** copy the failing test output (test name + error + stack trace) → paste into Chat: "This test is failing. Looking at the implementation and the test, what is wrong and how do I fix it?"
  - **Expect some failures:** Copilot-generated tests catching bugs in Copilot-generated implementation is the system working correctly — the TDD virtuous cycle in action; if more than 2 tests are failing at 5 minutes in, triage and prioritise real implementation bugs over test-setup issues

#### Phase 4 — Documentation & PR (6 min)
- Generate docstrings with `/doc` for public functions:
  - **Be selective:** focus on the public API surface — route handlers, service methods, exported utilities; internal helpers can remain undocumented
  - **Enrich the output:** immediately after `/doc` generates a block, add one sentence that Copilot couldn't infer — the *why* behind the design decision, a known limitation, or a usage example; generated structure + human insight produces documentation that is both fast and valuable

- Write a README section and PR description:
  - **The README prompt:** "`@workspace` Write a README section for the feature I just built. Include: what it does in one sentence, example `curl` commands for each endpoint, the expected request/response format, and the error responses. Keep it under 30 lines"
  - **PR description:** if the branch is pushed to GitHub, use the Copilot sparkle button in the PR creation form — it generates a structured summary from the diff; add the business rationale and "How to Test" steps manually; the capstone PR is a genuine portfolio artifact — keep it

---

### 3. Show & Tell (15 min)

3–4 volunteers share their screen and walk through what they built.

- **What to show:** not just the final code — the journey; encourage volunteers to show their Chat history (the prompts they used), at least one moment where Copilot surprised them (positively or negatively), and the test results; the process is more educational than the artifact
- **Facilitator asks three questions (keep each answer to 60–90 seconds):**
  1. **"What was the best prompt you used today?"** — surfaces concrete, reusable prompt patterns for the room; write the best ones on a shared whiteboard for participants to photograph; follow up: "Would you have known to write that prompt before this workshop? What's different about how you're thinking about prompting now?"
  2. **"Where did Copilot surprise you — positively or negatively?"** — normalises both success and failure; common positive surprises: Copilot inferred the correct business rule from surrounding code, generated a better error handling pattern than the participant would have written; common negative surprises: code looked correct but had a subtle logic error (reinforce: always review), suggested a deprecated API; for negative surprises: "This is exactly why human review is the non-negotiable step — Copilot gave you a head start; the correctness is your responsibility"
  3. **"What would you do differently?"** — reflection on process is the highest-value learning; common answers: "I'd write `copilot-instructions.md` before writing any code", "I'd use Agent Mode earlier for the scaffolding", "I'd write more specific edge case prompts"; capture these on a "Habits to Build" whiteboard — this feeds directly into Section 4
- **Celebrate creative approaches and honest failures equally:** teams that share and laugh at failed prompts build psychological safety around AI experimentation; the willingness to try unconventional prompts is what produces the biggest productivity wins; every failed Copilot interaction is a data point, not a reason to reject Copilot

---

### 4. Your Copilot Action Plan & Closing (10 min)

- Each participant writes down 3 things on a card:
  1. **One Copilot feature they'll use starting *tomorrow*:**
     - Not "use Copilot more" — be specific: "Set up `copilot-instructions.md` for my main project tonight", "Use Agent Mode the next time I add a new endpoint", "Try `/tests` on the untested module I've been avoiding", "Use Chat instead of Googling the next cryptic error message"
     - The "tomorrow" constraint is deliberate — the friction of starting is lowest in the 48 hours after an energising learning experience

  2. **One habit they'll change to work better with AI:**
     - Based on today — what's one thing they'll do differently? Maybe: open related files in tabs before coding, write a comment before triggering a completion, add one review pass specifically for edge cases in AI-generated code, write `copilot-instructions.md` first on every new project
     - Encourage the *smallest* habit that would have a meaningful impact — small habits that stick beat ambitious habits that don't

  3. **One thing they want to learn more about:**
     - Reframes the end of the workshop as a beginning; common answers: Copilot Extensions, Knowledge Bases, MCP and custom Agent Mode tools, measuring team-level Copilot usage, Copilot Workspace
     - Suggest participants add this as a calendar block in the next 2 weeks — not just a note; the time commitment is what converts learning intentions into outcomes

- **Facilitator summary — the three non-negotiables to repeat:**
  1. **Always review AI-generated code** — you own everything you commit, regardless of how it was generated
  2. **Context is everything** — the quality of your input (prompt, open tabs, instructions file) determines the quality of Copilot's output
  3. **Copilot amplifies skill, it doesn't replace it** — invest in your fundamentals alongside your AI tool proficiency; the developers who get the most from Copilot are the ones with the strongest underlying skills

- **Resources for continued learning:**
  - **[GitHub Copilot docs](https://docs.github.com/en/copilot):** bookmark the "What's new" page for the Copilot changelog — new features ship every 2–4 weeks; reading it monthly keeps you current without continuous deep research
  - **[GitHub Skills](https://skills.github.com/exercises/copilot-codespaces-vscode):** free interactive exercises running in GitHub Codespaces — ideal for reinforcing today's learning in the week after the workshop while muscle memory is still forming
  - **[GitHub YouTube — Copilot playlist](https://www.youtube.com/@GitHub/search?query=copilot):** short (5–15 min) demo videos of new features as they ship; the "GitHub Copilot in Minutes" series is designed for busy developers
  - **[GitHub Next (githubnext.com)](https://githubnext.com/):** experimental projects that preview where Copilot is heading — Copilot Workspace, AI-native IDEs, multi-agent development; a 6–12 month preview of what will become mainstream features

- **The closing:**
  > *"You started today uncertain about AI. You're leaving having built something real with it, having reviewed AI-generated code critically, having understood its limits, and having a specific plan for how to use it better tomorrow. That is the AI-augmented developer. That is you."*

---

## 💡 Ideas for Exercises & Interactivity

### The "Worst Prompt" Moment (during sprint)
During the sprint, participants note their worst or funniest prompt that produced a terrible result. Share at the Show & Tell — a prize for the most creative failure. Normalises experimentation and builds psychological safety.

### Swap & Review (bonus if time allows)
Once participants complete the capstone, swap code with a neighbour. Use Copilot to review, refactor, and improve the other person's code — without talking to them. Debrief: what did you change? Do they agree with the changes?

### One-Day Workshop in One Tweet
Each participant summarises the day in 280 characters. Read aloud at the closing. Captures key takeaways and provides feedback for the facilitator.

---

## 🔗 Resources & References
- [GitHub Copilot: generating tests](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide#generating-tests)
- [Copilot code review on pull requests](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [Copilot Autofix for CodeQL alerts](https://docs.github.com/en/code-security/code-scanning/managing-code-scanning-alerts/responsible-use-autofix-code-scanning)
- [GitHub Skills: Code with GitHub Copilot](https://skills.github.com/exercises/copilot-codespaces-vscode)
- [GitHub Copilot changelog](https://github.blog/changelog/label/copilot/)
- [GitHub Next: Experimental Copilot projects](https://githubnext.com/)
- [GitHub Copilot community discussions](https://github.com/orgs/community/discussions/categories/copilot)

---

## 🗒️ Facilitator Notes
- Pre-provision starter repositories in GitHub Codespaces — at minimum TypeScript/Express and Python/FastAPI variants; participants cloning and installing dependencies during the session loses 10 minutes
- The Show & Tell is the emotional highlight of the day — protect its time even if the sprint runs long; it is more important than completing Phase 4
- During the sprint, circulate actively in the first 5 minutes of Phase 1 — ensure every participant has created their `copilot-instructions.md`; this is the single highest-leverage action and the most commonly skipped
- Keep the closing energetic: the final message should send participants out the door excited to try something tomorrow, not thoughtfully cautious; end with momentum

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 3 — Speak AI's Language: Mastering Prompts, Workflow & Best Practices](../chapter-03/README.md)
