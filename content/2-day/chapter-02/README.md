[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 1 — Welcome to the AI Revolution!](../chapter-01/README.md) | [Chapter 3 — Power with Purpose: Using AI Responsibly →](../chapter-03/README.md)

---

# Chapter 2— Meet Your New Best Friend: GitHub Copilot

> **Duration:** 90 minutes | Day 1, 10:45 – 12:15

Hands-on introduction to GitHub Copilot in the IDE. Participants go from zero to confidently using completions and Copilot Chat by the end of the session.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Install and configure GitHub Copilot in VS Code (and JetBrains IDEs)
- Use and navigate inline code completions effectively
- Open and use Copilot Chat for Q&A and code generation
- Switch between AI models available in Copilot
- Explain the difference between completions, inline chat, and the chat panel

---

## 📋 Content Outline

### 1. Setup & First Contact (15 min)
- Installing the GitHub Copilot and GitHub Copilot Chat extensions in VS Code
  - **Two separate extensions:** *GitHub Copilot* handles inline completions; *GitHub Copilot Chat* adds the conversational panel and inline chat — both are needed and both are free to install
  - **Install path:** Extensions sidebar (`Ctrl+Shift+X`) → search "GitHub Copilot" → install both; they are published by GitHub (verified publisher badge); restart not usually required
  - **Codespaces:** if using GitHub Codespaces, both extensions are pre-installed and pre-authenticated — participants can skip local setup entirely and be productive immediately
  - **Verify installation:** the Copilot icon should appear in the VS Code status bar (bottom right); hovering shows the current status and the signed-in account
- Signing in with GitHub account and verifying license/free tier
  - **Sign-in flow:** after installing, VS Code prompts to sign in via browser — click "Allow", complete GitHub OAuth, return to VS Code; the extension stores the token securely in the OS credential manager
  - **Verify access:** open the Copilot status bar icon → it should show "Ready" or "Active"; alternatively, run `GitHub Copilot: Check Status` from the Command Palette (`Ctrl+Shift+P`)
  - **Free tier confirmation:** navigate to [github.com/settings/copilot](https://github.com/settings/copilot) — the page shows current plan, monthly usage (completions and chat messages remaining), and quota reset date
  - **If quota is exhausted:** the status bar icon changes to a warning state and completions silently stop — easy to miss; remind participants to check their quota at the start of the workshop
  - **Enterprise/Business users:** the organisation admin may have pre-assigned a seat; participants should confirm with their IT/engineering manager if they don't see Copilot in account settings
- The Copilot status bar icon — what the states mean (active, inactive, error)
  - **Active (icon lit):** Copilot is connected, authenticated, and ready to suggest; completions will appear as ghost text as you type
  - **Inactive / paused (icon dimmed):** Copilot is installed and authenticated but completions are disabled — this happens when you manually disable it, or when the current file type is excluded in settings
  - **Error (icon with warning):** authentication failed, network unreachable, or quota exhausted — click the icon for a specific error message and suggested action
  - **Per-language and per-repository toggle:** right-clicking the status bar icon shows options to disable Copilot for the current language or for the current repository — useful for files you don't want AI assistance on (e.g., `.env` files, sensitive configs)
  - **Spinning indicator:** Copilot is actively fetching a suggestion from the API — visible on slower connections; if it spins indefinitely, it usually indicates a network/proxy issue
- Quick tour of JetBrains plugin for those not on VS Code
  - **Install path:** JetBrains IDE → Settings → Plugins → Marketplace → search "GitHub Copilot" → Install → restart IDE; the plugin is published by GitHub
  - **Sign-in:** after restart, a notification prompts to log in via GitHub; follow the browser OAuth flow — identical to VS Code
  - **Key differences from VS Code:** inline completions behave the same; Chat is accessible via a side panel or `Alt+\`; some slash commands and `@` participants may differ slightly between IDEs — check the JetBrains-specific docs
  - **Keyboard shortcuts differ:** `Tab` to accept, `Esc` to dismiss — same; but cycling suggestions is `Alt+]` / `Alt+[` in VS Code vs. `Alt+[` / `Alt+]` in some JetBrains IDEs (verify for the specific IDE version)
  - **Feature parity note:** JetBrains support is slightly behind VS Code on the newest features (Agent Mode, MCP) — flag this for affected participants so they know what to expect
- Supported languages — and how well Copilot performs across them
  - **Tier 1 — strongest performance:** Python, JavaScript, TypeScript, Go, Ruby, Java, C#, C++ — these dominate public GitHub repos and Copilot's training data; suggestions are highly accurate, idiomatic, and contextually aware
  - **Tier 2 — good performance:** Rust, PHP, Kotlin, Swift, Shell/Bash, SQL, HTML/CSS — solid completions, occasionally less idiomatic; still a significant productivity boost
  - **Tier 3 — limited but functional:** R, MATLAB, Lua, Elixir, Clojure, Dart — fewer training examples means more hallucinations and less idiomatic output; useful for boilerplate, less reliable for complex logic
  - **Non-code files:** Copilot works in Markdown, YAML, JSON, Dockerfile, `.env` templates, and configuration files — often surprisingly helpful for infra-as-code and CI/CD pipelines
  - **Performance tip:** the more code you have in the current file and open tabs, the better the suggestions — Copilot needs context to infer the project's patterns, naming conventions, and style

### 2. Code Completions — Ghost Text (20 min)
- How ghost text works: trigger, accept (`Tab`), dismiss (`Esc`), partial accept (`Ctrl+→`)
  - **What "ghost text" is:** a greyed-out inline suggestion that appears as you type or pause — it is not in your file yet; it is a visual overlay showing what Copilot would insert if you accept
  - **Triggering a suggestion:** suggestions appear automatically after a short pause, or immediately after typing a comment, a function signature, or a meaningful variable name; you can also trigger manually with `Alt+\` (VS Code)
  - **Accept (`Tab`):** inserts the entire ghost text suggestion at the cursor position; this is the most common action
  - **Dismiss (`Esc`):** removes the ghost text without inserting anything; keeps you in flow without committing to a suggestion
  - **Partial accept (`Ctrl+→`):** accepts only the next *word* of the suggestion rather than the whole thing — useful when the beginning of a suggestion is right but the tail needs correction; press repeatedly to accept word by word
  - **Why partial accept matters:** prevents the common frustration of rejecting a mostly-correct suggestion just because one variable name is wrong — accept what's right, overtype what isn't
- Cycling through alternative suggestions (`Alt+]` / `Alt+[`)
  - **Multiple suggestions exist:** Copilot fetches several alternative completions in parallel, not just one; the ghost text shows the top-ranked option by default
  - **`Alt+]` (next) / `Alt+[` (previous):** cycles through the ranked alternatives without opening any panel — instant, stays inline
  - **When to cycle:** when the first suggestion doesn't match your intent but you suspect a valid alternative exists; common when variable names or return types differ between alternatives
  - **"Open Completions Panel" (`Ctrl+Enter`):** opens a dedicated panel showing up to 10 alternative suggestions side-by-side — useful when you want to compare and pick the best one, or when teaching the feature
  - **Practical advice:** most experienced users rarely cycle — they either accept and modify, or dismiss and guide Copilot with a better comment; cycling is more useful for beginners building intuition
- What Copilot reads to generate suggestions:
  - **Open file content (above and below cursor):** the most heavily weighted context; code before the cursor tells Copilot what's been defined; code *after* the cursor (the "fill-in-the-middle" technique) constrains the suggestion to fit the structure that follows
  - **Other open tabs:** Copilot scans other open editor tabs for relevant patterns — having your interfaces, types, and related utilities open in tabs meaningfully improves suggestions; close irrelevant tabs to reduce noise
  - **File name and language:** the filename and detected language set Copilot's expectations — a file called `userService.ts` signals TypeScript patterns, service-layer conventions, and likely dependency injection; a file called `Makefile` signals build tool patterns
  - **Imports and type definitions:** the imports at the top of the file signal which libraries and types are in scope — Copilot uses these to suggest library-appropriate methods and correctly typed expressions
  - **`copilot-instructions.md`:** if present, the custom instructions file is injected into every request — this is how you teach Copilot your project's conventions without repeating them in every prompt
- Writing effective comments to guide completions
  - **Comments are the highest-signal context:** a clear comment immediately above the cursor is the strongest way to direct what Copilot generates — more reliable than hoping it infers intent from surrounding code
  - **Be specific and verb-first:** `// Returns the total price including tax and shipping, rounded to 2 decimal places` beats `// calculate price`; Copilot will generate a much more accurate function from the specific version
  - **Describe inputs, outputs, and constraints in the comment:** `// Accepts an array of ISO date strings, returns the most recent one, or null if the array is empty` gives Copilot everything it needs to write a correct implementation
  - **Use JSDoc / docstring format as a prompt:** starting a `/** @param ... @returns ... */` block before a function and pressing Enter triggers a high-quality implementation matching the declared signature
  - **Step-by-step comments for complex logic:** write the algorithm as numbered comments first (`// 1. Sort by date descending`, `// 2. Filter out weekends`, `// 3. Return the first result`), then let Copilot fill in each step — this is the "comment-driven development" pattern
  - **Don't over-describe:** a comment that's longer than the function itself is a sign to simplify; aim for one clear sentence that captures the *what*, not the *how*
- Live demo: building a small utility class purely through completions
  - **Suggested demo script:** create a new `dateUtils.ts` file; start with a file-level comment (`// Utility functions for date manipulation — no external dependencies`); then write a series of JSDoc comments for 3–4 functions and let Copilot complete each one
  - **Good demo functions to use:** `isWeekend(date: Date)`, `formatRelative(date: Date)` ("2 days ago"), `getStartOfWeek(date: Date)`, `daysBetween(a: Date, b: Date)` — all have clear intent, non-trivial implementations, and visible correctness
  - **What to highlight:** how opening the file with a meaningful name and top-level comment sets the tone for all subsequent suggestions; how each accepted suggestion provides context for the next; how the file "teaches itself" as it grows
  - **Deliberate mistake to include:** introduce a vague comment mid-demo (`// fix the date`) and show the poor suggestion it produces — then improve the comment and show the improved result; demonstrates the direct link between comment quality and output quality

### 3. Copilot Chat — Conversational AI in the IDE (25 min)
- Opening the Chat panel vs. inline chat (`Ctrl+I` / `Cmd+I`)
  - **Chat panel:** a persistent side panel (toggle with the Copilot icon in the activity bar or `Ctrl+Alt+I`); maintains full conversation history for the session; ideal for longer back-and-forth, exploration, and multi-step tasks; supports all `@` participants, `#` variables, and slash commands
  - **Inline chat (`Ctrl+I` / `Cmd+I`):** opens a small prompt bar directly in the editor at the cursor position; the response is applied to the code in-place without leaving the editor; ideal for quick, targeted edits ("add error handling to this function", "make this async")
  - **When to use each:** inline chat for surgical edits to specific code; Chat panel for questions, explanations, generating new files, or anything requiring a full response you want to read before applying
  - **New chat vs. continuing a thread:** start a new Chat thread when changing topic — stale context from a prior conversation can mislead Copilot; use "New Chat" (`+` button) to clear history
  - **Chat history persistence:** conversation history lasts for the VS Code session; closing and reopening VS Code clears it — participants may be surprised when their earlier context disappears
- **Slash commands:**
  - `/explain` — understand unfamiliar code
    - Select any code block, open inline chat or Chat panel, and type `/explain`; Copilot provides a plain-English walkthrough of what the code does, line by line if needed
    - Works on entire files, individual functions, or even a single expression — the more specific the selection, the more focused the explanation
    - Useful for onboarding to a new codebase, understanding a library's internals, or decoding legacy code written by someone who left the team
    - Follow-up prompts work well: "why is this using a closure here?" or "what happens if the input is null?" extend the explanation conversationally
  - `/fix` — diagnose and fix errors
    - Select broken code or paste an error message, then type `/fix`; Copilot diagnoses the problem and proposes a corrected version with an explanation of what was wrong
    - Most effective when the error message is included — paste the full stack trace or compiler error into the chat alongside the code
    - Applies the fix as a diff you can accept, reject, or partially accept; does not auto-commit — you review before anything changes
    - Can be combined with `@terminal`: paste the terminal error and run `/fix` for Copilot to see both the code and the exact error output together
  - `/tests` — generate unit tests
    - Select a function or class and type `/tests`; Copilot generates a test file (or test block) covering the main happy path, common edge cases, and error conditions
    - Specify the testing framework in the prompt if not obvious from the project: `/tests using Vitest with mocked fetch`
    - Output is a starting point, not complete coverage — participants should review, add business-logic-specific cases, and verify assertions are meaningful, not trivially true
    - Works best when the function being tested has clear inputs, outputs, and a docstring — the more context Copilot has about intent, the better the tests
  - `/doc` — generate documentation comments
    - Select a function, class, or module and type `/doc`; Copilot generates JSDoc, Javadoc, Python docstrings, or the appropriate format for the language
    - Include parameter types, return values, thrown exceptions, and usage examples — Copilot infers these from the implementation but may miss nuance, so review carefully
    - Particularly useful for documenting existing undocumented code; ask `/doc` on the whole file for a systematic sweep
    - Follow up with "add an example showing how to call this with invalid input" to enrich the generated docs
  - `/new` — scaffold a new file/project
    - Type `/new` followed by a description: `/new Express.js REST API with TypeScript, JWT auth, and a /users CRUD endpoint`; Copilot generates a folder structure and file contents
    - Output is created as a proposed diff you can accept into the workspace — not auto-written to disk without your confirmation
    - Most useful for bootstrapping a new microservice, utility module, or configuration file from a natural language spec
    - Combine with custom instructions for framework-specific scaffolding that matches your team's patterns
  - `/newNotebook` — create a Jupyter notebook
    - Type `/newNotebook` with a description of what the notebook should explore: `/newNotebook Analyse sales data CSV: load, clean, plot monthly trends, and summarise key statistics`
    - Generates a `.ipynb` file with pre-populated cells — markdown headers, code cells, and even visualisation boilerplate
    - Particularly useful for data scientists and analysts who want to prototype analyses quickly; less relevant for pure software engineering contexts (flag this to participants)
- **Chat participants (@ variables):**
  - `@workspace` — ask questions about the entire repo
    - Indexes the entire open workspace and performs semantic search before answering — not just keyword matching; understands code relationships
    - Best for: "where is authentication handled?", "which files import the `OrderService`?", "how does error handling work in this project?", "what does this repo do overall?"
    - Response includes file references and code snippets — click to navigate directly to the relevant line
    - Slower than asking about a specific file (`#file`) because it scans the whole codebase; trade response time for broader answers
  - `@vscode` — ask about IDE settings and commands
    - Answers questions about VS Code itself: keyboard shortcuts, settings, extensions, debugger configuration, tasks — without leaving the editor
    - Examples: "how do I configure the Jest test runner in VS Code?", "what's the shortcut to split the editor?", "how do I set up launch.json for Node.js debugging?"
    - Pulls from VS Code's own documentation — more accurate than asking a general LLM about IDE-specific config
    - Particularly useful in workshops where participants are unfamiliar with VS Code setup details
  - `@terminal` — explain terminal errors
    - References the content of the integrated terminal — Copilot can "see" what's currently in the terminal output
    - Type `@terminal /explain` and Copilot explains the last error or command output in plain English
    - Works great for cryptic compiler errors, failed test output, Docker errors, and `npm install` dependency conflicts
    - Can be followed with `/fix` to get a suggested code change based on the terminal error
- **Context variables (# variables):**
  - `#file` — attach a specific file
    - Type `#file` and select a file from the picker; Copilot includes the full file content in the prompt — even if it's not currently open in the editor
    - Significantly improves response relevance when asking about a specific module or config file: "Refactor `#file:userController.ts` to use async/await throughout"
    - More targeted than `@workspace` — use when you know exactly which file is relevant; saves tokens and response time
    - Multiple `#file` references can be chained in one prompt: `Based on #file:schema.prisma and #file:userService.ts, write the missing create user endpoint`
  - `#selection` — reference current selection
    - Automatically references whatever text is highlighted in the active editor — equivalent to copy-pasting the selection into the chat but cleaner
    - Copilot sees both the selection and its surrounding context (file name, imports, nearby code) — richer than pasting text alone
    - Default behaviour in inline chat: `Ctrl+I` with a selection open automatically scopes to that selection without needing to type `#selection` explicitly
  - `#codebase` — search across the codebase
    - Performs a semantic search across all files in the workspace to find relevant code snippets before answering — similar to `@workspace` but used as an inline variable in a longer prompt
    - Use when you want Copilot to find context itself rather than pointing it at a specific file: "Using #codebase, explain how database transactions are handled in this project"
    - Slower than `#file` but more powerful when you don't know which file contains the relevant code
- Inline chat for quick, context-aware edits without leaving the editor
  - **The workflow:** select code → `Ctrl+I` → type instruction → review diff → Accept / Discard; the entire interaction happens in the editor without switching panels
  - **Best use cases:** renaming variables consistently throughout a selection, adding error handling to a function, converting a callback to a promise, adding type annotations, translating a code block to another language
  - **The diff view:** Copilot shows proposed changes as a red/green diff inline — additions in green, removals in red; you can accept all, discard all, or use "Accept Next Line" to step through changes individually
  - **Iterating with follow-ups:** if the first inline chat result isn't right, you can type a follow-up instruction in the same inline chat bar without closing it — Copilot refines its previous attempt
  - **Keyboard-first:** experienced users rarely touch the mouse during inline chat — `Ctrl+I`, type instruction, `Enter`, review, `Tab` to accept; entire edit in under 10 seconds for simple tasks

### 4. Choosing Your AI Model (10 min)
- Models available in Copilot: GPT-4o, Claude Sonnet, Gemini, o3-mini, and more
  - **GPT-4o (OpenAI):** the general-purpose workhorse — fast, strong across all programming languages, excellent at following complex instructions; the default for most Copilot plans; good balance of speed and quality for everyday coding tasks
  - **Claude Sonnet (Anthropic):** known for long-context accuracy, nuanced reasoning, and particularly strong code explanation and documentation quality; often preferred for refactoring large files or explaining complex systems
  - **Gemini (Google):** strong multilingual support and long-context window; a useful alternative when working with Google Cloud APIs, Android/Kotlin, or when you want a different "perspective" on a problem
  - **o3 / o3-mini (OpenAI reasoning models):** "thinking" models that reason step-by-step before answering — significantly slower but produce higher accuracy on hard problems: complex algorithms, tricky debugging, architecture decisions; not suitable for fast completions
  - **Model availability varies by plan:** Free tier has access to a subset; Individual and Business unlock more models; check the model picker dropdown in Copilot Chat for what's available on your plan
- When to use which: reasoning models vs. speed, long context tasks
  - **Default (GPT-4o or equivalent):** everyday coding — completing functions, generating tests, explaining code, writing documentation; optimises for speed and general accuracy
  - **Reasoning models (o3, o3-mini):** hard problems where correctness matters more than speed — debugging a subtle concurrency bug, designing a data structure, reasoning about security edge cases, solving complex algorithmic problems; expect 15–60 second response times
  - **Long context tasks (Claude Sonnet, Gemini):** when you need to attach large files, long transcripts, or entire codebases as context — these models maintain coherence over more tokens; useful for summarising a 3,000-line legacy file or refactoring across many files at once
  - **Rule of thumb:** start with the default for 90% of tasks; switch to a reasoning model when you've tried the default and the answer wasn't good enough; try Claude/Gemini when working with very large context or when explanation quality is paramount
- How to switch models in the Chat panel
  - **In VS Code:** click the model name dropdown in the Copilot Chat input bar (bottom of the Chat panel) — shows all models available on your plan with a brief description of each
  - **Model persists per Chat thread:** switching models applies to the current conversation and is remembered for subsequent messages in that thread; opening a new Chat thread resets to the last-used model
  - **In JetBrains:** accessible via the settings gear icon in the Copilot Chat panel; model switching UI may look slightly different but functions the same way
  - **On GitHub.com:** model picker is available in the github.com Copilot Chat interface — same models, same selection mechanism
  - **No performance cost to trying:** switching models mid-conversation is free; there's no penalty for testing a different model on the same question — encourage experimentation
- Model differences in code quality and explanation style
  - **Code generation quality:** all current Copilot models produce good code for standard tasks; differences emerge on complex, multi-step problems or highly specific frameworks where training data is sparse
  - **Explanation style:** GPT-4o tends to be concise and direct; Claude Sonnet tends to be more thorough and narrative, explaining *why* not just *what*; Gemini often structures responses with more bullet points and headers — personal preference plays a role here
  - **Instruction following:** reasoning models (o3) are notably better at following complex, multi-constraint instructions precisely; other models may take shortcuts or miss constraints in a long prompt
  - **Practical suggestion for participants:** try the same prompt on two different models and compare — not to find a "winner" but to build intuition for when each model excels; the Model Comparison exercise in the next section is designed for exactly this

### 5. Copilot Chat on GitHub.com (10 min)
- Copilot Chat in the browser — what's available
  - **Access:** navigate to any GitHub.com page and click the Copilot icon (top-right navigation bar) or the chat bubble that appears on repository, PR, issue, and file pages; requires a Copilot plan (Free tier included)
  - **Persistent across GitHub.com:** the chat panel stays open as you navigate between pages — context is maintained so you can ask follow-up questions about the current repo, PR, or issue without restarting the conversation
  - **Same models as the IDE:** the model picker is available on GitHub.com — switch between GPT-4o, Claude Sonnet, etc. depending on the task
  - **Limitations vs. IDE Chat:** no inline code editing, no slash commands that modify files, no `@terminal` — the browser experience is read-and-understand focused; for making code changes, the IDE remains the primary surface
  - **GitHub Skills / Copilot Enterprise users:** organisations on Enterprise get additional features in the browser, including Knowledge Base access and organisation-scoped custom instructions applied automatically
- Asking questions about a repository, file, PR, or issue directly on github.com
  - **Repository context:** when Copilot Chat is open on a repository page, it can answer questions about the entire repo — "what does this project do?", "what's the main entry point?", "which files handle authentication?" — without needing to clone locally
  - **File-level questions:** open any file on github.com, open Copilot Chat, and ask about that specific file — useful for code review prep, onboarding to an unfamiliar codebase, or understanding a library's source
  - **PR context:** on a Pull Request page, Copilot Chat has access to the PR diff, description, and comments — ask "summarise what this PR changes", "are there any obvious bugs in this diff?", "what's the impact of removing this method?"
  - **Issue context:** on an issue page, Copilot Chat can read the issue body, comments, and linked code — ask "what would be a good first step to fix this?", "are there similar issues already?", "write a draft comment asking the reporter for more information"
  - **Cross-reference capability:** Copilot on GitHub.com can connect information across issues, PRs, and files in the same repository — a capability not available in the IDE
- Copilot for Pull Requests: auto-generated PR descriptions
  - **How to trigger:** when creating or editing a PR on github.com, click the ✨ sparkle icon in the PR description text box — Copilot analyses the diff and generates a structured PR description automatically
  - **What it generates:** typically includes a summary of what changed, why (if inferable from commit messages or issue references), and a list of the key files modified; formatted in Markdown ready to submit
  - **Quality depends on diff clarity:** clean, focused diffs (one feature or fix per PR) produce excellent descriptions; large, mixed-purpose PRs produce vague summaries — this is a useful forcing function for better PR hygiene
  - **Edit before submitting:** the generated description is a starting point — reviewers and the team still benefit from a human adding the *business context* ("this fixes the login timeout bug reported by Customer X") that Copilot can't infer from code alone
  - **Team adoption tip:** agreeing as a team to always use Copilot-generated PR descriptions as a baseline significantly reduces the "empty PR description" problem; it's faster to edit a good description than to write one from scratch

## 🧪 Chapter 2 Exercises

- [Exercise 201 — Factorial Calculator](../../../exercises/chapter-02/201/README.md) — practise ghost-text completions and compare iterative versus recursive solutions
- [Exercise 202 — Palindrome Checker](../../../exercises/chapter-02/202/README.md) — steer ghost-text with line-by-line comments and review the generated logic carefully
- [Exercise 203 — Mystery Processor](../../../exercises/chapter-02/203/README.md) — use `/explain` to understand obfuscated code before looking at the tests
- [Exercise 204 — Shortest Path](../../../exercises/chapter-02/204/README.md) — repair a buggy Dijkstra implementation with better `/fix` prompts
- [Exercise 205 — Caesar Cipher](../../../exercises/chapter-02/205/README.md) — generate a starter test suite with `/tests` and improve the coverage
- [Exercise 206 — String Search](../../../exercises/chapter-02/206/README.md) — use `/doc` to document existing KMP code and make it easier to understand

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Zero to Function in 60 Seconds (10 min)
Participants open a blank file and race to implement a specified function using *only* Copilot completions — no typing logic themselves. Debrief: who got closest? What prompt strategies worked?

### Exercise: Slash Command Scavenger Hunt (15 min)
Give participants a small, unfamiliar codebase (provided as a zip). Tasks:
1. Use `/explain` on the most confusing file
2. Use `/fix` on a file with a deliberate bug
3. Use `/tests` to generate tests for a function
4. Use `/doc` to document an undocumented class
Compare results in pairs.

### Exercise: Model Comparison (5 min)
Ask the same question to two different Copilot models (e.g., GPT-4o vs. Claude Sonnet). Compare output quality and style. Discuss: which did you prefer and why?

### Pair Programming Challenge (10 min)
In pairs: one person drives the IDE, the other directs Copilot using only natural language instructions (no typing code). Build a small feature together. Swap roles.

---

## 🔗 Resources & References
- [Getting started with GitHub Copilot](https://docs.github.com/en/copilot/getting-started-with-github-copilot)
- [GitHub Copilot keyboard shortcuts](https://docs.github.com/en/copilot/configuring-github-copilot/configuring-github-copilot-in-your-environment)
- [Copilot Chat slash commands reference](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide)
- [Copilot Chat participants and variables](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/copilot-chat-context)
- [Changing the AI model for Copilot Chat](https://docs.github.com/en/copilot/using-github-copilot/ai-models/changing-the-ai-model-for-copilot-chat)

---

## 🗒️ Facilitator Notes
- Allocate extra time for setup issues — always a few participants with proxy/auth problems
- Keep a Copilot-enabled machine ready to mirror for participants who can't get it working
- Emphasise: accepting every suggestion uncritically is *not* the goal — critical evaluation is part of the skill

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 1 — Welcome to the AI Revolution!](../chapter-01/README.md) | [Chapter 3 — Power with Purpose: Using AI Responsibly →](../chapter-03/README.md)