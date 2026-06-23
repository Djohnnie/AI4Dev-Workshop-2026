[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 3 — Power with Purpose: Using AI Responsibly](../chapter-03/README.md) | [Chapter 5 — Speak AI's Language: Mastering Prompts & Context →](../chapter-05/README.md)

---

# Chapter 4 — Let Your AI Co-Pilot Take the Wheel

> **Duration:** 90 minutes | Day 1, 15:00 – 16:30

The biggest chapter of the workshop: seven ways to put Copilot to work across the IDE, the terminal, GitHub, and reusable agent files — one section at a time. Participants move from occasional user to confident daily driver, with one principle running through every surface: Copilot gets the head start, you stay in control, and you own everything you commit.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Use Copilot **Agent Mode** to complete autonomous multi-step, multi-file tasks while reviewing every change
- Use **Plan Mode** to design and approve an implementation strategy before any code is written
- Drive **GitHub Copilot CLI** to do agentic work in the terminal — suggest, explain, and run real shell commands
- Use **Copilot on GitHub.com** for repo chat, PR descriptions, code review, issue triage, and natural-language search
- Author **Custom Instructions** (`.github/copilot-instructions.md`) to teach Copilot a team's stack and conventions
- Create reusable **prompt files**, **skill files**, and **custom agent files** to package team expertise at the right level
- Pick the right scope and tool — Inline Chat, Copilot Edits, or Agent Mode — for any given change

---

## 📋 Content Outline

### 1. Agent Mode — Agentic Coding *(Section 1 of 7)*

- **From occasional user to daily driver** — the chapter previews the seven surfaces: Agent Mode, Plan Mode, Copilot CLI, Copilot on GitHub.com, Custom Instructions, Prompt & Skill Files, and Custom Agents. Seven surfaces, one workflow — you stay in control of every one.
- **Chat vs. Agent Mode — the conceptual shift:** Copilot Chat *suggests* (you ask, it answers with text or a code block, you apply the change, the loop ends each turn, you attach `#file` / `@workspace`). Agent Mode *does* (you set a goal, it reads files, edits, runs commands, iterates until the goal is met, self-corrects on test failures, and gathers context automatically).
- **The agent loop:** a high-level goal drives a repeating cycle — **Goal** (describe the task in plain English) → **Plan** (break into ordered steps) → **Act** (edit files, run terminal commands) → **Observe** (read results, test output, errors) → **Adjust** (fix failures and continue). It reports back when done or when it needs your input.
- **What "autonomous" really means:** Agent Mode replaces 10–20 manual steps but is not unsupervised. It *can* find relevant files, make consistent edits across many, run tests and read results, and fix its own failures. It *will not* run terminal commands without asking, hide what it intends to do, write to disk before you accept, or act outside your decision points. Think of it as a capable contractor who reads the brief, does the work, and sends a summary to approve.
- **Enabling Agent Mode in VS Code:** open the Copilot Chat panel → pick "Agent" from the mode dropdown (or type `/agent`) → a **Tools** section appears listing what the agent can use. Requires a recent Copilot Chat extension and VS Code build (update both if "Agent" is missing); available on paid plans (Individual, Business, Enterprise) — verify current availability in the docs.
- **The tools Agent Mode can reach:** the model decides which tool to call at each step — **read files** (learn structure, naming, patterns), **write/edit files** (diffs appear in the diff view, never silently written to disk), **terminal** (run tests, builds, linters; read stdout/stderr), **workspace search** (semantic discovery of relevant files and symbols), and **MCP tools** (anything you register). The key idea: Chat has none of these by default — tools are what turn answers into actions.
- **Staying in control:** every action has a checkpoint. **Approve commands** — it shows the exact command and waits; read it, edit it, or reject it (auto-approve exists, but avoid it on a real dev machine). **Review diffs** — every edit is a diff in the standard change view, your audit trail. **Keep or Undo** — at the end you see a summary; keep all, undo all, or undo specific files. Full rollback is always available.
- **When Agent Mode shines:** let the agent drive when the goal is clear but the path is tedious — repetitive cross-codebase changes, convention-following boilerplate, test-fix-rerun cycles, migrating a pattern everywhere. Stay manual for exploratory work, nuanced business-logic decisions, learning by doing, and unclear or shifting requirements. **Hybrid is best:** agent for scaffolding, manual for business logic and edge cases. If it loops 3–4 times on the same problem, step in with more specific guidance.
- **MCP — extending Agent Mode:** the Model Context Protocol is a universal plug for AI tool use — an open standard from Anthropic, adopted by GitHub. Register an MCP server and Agent Mode gains tools beyond files and terminal (read a Jira ticket and update its status, query a database schema, trigger a deployment pipeline). Configure servers in VS Code settings; best shown as a facilitator demo since it needs pre-configuration — flag it as "what's possible".

### 2. Choosing Scope — Copilot Edits & the Three Tools

- **Copilot Edits — multi-file editing for bounded changes:** you pick the files, describe the change once, and review coordinated diffs. Best when you already know the files to change, the update must stay consistent across them, and you want a faster loop than inline chat. Workflow: add a working set → ask for the coordinated change once → review, accept, reject, or refine per file. Edits is guided and predictable; Agent Mode is broader and more autonomous. A rename across model, service, endpoint, and tests becomes one prompt instead of four chats — the diffs are the product.
- **Inline Chat vs. Edits vs. Agent Mode — pick by scope:** **Inline Chat** (`Ctrl+I`) acts on the selection in one file only — you manage cross-file consistency yourself. **Copilot Edits** (`Ctrl+Shift+I`) — a few files you choose; define a working set, describe the change once, get consistent, human-directed edits. **Agent Mode** — many files it chooses; set a goal and it decides which files to touch, runs commands, iterates, self-corrects. Know the files → use Edits. Don't → use Agent Mode.
- **The working set:** the list of files Copilot Edits is allowed to change. Fill it by drag & drop from the Explorer, the `+` (Add Files) button, or `#file:name.ts` in the prompt. Keep it focused — "Add Open Editors" pulls in all tabs; remove a file with the `×` next to it. The sweet spot is **3–8 files**; too few and you might as well use inline chat, too many and the model misses cross-file consistency. More than ~10 files? Consider Agent Mode.
- **Iterating — accept, reject, undo:** each file gets a standard diff (red removed, green added) with its own Accept / Reject. Use **Accept All** when satisfied, **Reject All** to roll everything back, or **per-file** to keep some and reject others. Don't reject everything when a change is partially right — accept what's correct, then write a follow-up prompt on just the files that need adjusting (e.g. "the test file should use Vitest's `vi.fn()` not Jest's `jest.fn()`"). Standard `Ctrl+Z` still works after accepting.

> **Exercises here:** Exercise 401 (rename a field with Agent Mode) and Exercise 402 (add input validation) put Agent Mode to work on a real ASP.NET Core API.

### 3. Plan Mode — Design Before You Build *(Section 2 of 7)*

- **What is Plan Mode?** An interactive collaboration mode that builds your implementation strategy before Copilot touches a single file — breaking large goals into sub-tasks and catching misunderstandings before they become wrong code. **No surprise files** (it asks clarifying questions first), **catch issues early** (scope, libraries, and design decisions are locked before generation), and **you stay in control** (read, revise, or reject each step). It's the pause button that makes AI coding predictable.
- **How Plan Mode works — four stages:** **(1) Clarifying Questions** — Copilot scans your codebase and asks targeted questions; **(2) Step-by-Step Blueprint** — actionable steps, pitfalls, and a proposed logic flow; **(3) Review & Edit** — read the draft, adjust requirements, or ask for revisions; **(4) Seamless Execution** — approve and hand off to Agent Mode. Step 3 is the safety gate — nothing is built until you give the go-ahead, and you can loop back to it as many times as you need.
- **Steps 1 & 2 — from questions to blueprint:** Copilot reads your high-level goal, scans existing code for context, and asks about scope, libraries, and design decisions — scope is locked before anything is generated ("no more *that's not what I meant* moments"). It then generates a structured list of actionable steps, highlights pitfalls upfront, proposes a logic flow in plain language, and breaks out sub-tasks with clear dependencies. The blueprint is editable — not a one-way output.
- **Steps 3 & 4 — review, then execute:** read the draft, adjust requirements, ask Copilot to revise specific steps, and repeat until the plan matches your intent — nothing is generated until you approve. Then hand the approved plan to Agent Mode, which implements each step in sequence and tracks sub-tasks as it goes. The approved plan is the contract — no drift; or run it on autopilot and let Copilot work through the list unattended.

> **Exercise here:** Exercise 403 — use Plan Mode to design and implement pagination, filtering, and sorting query parameters for the `GET /tasks` endpoint.

### 4. GitHub Copilot CLI — AI in the Terminal *(Section 3 of 7)*

- **Installing GitHub Copilot CLI:** a standalone CLI — install once, authenticate once, works in any terminal. Prerequisites: an active GitHub Copilot subscription, PowerShell v6+ on Windows, and Node.js 22+ for the npm method. Install with `winget install GitHub.Copilot` (Windows), `brew install copilot-cli` (macOS), or `npm install -g @github/copilot` (all). First launch prompts for `/login` — your GitHub account is the key, no separate token. Keep it current with the matching upgrade command; admins can restrict access from the org's Copilot settings.
- **The workflow shift — running outside the IDE:** in large codebases, IDE extensions share one process (Git, formatters, Chat all compete for resources) and a big agent task can freeze VS Code entirely. The CLI runs in a **separate process**, so the IDE stays responsive. It still connects to your active VS Code instance for full workspace context, supports `@` mentions and file additions, and has **no Keep / Undo dialogs** — tasks finish autonomously and you review the Git diff when done. Reach for the CLI for repo-wide refactors, large monorepos, and parallel agent tasks; Chat stays right for single-file edits and inline completions.
- **More than code — everything in your directory:** open a terminal in any folder and code, docs, slides, configs, and projects are all in scope — there's no file-type restriction. Examples span documentation (README/CHANGELOG, API reference, ADRs, OpenAPI specs), slides & diagrams (SVG assets, Marp/Reveal.js decks, speaker notes), and code & projects (scaffold whole solutions, update configs across a monorepo, Dockerfiles and CI/CD). Ask once, change many — from a single terminal, the whole project is your workspace.
- **Shell commands — agentic development in action:** Copilot CLI doesn't just suggest — it invokes real binaries (`dotnet`, `git`, `docker`, `kubectl`, anything in your `PATH`) with your approval. The loop is *reason → pick tool → show command → await approval → execute → read output → adapt*. Before each command it shows exactly what it will run; you confirm or cancel. This is real agentic execution, not just MCP tool calls — and you are always in control.

> **Exercise here:** Exercise 404 — vibe-code a .NET 10 WinForms slot machine from scratch with the CLI; prompt first, code second.

### 5. Copilot on GitHub.com — In the Browser *(Section 4 of 7)*

- **Five surfaces in the browser:** Copilot detects the page context automatically. **Chat in the browser** — ask about any repo, file, PR, or issue, including open-source repos you don't own, without cloning. **PR descriptions** — the sparkle icon summarises the full diff (what changed, why, how to test); good commit hygiene improves the output. **Code review** — request Copilot as a reviewer for line-level suggestions you can apply in one click. **Issues** — summarise a 50-comment thread, suggest next steps, draft a reply, or find related/duplicate issues. **Natural-language search** — "Where is the email sent?" finds code by behaviour, no exact symbol name needed (ideal for onboarding). No editing or running commands from the browser — for changes, the IDE is still the right tool.
- **Copilot Code Review, up close:** a pre-flight check that catches obvious issues before human reviewers spend their time. It flags logic bugs (off-by-one, missing null checks), security issues (SQL injection, XSS, missing auth), and performance problems (N+1, unbounded loops) — not style or formatting, so no nit comments. Request "Copilot" in the Reviewers panel; it posts inline comments each explaining *why*, and "Apply suggestion" commits the fix. **Copilot Autofix** can also suggest fixes for CodeQL security alerts. It complements human review, it does not replace it — humans still own architecture, business-logic correctness, and organisational context.

> **Exercise here:** Exercise 405 — stay in the browser to chat about repos, recap issues, find code, summarise and review PRs, and assign an issue to GitHub Copilot's online coding agent.

### 6. Custom Instructions — Teach Copilot Your Standards *(Section 5 of 7)*

- **Three layers of instructions:** all present layers apply at once, combined into the model's context on every request. **Organisation** — set by enterprise admins, applies org-wide. **Repository** — `.github/copilot-instructions.md`, shared with the whole team. **User** — personal preferences in VS Code settings that follow you across every workspace. The repo file is the workhorse: committed and version-controlled, it goes through code review like any config and is read automatically on every request. Instructions are guidelines, not hard rules — they influence the model, they don't rigidly constrain it. Keep the repo file focused (under ~500 words); on conflict, the repo file usually wins for project-specific rules.
- **What to put in the file** — be specific; the more concrete the rule, the more reliably Copilot follows it:
  - **Style & conventions** — naming, exports, error handling, comment policy (*why*, not *what*). E.g. "Named exports only. Use `AppError` for user-facing errors."
  - **Tech stack** — framework versions, preferred libs, database/ORM. E.g. "React 19, TS 5.5, Vite 6. Use `zod` and `date-fns` — never `moment.js`."
  - **Testing requirements** — framework, file location, coverage, assertion & mocking style. E.g. "Vitest + Testing Library. Mock with `vi.fn()`. Happy path + 2 edges + 1 error."
  - **Output preferences** — verbosity, strictness, response format. E.g. "`strict: true` — never use `any`. Be concise, no preamble."

> **Exercise here:** Exercise 406 — no starter app; create a repo, add `.github/copilot-instructions.md`, and teach Copilot your stack, conventions, test rules, and output style. The point is enriching prompt context, not building a project.

### 7. Prompt Files & Skill Files — Capture Team Knowledge *(Section 6 of 7)*

- **Anatomy of a prompt file:** a `.prompt.md` in `.github/prompts/` captures a great one-off prompt, version-controlled forever. YAML front matter sets `title` (shown in the VS Code picker), `description`, and `mode` (`ask` · `edit` · `agent`); the body is the prompt itself, with `${input:...}` to prompt for a value at run time and `#file:...` to attach context every time. Invoke it with `#` in Chat — a 30-minute task becomes 90 seconds.
- **Prompt files vs. skill files** — same goal (reuse expertise); the difference is *who* decides to load it:
  - **Prompt file** — *you* invoke it, picking it from the Chat picker. A named task you run on demand (`.github/prompts/*.prompt.md`), e.g. "scaffold an endpoint".
  - **Skill file** — *the agent* reaches for it, reading each description and deciding. An expert process for a type of work (`.github/skills/<name>/SKILL.md`) that can bundle runnable scripts.
  - `SKILL.md` follows the open agentskills spec — compatible with Copilot, Claude, and other agents.

> **Exercise here:** Exercise 407 — in a tiny sandbox repo, create a prompt file you invoke and a skill file the agent loads, then compare manual invocation vs. auto-loading.

### 8. Custom Agents — Package Specialist Teammates *(Section 7 of 7)*

- **What an agent file is:** custom agents are specialized versions of Copilot tailored to a workflow, domain, or team process. You define them once in an agent profile Markdown file with YAML frontmatter plus a prompt body, then select them like a specialist teammate instead of re-explaining the same rules every time. Use them when the work needs its own behavior, tool boundary, or MCP access — not just better default wording.
- **Anatomy of the profile:** `description` explains the specialty; `name` is the display label; the Markdown body is the actual behavior prompt. Optional properties tighten the contract: `tools` limits what the agent can touch, `mcp-servers` adds private tool access, and some environments also honor `target` and `model`. The prompt should answer four things clearly: what the agent is expert at, when to use it, what it must avoid, and what a good result looks like.
- **Where it lives and how you use it:** store shared agent profiles in `.github/agents/` at repository scope; the CLI also supports personal agents in `~/.copilot/agents/`; org and enterprise scopes exist for broader rollout. On GitHub.com, create/select agents from the agents tab or when assigning an issue. In the CLI, use `/agent`, mention the agent by name, or run `copilot --agent <name> --prompt "..."`. In supported IDEs, pick the agent from the agent dropdown. One profile, many surfaces.
- **Agent orchestration with the Copilot SDK:** inside an app, custom agents can also be attached directly to a Copilot SDK session via `customAgents`. Each one gets its own prompt, tool set, and optional MCP servers; the runtime can infer the best match, spin it up as a **sub-agent** in an isolated context, and stream lifecycle events back to the parent session. Think *specialist teammates inside one orchestrated conversation*.
- **How it fits with the other files:** custom instructions are always-on defaults, prompt files are reusable templates you invoke manually, skill files are expert workflows that the agent auto-loads when relevant, and custom agents are selectable specialists with their own identity, prompt, and tool restrictions. Great examples: a read-only architecture reviewer, a release-notes drafter, or a security auditor that only inspects changed files.

> **Exercise here:** Exercise 408 — use four custom agent files on the same tiny starter repo, compare how security, test, docs, and refactor specialists behave, and keep the most useful output.

### 9. Wrap-Up, Quizzes & Labs

- **Your daily-driver toolkit:** seven tools, one principle — reach for the right scope and review everything you keep. Agent Mode and Plan Mode shape autonomous work; Copilot Edits & CLI coordinate change in the IDE and terminal; GitHub.com covers PRs, issues, and search; instructions, prompt files, and skill files capture reusable context; custom agents package specialist teammates with their own prompt and tool boundary. The thread through all seven: Copilot gets the head start, you stay in control, and you own everything you commit.
- **Interactive Quiz 10** — which activity best fits Copilot on GitHub.com without cloning first? *(Answer: recapping an open issue, reviewing a PR, and asking repo questions in the browser.)*
- **Interactive Quiz 11** — what stays true when using Agent Mode or a CLI-driven workflow for a real change? *(Answer: you can let Copilot accelerate the work, but you still approve and own what gets committed.)*
- **Interactive Quiz 12** — how do instructions, prompt files, and skill files work together? *(Answer: instructions set defaults, prompt files are invoked on purpose, and skill files can auto-load expert processes.)*
- **Lab 401 — Ultimate Snake (Web):** use Agent Mode to turn a browser starter (ASP.NET Core host + HTML/CSS/JS) into a playable Snake game — Space to start/pause, arrow keys without instant reversal, wrap-around movement, food growth, score tracking, and game-over on self-collision. Keep the UI shell, let Agent Mode coordinate the logic, and review the diffs. The goal is a playable game and experiencing the agent as a collaborator — not perfect production architecture.
- **Lab 402 — Ultimate Snake with Copilot CLI:** build the same game from an empty folder, driven entirely from the terminal. Use Copilot CLI — and only the CLI — to suggest commands, explain unknown ones, run them yourself, and inspect output after each step, ending with the same playable Snake app as Lab 401.

---

## 🧪 Chapter 4 Exercises

- **[Exercise 401 — Rename a Field with Agent Mode](../../../exercises/chapter-04/exercise-401/README.md)** — use Agent Mode to rename `Name` to `Title` across an entire ASP.NET Core codebase without touching a file manually; review every diff, then `dotnet build` and confirm the JSON shows `title`.
- **[Exercise 402 — Add Input Validation](../../../exercises/chapter-04/exercise-402/README.md)** — confirm an empty title returns `201`, then have Agent Mode validate `Title` and return `400` on bad input; review the logic and error format and test with the `.http` file.
- **[Exercise 403 — Paginate, Filter and Sort](../../../exercises/chapter-04/exercise-403/README.md)** — use Plan Mode to design and implement pagination, filtering, and sorting query parameters for `GET /tasks`; review every step before approving execution.
- **[Exercise 404 — Vibe-Code a Slot Machine](../../../exercises/chapter-04/exercise-404/README.md)** — use Copilot CLI to scaffold a .NET 10 WinForms slot machine (3 reels, 100 credits, bets of 1/5/10, payouts, reset), approve the commands, verify gameplay, and extend with polish.
- **[Exercise 405 — Explore Copilot on GitHub.com](../../../exercises/chapter-04/exercise-405/README.md)** — browser only: ask about repos, recap an issue, find code by behaviour, summarise and review a PR, and assign an issue to Copilot's online coding agent.
- **[Exercise 406 — Create a Repository Instruction File](../../../exercises/chapter-04/exercise-406/README.md)** — create a repo and a `.github/copilot-instructions.md` capturing style, conventions, tech stack, test rules, and output preferences (no starter app).
- **[Exercise 407 — Experiment with Prompt Files and Skill Files](../../../exercises/chapter-04/exercise-407/README.md)** — in a sandbox repo, add a `.prompt.md` you invoke and a `SKILL.md` the agent loads, then compare manual invocation vs. auto-loading.
- **[Exercise 408 — Bug Bash with Custom Agents](../../../exercises/chapter-04/exercise-408/README.md)** — copy four custom agent files into `.github/agents/`, run a security, test, docs, and refactor bug bash on the same tiny repo, and compare how specialization changes Copilot's output.
- **[Lab 401 — Ultimate Snake (Web)](../../../labs/chapter-04/lab-401/README.md)** — use Agent Mode to complete a browser-based Ultimate Snake game from a provided ASP.NET Core + HTML/CSS/JS starter.
- **[Lab 402 — Ultimate Snake from Scratch with GitHub Copilot CLI](../../../labs/chapter-04/lab-402/README.md)** — build the same Snake game from an empty folder using only GitHub Copilot CLI from the terminal.

---

## 🔗 Resources & References
- [Using Copilot Agent Mode](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/using-copilot-coding-agent)
- [Copilot Edits (multi-file editing)](https://docs.github.com/en/copilot/using-github-copilot/copilot-edits)
- [GitHub Copilot CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli/about-github-copilot-in-the-cli)
- [Requesting a Copilot code review](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [Adding custom instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
- [Reusable prompt files for GitHub Copilot](https://docs.github.com/en/copilot/customizing-copilot/using-copilot-with-prompt-files)
- [Agent skills for GitHub Copilot](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/cloud-agent/add-skills)
- [About custom agents](https://docs.github.com/en/copilot/concepts/agents/cloud-agent/about-custom-agents)
- [Creating custom agents for Copilot cloud agent](https://docs.github.com/en/copilot/how-tos/copilot-on-github/customize-copilot/customize-cloud-agent/create-custom-agents)
- [Creating and using custom agents for GitHub Copilot CLI](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/create-custom-agents-for-cli)
- [Custom agents and sub-agent orchestration in Copilot SDK](https://docs.github.com/en/copilot/how-tos/copilot-sdk/features/custom-agents)
- [Copilot customization cheat sheet](https://docs.github.com/en/copilot/reference/customization-cheat-sheet)
- [MCP with GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/extending-copilot-chat-with-mcp)
- [Model Context Protocol (Anthropic)](https://modelcontextprotocol.io)

---

## 🗒️ Facilitator Notes
- This is the largest chapter (52 slides, seven sections) — keep momentum by treating each section as a self-contained beat, and use the per-section exercises as natural pauses.
- Agent Mode and the CLI need internet access and can be slow — pre-run the demos and factor the wait into exercise timing.
- Ensure participants have GitHub Copilot CLI installed and `/login` done before the CLI section (`winget` / `brew` / `npm install -g @github/copilot`).
- MCP needs pre-configuration — present it as a facilitator demo ("what's possible"), not a participant exercise.
- Emphasise the through-line at every checkpoint: review diffs, approve commands, own what you commit. The two Snake labs (Agent Mode vs. CLI-only) are a strong way to end Day 1 on the same goal from two different surfaces.

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 3 — Power with Purpose: Using AI Responsibly](../chapter-03/README.md) | [Chapter 5 — Speak AI's Language: Mastering Prompts & Context →](../chapter-05/README.md)