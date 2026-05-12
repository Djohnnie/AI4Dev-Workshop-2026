[🏠 Workshop Home](../../../README.md) | [← Chapter 2 — Meet Your New Best Friend & Let It Take the Wheel](../chapter-02/README.md) | [Chapter 4 — Get Your Hands Dirty: Real-World AI in Action →](../chapter-04/README.md)

---

# Chapter 3 — Speak AI's Language: Mastering Prompts, Workflow & Best Practices

> **Duration:** 90 minutes | Day 1, 13:15 – 14:45

Great prompts unlock great output. Good habits turn daily Copilot use into compounding team productivity. This chapter combines the art of precision prompting with the discipline of an AI-augmented workflow — teaching participants how to communicate exactly what they want, review what they get, and build the habits that separate power users from casual ones.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain how Copilot assembles context and why surrounding code and open tabs matter
- Write structured prompts using the Task/Context/Examples/Constraints framework
- Use `@workspace`, `#file`, and `#selection` to give Copilot the right information
- Apply advanced prompting patterns: comment-driven, test-first, persona, and stepwise decomposition
- Follow the Think → Prompt → Review → Iterate loop to produce production-quality output
- Review AI-generated code with the appropriate level of scepticism
- Interpret GitHub Copilot usage metrics and set realistic expectations for impact
- Create prompt files and define custom agents to build a reusable team AI skill library

---

## 📋 Content Outline

### 1. How Copilot Reads Your Code (10 min)
- The context window revisited: what Copilot actually sees
  - **Not the whole project:** Copilot never sees your entire codebase at once — it sees a carefully assembled slice of the most relevant content that fits within the model's token limit; everything outside that window is invisible to the model
  - **The extension assembles the prompt:** the Copilot extension automatically gathers and ranks context signals before every request — you can influence this assembly, but you can't directly control it (except through context variables in Chat)
  - **Token budget matters:** a typical request has a context budget of 8,000–32,000 tokens depending on the model; the extension fills this budget by including the highest-priority content first and discarding lower-priority content if the budget runs out
  - **Practical implication:** if the most relevant context doesn't fit — because irrelevant open tabs, long chat history, or a large file consumed the budget — Copilot produces generic suggestions rather than project-aware ones; actively managing what Copilot sees is a learnable skill

- Priority of context signals (highest to lowest):
  1. Cursor position and surrounding code
     - **The primary signal:** the 50–200 lines above and below the cursor are the highest-priority context; they tell Copilot the language, indentation style, variable names in scope, and the specific problem being solved
     - **Fill-in-the-middle (FIM):** Copilot is trained to generate text that fits *between* a prefix (above the cursor) and a suffix (below the cursor); if a `return result;` already exists below, Copilot generates code that correctly populates `result` — sketch the structure first, let Copilot fill the middle
  2. Open editor tabs (especially similar-named files)
     - **Tab scanning is automatic:** Copilot scans all open tabs and ranks them by relevance; if you're editing `userController.ts`, tabs named `userService.ts`, `userModel.ts`, and `user.test.ts` score very highly
     - **The "tab setup" habit:** before a Copilot-assisted feature, spend 60 seconds opening the 3–5 most relevant files — the interface being implemented, related services, type definitions, the test file; this single habit has the biggest impact on suggestion quality of anything in this chapter
     - **Close irrelevant tabs:** tabs from unrelated modules consume token budget without contributing useful context; close them before a focused session
  3. Imported modules and type definitions
     - **Imports signal the available vocabulary:** the imports at the top of your file tell Copilot which libraries, types, and utilities are in scope — it uses these to generate correctly typed method calls rather than hallucinating APIs
     - **Missing imports lead to invented APIs:** if a library isn't imported, Copilot may suggest methods that don't exist in your installed version; have the right imports in place before asking for completions
  4. Comments immediately above the cursor and `copilot-instructions.md`
     - **The highest-signal manual input:** a clear, specific comment written immediately above where you want code generated is the most reliable way to guide a completion — it is read first, weighted heavily, and directly frames the generation task
     - **Always-on project context:** content from `.github/copilot-instructions.md` is injected into every Copilot request in the repository (covered in Chapter 2) — keep it current and use it to establish stack, style, and testing conventions so you don't have to repeat them in every prompt

### 2. The Anatomy of a Great Prompt (20 min)
- The four ingredients of an effective prompt:
  1. **Task** — what do you want? (verb-first: "Create", "Refactor", "Explain", "Fix")
     - **Start with a strong action verb:** "Create" implies generating from scratch; "Refactor" implies improving without changing behaviour; "Explain" implies prose output; "Fix" implies a known problem to solve — using the wrong verb produces the wrong type of response
     - **Be specific about scope:** "Create a TypeScript function that..." is stronger than "Create a function"; "Refactor to reduce cyclomatic complexity without changing the public interface" is stronger than "Refactor"
     - **One task per prompt:** resist the urge to bundle — "Create the function, add error handling, and write tests" produces lower quality than three focused prompts; Copilot trades depth for breadth when given multiple tasks
  2. **Context** — what is the environment? (language, framework, constraints)
     - **Language and runtime:** even if Copilot can detect the language from the file, stating it explicitly removes ambiguity and is essential when asking in Chat without a file open
     - **Framework and version:** "using Express 5" vs. "Express 4" produces different middleware syntax; "React 19 with the new compiler" affects whether hooks, Server Components, or Actions are appropriate suggestions
     - **Who will use this code:** "this function will be called by third-party developers via a public SDK" changes documentation requirements, error handling verbosity, and input validation needs
  3. **Examples** — show the shape of what you want (input/output examples, similar code)
     - **Input/output examples are the most powerful clarifier:** `// Input: ["alice", "BOB"] → Output: ["Alice", "Bob"]` tells Copilot the exact transformation expected better than any English description
     - **Show a similar existing function:** "Write a function similar to `formatCurrency` already in this file, but for percentages" — Copilot infers the expected style, structure, and error handling approach from the example
     - **Negative examples:** "Do NOT return an error object like `{ error: string }` — throw a typed exception instead" — showing what you don't want is as informative as showing what you do
  4. **Constraints** — what should it *not* do?
     - **Dependency constraints:** "Do not introduce any new npm dependencies" prevents Copilot from suggesting a clean solution that adds an unapproved library
     - **Compatibility constraints:** "This function must remain compatible with the existing signature — do not change parameter names or types" prevents Copilot from producing a breaking change
     - **Performance constraints:** "This function runs in a tight loop processing 1M records — optimise for speed over readability" completely changes the implementation approach

- Prompt anti-patterns to avoid:
  - **Vague tasks ("make this better"):** "better" is undefined — Copilot guesses; fix by replacing adjectives with specific, measurable goals ("Reduce cyclomatic complexity to below 5", "Add input validation to prevent SQL injection")
  - **Missing context ("write a function"):** without context, Copilot defaults to the most statistically common interpretation — always answer the implicit questions: what does it do, what are its inputs/outputs, what language and framework?
  - **Conflicting constraints ("make it fast but also use a nested loop"):** Copilot produces a compromise that satisfies neither; when constraints tension, acknowledge the tradeoff explicitly ("Use a nested loop — O(n²) is acceptable given n < 100 — but optimise the inner loop body")

- One-shot vs. few-shot prompting:
  - **One-shot:** a single prompt with no examples — works well for common, well-understood tasks where the model has strong priors (sorting, CRUD, API calls)
  - **Few-shot:** providing 1–3 examples of the output pattern before asking Copilot to apply it — significantly improves output for non-standard patterns, domain-specific formats, or specific style requirements
  - **How to apply:** paste 1–2 examples labelled "Example:", then write "Now apply the same pattern to: [your actual task]" — the model infers the pattern from examples and replicates it

- Iterative prompting: treating Copilot Chat as a conversation, not a single query:
  - **The biggest beginner mistake:** writing one long, complex prompt and expecting a perfect result; expert Copilot users write shorter initial prompts and refine through conversation — faster and more reliable
  - **The iterative loop:** prompt → review output → identify the specific gap → send a targeted follow-up ("The function looks right but it doesn't handle an empty array — fix that") → repeat until satisfied
  - **Follow-ups beat re-prompts:** instead of discarding a response and starting over, build on it — "Keep everything the same but change the error type from `Error` to `ValidationError`" is faster than re-asking from scratch
  - **When to start a new thread:** if the conversation has gone in a direction that doesn't serve your goal, start fresh — accumulated context from a derailed conversation can mislead subsequent responses

### 3. Context Variables — Feeding Copilot the Right Information (12 min)
- **`@workspace`** — query the entire codebase; best for "how does X work in this project?"
  - **What it does:** triggers a semantic search across every indexed file in the workspace — Copilot finds the most relevant code snippets across all files and injects them into the prompt automatically; you don't need to know which file contains the code
  - **Best use cases:** architectural questions ("How does this app handle authentication?"), cross-cutting investigations ("Which components use the `useAnalytics` hook?"), onboarding, understanding data flow
  - **Important caveat:** `@workspace` performs *search*, not *whole-codebase analysis* — it finds relevant snippets, not all code; for very large codebases, the highest-ranked snippets may miss edge cases in rarely-touched files
  - **Combining with a question:** "`@workspace` How is the payment flow tested?" is much better than `@workspace` alone — the question shapes which snippets are retrieved; more specific question = more targeted context

- **`#file`** — attach a specific file to the prompt
  - **What it does:** explicitly includes the full content of a named file in the Chat prompt — more reliable than relying on tab scanning because you know exactly what's in context
  - **When to prefer over `@workspace`:** when you know *exactly* which file is relevant — `#file` is precise and fast; `@workspace` is broad and slower; use `#file` for "refactor this function in `userService.ts`", use `@workspace` for "how does user authentication work across this project?"
  - **Multiple files:** "`#file:userService.ts` `#file:userModel.ts` Refactor `createUser` to use the types from the model file" — both files are included in full

- **`#selection`** — reference the current text selection
  - **What it does:** automatically includes the text currently selected in the editor in the Chat prompt — prevents copy-pasting code into the Chat window
  - **Workflow:** select the relevant code in the editor → open Chat → write your prompt — `#selection` is injected automatically in inline Chat; reference it explicitly in the panel
  - **Combining:** "`#selection` doesn't match the interface in `#file:userModel.ts` — update it to comply" — very powerful for targeted refactoring

- Demo: solving a real debugging problem using only context variables (no manual file browsing):
  - **Step 1:** "`@workspace` There's a bug where `processOrder` occasionally sends duplicate emails. Which part of the code handles email sending, and where could a race condition occur?"
  - **Step 2:** attach the identified file: "`#file:orderProcessor.ts` Explain why the email might be sent twice if two requests arrive simultaneously"
  - **Step 3:** "Suggest a fix using a distributed lock or idempotency key. Assume we have Redis available"
  - **Debrief:** the developer diagnosed and proposed a fix without opening a single file manually — purely through Copilot Chat + context variables

### 4. Advanced Prompting Patterns for Code (13 min)
- **Comment-driven development:** writing the spec as comments, letting Copilot fill the implementation
  - **The pattern:** instead of writing a function and then adding comments, write all the comments first as if they were documentation for code that doesn't exist yet, then let Copilot generate the implementation comment by comment
  - **Why it works:** comments sit immediately above the code position and have the highest weight in the context window; a detailed comment is effectively a specification that Copilot translates directly into code
  - **How to do it:** write a JSDoc block with `@param`, `@returns`, and `@throws`; add inline comments describing each major step; position the cursor below the last comment and trigger a completion — Copilot generates the full function body
  - **The discipline payoff:** forces you to think clearly about what you want *before* you write it — the comment-writing process reveals ambiguities in your own understanding before they become bugs

- **Test-first prompting:** describe the test → let Copilot write the implementation that passes it
  - **Option A — test-from-spec:** describe desired behaviour as "should" statements ("This function should return null for empty input, throw a `ValidationError` for invalid emails, and return the email lowercase for valid input") → ask Copilot to write tests matching this spec, then ask it to write the implementation
  - **Option B — implementation-from-test:** write the test yourself, then say "Write an implementation of `validateEmail` that would make all these tests pass" and attach the test file with `#file`; Copilot reads the assertions and reverse-engineers the implementation
  - **Why it produces better code:** the test constrains the solution space — Copilot can't take shortcuts that would break the test; you get an implementation that is correct by construction (with respect to the test coverage)

- **Persona prompting:** priming Copilot with a role to get more domain-focused output
  - **What it does:** activating a role activates the statistical patterns associated with that role in the model's training data — "you are a security auditor" produces more domain-specific, technically deeper answers than asking the same question without the persona
  - **Effective patterns:** "You are a security auditor reviewing this code for OWASP vulnerabilities", "You are a senior engineer with distributed systems experience — review this design for failure modes", "You are a technical writer — rewrite this README so a junior developer can follow it without assistance"
  - **Combine with constraints:** "You are a performance engineer. Review this function and suggest optimisations *without changing its public API and without introducing new dependencies*" — persona plus constraints produces precisely focused output

- **Stepwise decomposition:** breaking a complex task into steps and prompting each separately
  - **The problem it solves:** complex tasks asked all at once ("Build a REST API with JWT auth, rate limiting, and logging") produce poor results — the model navigates too many design decisions simultaneously and frequently produces shallow or inconsistent output
  - **The pattern:** decompose into 4–6 logical steps; prompt each step separately using the output of the previous step as context for the next
  - **Example decomposition:** (1) "Define the TypeScript types for a User entity and its DTOs" → (2) "Write the Prisma schema for User" → (3) "Write `UserRepository` using Prisma" → (4) "Write `UserService` with business logic" → (5) "Write the Express routes wiring the service to HTTP"
  - **When to let Agent Mode do it:** if you're using Agent Mode, it does stepwise decomposition automatically; manual stepwise prompting is most valuable in Chat mode where Agent Mode isn't appropriate

### 5. The Think → Prompt → Review → Iterate Loop (10 min)
- The AI-augmented workflow loop:
  - **Think:** before prompting, spend 30–60 seconds clarifying what you actually want — what are the inputs and outputs, what are the constraints, what would a correct result look like; the quality of this thinking directly determines the quality of the prompt
  - **Prompt:** write the prompt using the four-ingredient framework — be specific, use the right context variables, attach relevant files
  - **Review:** never accept Copilot output without reading it — check for correctness, check for edge cases, check that it matches your actual intent (not just your prompt); this step is non-negotiable
  - **Iterate:** if the output is almost right but not quite, send a targeted follow-up rather than starting over; most tasks require 2–4 iterations to reach production-quality output
  - **The exit condition:** the loop ends when you'd be comfortable with a senior colleague reviewing the output without knowing it was AI-generated — not when you think "this is probably fine"

- When to use which Copilot feature — decision rule:
  - **Completions (ghost text):** single file, local task, writing flow — a function body, a conditional branch, the next method in a class
  - **Inline Chat:** specific instruction for a specific selection — "Refactor this to use async/await", "Explain what this regex does"; stays in editor flow
  - **Chat panel:** multi-file context needed (`@workspace`, multiple `#file`), exploration, debugging, architecture discussions
  - **Copilot Edits:** change spans 2–10 files and you want to review all changes as a coordinated diff before accepting
  - **Agent Mode:** complex task that requires running code to verify — the agent iterates until tests pass; for when you'd otherwise do multiple manual run-check-fix cycles
  - **Rule of thumb:** single file, known outcome → completions or inline chat; multi-file, known outcome → Copilot Edits; complex/unknown outcome → Agent Mode; exploratory/diagnostic → Chat panel

- Git hygiene with Copilot:
  - **Generating commit messages:** in VS Code Source Control, click the Copilot sparkle icon above the commit message field — Copilot reads the staged diff and generates a conventional commit message (`type(scope): description`) explaining *why* the change was made, not just *what*
  - **Atomic commits as a forcing function:** Copilot's commit message generator implicitly encourages atomic commits — if the diff contains unrelated changes, the generated message is incoherent, which signals that the commit should be split; use this as a natural enforcer of commit discipline

### 6. Code Review in the Age of AI (12 min)
- New challenges: AI-generated code can *look* correct but have subtle bugs
  - **The "confidently wrong" problem:** LLMs generate text that is statistically plausible — which means AI-generated code often looks clean, well-structured, and idiomatic while containing logical errors that don't trigger linters or type checkers; the code *feels* reviewed because it's polished
  - **Pattern blindness:** AI-generated code uses common patterns consistently, which makes it easy for reviewers to pattern-match and approve without verifying the logic — the "looks like good code" heuristic is less reliable when the author is an AI that excels at producing recognisable patterns
  - **The new reviewer mindset:** shift from "does this code look right?" to "does this code *do* what the ticket requires?" — read the requirement first, then read the code; don't let code quality influence your assessment of its correctness

- What to check when Copilot wrote the code:
  - **Does the logic actually match the requirements?** Read the ticket first, then the code; AI-generated code frequently solves a slightly different problem than the one specified; verify that every error path and edge case in the requirements is represented — not just the success scenario
  - **Are edge cases handled?** Scan for every place an external value is used (API responses, database results, user input, function parameters) and ask: "What happens if this is null or undefined?"; also check boundary conditions — empty arrays, numeric ranges, string edge cases, timezone handling
  - **Are tests meaningful?** Copilot sometimes generates tests that assert on the implementation rather than the behaviour — a test that can never fail regardless of the bug is worse than no test; check test *assertions*, not just test *presence*
  - **Is there dead code?** Copilot often generates slightly more scaffolding than needed — unused helper variables, imports relevant to an earlier draft, `TODO` comments that were forgotten; run a linter on the PR diff to catch these before review

- Using Copilot to review Copilot's own output:
  - **The `/explain` review pattern:** select a block of Copilot-generated code you're unsure about and run `/explain` — Copilot produces a plain-English walkthrough of the logic; if the explanation doesn't match what you expected the code to do, you've found a bug
  - **Why this works:** `/explain` uses a different reasoning path than code generation — it reads the code and describes what it does, rather than generating code from a description; discrepancies between what you asked for and what `/explain` describes reveal where generation went wrong
  - **"What could go wrong?" prompt:** "This is the code you just generated. What are the most likely edge cases that could cause this to fail in production?" — Copilot will often identify issues in its own output when explicitly asked to look for them
  - **Asking for alternatives:** "What are 2 alternative approaches to what you generated, with different tradeoffs? Which would you recommend and why?" — forces Copilot to evaluate its own output and often surfaces that the first approach wasn't optimal

### 7. Measuring the Impact of GitHub Copilot (8 min)
- Key Copilot Metrics to track (via the GitHub Metrics API):
  - **Suggestion acceptance rate:** the ratio of accepted suggestions to total suggestions shown — a team rate below 25% suggests the context setup (custom instructions, tab habits) needs improvement; above 50% may indicate over-reliance (not critically evaluating suggestions); the *trend over time* is more useful than the absolute number
  - **Lines of code accepted:** a developer might accept 30% of suggestions but those 30% might be very large multi-line functions — LOC accepted captures the actual volume of AI-generated code in the codebase, not just the number of accept actions
  - **Active users vs. licensed users:** unused licences are wasted budget; a ratio below 60% weekly active users / licensed seats after the first month signals a rollout problem, not a product problem
  - **Language and feature breakdown:** acceptance rates vary significantly by language (TypeScript and Python highest; proprietary DSLs lowest); track completions vs. Chat usage split — high Chat relative to completions often correlates with more senior Copilot users

- Beyond vanity metrics — what actually matters:
  - **PR throughput:** measure PRs merged per developer per week before and after adoption — a 15–30% increase at maintained quality is a realistic target for a well-adopted rollout
  - **Defect escape rate:** if Copilot is improving code quality (better test generation, better error handling), bugs reaching production should decline over time; if increasing, Copilot's output is introducing more bugs than it prevents
  - **DORA metrics connection:** the most credible way to measure organisational impact is through DORA metrics (deployment frequency, lead time, change failure rate, MTTR) — measured before and after adoption; these are immune to gaming and directly reflect business value
  - **The "55% faster" headline:** GitHub's published figure comes from randomised controlled trials on specific, bounded tasks — real-world improvement for mixed developer workflows is typically 10–35%; do not use the headline figure as an expectation-setter

- Setting realistic expectations:
  - **Copilot can measurably improve:** coding speed on well-understood problems, test coverage volume, documentation completeness, time on boilerplate, context-switching cost (staying in the IDE for more tasks), developer confidence in unfamiliar codebases
  - **Copilot cannot directly improve:** requirement quality, architectural decision-making, system design quality, meeting overhead, CI/CD pipeline wait times — these are genuine productivity bottlenecks that Copilot doesn't touch
  - **The 6-month ramp:** most teams see the largest productivity gains 3–6 months after initial adoption, not immediately; the initial period involves building new habits, updating custom instructions, and developing trust in output; do not evaluate ROI at week 2

### 8. Staying Sharp as a Developer (5 min)
- The skills that become *more* important with AI:
  - **System design and architecture thinking:** Copilot can implement any pattern you describe — but you have to know which pattern to choose and why; a poor design choice is now implemented across 10 files in minutes rather than discovered slowly over hours of manual coding; architectural decision quality becomes the primary lever on code quality
  - **Code review and critical evaluation:** as the volume of AI-generated code increases, code review becomes the primary quality gate — the reviewer is the last human in the loop before production; the skill shifts from "does this look right?" to "does this *do* the right thing?"
  - **Prompt engineering and AI communication:** the ability to communicate precisely with an AI to get high-quality output is a genuine new skill — it requires clarity of thought, domain knowledge, and iterative refinement; it transfers to every AI tool you'll use
  - **Domain knowledge and business logic:** Copilot writes code, but it doesn't know your business rules — what a "valid order" means in your system, which edge cases are legal requirements; developers with deep domain knowledge give Copilot much more specific, accurate prompts and get dramatically better output

- Avoiding over-reliance:
  - **The atrophy risk:** cognitive skills decay without practice — if developers never write code without Copilot assistance, foundational skills (algorithm design, debugging without a tool) weaken; this creates brittleness when Copilot isn't helpful
  - **Deliberate practice:** schedule regular "Copilot off" coding sessions — interviews, coding katas, algorithm challenges — not because AI won't be available in real work, but because the constraint forces the mental muscle-building that keeps foundational skills sharp
  - **Learning with Copilot:** use `/explain` aggressively to ensure you understand everything you commit, regardless of its origin — "Explain this regex character by character", "Explain why this uses a closure here"; the explanation is available instantly, infinitely patient, and pitched at whatever level you ask for
  - **The compounding advantage:** AI tools amplify existing skill — a strong developer with Copilot is dramatically more productive than a weak developer with Copilot; investing in fundamental development skills *and* AI tool proficiency is the highest-leverage career investment available right now

### 9. Prompt Files, Skill Files & Custom Agents (10 min)
- Prompt files — reusable skill templates for your team
  - **The concept:** a `.prompt.md` file in `.github/prompts/` is a named, version-controlled prompt template for a recurring task — the team's best "add error handling" or "scaffold a new endpoint" prompt captured once and shared with everyone; invoke it from Chat by typing `#` and selecting from the list
  - **What it contains:** YAML front matter (`title`, `description`, `mode: ask/edit/agent`) followed by the full prompt body; supports `${input:variableName}` for dynamic values and embedded `#file:` references to always include the right context automatically
  - **The team skill library:** over time, a team builds a library — `create-rest-endpoint.prompt.md`, `code-review-checklist.prompt.md`, `write-adr.prompt.md`, `generate-db-migration.prompt.md`; each encodes hard-won prompting expertise and makes it accessible to every team member from day one, including new joiners
  - **Mode matters:** `ask` skills return a Chat response; `edit` skills apply changes in-place to selected code; `agent` skills trigger an autonomous loop — use agent mode for multi-step tasks (scaffold an endpoint, generate tests, update docs) in a single invocation
- Skill files — instructions the agent applies automatically
  - **The key difference from prompt files:** you invoke prompt files manually by picking them; Copilot invokes skill files *on its own* based on the task — you write the process once and the agent uses it automatically whenever relevant
  - **File format:** a `SKILL.md` file in `.github/skills/<skill-name>/` with YAML front matter (`name`, `description`) and a Markdown body of expert instructions; can include runnable scripts; follows the open [`agentskills`](https://github.com/agentskills/agentskills) standard (compatible with Claude and other agents)
  - **When to use each:** use a **prompt file** when you want a specific prompt you invoke deliberately for a named task; use a **skill file** when you want the agent to automatically apply an expert process whenever it encounters a category of work (e.g., "always follow this debugging process for CI failures")
- Custom agent files — specialised AI assistants for your project
  - **Beyond generic agents:** Agent Mode is general-purpose; a custom agent adds a focused persona, a restricted scope, and always-on context — `@test-engineer` that only ever evaluates test quality gives more targeted, expert-level output than a general agent asked to review tests
  - **What a custom agent defines:** the agent's persona ("You are a security-focused reviewer who only examines authentication and authorisation code"), its scope (which directories or file patterns it reads), and which tools it can use; agent definition files live in the repository alongside the code they serve
  - **High-value custom agents to build:** `@api-designer` (always reads your OpenAPI spec and existing routes), `@docs-writer` (always references your documentation standards), `@migration-expert` (specialised in your ORM patterns); each eliminates the setup overhead of attaching the right context on every invocation
  - **Version-controlled governance:** agent and skill files are committed to the repository — changes go through code review; the team's collective AI literacy grows over time and transfers automatically to every new developer who joins

> **⏱ Facilitator note:** If time is short, §7 (Measuring Impact) or §8 (Staying Sharp) can be trimmed to 3 minutes each to accommodate this section. The skills-and-agents content is valuable for participants who will be rolling out Copilot to their teams.

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Prompt Makeover (15 min)
Display 5 "bad" prompts on screen. Teams compete to rewrite them using the four-ingredient framework. Run both the original and improved prompt in Copilot live and compare the output quality in the room.

Example bad prompts to improve:
- "help with my code"
- "write a sort function"
- "fix the bug"
- "make it work with TypeScript"
- "add error handling"

### Exercise: Context Variable Challenge (12 min)
Participants are given an unfamiliar codebase. Using *only* `@workspace` and `#file` references (no scrolling through code manually), they must answer 5 questions in 8 minutes:
1. What database ORM does this project use?
2. Where is authentication handled?
3. What does the `processOrder` function do?
4. Are there any TODO comments in the codebase?
5. What testing framework is used?

Debrief: how did context variables change the discovery experience vs. grep/manual browsing?

### Exercise: AI Code Review Red Team (12 min)
Participants are given a PR containing entirely Copilot-generated code (provided by facilitator). Their job: find as many real issues as possible in 8 minutes, using only their eyes and Copilot Chat (`/explain`, "what could go wrong?"). Score points for real bugs vs. false positives.

---

## 🔗 Resources & References
- [Prompt engineering for GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/prompt-engineering-for-github-copilot)
- [Reusable prompt files for GitHub Copilot](https://docs.github.com/en/copilot/customizing-copilot/using-copilot-with-prompt-files)
- [Agent skills for GitHub Copilot](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/cloud-agent/add-skills)
- [Copilot Chat context and variables](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/copilot-chat-context)
- [Best practices for using GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/best-practices-for-using-github-copilot)
- [GitHub Copilot Metrics API](https://docs.github.com/en/rest/copilot/copilot-metrics)
- [GitHub blog: How to measure the impact of GitHub Copilot](https://github.blog/enterprise-software/developer-productivity/how-to-measure-the-impact-of-github-copilot/)
- [GitHub blog: Prompts, tips, and use cases](https://github.blog/developer-skills/github/how-to-use-github-copilot-prompts-tips-and-use-cases/)

---

## 🗒️ Facilitator Notes
- The "Prompt Makeover" exercise is consistently the most engaging exercise in the entire workshop — give it its full time and let the room vote on the best rewrite
- The code review red team exercise often generates the most discussion — AI-generated code that looks polished but is logically wrong surprises participants every time
- The metrics section is most relevant to tech leads and engineering managers; if the audience is predominantly individual contributors, spend the time saved on the advanced prompting patterns instead
- After the context variable demo, ask the room: "How would you have found that answer without Copilot?" — the contrast is striking and reinforces the habit
- Remind participants that the prompt framework (Task/Context/Examples/Constraints) applies equally to ChatGPT, Claude, and any other AI tool they use — this is a transferable skill

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 2 — Meet Your New Best Friend & Let It Take the Wheel](../chapter-02/README.md) | [Chapter 4 — Get Your Hands Dirty: Real-World AI in Action →](../chapter-04/README.md)
