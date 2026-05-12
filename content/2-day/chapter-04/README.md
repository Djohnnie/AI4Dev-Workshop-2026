[🏠 Workshop Home](../../../README.md) | [← Chapter 3 — Power with Purpose: Using AI Responsibly](../chapter-03/README.md) | [Chapter 5 — Speak AI's Language: Mastering Prompts & Context →](../chapter-05/README.md)

---

# Chapter 4 — Let Your AI Co-Pilot Take the Wheel

> **Duration:** 90 minutes | Day 1, 15:00 – 16:30

Deep dive into GitHub Copilot's full feature set across the IDE, terminal, and GitHub platform. Participants move from occasional user to confident daily driver.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Use Copilot Agent Mode to complete multi-step coding tasks autonomously
- Leverage Copilot for the terminal (Copilot CLI)
- Use Copilot on GitHub.com for PR reviews, issue triage, and code search
- Apply custom instructions to tailor Copilot's behaviour to their project
- Create prompt files to codify recurring tasks as reusable, shareable skill templates
- Navigate and use the Copilot Edit (multi-file edit) feature

---

## 📋 Content Outline

### 1. Copilot Agent Mode — Agentic Coding (25 min)
- What is Agent Mode? Autonomous multi-step task completion inside VS Code
  - **The conceptual shift:** regular Copilot Chat answers questions and generates code snippets that *you* then apply; Agent Mode acts — it reads files, writes code, runs commands, observes results, and iterates, all within a single task loop driven by your high-level goal
  - **The agent loop:** you give Agent Mode a task in natural language → it forms a plan → executes steps one at a time (file read, edit, terminal command) → observes outcomes → adjusts and continues → reports back when done or when it needs your input
  - **What "autonomous" means in practice:** Agent Mode can complete tasks that previously required 10–20 manual steps — finding relevant files, understanding the codebase structure, making consistent edits across multiple files, running tests, fixing failures — without you directing each step
  - **It is not fully autonomous:** Agent Mode pauses and asks for approval before running terminal commands (by default), and surfaces a summary of what it intends to do before starting — you remain in control at every decision point
  - **Best framing for participants:** think of Agent Mode as a capable contractor who reads the brief, does the work, and sends you a summary to approve — rather than a colleague who needs direction at every step
- How it differs from Chat: Agent Mode reads files, runs terminal commands, edits multiple files, iterates
  - **Chat:** you ask a question or request code → Copilot returns a text response or code block → *you* decide what to do with it (copy, apply, ignore); the loop ends after each exchange
  - **Agent Mode:** you set a goal → Agent Mode reads the relevant files itself (no need to attach `#file` references manually) → edits multiple files → runs tests in the terminal → sees the test results → fixes failures → repeats until the goal is met
  - **Tool access:** Agent Mode has access to a set of "tools" — file system read/write, terminal command execution, web search (if enabled), and any MCP-connected tools; Chat has none of these by default
  - **Iteration:** Agent Mode can run in a loop — if it introduces a bug, it can detect it (via test failure or compiler error) and fix it without your intervention; Chat responses are one-shot
  - **Context gathering is automatic:** in Chat you must manually attach context with `#file`, `@workspace`, etc.; Agent Mode searches the codebase itself to find what it needs — it uses `@workspace` semantics internally
  - **The key practical difference:** use Chat when you want to *understand or get a suggestion*; use Agent Mode when you want to *get something done*
- Enabling Agent Mode in VS Code (Copilot Chat > switch to "Agent" mode)
  - **How to switch:** open the Copilot Chat panel → click the mode dropdown at the top of the input area (shows "Ask" or "Edit" by default) → select "Agent"; alternatively type `/agent` in the Chat input to switch modes
  - **Requirements:** Agent Mode requires the GitHub Copilot Chat extension version 0.22 or later and VS Code 1.93 or later; update both if the mode option is not visible
  - **Plan availability:** Agent Mode is available on all paid Copilot plans (Individual, Business, Enterprise); it is not available on the free tier as of early 2026 — verify current availability in the docs before the session
  - **The tools panel:** when Agent Mode is active, a "Tools" section appears in the Chat panel showing which tools are currently available (file system, terminal, search, MCP tools) — useful to show participants what Agent Mode can do
  - **Returning to Chat mode:** click the mode dropdown again and select "Ask" — any in-progress agent session is paused; the conversation history is preserved
- Live demo: "Add input validation to all API endpoints and write tests for them"
  - **Why this is the ideal demo task:** it is multi-file (multiple route handlers), requires understanding existing code (reading the routes first), produces verifiable output (tests pass or fail), and shows clear before/after value — the audience immediately understands what Agent Mode did
  - **Suggested demo repo:** a small Express.js or FastAPI app with 4–6 route handlers and no validation; pre-committed to a branch so the before state is clean
  - **Narrate the agent loop as it runs:** point out when Agent Mode reads a file ("it's scanning the routes directory"), when it starts editing ("now it's adding validation to the POST handler"), when it runs tests ("it's running `npm test` to check its work"), and when it self-corrects ("a test failed — watch it fix itself")
  - **Pause at the approval prompt:** when Agent Mode asks permission to run a terminal command, let it linger — show the audience the approval dialog, explain what it's about to run, then approve; this demonstrates the human-in-the-loop safety mechanism
  - **Expected duration:** the demo task takes 3–5 minutes of real time — pre-run it at least twice to know the timing and any edge cases; have a recording as backup
- How Agent Mode decides what tools to use (file read, terminal, search)
  - **Tool selection is model-driven:** Agent Mode prompts the underlying model with the task and the list of available tools; the model decides which tool to call at each step based on what information it needs and what action it needs to take
  - **File read tool:** used to inspect existing code before editing — Agent Mode reads the relevant files first to understand structure, naming conventions, and existing patterns before generating any changes
  - **File write/edit tool:** applies diffs to files in the workspace; edits are staged as proposed changes that appear in the diff view — not silently written to disk
  - **Terminal tool:** executes commands (test runners, build tools, package managers, linters) and reads stdout/stderr to determine success or failure; this is how Agent Mode "knows" if its code works
  - **Workspace search tool:** semantic search across the codebase to find relevant files, symbols, and patterns — Agent Mode uses this to discover where things are without you having to tell it
  - **MCP tools (if configured):** any tools registered via the Model Context Protocol — database queries, API calls, deployment commands, ticket system lookups — extend what Agent Mode can do beyond the local file system
- Reviewing and approving Agent Mode actions — staying in control
  - **The approval model:** Agent Mode presents its intended plan at the start and asks for confirmation before running terminal commands; file edits are shown as diffs that you can inspect, accept, reject, or partially accept
  - **The "Keep" / "Undo" buttons:** after Agent Mode completes, VS Code shows a summary of all changes made; you can keep all, undo all, or selectively undo specific file changes — full rollback is always available
  - **Terminal command approval:** by default, Agent Mode shows the exact command it wants to run and waits for your approval before executing; you can read the command, modify it if needed, or reject it — the agent will adapt
  - **Auto-approve mode (use with caution):** it is possible to configure Agent Mode to auto-approve terminal commands — useful in sandboxed environments for speed, but risky on a real development machine where a misguided `rm -rf` could cause damage; do not enable for workshop participants
  - **The diff is your audit trail:** every file change Agent Mode makes is shown as a diff in the standard VS Code change view; review these the same way you'd review any incoming code change — because in effect, you are
- When Agent Mode shines vs. when manual guidance is better
  - **Agent Mode shines:** repetitive cross-codebase changes (adding logging to every function, renaming a concept everywhere, migrating a pattern), generating boilerplate for a new feature following existing conventions, running test-fix-rerun cycles, and tasks where the goal is clear but the path is tedious
  - **Manual Chat is better:** exploratory tasks where you're not sure what you want (you need the back-and-forth), tasks requiring nuanced business logic decisions at each step, tasks where you want to learn by doing rather than just getting the output, and any situation where you need to reason through the problem yourself before implementing
  - **Hybrid approach:** start with Agent Mode for the scaffolding and boilerplate, then switch to manual Chat or direct editing for the business logic and edge cases — the best real-world workflow combines both
  - **Signs Agent Mode is struggling:** it loops more than 3–4 times on the same problem, asks clarifying questions repeatedly, produces changes that break more than they fix, or runs test commands in an endless fix-fail-fix cycle; these are signals to step in and provide more specific guidance
- MCP (Model Context Protocol): extending Agent Mode with custom tools
  - **What MCP is:** an open standard (published by Anthropic, adopted by GitHub and others) that defines how AI agents connect to external tools and data sources; think of it as a universal plug interface for AI tool use
  - **How it extends Agent Mode:** by registering an MCP server with VS Code, you give Agent Mode new tools beyond the built-in file system and terminal — for example, a Jira MCP server lets Agent Mode read tickets and update issue status; a database MCP server lets it query your schema
  - **MCP server examples relevant to developers:** GitHub MCP server (read/write issues, PRs, repo data), database schema explorer (query table definitions), internal API documentation server, deployment pipeline trigger (run a staging deployment from Agent Mode)
  - **Setup:** configure MCP servers in VS Code's `settings.json` under `"github.copilot.mcp.servers"`; each server specifies a command or URL that VS Code connects to; the tools it provides are then available in Agent Mode's tool list
  - **Workshop note:** MCP setup requires pre-configuration and is best shown as a facilitator demo rather than a participant exercise — flag it as "what's possible" and point to the MCP documentation for participants who want to explore after the workshop

### 2. Copilot Edits — Multi-File Editing (15 min)
- Copilot Edits vs. inline chat: editing across multiple files simultaneously
  - **Inline chat limitation:** inline chat (`Ctrl+I`) operates on the currently selected code in the active file — it has no awareness of other files and cannot coordinate changes across them; if a refactor touches 4 files, you must run inline chat 4 times and manage consistency yourself
  - **Copilot Edits solves this:** you define a "working set" of files, describe the change once, and Copilot Edits applies consistent, coordinated changes across all of them in a single operation
  - **When this matters:** renaming a type that's used in a model, a controller, and a test file; updating an API response shape that's referenced in multiple consumers; adding a new field to a data model and updating all the places that read or write it
  - **Not the same as Agent Mode:** Copilot Edits is a targeted, human-directed multi-file edit — you choose the files, you describe the change, it applies it; Agent Mode is more autonomous and decides itself which files to touch; Edits is faster and more predictable for known, bounded changes
  - **The key mental model:** Copilot Edits is a "smart find-and-refactor" across multiple files; Agent Mode is a "do this whole task autonomously"; use Edits when you know exactly which files to change, Agent Mode when you don't
- Opening Copilot Edits (the "pencil" icon in Chat panel)
  - **Access in VS Code:** click the pencil/edit icon in the Copilot Chat panel header, or select "Copilot Edits" from the Copilot menu in the top navigation; alternatively use the command `Copilot: Open Edits` from the Command Palette (`Ctrl+Shift+P`)
  - **The Edits panel layout:** the panel shows two areas — the working set (files you've added) at the top, and the prompt input at the bottom; changes will be applied as diffs that appear in the standard VS Code diff viewer
  - **Distinct from Chat history:** Copilot Edits sessions are separate from Copilot Chat conversations — they have their own history and working set; switching between Chat and Edits does not carry context across
  - **Keyboard shortcut:** `Ctrl+Shift+I` (VS Code default) opens Copilot Edits directly — worth teaching this shortcut early so participants can reach it quickly during exercises
- Adding files to the working set
  - **Drag and drop:** drag files from the VS Code Explorer panel directly into the Copilot Edits working set area — the most intuitive method for most participants
  - **The `+` button:** click the `+` (Add Files) button in the working set panel and type to search for files by name — useful for deeply nested files that are hard to navigate to in the Explorer
  - **`#file` in the prompt:** typing `#file:filename.ts` in the Edits prompt input automatically adds that file to the working set — useful when you already know the file name and don't want to switch to the Explorer
  - **Open editors shortcut:** clicking "Add Open Editors" adds all currently open tabs to the working set at once — a quick way to work on a set of files you've already been looking at
  - **Recommended working set size:** 3–8 files is the practical sweet spot; too few and you might as well use inline chat; too many and the model may miss subtle cross-file consistency requirements; if a change touches more than ~10 files, consider Agent Mode instead
  - **Remove files:** click the `×` next to any file in the working set to remove it before running the edit — keep the working set focused on only what needs to change
- Iterating on edits: accepting, rejecting, and undoing changes
  - **The diff review flow:** after Copilot Edits runs, each modified file shows a standard diff view (red = removed, green = added); you navigate between changed files using the arrows in the Edits panel
  - **Accept all:** click "Accept All" to apply every proposed change across all working set files simultaneously — use when you've reviewed the diffs and are satisfied with all of them
  - **Reject all:** click "Reject All" to discard all proposed changes and return every file to its pre-edit state — a full rollback in one click
  - **Per-file accept/reject:** each file in the working set has its own Accept/Reject buttons — you can accept changes in 3 files and reject them in 1 without affecting the others
  - **Iterating with follow-up prompts:** if a change is partially right, don't reject everything — accept what's correct, then write a follow-up prompt in the Edits panel ("The UserService changes are correct, but the test file should use Vitest's `vi.fn()` not Jest's `jest.fn()`") and run again on just the files that need adjustment
  - **Undo after accepting:** VS Code's standard undo (`Ctrl+Z`) works on accepted Edits changes — each file's edit history is preserved; you can undo individual edits per file after accepting if needed
- Demo: refactoring a module split across 4 files in a single prompt
  - **Suggested demo scenario:** a `user` module with `user.model.ts`, `user.service.ts`, `user.controller.ts`, and `user.test.ts`; task is to rename the `createdAt` field to `registeredAt` consistently across all four files, plus update the JSDoc comments that reference it
  - **Why this demo works:** the change is simple enough to understand in 10 seconds, complex enough to be tedious manually (find every occurrence across 4 files, check JSDoc, update tests), and the diff clearly shows Copilot's consistency — every reference updated, nothing missed
  - **What to highlight:** add all 4 files to the working set, write one concise prompt ("Rename `createdAt` to `registeredAt` throughout this module, including JSDoc comments and test assertions"), run it, then walk through each file's diff showing the consistent changes
  - **Contrast with the manual alternative:** point out that doing this manually would require 4 inline chats, or a regex find-and-replace that wouldn't understand the JSDoc context — Copilot Edits handles both in one prompt

### 3. GitHub Copilot CLI (15 min)
- Installing `gh copilot` (GitHub CLI extension)
  - **Prerequisites:** the GitHub CLI (`gh`) must be installed first — download from [cli.github.com](https://cli.github.com); verify with `gh --version`; authenticate with `gh auth login` if not already done
  - **Install the Copilot extension:** run `gh extension install github/gh-copilot` — installs the extension from GitHub's official extension repository; takes under 10 seconds
  - **Verify the installation:** run `gh copilot --help` — should display the available subcommands (`suggest`, `explain`) and options; if this fails, run `gh extension upgrade gh-copilot` to update
  - **Authentication:** `gh copilot` uses the same GitHub authentication as `gh auth login` — if you're signed into the CLI, you're signed into Copilot CLI; no separate API key or token needed
  - **Update regularly:** the Copilot CLI extension is updated frequently; add `gh extension upgrade --all` to your periodic maintenance routine to keep it current
  - **Windows note:** works in PowerShell, Command Prompt, and Windows Terminal; Windows participants may need to run the installer as Administrator if `gh` isn't on the system PATH
- `gh copilot suggest` — get a shell command from a natural language description
  - **Basic usage:** `gh copilot suggest "find all files modified in the last 7 days"` — prompts Copilot for a shell command, displays it, and asks whether to copy it to clipboard, explain it, or revise the request
  - **The interactive loop:** after displaying a suggestion, the CLI offers three options: (1) Copy command to clipboard, (2) Explain the command, (3) Revise the request — you stay in the loop until you have exactly what you need
  - **Shell targeting:** by default, `suggest` targets your current shell; use `-t shell`, `-t gh`, or `-t git` to specify the type of command you want — shell commands, GitHub CLI commands, or git commands respectively
  - **When it excels:** complex one-liners that require composing multiple Unix tools (`find`, `xargs`, `grep`, `awk`, `sed`); `git` commands beyond the basics (`bisect`, `rebase --interactive`, `reflog`); `docker` and `kubectl` commands with many flags; `jq` queries on nested JSON
  - **The conversational revision loop:** if the first suggestion isn't quite right, choose "Revise" and add constraints — "make it recursive", "exclude node_modules", "output as CSV" — without starting over; this is more efficient than re-typing the full description
  - **Practical tip for participants:** bookmark `gh copilot suggest` as the replacement for "google this shell command + copy from Stack Overflow"; it's faster, the result is contextualised to your request, and the revision loop avoids the trial-and-error of adapting someone else's command
- `gh copilot explain` — understand what a cryptic command does
  - **Basic usage:** `gh copilot explain "tar -czf archive.tar.gz --exclude='*.log' /var/www"` — returns a plain-English breakdown of what every flag and argument does
  - **Integration with shell history:** combine with your shell's history search: `gh copilot explain "$(history | grep docker | tail -1)"` — explain the last Docker command you ran; useful for understanding commands written by colleagues or copied from documentation
  - **Explaining scripts, not just one-liners:** paste a multi-line shell script into the explain prompt to get a section-by-section breakdown — useful for understanding CI pipeline scripts, Dockerfiles, or Makefiles you've inherited
  - **Common use cases:** decoding inherited CI/CD scripts, understanding error messages from complex build systems, demystifying `git` reflog or rebase sequences, understanding infrastructure-as-code snippets (`terraform`, `helm`, `kubectl apply -f`)
  - **The learning opportunity:** `explain` is not just for getting answers — it's a learning tool; encourage participants to explain commands they already understand to see how well Copilot explains them, then use it on commands they don't
- Supported shells: bash, zsh, PowerShell, fish
  - **Shell detection:** `gh copilot suggest` automatically detects the current shell from the `$SHELL` environment variable (Unix) or process name (Windows); suggestions are tailored to the detected shell's syntax and idioms
  - **Bash:** the most common Linux/CI shell; suggestions use standard POSIX-compatible patterns; `&&`, `||`, pipe chains, `$(command substitution)` all generated correctly
  - **Zsh:** macOS default since Catalina (2019); Copilot generates zsh-compatible syntax including glob extensions, `zmv`, and zsh-specific parameter expansions where relevant
  - **PowerShell:** Windows-native; Copilot generates `Get-ChildItem`, `Where-Object`, `ForEach-Object` pipelines rather than Unix equivalents; correctly uses `$env:VARIABLE` syntax, backtick line continuation, and PowerShell-native cmdlets
  - **Fish:** the friendly interactive shell; Copilot generates fish-compatible syntax (`set` instead of `export`, `status`, `functions`); worth flagging that fish is less commonly represented in training data so suggestions may be less idiomatic
  - **Override the target shell:** use the `-t` flag if you're generating commands for a *different* shell than the one you're running in — for example, generating a bash command to use in a CI pipeline while working in PowerShell locally
- Use cases: complex `git`, `docker`, `kubectl`, `jq` commands on demand
  - **Git:** `gh copilot suggest -t git "show all commits by John in the last month that touched the payments module"` — generates the appropriate `git log` with `--author`, `--since`, and `--` path filter; far faster than reading the `git log` man page
  - **Docker:** "create a multi-stage Dockerfile build command that targets the production stage and passes a build arg for the version number" — generates the full `docker build --target --build-arg` command correctly
  - **kubectl:** "get all pods in the payments namespace that are not in Running state and show their restart count" — generates the `kubectl get pods` with `--field-selector`, `-o jsonpath`, or `--template` as appropriate; kubectl has hundreds of flags and Copilot removes the need to memorise them
  - **jq:** "extract the name and email from each user in this array and output as CSV" — `jq` is famously difficult to compose; Copilot generates the correct filter path, iteration, and string interpolation; this alone saves significant documentation lookup time
  - **The broader principle:** any CLI tool with complex flags and syntax is a target for `gh copilot suggest`; participants should think of it as a command-line oracle for any tool they use occasionally but don't have fully memorised
- Demo: "Write a one-liner to find all TODO comments in TypeScript files"
  - **The prompt:** `gh copilot suggest "find all TODO comments in TypeScript files in the current directory, show file name and line number"` — a realistic, specific task that participants immediately relate to
  - **Expected output:** something like `grep -rn "TODO" --include="*.ts" .` or `find . -name "*.ts" -exec grep -Hn "TODO" {} \;` — either is correct; walk through why both work and what the flags mean
  - **Then use `explain`:** take the suggested command and pipe it to `gh copilot explain` to show the dual workflow — suggest to get the command, explain to understand it; this is the full CLI loop most participants will use in practice
  - **Live execution:** run the command in the terminal against the workshop repository — show real output with file names and line numbers; if the workshop repo has TODOs pre-seeded, the output is immediately satisfying and concrete

### 4. Copilot on GitHub.com (15 min)
- **Copilot Chat in the browser:** ask questions about any repository, file, PR, or issue
  - **Contextual awareness:** Copilot Chat on GitHub.com automatically detects the page context — on a repository page it knows the repo; on a PR it knows the diff; on an issue it knows the thread; you don't need to manually specify context the way you do in the IDE
  - **Ask about repos you don't own:** one of the most powerful use cases is asking questions about open-source repositories without cloning them — "how does authentication work in this Express template?", "what does this Terraform module provision?", "is there a rate limiting implementation here?"
  - **Cross-page navigation:** the Chat panel persists as you navigate between files and PRs in the same repository — you can ask about a file, navigate to a related PR, and ask a follow-up without losing the conversation thread
  - **Limitations:** no code editing from the browser Chat; cannot run commands; does not have access to private repositories unless Copilot is enabled on the organisation; for making changes, the IDE remains the right tool
- **Automatic PR descriptions:** Copilot-generated summaries based on the diff
  - **Trigger:** when creating or editing a PR description on github.com, click the ✨ sparkle icon above the description text area; Copilot analyses the full diff and commit messages and generates a structured description in seconds
  - **Structure of generated descriptions:** typically includes a "What changed" summary, a "Why" section (inferred from commit messages and issue references), a list of key files modified, and sometimes a "Testing" section noting what tests were added
  - **Connected issues and commits matter:** if your commits reference issue numbers (`Fixes #123`) or are descriptively named ("Add rate limiting middleware"), the generated description quality improves significantly — another incentive for good git hygiene
  - **Enterprise at scale:** on Copilot Enterprise, PR description generation can be configured as a default step in the PR template, making AI-generated descriptions the starting point for every PR across the organisation
  - **What participants should do with the output:** use it as a first draft — add business context that Copilot can't infer from code alone ("This resolves the timeout issue reported by Customer X in ticket #456"), check accuracy of the summary, and personalise the tone before submitting
- **Copilot code review:** request an AI review on any PR — line-level suggestions
  - **How to request:** on any open PR, click "Request review" and select "Copilot" as the reviewer (appears alongside human team members); alternatively click "Copilot review" in the PR sidebar; Copilot begins analysing the diff immediately
  - **What Copilot reviews for:** code correctness, potential bugs, missing error handling, security patterns (SQL injection, XSS), performance concerns, and documentation gaps — it focuses on substantive code issues, not style or formatting (no "missing semicolon" comments)
  - **Line-level suggestions:** Copilot posts inline review comments at the specific lines it wants to flag, with an explanation of the concern and a suggested fix code snippet; the same UI as human reviewer comments
  - **Applying suggestions:** suggested code changes can be applied directly in the browser with one click ("Apply suggestion"), creating a new commit on the PR branch — no need to pull locally, make the change, and push
  - **Complements human review, doesn't replace it:** Copilot reviews are a "pre-flight check" that catches obvious issues before human reviewers spend time on them; human reviewers should still focus on architecture, business logic correctness, and organisational context
  - **Responding to Copilot comments:** you can reply to Copilot's review comments, resolve them, or mark them as "Won't fix" — the same workflow as responding to any human reviewer; Copilot doesn't re-review based on replies (no conversation loop on github.com)
- **Copilot for issues:** ask Copilot to summarise or suggest next steps for an issue
  - **Issue summarisation:** on any issue page, ask "Summarise this issue and its current status" — Copilot reads the original issue body, all comments, linked PRs, and labels to produce a concise summary; invaluable for long issues with 50+ comments
  - **Suggest next steps:** ask "What should be done to resolve this issue?" — Copilot analyses the discussion and suggests concrete next steps; useful for triage, for contributors picking up issues, or for PMs trying to understand what's blocking a bug fix
  - **Draft a response:** ask "Draft a comment asking the reporter for more information about their environment" — Copilot writes a polite, professional request for the reproduction steps, OS version, or error logs needed to debug the issue
  - **Issue-to-code bridge:** on Copilot Enterprise with Copilot Workspace, clicking "Open in Copilot Workspace" from an issue begins an autonomous planning and implementation session; Copilot Workspace reads the issue, proposes code changes, and opens a draft PR — the full issue-to-PR pipeline with minimal human input
  - **Triaging multiple issues:** ask "Are there other open issues related to this authentication bug?" — Copilot can search across the repository's issues to find duplicates or related threads, reducing the triaging effort for maintainers
- **Copilot Search:** natural language code search across the repository
  - **What it replaces:** traditional code search requires knowing the exact symbol name, file path, or regex pattern; natural language search lets you describe *what you're looking for* without knowing where it is
  - **How to access:** in the GitHub.com search bar, switch to "Code" search and phrase your query in natural language — or use Copilot Chat with `@workspace` semantics to describe the code you're looking for
  - **Example queries:** "Where is the email notification sent?", "Find the function that calculates shipping costs", "Show me where database connections are pooled" — these work even when you don't know the function name or file
  - **Semantic matching:** natural language search uses embeddings and semantic similarity rather than exact text matching — it finds code that *does* what you describe, not just code that *contains* the words you typed
  - **Use case for onboarding:** one of the highest-value use cases is new team member onboarding — finding where things are in an unfamiliar codebase without needing to ask a colleague or spend 20 minutes grepping; combine with `@workspace` Chat for the fastest codebase orientation

### 5. Custom Instructions — Teaching Copilot Your Standards (15 min)
- The `.github/copilot-instructions.md` file — repository-scoped instructions
  - **What it is:** a Markdown file committed to the repository at `.github/copilot-instructions.md`; its content is automatically injected into every Copilot Chat and completion request made in that repository — without the developer needing to paste context manually
  - **Scope:** instructions apply to anyone using Copilot in that repository — the whole team benefits from the same context; update the file and every team member's Copilot improves immediately on next pull
  - **Discovery:** Copilot reads this file automatically; developers don't need to reference it or do anything special — it works silently in the background on every request
  - **Version controlled:** because it's a committed file, changes go through code review like any other configuration; propose changes via PR, review the effect, merge — treat it as a living configuration document
  - **What it cannot do:** override GitHub's safety policies, force Copilot to use a specific model, or guarantee a specific output format 100% of the time — instructions influence the model, they don't rigidly constrain it; write them as guidelines, not hard rules
  - **File size guidance:** keep it focused and under 500 words — a very long instructions file dilutes the signal; the model pays less attention to instructions buried deep in a long document; prioritise the most impactful rules
- VS Code user-level custom instructions (settings)
  - **Where to set it:** VS Code Settings (`Ctrl+,`) → search "copilot instructions" → "GitHub Copilot: Chat → Custom Instructions"; enter free-text instructions that apply to all your Copilot sessions across all repositories
  - **Scope:** user-level instructions apply to every workspace you open — unlike `.github/copilot-instructions.md` which is per-repository; ideal for personal preferences that follow you everywhere (preferred explanation style, language, verbosity)
  - **Layering with repository instructions:** both apply simultaneously — user-level instructions are combined with the repository's `.github/copilot-instructions.md`; if they conflict, the repository file typically takes precedence for project-specific rules
  - **What belongs here:** personal preferences that don't belong in a shared repo file — "Always explain your reasoning before showing code", "I prefer concise answers over thorough ones", "I am working in British English", "When in doubt, ask me a clarifying question rather than assuming"
  - **JetBrains equivalent:** JetBrains IDEs support custom instructions via the Copilot plugin settings under "Custom instructions" — same concept, slightly different UI location
- What to put in custom instructions:
  - Coding style and conventions
    - **Language and style guide reference:** "This project follows the Google TypeScript Style Guide. Prefer `const` over `let`. Use named exports only — no default exports."
    - **Naming conventions:** "Use `camelCase` for variables and functions, `PascalCase` for classes and types, `SCREAMING_SNAKE_CASE` for constants. Prefix private class members with `_`."
    - **Error handling pattern:** "All async functions must use try/catch. Never swallow errors silently — always log or rethrow. Use the project's `AppError` class for user-facing errors."
    - **Comment policy:** "Do not add comments to self-explanatory code. Only comment *why*, not *what*. JSDoc is required for all public functions and classes."
  - Tech stack context (framework versions, preferred libraries)
    - **Be specific about versions:** "This project uses React 19 with the new compiler, TypeScript 5.5, Vite 6, and Tailwind CSS 4. Do not suggest class-based React components or the legacy Context API."
    - **Preferred libraries:** "Use `zod` for all schema validation. Use `date-fns` for date manipulation — never `moment.js`. Use `axios` for HTTP requests — never the native `fetch` API."
    - **Database and ORM:** "This project uses PostgreSQL 16 via Prisma ORM. Write all database queries using Prisma Client — never raw SQL unless specifically asked."
    - **Testing framework:** "Tests use Vitest and Testing Library. Mock external dependencies using `vi.fn()`. Do not use Jest syntax."
  - Testing requirements (always use Jest, always include edge cases)
    - **Coverage expectations:** "Every new function must have a corresponding test. Tests should cover: the happy path, at least two edge cases, and one error/failure scenario."
    - **Test file location:** "Place test files in a `__tests__` directory adjacent to the source file. Name test files `<filename>.test.ts`."
    - **Assertion style:** "Use `expect(...).toEqual(...)` for deep equality. Use `expect(...).toThrow(...)` for error cases. Do not use `toBe` for object comparisons."
    - **Mocking policy:** "Mock all external HTTP calls. Do not make real network requests in tests. Use `vi.mock()` for module-level mocks."
  - Output preferences (no comments unless requested, TypeScript strict mode)
    - **Verbosity:** "Be concise. Do not repeat the question back to me. Do not add a preamble — go straight to the answer or code."
    - **TypeScript strictness:** "TypeScript is configured with `strict: true`. Never use `any`. Use `unknown` for truly unknown types and narrow with type guards."
    - **Response format:** "When showing code, show the full relevant function or block — not just the changed lines. Include imports if they are new."
    - **Explanations:** "After generating code, add a brief (2–3 sentence) explanation of what it does and any assumptions made — unless I ask you not to."
- Demo: creating a `copilot-instructions.md` and seeing the immediate effect on suggestions
  - **Demo setup:** prepare a repository with no custom instructions; ask Copilot a question that would benefit from context (e.g., "write a utility function to parse dates") and show the generic output
  - **Create the file:** add `.github/copilot-instructions.md` with 4–5 specific rules (use `date-fns`, TypeScript strict mode, no comments, named exports, Vitest for tests); save the file — no restart needed
  - **Ask the same question again:** "write a utility function to parse dates" — show how the output now uses `date-fns`, has proper TypeScript types, no inline comments, and a named export — without you mentioning any of those requirements in the prompt
  - **The "aha" moment:** the participant types the same prompt and gets a noticeably better-fitted result purely because of the instructions file; this is the most compelling demo for getting teams to adopt custom instructions immediately
  - **Iterate live:** add one more instruction mid-demo ("always add JSDoc") and show the next response now includes JSDoc — demonstrates the real-time, iterative nature of tuning the file
- Organisation-level custom instructions (Copilot Enterprise)
  - **What they are:** organisation administrators can set a system-level instruction that is injected into every Copilot Chat request across the entire organisation — all repositories, all users, without any per-repo file needed
  - **The difference from `.github/copilot-instructions.md`:** org-level instructions apply everywhere in the org automatically; repo-level instructions apply only in that specific repository; both apply simultaneously when present, combined into the model's system prompt
  - **Use cases:** enforcing organisation-wide standards that don't belong in any single repository — "Always recommend our internal `acme-sdk` package over third-party equivalents", "Code must comply with our Security Policy — never suggest disabling SSL verification", "Responses must be in British English"
  - **Admin access required:** organisation-level custom instructions are configured by Copilot Enterprise administrators in the organisation's GitHub settings under Copilot → Policies; individual users cannot see or override the org-level instruction text
  - **Governance benefit:** ensures every developer in the organisation gets a consistent Copilot experience aligned to company standards — no individual needing to remember to set their personal instructions, no inconsistency between teams

### 6. Prompt Files & Skill Files — Capturing Team Knowledge (15 min)
- What prompt files are and how they differ from custom instructions
  - **A different layer of customisation:** `.github/copilot-instructions.md` sets *global* defaults that apply silently on every request; prompt files define *named, reusable skill templates* for specific recurring tasks that you invoke deliberately when you want them
  - **The file format:** a `.prompt.md` file is a Markdown file with a YAML front matter block defining `title`, `description`, and `mode`, followed by the full prompt body — the same quality you'd craft for a well-written one-off Chat prompt, but captured and version-controlled permanently
  - **Stored in `.github/prompts/`:** place prompt files at `.github/prompts/<skill-name>.prompt.md`; VS Code discovers them automatically and makes them selectable in Chat without any additional configuration
  - **Team-wide reuse:** because prompt files are committed alongside the codebase, every team member gets the same curated skill library — write a great prompt once, share the quality improvement with the team indefinitely
- Structure of a prompt file
  - **Front matter fields:** `title` — shown in the VS Code picker; `description` — a tooltip explaining when to use the skill; `mode` — `ask` (Chat response), `edit` (applies changes to selected code), or `agent` (runs an autonomous task loop until the goal is met)
  - **Prompt body:** a full natural-language instruction written exactly as you'd type it into Chat; supports `${input:variableName}` placeholders that prompt the user for values at invocation time — e.g., `${input:resourceName}` shows a small VS Code dialog before the prompt is sent
  - **Embedded file references:** include `#file:src/routes/users.ts` directly in the prompt body to ensure the right context is always attached — the skill "knows" which files it needs without the developer having to remember each time
  - **Example prompt file:**
    ```yaml
    ---
    title: Create REST Endpoint
    description: Scaffold a CRUD endpoint with validation, error handling, and tests
    mode: agent
    ---
    Create a REST endpoint for `${input:resourceName}` following the patterns in
    #file:src/routes/users.ts. Use our AppError class for error handling and write
    Vitest tests covering the happy path and key error cases.
    ```
- Practical prompt file examples for a development team
  - **`create-rest-endpoint.prompt.md`:** scaffolds a full CRUD endpoint with validation, error handling, and tests — parameterised by resource name; a 30-minute boilerplate task becomes a 90-second invocation
  - **`code-review-checklist.prompt.md`:** runs your team's specific review criteria against any selected code block — security checks, naming conventions, edge case coverage, missing test assertions; standardises review quality across all reviewers
  - **`add-error-handling.prompt.md`:** applies your project's error handling pattern to any selected async function — point Copilot at the function, invoke the skill, review and accept
  - **`write-adr.prompt.md`:** generates an Architectural Decision Record from your organisation's template — parameterised by decision title; captures the decision, context, alternatives, and consequences in one structured document
  - **`generate-db-migration.prompt.md`:** given a description of a schema change, generates a migration file following your ORM conventions — eliminates the syntax lookup that slows migration authoring
- How to invoke prompt files in VS Code
  - **Via the Chat picker:** type `#` in the Chat input and select from the prompt file list — VS Code shows each file's `title` and `description`; selecting it renders the prompt in the Chat input ready to send or edit further
  - **Via direct reference:** type `#file:.github/prompts/create-rest-endpoint.prompt.md` in the Chat input for direct access when you already know which skill you want
  - **Filling in variables:** if the prompt contains `${input:}` placeholders, VS Code shows an input dialog for each variable before assembling and sending the final prompt
  - **Mode determines the outcome:** `ask` returns a response to read; `edit` applies changes in-place; `agent` runs an autonomous loop — use agent mode for multi-step tasks like scaffolding a full feature with tests and documentation
- Skill files — what the agent reaches for automatically
  - **The key distinction from prompt files:** you invoke prompt files manually by picking them from the Chat picker; Copilot invokes skill files *on its own* — it reads each skill's `description` and decides whether the current task warrants loading it; the agent reaches for the right skill without being asked, the way an expert reaches for the right tool
  - **The file format:** each skill lives in its own subdirectory with a `SKILL.md` file (this filename is required) under `.github/skills/<skill-name>/` for project skills, or `~/.copilot/skills/<skill-name>/` for personal skills shared across all your projects; the `SKILL.md` file has YAML front matter (`name`, `description`, optional `allowed-tools`) and a Markdown body with detailed instructions, step-by-step processes, and examples
  - **Skills can bundle runnable scripts:** place a shell script alongside `SKILL.md` in the same directory and reference it in the instructions; add `allowed-tools: shell` to the front matter to allow the agent to run it without prompting for permission — e.g., an `image-convert` skill bundles a `convert-svg-to-png.sh` script with instructions for when and how to call it
  - **An open standard:** the SKILL.md format follows the [`agentskills`](https://github.com/agentskills/agentskills) open specification — skills written for GitHub Copilot are compatible with Claude and other supporting agents; a colleague can share a skill from the [`github/awesome-copilot`](https://github.com/github/awesome-copilot) community collection and you simply drop the directory into `.github/skills/`
  - **Example — CI debugging skill:**
    ```yaml
    ---
    name: github-actions-failure-debugging
    description: Guide for debugging failing GitHub Actions workflows. Use this when asked to debug failing GitHub Actions workflows.
    ---
    1. Use list_workflow_runs to find recent failing runs for the PR
    2. Use summarize_job_log_failures for an AI summary without consuming the full log
    3. If needed, use get_job_logs for the complete failure output
    4. Reproduce the failure locally and fix it before committing
    ```
  - **Example — project conventions skill:** a `project-patterns` skill that documents your error handling conventions, the specific `AppError` subclasses available, and correct usage examples; Copilot loads it automatically when working on error-related tasks — the right context arrives without the developer manually attaching files
  - **Prompt files vs. skill files — when to use each:**
    - Use a **prompt file** when you want a specific reusable prompt you invoke deliberately for a named task (e.g., "scaffold an endpoint with our pattern")
    - Use a **skill file** when you want the agent to consistently apply an expert process whenever it encounters a category of work (e.g., "always follow this debugging procedure for CI failures")

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Agent Mode in Action (20 min)
Participants are given a small Express.js (or equivalent) app with no input validation and no tests. Task: use Agent Mode to:
1. Add validation to all route handlers
2. Write unit tests for each route
3. Fix any issues Agent Mode introduces

Debrief: what did Agent Mode do well? Where did it need correction?

### Exercise: CLI Scavenger Hunt (10 min)
Participants use `gh copilot suggest` to discover shell commands for 5 tasks they'd normally Google. Compare the suggested commands to what they'd have typed. Discuss accuracy.

### Exercise: Write Your Custom Instructions (10 min)
Each participant (or team) writes a `.github/copilot-instructions.md` for their real or a sample project. Swap with another team — try to "break" each other's instructions with edge-case prompts.

### Closing Challenge: Day 1 Rapid Fire (10 min)
Teams compete: who can complete a given list of tasks fastest using any Copilot feature covered today? Tasks include: explain a function, fix a bug, generate a test, write a CLI command, add a PR description.

---

## 🔗 Resources & References
- [Using Copilot Agent Mode](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/using-copilot-coding-agent)
- [Copilot Edits (multi-file editing)](https://docs.github.com/en/copilot/using-github-copilot/copilot-edits)
- [GitHub Copilot for CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli/about-github-copilot-in-the-cli)
- [Requesting a Copilot code review](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [Adding custom instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
- [Reusable prompt files for GitHub Copilot](https://docs.github.com/en/copilot/customizing-copilot/using-copilot-with-prompt-files)
- [Agent skills for GitHub Copilot](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/cloud-agent/add-skills)
- [MCP with GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/extending-copilot-chat-with-mcp)

---

## 🗒️ Facilitator Notes
- Agent Mode requires internet access and can be slow — factor this into exercise timing
- Ensure participants have `gh` CLI installed before this session (ideally in prerequisites)
- End Day 1 with energy: the "Rapid Fire" challenge creates a great buzz going into dinner

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 3 — Power with Purpose: Using AI Responsibly](../chapter-03/README.md) | [Chapter 5 — Speak AI's Language: Mastering Prompts & Context →](../chapter-05/README.md)