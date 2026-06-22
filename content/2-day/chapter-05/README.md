[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 4 — Let Your AI Co-Pilot Take the Wheel](../chapter-04/README.md) | [Chapter 6 — AI Across the Entire Software Lifecycle →](../chapter-06/README.md)

---

# Chapter 5 — Speak AI's Language: Mastering Prompts & Context

> **Duration:** 90 minutes | Day 2, 09:00 – 10:30

Copilot's output quality is directly proportional to the prompt and context it receives: a **clear prompt** (task, examples, constraints) plus the **right context** (open files, variables, instructions) yields **quality output** that is project-aware, precise, and correct. Prompt engineering is not magic words — it's engineering. This chapter turns vague requests into precise, repeatable results across five sections.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain how Copilot assembles its prompt from a ranked context window, fill-in-the-middle, and the "tab setup" habit
- Structure prompts around the four ingredients — task, context, examples, constraints — and avoid common anti-patterns
- Choose between one-shot and few-shot prompting and refine results through iterative follow-ups
- Use Copilot's `@` participants and `#` variables to feed it precisely the right context
- Apply advanced patterns: comment-driven development, test-first, persona, chain-of-thought, stepwise, and diff-driven prompting
- Match the right tool — completions, inline Chat, Chat panel, or Agent Mode — to the task
- Write layered custom instructions that are directive, specific, scoped, and tested

---

## 📋 Content Outline

### 1. How Copilot Reads Your Code

**The context window**
- Copilot never sees your whole project — only a ranked slice that fits the model's context window.
- What's in the window: code around your cursor, snippets from open tabs, imports & type definitions in scope, nearby comments, `copilot-instructions.md`, and the session chat history.
- What it can't see: everything outside the window — closed files, unindexed modules, the rest of the repository.
- Irrelevant open tabs and long chat history consume the window and push out what actually matters. Window size varies by model and changes over time — manage what fills it, or get generic answers.

**Priority of context signals** (highest weight first)
1. **Cursor position & surrounding code** — the primary signal: language, names in scope, the exact problem being solved.
2. **Open editor tabs** — similar-named files score highest (`userController` pulls in `userService`, `userModel`).
3. **Imported modules & type definitions** — imports signal the vocabulary; missing ones lead to hallucinated APIs.
4. **Comments immediately above the cursor** — your highest-signal manual input; a specific comment is the spec.
5. **`copilot-instructions.md`** — always-on background context; sets style but yields to the signals at the cursor.

**Fill-in-the-Middle (FIM)**
- Copilot reads above *and* below the cursor — the code that follows constrains what it generates.
- A `return` that already exists makes the generated body more accurate, because it must populate that return.
- The workflow: sketch the structure first — signature at the top, return at the bottom — then fill the middle.

**The "tab setup" habit**
- 60 seconds opening the right files is the highest-leverage thing you can do, with zero extra prompting.
- Zero tabs open → generic, statistically-average code, invented method names, patterns that don't match yours.
- 3–5 related tabs → calls that match your interfaces, your naming and conventions, tests in your style — far less to fix.
- Open the interface, the related services, and the test file, then close the irrelevant ones.

*→ Exercise 501 — Context Window Copilot Clone (build a tiny VS prompt tool; compare GPT-4o with no context, FIM, and open tabs).*

### 2. The Anatomy of a Good Prompt

**The four ingredients** — state the ones that matter, skip the ones that don't.
- **Task** — what do you want? Verb-first and singular: "Create", "Refactor", "Explain", "Fix".
- **Context** — what's the environment? Language, framework, version, who calls it — e.g. "in TypeScript, using Express 5".
- **Examples** — show the shape: input/output pairs or a similar function — "like `formatCurrency`, but for percentages".
- **Constraints** — what must it *not* do? Real limits only: "No new dependencies", "keep the public signature unchanged".
- One task per prompt — Copilot trades depth for breadth when you bundle.

**Start with the right verb**
- The opening verb sets the register: Create, Refactor, Extract, Convert, Migrate, Optimise, Simplify, Document, Test, Debug, Review, Explain.
- Weak: "Refactor this" (no goal — Copilot guesses what "better" means).
- Strong: "Refactor this to reduce cyclomatic complexity without changing the public interface" (scope + measurable goal + constraint). The specifics carry the weight.

**Prompt anti-patterns** — three ways prompts go wrong, and the fix for each.
- **Vague task** ("make this better") → name a specific, measurable goal; every adjective hides a precise meaning, so use it.
- **Missing context** ("write a function") → answer the implicit questions: inputs, outputs, language, caller, failure behaviour.
- **Conflicting constraints** ("make it fast but use a nested loop") → name the tradeoff explicitly: "O(n²) is fine for n < 100 — optimise the inner loop".

**One-shot vs. few-shot**
- Default to **one-shot** (no examples, just the ask) — cheapest and fastest, great where the model has strong priors: sorting, CRUD, API calls.
- Reach for **few-shot** (1–3 examples, then the ask) for custom DSLs, config formats, or specific response shapes; label them "Example:", then "now apply the same pattern to...". Costs more tokens — worth it when quality lags.

**Iterative prompting**
- Treat Chat as a conversation, not a single query: Prompt → Review (find the one specific gap) → Follow up (target just that gap, don't start over) → repeat.
- Follow-ups beat re-prompts: "Keep everything, but change the error type to `ValidationError`." The thread keeps its history, so you needn't repaste.
- If the thread derails, start a new one — stale context misleads as much as it helps.

*→ Exercise 502 — Prompt Arena (a web app that scores prompts 0%→100% on the four ingredients, one-shot vs few-shot, and anti-pattern avoidance).*

### 3. Context Variables

**The `@` participants** — prefix a question with `@` to route it to a specialist.
- **`@workspace`** — your codebase: semantic search across all indexed files. "How does auth work in this project?" It finds snippets, not all code.
- **`@vscode`** — the IDE itself: settings, keybindings, `launch.json`, extensions. "How do I configure the Node debugger?" Not for coding tasks.
- **`@terminal`** — the shell: reads recent output, knows your shell & OS. Paste a cryptic error and ask "why, and how to fix?".
- Always pair the `@` with a specific question.

**The `#` variables** — use `#` to pin exact context into a single prompt.
- **`#file`** — attach a named file in full; more reliable than tab scanning because you control what's in context.
- **`#selection`** — the text selected in the editor; no copy-paste. "Rewrite `#selection` using Strategy."
- **`#codebase`** — semantic search as an inline variable; pulls snippets for one prompt, not the whole chat.
- **`#sym`** — reference a named symbol; Copilot pulls its definition and usages. "Explain `#sym:processOrder`."
- **`#def`** — the definition of the symbol under the cursor ("Go to Definition" as context). "What breaks if I change `#def`'s signature?"
- Combine them: "`#selection` doesn't match `#file:userModel.ts` — update it to comply."

**`@workspace` vs. `#file`** — broad when you don't know where it is; precise when you do.
- `@workspace`: broad & exploratory, architectural/cross-cutting, ~1–3 seconds slower (retrieval). "How is the payment flow tested?"
- `#file`: precise & fast, for targeted edits and explanations; attach several at once. "Refactor `createUser` in `userService.ts`."
- Search to find it — attach it once you know where it lives.

*→ Exercise 503 — Context Variables Playground (compare no variable, `@workspace`, and `#file` on the same discovery prompts).*

### 4. Advanced Prompting Patterns

**Comment-driven development**
- Write the spec as comments first — they sit at the highest-weight position and become the specification.
- Write XML comments with `<summary>`, `<param>`, `<returns>`; add inline comments for each major step; place the cursor below and trigger a completion. Copilot generates the body — you never typed a line of implementation.
- The payoff: writing the comment forces clear thinking, so ambiguities surface early.

**Test-first prompting** — the test constrains the solution space.
- **A — Test from spec:** describe behaviour as "should" statements and ask Copilot to write the tests, then the implementation.
- **B — Implementation from test:** write the test yourself, attach it, and have Copilot reverse-engineer a passing implementation ("`#file:email.test.ts` Write an implementation of `validateEmail` that makes all these tests pass").
- Gotcha — green is not the same as correct: Copilot may pass the tests while hardcoding values or missing uncovered edges. Always review the implementation, don't just check that the suite is green.

**Persona & chain-of-thought** — two ways to prime the model.
- **Persona prompting:** assign a role to apply a specific lens — "You are a security auditor. Review this code for OWASP vulnerabilities." Best for review tasks (security, performance, accessibility). It activates patterns, it doesn't add knowledge — stay sceptical on niche topics.
- **Chain-of-thought:** ask it to reason before it writes code — "Think step by step about an LRU cache with O(1) get/put, then implement." Surfaces intermediate decisions that focus the final code; use it when correctness matters more than speed (responses run longer).
- Persona = *what* to look for. Chain-of-thought = *how* to reason.

**Stepwise & diff-driven** — two ways to structure the work.
- **Stepwise decomposition:** break a big task into 4–6 steps, prompting each with the previous output as context (types → schema → repository → service → routes). Agent Mode does this automatically; do it by hand in Chat mode.
- **Diff-driven prompting:** show one real edit first, then apply the same pattern to the next file — e.g. `logger.LogInformation("Saved " + orderId)` → `logger.LogInformation("Saved {OrderId}", orderId)`, then "Apply this logging change to `PaymentService.cs`." Examples transfer naming, shape, and style faster than prose — but review the output, examples can over-apply.

**Completions vs. Chat vs. Agent** — match the tool to the task.
- **Completions** — what to write next, in a flow state; small, local edits based on nearby code; cycle alternatives.
- **Inline Chat** — ask about a selection; refactor with a specific instruction while staying in editor flow.
- **Chat panel** — figure out *how*: codebase-wide context, `@workspace` and many `#file`, debug by conversation, architectural questions.
- **Agent Mode** (Chapter 4) — get it done: spans multiple files, runs & verifies code, decomposes itself, automates the loop.

*→ Exercise 504 — Prompt Pattern Playground (try comment-driven, test-first, persona, stepwise, and diff-driven patterns on prebuilt code).*

### 5. Custom Instructions

**Layered instructions** — three levels stack, they don't cancel out.
- **User** (baseline) — VS Code settings; every session, every project; personal preferences.
- **Repository** (team baseline) — `copilot-instructions.md`; committed, shared, code-reviewed; project context.
- **Session** (highest priority) — set at the start of a chat thread; extends the rest for one conversation.
- All three are injected together; the most recent (session) takes priority on conflict. Because the repo file is in source control, it can be reviewed, versioned, and audited.

**Specific, but not brittle** — be directive and concrete, but scope each rule so it doesn't break on edge cases.
- Be directive, not preferential: "prefer var for locals" is treated as a suggestion → "Use explicit C# types for local variables. Do not use var."
- Scope so it won't break: "every file needs XML docs" breaks on tests and helpers → "Add XML docs to public types and public members in `.cs` files."
- Organise by category under headings — `## Tech Stack`, `## Style`, `## Testing`, `## Output` — so you can spot redundancy and conflict.
- Short beats long: a 15-line file Copilot follows beats a 100-line file it half-ignores when the budget runs out.

**Test & iterate the file** — treat instructions like configuration code; validate every rule with a real prompt.
- Loop: **Change** (add/edit one rule) → **Probe** (ask a prompt it should affect) → **Check** (did output move the right way?) → **Refine** (reword and repeat).
- Common failure modes: rules phrased as a preference ("prefer X") get treated as optional — be directive; if user, repo, and session rules disagree the result gets confusing — check all three layers.

**The Prompt Engineer's Checklist** — five habits that turn vague requests into precise results:
1. **Manage the window** — open the relevant files, close the noise; Copilot only sees a ranked slice.
2. **Use the four ingredients** — task, context, examples, constraints; one task per prompt.
3. **Feed it the right context** — `@workspace` to find, `#file` to pin, `#sym` for a symbol.
4. **Reach for a pattern** — comment-driven, test-first, persona, chain-of-thought — then iterate.
5. **Tune your custom instructions** — directive, specific, scoped, short, and tested against a real prompt.
- Context is the product. Engineer it.

**Interactive quizzes** — three knowledge checks run through the chapter:
- **Quiz 13:** Which action most directly improves Copilot's next suggestion before you type any new prompt? *(Open the related interface, service, and test files and close unrelated tabs.)*
- **Quiz 14:** Which prompt best uses task, context, examples, and constraints together? *(The Express/TypeScript JWT middleware prompt with a defined response shape and "no new packages".)*
- **Quiz 15:** What is the key difference between prompt files and skill files in Copilot? *(Prompt files are named prompts you invoke deliberately; skill files load automatically when the agent decides they apply.)*

---

## 🧪 Chapter 5 Exercises

- **[Exercise 501 — Context Window Copilot Clone](../../../exercises/chapter-05/exercise-501/README.md)** — Build a tiny Visual Studio prompt tool: scaffold a VSIX command, dialog, prompt box, and mode selector; wire one GPT-4o model through three context modes (no context, FIM, open tabs); compare the answers and inspect the composed prompt to see why each changes.
- **[Exercise 502 — Prompt Arena](../../../exercises/chapter-05/exercise-502/README.md)** — Build a shareable ASP.NET Core web app that scores prompts 0%→100% against the four ingredients, one-shot vs few-shot use, and anti-pattern avoidance, using a custom system prompt as the rubric and a separate LLM call to coach the player. Deploy once and let teams compete.
- **[Exercise 503 — Context Variables Playground](../../../exercises/chapter-05/exercise-503/README.md)** — Use a prebuilt .NET app to compare no variable, `@workspace`, and `#file` on the same discovery prompts: map the repo, zoom into one file, and re-run prompts with and without variables to spot the delta. No coding required — explore and discuss context fit.
- **[Exercise 504 — Prompt Pattern Playground](../../../exercises/chapter-05/exercise-504/README.md)** — Use one prebuilt .NET app to try section 4's advanced patterns: comment-driven and test-first anchors, a security persona or stepwise breakdown, and a diff-driven logging transform reused across files. A playground for comparing prompting styles, not building features.
- **[Lab 501 — Ultimate Snake with Instructions, Prompt Files, and Skills](../../../labs/chapter-05/lab-501/README.md)** — Rebuild the Chapter 4 Snake game from an almost-empty folder (README plus `.github` guidance assets). Read `.github/copilot-instructions.md` first, invoke the scaffold prompt file, refine with task/context/examples/constraints, and validate against the included skill checklist. The win is feeling how durable prompt context in the repo improves output — vibe-coding, but now with intention.

---

## 🔗 Resources & References
- [Prompt engineering for GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/prompt-engineering-for-github-copilot)
- [Copilot Chat context and variables](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/copilot-chat-context)
- [Best practices for using GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/best-practices-for-using-github-copilot)
- [Adding custom instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
- [GitHub blog: How to use GitHub Copilot: Prompts, tips, and use cases](https://github.blog/developer-skills/github/how-to-use-github-copilot-prompts-tips-and-use-cases/)

---

## 🗒️ Facilitator Notes
- This session is the pivot point of the workshop — it turns casual Copilot users into power users.
- Demo the "tab setup" difference live: run the same prompt with zero tabs open vs. 3–5 related tabs open — it is one of the most impactful two-minute demos in the workshop.
- Keep exercises 503 and 504 in "playground mode": the goal is to compare context and prompting styles, not to build features.
- Reinforce the closing message throughout — context is the product, so engineer it deliberately rather than retrying random prompts.

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 4 — Let Your AI Co-Pilot Take the Wheel](../chapter-04/README.md) | [Chapter 6 — AI Across the Entire Software Lifecycle →](../chapter-06/README.md)
