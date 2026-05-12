[🏠 Workshop Home](../../../README.md) | [← Chapter 1 — Welcome to the AI Revolution & Power with Purpose](../chapter-01/README.md) | [Chapter 3 — Speak AI's Language: Mastering Prompts, Workflow & Best Practices →](../chapter-03/README.md)

---

# Chapter 2 — Meet Your New Best Friend & Let It Take the Wheel

> **Duration:** 90 minutes | Day 1, 10:45 – 12:15

From zero to confident daily driver. Participants get hands-on with the full Copilot feature set — ghost text completions, Copilot Chat, Agent Mode, multi-file edits, the Copilot CLI, and custom instructions — leaving with the skills to use Copilot productively on their own codebase from tomorrow morning.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Install, configure, and verify GitHub Copilot in VS Code
- Use inline completions effectively — accepting, dismissing, and guiding with comments
- Use Copilot Chat with the key slash commands and context variables
- Choose the right AI model for the task at hand
- Run Agent Mode to complete a multi-step task autonomously, with appropriate human oversight
- Use Copilot Edits to apply coordinated changes across multiple files in one prompt
- Use the GitHub Copilot CLI to translate plain-English descriptions into shell commands and explain cryptic terminal commands
- Create a `copilot-instructions.md` file that tailors Copilot's behaviour to a project's standards
- Create prompt files to share reusable skill templates with the team

---

## 📋 Content Outline

### 1. Setup & First Contact (8 min)
- Installing the GitHub Copilot and GitHub Copilot Chat extensions in VS Code
  - **Two separate extensions:** *GitHub Copilot* handles inline completions; *GitHub Copilot Chat* adds the conversational panel and inline chat — both are required; both published by the verified GitHub publisher in the VS Code Marketplace
  - **Install path:** Extensions sidebar (`Ctrl+Shift+X`) → search "GitHub Copilot" → install both; the Copilot icon appears in the status bar (bottom right) immediately on installation
  - **Codespaces fallback:** if local setup fails, a pre-configured Codespace has both extensions pre-installed and pre-authenticated — keep the URL ready; no setup required
  - **JetBrains:** Settings → Plugins → Marketplace → "GitHub Copilot" → Install → restart; same core features, slightly different keyboard shortcuts; Agent Mode and MCP are currently VS Code-first
- Signing in and verifying access
  - **Sign-in flow:** after installing, VS Code prompts to sign in via browser OAuth — the extension stores the token in the OS credential manager
  - **Verify:** the status bar icon should show "Ready"; type `// function that returns the current date` in a new file — ghost text appearing within a second confirms everything is working
  - **Most common failure:** corporate proxies blocking `copilot-proxy.githubusercontent.com` — IT needs to whitelist this domain; use Codespaces as an immediate fallback
- The Copilot status bar icon — what the states mean
  - **Active (icon lit):** connected, authenticated, and ready — completions will appear as you type
  - **Inactive / dimmed:** completions disabled — manually paused or the current file type is excluded; right-click to re-enable for the current language
  - **Error / warning:** authentication failed, network unreachable, or quota exhausted — click the icon for the specific message; re-sign-in resolves most auth errors

### 2. Code Completions — Ghost Text (15 min)
- How ghost text works: trigger, accept, dismiss, partial accept
  - **What ghost text is:** a greyed-out inline suggestion that appears as you type or pause — not in your file yet; a visual overlay showing what Copilot would insert if you accept; no commitment until you press `Tab`
  - **Triggering:** suggestions appear automatically after a short pause, or immediately after typing a comment, function signature, or meaningful variable name; trigger manually with `Alt+\` if nothing appears
  - **Accept (`Tab`):** inserts the entire ghost text suggestion at the cursor position
  - **Dismiss (`Esc`):** removes the ghost text without inserting anything
  - **Partial accept (`Ctrl+→`):** accepts only the next *word* of the suggestion — press repeatedly to accept word by word; prevents rejecting a mostly-correct suggestion just because one variable name at the end is wrong
- Cycling through alternatives and the Completions Panel
  - **Multiple suggestions exist:** Copilot fetches several alternatives in parallel; `Alt+]` / `Alt+[` cycles through them inline without opening any panel
  - **Completions Panel (`Ctrl+Enter`):** shows up to 10 alternatives side-by-side — useful for teaching or when the right approach is genuinely unclear
  - **Practical advice:** most experienced users accept and modify rather than cycling — it's faster to accept the closest suggestion and overtype the wrong part than to search for a perfect match
- What Copilot reads to generate suggestions
  - **Open file content (above AND below cursor):** the most heavily weighted context; code *after* the cursor (fill-in-the-middle) constrains the suggestion to fit what follows — place a `return` statement below before asking for a function body
  - **Other open editor tabs:** open your interfaces, types, and related utilities to improve suggestion quality; close irrelevant tabs to reduce noise and save context budget
  - **Imports and type definitions:** imports signal which libraries are in scope — Copilot generates library-appropriate code when the right imports are already present
  - **`copilot-instructions.md`:** if present, injected into every request — this is how you teach Copilot your project's conventions without repeating them in every prompt (covered in Section 7)
- Writing effective comments to guide completions
  - **Comments are the highest-signal input:** a clear comment immediately above the cursor is the strongest way to direct output — more reliable than hoping Copilot infers intent from surrounding code
  - **Be specific and verb-first:** `// Returns the total price including tax and shipping, rounded to 2 decimal places` beats `// calculate price` every time; specificity directly determines output quality
  - **Describe inputs, outputs, and edge cases:** `// Accepts an array of ISO date strings, returns the most recent one, or null if the array is empty` gives Copilot everything needed for a correct implementation
  - **JSDoc / docstring as a prompt:** starting a `/** @param ... @returns ... */` block and pressing Enter triggers a high-quality implementation matching the declared contract — one of the highest-ROI completion patterns
  - **Comment-driven development:** write the algorithm as numbered comments first (`// 1. Sort by date descending`, `// 2. Filter out weekends`, `// 3. Return the first`), then let Copilot fill in each step
- Live demo: building a small utility purely through completions
  - **Demo script:** create `dateUtils.ts` with a file-level comment; write JSDoc stubs for `isWeekend()`, `formatRelative()`, and `daysBetween()` and let Copilot complete each body
  - **Deliberate bad prompt:** mid-demo, write `// fix the date` and show the vague result; then improve to a specific comment and show the quality jump — the most memorable 60-second teachable moment in the chapter

### 3. Copilot Chat — Conversational AI in the IDE (15 min)
- Chat panel vs. inline chat (`Ctrl+I`)
  - **Chat panel:** a persistent side panel maintaining full conversation history; ideal for questions, exploration, or anything requiring a full response to read before applying; toggle with the Copilot icon in the activity bar
  - **Inline chat (`Ctrl+I`):** a small prompt bar directly in the editor at the cursor; response applied to code in-place; ideal for surgical edits — "add error handling to this function", "convert this to async/await"
  - **When to use each:** inline chat for targeted edits on selected code; Chat panel for multi-turn reasoning, codebase questions, or generating new files
  - **Start fresh when changing topic:** use the `+` button to open a new thread — accumulated context from a prior conversation can mislead subsequent responses
- Key slash commands
  - `/explain` — select any code block, type `/explain`; Copilot produces a plain-English walkthrough; invaluable for onboarding, legacy code, or understanding library internals; follow up conversationally: "why is this using a closure here?"
  - `/fix` — select broken code (or paste an error message), type `/fix`; Copilot diagnoses and proposes a corrected version as a diff; most effective when the full stack trace is included alongside the code
  - `/tests` — select a function or class, type `/tests`; Copilot generates a test file covering the happy path and common edge cases; specify the framework if not obvious: `/tests using Vitest with mocked fetch`; output is a starting point — review assertions for meaning
  - `/doc` — select a function or class, type `/doc`; Copilot generates JSDoc, Javadoc, Python docstrings, or the correct format for the language; review parameter descriptions for accuracy — Copilot infers from the implementation but may miss intent or known limitations
- Key context variables
  - `@workspace` — performs semantic search across all indexed files before answering; best for: "where is authentication handled?", "how does error handling work in this project?", "which files import `OrderService`?"; click file references in the response to navigate to the relevant line
  - `#file` — type `#file` and select from the picker; includes the full file in the prompt; more targeted and faster than `@workspace` when you know which file matters; chain multiple: `Based on #file:schema.prisma and #file:userService.ts, write the missing createUser endpoint`
  - `#selection` — automatically includes the highlighted code; Copilot also sees surrounding context (imports, nearby code); this is the default in inline chat: `Ctrl+I` with a selection open scopes to it automatically
- Inline chat workflow for quick edits
  - **The full keyboard flow:** select code → `Ctrl+I` → type instruction → `Enter` → review red/green diff → `Tab` to accept or `Esc` to discard; entire edit in under 10 seconds for simple tasks
  - **Iterating:** if the result isn't right, type a follow-up in the same inline chat bar without closing — Copilot refines its previous attempt using conversation history

### 4. Choosing Your AI Model (5 min)
- Models available and when to use each
  - **GPT-4o (default):** the general-purpose workhorse — fast, strong across all languages, excellent at following instructions; right choice for 90% of everyday tasks (completions, test generation, documentation, quick explanations)
  - **Claude Sonnet:** strong at long-context accuracy and nuanced reasoning; preferred for refactoring large files, explaining complex systems, or when explanation quality is paramount
  - **o3 / reasoning models:** step-by-step reasoning before answering — significantly slower (15–60 seconds) but higher accuracy on hard problems: complex algorithms, tricky debugging, architecture decisions; not for fast completions
  - **Rule of thumb:** start with the default; switch to a reasoning model when the default answer wasn't good enough; try Claude when attaching large files or when explanation quality matters most
- How to switch models
  - **In VS Code:** click the model name dropdown in the Copilot Chat input bar — shows all models available on your plan; the selection persists for that conversation thread
  - **No cost to experimenting:** switching is free — try the same prompt on two models and compare; builds intuition for when each excels

### 5. Agent Mode — Agentic Coding (15 min)
- What is Agent Mode? Autonomous multi-step task completion inside VS Code
  - **The conceptual shift:** regular Copilot Chat gives you answers and code you then apply; Agent Mode *acts* — it reads files, writes code, runs commands, observes results, and iterates, all within a single task loop driven by your high-level goal
  - **The agent loop:** you give a task in natural language → Agent Mode forms a plan → executes steps (file read, edit, terminal command) → observes outcomes → adjusts and continues → reports back when done or when it needs input
  - **What "autonomous" means in practice:** Agent Mode can complete tasks that previously required 10–20 manual steps — finding relevant files, making consistent edits across multiple files, running tests, fixing failures — without you directing each step
  - **It is not fully autonomous:** Agent Mode pauses and asks for approval before running terminal commands (by default), and presents a plan at the start — you remain in control at every decision point
  - **Best framing:** think of Agent Mode as a capable contractor who reads the brief, does the work, and sends you a summary to approve — rather than a colleague who needs direction at every step
- How to enable Agent Mode in VS Code
  - **How to switch:** open the Copilot Chat panel → click the mode dropdown at the top of the input area → select "Agent"; or type `/agent` in the Chat input
  - **Requirements:** GitHub Copilot Chat extension v0.22+ and VS Code 1.93+; available on all paid Copilot plans (not Free tier as of early 2026 — verify current availability)
  - **Tool visibility:** when Agent Mode is active, a "Tools" panel shows which tools are available (file system, terminal, search, and any MCP tools) — useful to show participants the scope of what it can do
- Live demo: "Add input validation to all API endpoints and write tests for them"
  - **Why this is the ideal demo task:** it is multi-file, requires reading existing code first, produces verifiable output (tests pass or fail), and shows clear before/after value
  - **Narrate the agent loop:** point out when it reads files, when it starts editing, when it runs tests, and when it self-corrects after a test failure — each moment illustrates a different capability
  - **Pause at the terminal approval prompt:** let it linger; show the exact command it wants to run; explain the safety model; then approve — demonstrates the human-in-the-loop mechanism
- Reviewing and approving Agent Mode actions — staying in control
  - **The diff is your audit trail:** every file change appears as a diff in the standard VS Code change view — review these the same way you'd review any incoming PR
  - **"Keep" / "Undo":** after completion, VS Code shows a summary of all changes; keep all, undo all, or selectively undo specific file changes — full rollback always available
  - **Auto-approve mode (avoid in workshops):** configuring Agent Mode to skip terminal approval is possible but risky on a real dev machine; do not enable for participants

### 6. Copilot Edits — Multi-File Editing (8 min)
- What Copilot Edits is and when to use it
  - **The problem it solves:** inline chat operates on one file at a time — if a refactor touches 4 files, you run inline chat 4 times and manage cross-file consistency yourself; Copilot Edits applies coordinated changes across a defined set of files in a single prompt
  - **Not the same as Agent Mode:** Copilot Edits is human-directed (you choose the files, you describe the change) and faster for bounded, well-understood changes; Agent Mode is more autonomous and decides which files to touch; use Edits when you know exactly what needs changing, Agent Mode when you don't
  - **Best use cases:** renaming a type used in a model, controller, and test file; updating an API response shape referenced in multiple consumers; adding a new field to a data model and updating all read/write locations
- Opening Copilot Edits and building the working set
  - **Access:** click the pencil/edit icon in the Copilot Chat panel header, or `Ctrl+Shift+I` from the Command Palette
  - **Adding files:** drag from the VS Code Explorer into the working set; click `+` (Add Files) to search by name; or type `#file:filename.ts` in the prompt to add automatically
  - **Working set size:** 3–8 files is the practical sweet spot; too many and the model may miss subtle cross-file consistency requirements; for changes touching more than ~10 files, use Agent Mode instead
- Accepting, iterating, and undoing changes
  - **Diff review:** each modified file shows a red/green diff; navigate between changed files using the arrows in the Edits panel
  - **Accept All / Reject All:** apply or discard all proposed changes in one click; per-file accept/reject is also available if some changes are right and others aren't
  - **Follow-up prompts:** accept what's correct, then write a follow-up in the Edits panel to refine only the remaining files — iterative editing without starting over
- Demo: renaming a field consistently across a 4-file module
  - **Suggested scenario:** `user.model.ts`, `user.service.ts`, `user.controller.ts`, `user.test.ts` — rename `createdAt` to `registeredAt` including JSDoc; one prompt, four files, all references updated consistently; contrast with the tedious manual alternative

### 7. GitHub Copilot CLI — AI in the Terminal (7 min)
- What GitHub Copilot CLI is and why it matters
  - **Terminal-native AI:** GitHub Copilot CLI brings Copilot to the command line as `gh copilot`, a plugin for the GitHub CLI (`gh`) — no browser, no IDE, no context switch; AI assistance exactly where developers already work
  - **The problem it solves:** remembering precise syntax for `git`, `docker`, `kubectl`, `awk`, `curl`, and hundreds of other tools is a genuine productivity tax; Copilot CLI lets you describe what you want in plain English and get back the exact command
  - **Who benefits most:** developers who live in the terminal, DevOps engineers, SREs, and anyone writing shell scripts or working with complex CLI tooling
- Installing and setting up
  - **Prerequisites:** the GitHub CLI (`gh`) must be installed and authenticated; install from [cli.github.com](https://cli.github.com); verify with `gh auth status`
  - **Install the extension:** `gh extension install github/gh-copilot`; verify with `gh copilot --version`
  - **Shell aliases (highly recommended):** run `gh copilot alias -- bash` (or `zsh` / `fish` / `powershell`) to register `ghcs` (suggest) and `ghce` (explain) as shell functions — enables fast invocation without typing the full `gh copilot` prefix every time
- The two core commands
  - **`gh copilot suggest "description"`:** type a natural-language description of what you want to do and Copilot returns the command; e.g., `ghcs "list all Docker containers that exited in the last hour"` → Copilot returns the exact `docker ps` incantation; optionally executes it immediately
  - **`gh copilot explain "command"`:** paste any cryptic command and Copilot explains it line-by-line in plain English; invaluable for understanding inherited scripts, Stack Overflow one-liners, or unfamiliar flags
  - **Command type selection:** on first run, `suggest` asks whether you want a shell command, `gh` command, or `git` command — narrows the search space for better results; the selection is remembered across sessions
- Demo: from plain English to running command in under 10 seconds
  - **Demo script:** in a terminal, run `ghcs "find all files larger than 10 MB modified in the last 7 days and list them by size"` — show the command Copilot returns, explain each flag, then run it; follow up with `ghce "git log --oneline --decorate --graph --all"` and show the structured plain-English explanation
  - **The key insight:** this is not about avoiding learning the tools — it is about spending cognitive energy on the problem, not on syntax recall; the same way a calculator does not make you bad at maths, Copilot CLI does not make you bad at the terminal

### 8. Custom Instructions — Teaching Copilot Your Standards (14 min)
- The `.github/copilot-instructions.md` file — repository-scoped instructions
  - **What it is:** a Markdown file at `.github/copilot-instructions.md`; its content is automatically injected into every Copilot Chat and completion request made in that repository — no developer action required; it works silently on every request
  - **Scope and sharing:** instructions apply to anyone using Copilot in that repository — the whole team benefits; update the file and every team member's Copilot improves immediately on next pull
  - **Version controlled:** changes go through code review like any other configuration; treat it as a living configuration document, not a one-time setup task
  - **Keep it focused:** under 500 words — a very long file dilutes the signal; prioritise the rules that have the most impact on output quality; this is not the place to paste your entire style guide
- What to put in custom instructions
  - **Coding style and conventions:** `"This project follows the Google TypeScript Style Guide. Use named exports only — no default exports. Prefix private class members with _."` — any convention that Copilot currently ignores because it can't infer it from the code
  - **Tech stack context:** `"This project uses React 19, TypeScript 5.5, and Vite 6. Do not suggest class-based React components or the legacy Context API."` and `"Use date-fns for all date manipulation — never moment.js."` — prevents Copilot from suggesting outdated or disallowed libraries
  - **Testing requirements:** `"Every new function must have a test. Tests use Vitest and Testing Library. Mock all external HTTP calls with vi.fn()."` — ensures generated test code matches the project's test setup without prompting each time
  - **Error handling pattern:** `"All async functions must use try/catch. Use the project's AppError class for user-facing errors. Never swallow errors silently."` — consistently applied across all generated code
  - **Output preferences:** `"Be concise. Do not add preamble or repeat the question. Go straight to the answer or code."` and `"TypeScript is configured with strict: true. Never use any — use unknown with type guards instead."` — personal or team productivity preferences
- VS Code user-level custom instructions
  - **Where to set it:** VS Code Settings (`Ctrl+,`) → search "copilot instructions" → GitHub Copilot: Chat → Custom Instructions; free-text instructions that apply to all repositories
  - **What belongs here vs. the repo file:** personal preferences that follow you everywhere (preferred explanation style, verbosity level, language) — not project-specific conventions which belong in the shared repo file
  - **Layering:** both apply simultaneously; repo file typically takes precedence for project-specific rules when they conflict with user-level preferences
- Demo: creating a `copilot-instructions.md` and seeing the immediate effect
  - **Demo flow:** ask Copilot a question ("write a utility function to parse dates") in a repo with no instructions; show the generic output; add `.github/copilot-instructions.md` with 4–5 rules (use `date-fns`, TypeScript strict, no inline comments, named exports, Vitest for tests); ask the same question; show how the output now conforms to all rules without mentioning them in the prompt
  - **The "aha" moment:** same prompt, noticeably better-fitted result, purely from the instructions file — the most compelling demonstration for getting teams to adopt custom instructions immediately
  - **Live iteration:** add one more instruction mid-demo ("always add JSDoc") and show the next response includes JSDoc — demonstrates that tuning is real-time and iterative
  - **Mention:** Copilot Enterprise administrators can set org-level instructions that apply across all repositories in the organisation — scaling the same approach to every developer automatically
- Prompt files — taking Custom Instructions one step further
  - **From global defaults to named skills:** `.github/copilot-instructions.md` applies silently to every request; prompt files (`*.prompt.md` in `.github/prompts/`) are *named, reusable skill templates* you invoke deliberately for specific recurring tasks — think standing order vs. detailed recipe
  - **The format at a glance:** a `.prompt.md` file has a YAML front matter (`title`, `description`, `mode: ask/edit/agent`) followed by the full prompt body; supports `${input:variableName}` placeholders for dynamic values; embed `#file:` references to ensure the right context is always included automatically
  - **Team skill library:** commit your best prompts as files — `create-rest-endpoint.prompt.md`, `code-review-checklist.prompt.md`, `write-adr.prompt.md`; every developer invokes the same high-quality, project-aware prompt by typing `#` in Chat and selecting from the list; no more re-inventing prompts that a colleague already perfected
  - **Invoke via the Chat picker:** type `#` in Chat and select from the prompt file list; VS Code shows the `title` and `description`; `${input:}` placeholders trigger an input dialog before the prompt is sent; `mode: agent` runs an autonomous loop for multi-step tasks
- Skill files — the auto-invoked counterpart
  - **Copilot decides, not you:** unlike prompt files, skill files are loaded by the agent *automatically* when the task matches the skill's description — no `#` picker invocation; you teach the agent "when you see X, follow this expert process" and it applies it on its own
  - **File format:** a `SKILL.md` file in `.github/skills/<skill-name>/` with YAML front matter (`name`, `description`, optional `allowed-tools`) and a Markdown body of detailed instructions; can also include runnable scripts in the same directory
  - **Open standard:** follows the [`agentskills`](https://github.com/agentskills/agentskills) open spec — skills work across GitHub Copilot, Copilot CLI, and Claude; community skills are available at [`github/awesome-copilot`](https://github.com/github/awesome-copilot)

### 9. Copilot on GitHub.com — A Quick Tour (5 min)
- Copilot Chat in the browser
  - **Contextual awareness:** Chat on GitHub.com automatically detects page context — on a repository it knows the repo, on a PR it knows the diff, on an issue it knows the thread; ask "what does this project do?" on any repo page without cloning
  - **Limitations vs. IDE Chat:** no inline code editing, no slash commands that modify files — the browser experience is read-and-understand focused; for making changes, the IDE remains the right tool
- Auto-generated PR descriptions and AI code review
  - **PR descriptions:** click the ✨ sparkle icon in the PR description text area — Copilot analyses the diff and generates a structured description in seconds; use it as a first draft and add business context before submitting
  - **AI code review:** click "Request review" and select "Copilot" as the reviewer — Copilot posts inline comments at specific lines with explanations and suggested fixes; apply one-click fixes directly in the browser; a pre-flight check before human reviewers engage

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Zero to Function in 60 Seconds (10 min — after ghost text)
Participants open a blank file and implement a specified function using *only* Copilot completions — no typing logic themselves. Debrief: what prompt strategies worked best?

### Exercise: Slash Command Scavenger Hunt (15 min — after Chat section)
Provide a small unfamiliar codebase. Tasks:
1. Use `/explain` on the most confusing file
2. Use `/fix` on a file with a deliberate bug
3. Use `/tests` to generate tests for a utility function
4. Use `/doc` to document an undocumented class

Compare results in pairs — same task, different prompts, different quality.

### Exercise: Agent Mode in Action (20 min — after Agent Mode section)
Participants are given a small Express.js app with no input validation and no tests. Task: use Agent Mode to:
1. Add validation to all route handlers
2. Write unit tests for each route
3. Fix any issues Agent Mode introduces

Debrief: what did Agent Mode do well? Where did it need correction?

### Exercise: Write Your Custom Instructions (10 min — closing)
Each participant (or team) writes a `.github/copilot-instructions.md` for their real or a sample project. Swap with another team — try to "break" each other's instructions with edge-case prompts. Discuss what makes an instruction effective vs. too vague.

---

## 🔗 Resources & References
- [Getting started with GitHub Copilot](https://docs.github.com/en/copilot/getting-started-with-github-copilot)
- [GitHub Copilot keyboard shortcuts](https://docs.github.com/en/copilot/configuring-github-copilot/configuring-github-copilot-in-your-environment)
- [Copilot Chat slash commands reference](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide)
- [Using Copilot Agent Mode](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/using-copilot-coding-agent)
- [Copilot Edits — multi-file editing](https://docs.github.com/en/copilot/using-github-copilot/copilot-edits)
- [GitHub Copilot CLI — using Copilot in the terminal](https://docs.github.com/en/copilot/using-github-copilot/using-github-copilot-in-the-command-line)
- [Adding custom instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
- [Reusable prompt files for GitHub Copilot](https://docs.github.com/en/copilot/customizing-copilot/using-copilot-with-prompt-files)
- [Agent skills for GitHub Copilot](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/cloud-agent/add-skills)
- [Requesting a Copilot code review](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [Changing the AI model for Copilot Chat](https://docs.github.com/en/copilot/using-github-copilot/ai-models/changing-the-ai-model-for-copilot-chat)

---

## 🗒️ Facilitator Notes
- Allocate extra buffer for setup issues at the start — always a few participants with proxy or auth problems; keep a Copilot-enabled machine ready to mirror
- For the CLI section, ask in advance if participants have `gh` installed; if not, have the install page ready and run the demo yourself while they follow along
- Agent Mode can be slow on first run — pre-run the demo task at least twice and have a recording as backup
- The custom instructions demo is the biggest "aha" moment — save time for it; don't skip it
- Emphasise throughout: accepting every suggestion uncritically is *not* the goal; critical evaluation is half the skill

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 1 — Welcome to the AI Revolution & Power with Purpose](../chapter-01/README.md) | [Chapter 3 — Speak AI's Language: Mastering Prompts, Workflow & Best Practices →](../chapter-03/README.md)
