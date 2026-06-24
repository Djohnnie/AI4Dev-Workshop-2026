# Workshop: Smarter AI Usage — Understanding Tokens & AI Credits

**Duration:** ~60 minutes  
**Audience:** Developers & knowledge workers using GitHub Copilot  
**Context:** GitHub Copilot is moving from Premium Requests to AI Credits (AIC)

---

## Agenda

| # | Topic | Time |
|---|-------|------|
| 1 | Why this matters — the AIC shift | 5 min |
| 2 | How tokens work | 8 min |
| 3 | How AI Credits map to tokens | 5 min |
| 4 | Strategy 1: Better prompting | 8 min |
| 5 | Strategy 2: Better planning | 6 min |
| 6 | Strategy 3: Context & conversation hygiene | 5 min |
| 7 | Strategy 4: Choosing the right tool & model | 4 min |
| 8 | Strategy 5: Agent & instruction files | 8 min |
| 9 | Strategy 6: Advanced developer techniques | 6 min |
| 10 | Bonus: Local LLMs — offload the work | 5 min |
| 11 | Q&A | 2 min |

---

## 1. Why This Matters — The AIC Shift (5 min)

GitHub Copilot is replacing the concept of **"premium requests"** with **AI Credits (AIC)**.

### Key changes:
- Every AI interaction now has a **measurable credit cost**
- Different models consume different amounts of AIC per interaction
- Copilot features (chat, agent, code completion) each have their own cost profile
- **You have a budget** — smarter usage = more value from it

### The opportunity:
> "This isn't about doing less AI — it's about doing AI better."

---

## 2. How Tokens Work (10 min)

### What is a token?
- Roughly **~4 characters** or **~0.75 words** in English
- Code tends to be token-dense (operators, brackets, indentation all count)
- Example tokenization:
  ```
  "Hello, world!"      → 4 tokens
  "function getUserById(id: number)" → ~9 tokens
  ```

### Token types — BOTH count toward your usage:
| Type | Description | Example |
|------|-------------|---------|
| **Input tokens** | Everything you send to the model | Your prompt + conversation history + file context |
| **Output tokens** | Everything the model generates back | Code, explanation, suggestions |

### The hidden cost: context window
- **The entire conversation history is re-sent on every turn**
- A 20-turn chat with 500 tokens/turn = ~10,000 input tokens per message by the end
- Long conversations are exponentially expensive

### Visual mental model:
```
Turn 1:  [Prompt 200 tokens] → [Response 300 tokens]           =  500 tokens
Turn 2:  [History 500 + Prompt 150] → [Response 250 tokens]   =  900 tokens
Turn 3:  [History 1400 + Prompt 100] → [Response 400 tokens]  = 1900 tokens
         ↑ history keeps growing!
```

---

## 3. How AI Credits Map to Tokens (5 min)

### The core formula

```
1 AI Credit = $0.01 USD

AIC used = (tokens / 1,000,000) × price_per_million_tokens / $0.01
```

Or more intuitively:

```
AIC = tokens × price_per_million / 10,000
```

Prices differ per **model**, per **token type** (input / cached input / output), and for some models per **context window size**.

---

### What are the three token types?

| Token type | What it is | Typical cost |
|------------|-----------|--------------|
| **Input tokens** | Your prompt + conversation history + file context | Medium |
| **Cached input tokens** | Context the model already "knows" from earlier in session | ~10× cheaper than input |
| **Output tokens** | Everything the model writes back to you | Most expensive |

> Output tokens typically cost **6–10× more** than input tokens — keep responses concise!

---

### Real pricing from GitHub Copilot docs (per 1M tokens, June 2026)

**OpenAI models:**

| Model | Category | Input | Cached input | Output |
|-------|----------|------:|-------------:|-------:|
| GPT-5.4 nano | Lightweight | $0.20 | $0.02 | $1.25 |
| GPT-5 mini | Lightweight | $0.25 | $0.025 | $2.00 |
| GPT-5.4 mini | Lightweight | $0.75 | $0.075 | $4.50 |
| GPT-5.4 | Versatile | $2.50 | $0.25 | $15.00 |
| GPT-5.3-Codex | Powerful | $1.75 | $0.175 | $14.00 |
| GPT-5.5 | Powerful | $5.00 | $0.50 | $30.00 |

**Anthropic models:**

| Model | Category | Input | Cached input | Output |
|-------|----------|------:|-------------:|-------:|
| Claude Haiku 4.5 | Versatile | $1.00 | $0.10 | $5.00 |
| Claude Sonnet 4.x | Versatile | $3.00 | $0.30 | $15.00 |
| Claude Opus 4.x | Powerful | $5.00 | $0.50 | $25.00 |
| Claude Fable 5 | Powerful | $10.00 | $1.00 | $50.00 |

**Google models:**

| Model | Category | Input | Cached input | Output |
|-------|----------|------:|-------------:|-------:|
| Gemini 3 Flash | Lightweight | $0.50 | $0.05 | $3.00 |
| Gemini 2.5 Pro | Powerful | $1.25 | $0.125 | $10.00 |
| Gemini 3.1 Pro | Powerful | $2.00 | $0.20 | $12.00 |

> Source: [docs.github.com/en/copilot/reference/copilot-billing/models-and-pricing](https://docs.github.com/en/copilot/reference/copilot-billing/models-and-pricing)

---

### Worked examples

**Example A — Simple question on Claude Sonnet 4.6:**
```
Prompt:  200 input tokens  → 200 × $3.00 / 10,000    = 0.06 AIC
Answer:  350 output tokens → 350 × $15.00 / 10,000   = 0.525 AIC
                                              Total  ≈ 0.59 AIC
```

**Example B — Same question on GPT-5 mini:**
```
Prompt:  200 input tokens  → 200 × $0.25 / 10,000    = 0.005 AIC
Answer:  350 output tokens → 350 × $2.00 / 10,000    = 0.07 AIC
                                              Total  ≈ 0.075 AIC
```
→ **8× cheaper** for the same interaction just by choosing a lighter model.

**Example C — Agent mode with long context on GPT-5.5:**
```
History: 50,000 input tokens  → 50,000 × $5.00 / 10,000   = 25 AIC (input alone!)
Answer:   2,000 output tokens →  2,000 × $30.00 / 10,000  =  6 AIC
                                                   Total  ≈ 31 AIC
```
→ A long agent session can burn **50× more** than a focused chat.

---

### What's FREE? Inline code completions!

> ✅ **Ghost text / inline code completions do NOT consume AI Credits** on any paid plan.  
> Only Chat, Agent, and Code Review features consume AIC.

---

### Plan allowances (as of June 2026)

| Plan | Monthly price | Included AIC | Extra AIC |
|------|--------------|-------------|-----------|
| Copilot Free | Free | Limited | — |
| Copilot Pro | $10/month | 1,000 AIC | $0.01/AIC |
| Copilot Pro+ | $39/month | 3,900 AIC | $0.01/AIC |
| Copilot Business | $19/user/month | Per-user pool | $0.01/AIC |
| Copilot Enterprise | $39/user/month | Per-user pool | $0.01/AIC |

---

### Rule of thumb:
> **Matching model capability to task complexity** is the single biggest lever.

Using GPT-5.5 to answer "how do I reverse a string in Python?" costs ~240× more than using GPT-5.4 nano for the same question.

---

## 4. Strategy 1: Better Prompting (12 min)

### 4.1 Be specific — vague prompts get verbose answers

❌ **Wasteful:**
```
Can you help me with my authentication code? I'm having some issues with it 
and I'm not really sure what's wrong. Maybe there's a better way to do it?
```

✅ **Efficient:**
```
My JWT validation in auth.middleware.ts returns 403 for valid tokens. 
Signature uses HS256 with env var JWT_SECRET. Suspect clock skew. 
Show fix only, no explanation needed.
```

**Why it matters:** Vague prompts trigger long clarifying answers. Specific prompts generate targeted output.

---

### 4.2 Explicitly ask for concise output

- Add `"be concise"`, `"no explanation"`, `"code only"`, `"one-liner if possible"`
- Ask for summaries instead of full rewrites: `"summarize the key changes only"`
- Specify format: `"bullet points"`, `"table format"`, `"max 3 sentences"`

---

### 4.3 Avoid preamble and filler text

❌ `"Hey, I was wondering if you could possibly help me with..."`  
✅ Just state the task directly

Each token of preamble also nudges the model toward a verbose response style.

---

### 4.4 Reference code, don't paste it (when possible)

- Use `@file`, `@workspace`, `#file:` references in Copilot Chat
- Only paste the **relevant snippet**, not the whole file
- If you need whole-file context, let the IDE inject it via file attachment

---

### 4.5 One task per prompt

Combining multiple questions often forces a longer combined answer.

❌ `"Refactor this function AND explain how async/await works AND write tests for it"`  
✅ Three separate, focused prompts — each producing a tight, relevant response

---

### 4.6 Use imperative language

- `"Refactor X"` not `"Could you perhaps refactor X?"`
- `"List 3 options"` not `"What are some possible options you might suggest?"`

---

### 4.7 Teach the model your constraints up front

```
Context: .NET 8, C#, minimal API, no extra libraries.
Task: Add rate limiting middleware. Code only.
```

This prevents the model from suggesting incompatible solutions you'll ask it to revise.

---

## 5. Strategy 2: Better Planning (8 min)

### 5.1 Think before you type

The most expensive prompt is the one followed by: `"Actually, never mind, let's approach this differently"`

**Pre-prompt checklist:**
- [ ] Do I know exactly what I need?
- [ ] Is AI the right tool for this specific sub-task?
- [ ] Do I have the relevant context ready?
- [ ] What format do I actually need the output in?

---

### 5.2 Design architecture before asking AI to implement

```
BAD workflow:
  → Ask AI to build feature X
  → It builds it wrong
  → Ask AI to rebuild it differently
  → Ask AI to add missing requirement
  → Total: 4x the tokens

GOOD workflow:
  → Sketch the approach yourself (whiteboard/notes)
  → Confirm design with 1 focused AI prompt if needed
  → Ask AI to implement each piece specifically
  → Total: 1-2x the tokens
```

---

### 5.3 Break large tasks into small, focused prompts

| Approach | Token cost | Quality |
|----------|-----------|---------|
| "Build me a full CRUD API" | Very high | Often needs many corrections |
| "Write the User model" → "Write the GET /users route" → "Write unit tests" | Moderate | Higher precision |

---

### 5.4 Know when NOT to use AI

- Simple lookups → use docs/IntelliSense/Stack Overflow
- Boilerplate you know by heart → just type it
- Repetitive simple patterns → create a snippet/template
- Understanding your own codebase deeply → read the code

> AI shines for: reasoning, generation, transformation, explanation of complex topics.

---

### 5.5 Prepare your context window

- Open the relevant files before starting chat
- Set a clear custom instruction / system prompt for the project
- Use `.github/copilot-instructions.md` for persistent project context

---

## 6. Strategy 3: Context & Conversation Hygiene (8 min)

### 6.1 Start fresh conversations for new topics

Every message in a conversation re-sends the full history as input tokens.  
When you switch topics, **start a new chat** — this is free and drastically cuts input costs.

---

### 6.2 Don't feed the same context repeatedly

If you pasted a 200-line file in turn 1, it's still costing you tokens in turn 20.

**Tip:** Summarize or replace context mid-conversation:
```
"Forget the earlier code. Here's the updated version: [paste new version only]"
```

---

### 6.3 Use conversation compaction wisely

Some tools/clients offer "summarize conversation" — use this for long-running agent sessions before they balloon.

---

### 6.4 Avoid correction spirals

```
Turn 1: "Write X"
Turn 2: "Actually, also do Y"
Turn 3: "Oh and use Z instead of A"
Turn 4: "The output is still wrong because..."
```

Each correction re-processes the full history. A **1-minute planning pause** before turn 1 can replace 5 correction turns.

---

### 6.5 Use targeted file context, not whole-project context

- `@workspace` scans everything — expensive for large repos
- `#file:specific.ts` targets exactly what's needed — much cheaper
- Be explicit: mention what the AI needs, not everything it *could* look at

---

## 7. Strategy 4: Choosing the Right Tool & Model (5 min)

### Copilot feature cost ladder (low → high):

```
Ghost text / inline completion   ← cheapest, great for code completion
  ↓
Copilot Chat (single-turn)       ← good for focused questions
  ↓
Copilot Chat (multi-turn)        ← watch conversation length
  ↓
Copilot Agent mode               ← most powerful, most expensive
```

**Use agent mode only for genuinely multi-step autonomous tasks.**  
For anything you can direct step-by-step yourself, chat is more efficient.

---

### Model selection guidelines:

| Task | Recommended model tier |
|------|----------------------|
| Simple code completion | Small / fast model |
| Explaining a function | Small / fast model |
| Debugging a complex race condition | Medium model |
| Architectural reasoning / system design | Large model |
| Multi-file refactoring with reasoning | Large model or agent |

**Tip:** Try with a smaller model first. If the answer is wrong or shallow, escalate.

---

## 8. Strategy 5: Agent & Instruction Files (8 min)

GitHub Copilot supports several types of files that inject context automatically or on demand. Understanding *when* each is loaded — and at what token cost — is key to using them efficiently.

---

### The four file types at a glance

| File | Location | When loaded | Token cost |
|------|----------|------------|-----------|
| **Repository instructions** | `.github/copilot-instructions.md` | Every Copilot request from this repo | Always — but cached |
| **Path-specific instructions** | `.github/instructions/*.instructions.md` | Only when the active file matches the `applyTo` pattern | Targeted — cheaper |
| **Prompt files** | `.github/prompts/*.prompt.md` | Only when you explicitly reference them | Zero unless called |
| **Agent guidance** | `AGENTS.md` (root or subfolder) | When Copilot Coding Agent works on this repo | Per-agent-session |

---

### `.github/copilot-instructions.md` — Repository instructions

This file is **injected into every Copilot Chat and code review request** from the repo. It's the right place for:
- Coding standards, naming conventions
- Framework/library preferences (`"Use .NET 8 minimal API, no MediatR"`)
- Test framework, PR conventions, security rules

**Token efficiency rules:**
- ✅ Injected once → benefits from **cached input pricing** (~10× cheaper after first use)
- ✅ Prevents you from repeating context in every prompt
- ❌ It's always included — a bloated file wastes tokens on every single request
- **Keep it under 500 lines.** Use bullet points, not paragraphs. No examples unless essential.

```markdown
<!-- .github/copilot-instructions.md -->
## Stack
- .NET 8, C#, Minimal API, xUnit, no MediatR
- Frontend: React 18, TypeScript, no class components

## Conventions
- Async/await everywhere, no .Result or .Wait()
- Repository pattern for data access
- PascalCase for public members, camelCase for private
```

---

### `.github/instructions/*.instructions.md` — Path-specific instructions

Apply rules only to files matching a glob pattern via frontmatter. This keeps the always-on repo instruction file small.

```markdown
---
applyTo: "**/*.test.ts"
---
- Use Vitest, not Jest
- Always mock external HTTP calls
- Prefer `it()` over `test()`
```

```markdown
---
applyTo: "src/api/**"
---
- All endpoints must have OpenAPI annotations
- Validate all inputs with FluentValidation
```

**Token benefit:** Only loaded when the active file matches — the frontend testing rules don't load when you're editing a C# backend file.

---

### `.github/prompts/*.prompt.md` — Reusable prompt files

These are **on-demand** — zero token cost until a developer explicitly calls them.  
Perfect for recurring, complex tasks that are hard to describe quickly.

```markdown
<!-- .github/prompts/add-feature.prompt.md -->
You are helping add a new feature to this codebase.

Steps:
1. Create the domain model in /src/Domain/
2. Add the repository interface in /src/Interfaces/
3. Implement the repository in /src/Infrastructure/
4. Add the endpoint in /src/Api/
5. Write xUnit tests covering happy path and validation errors

Before writing code, confirm the feature name and data shape with me.
```

**How to use in Copilot Chat:**
```
/add-feature      ← if registered as a slash command
# or reference it with:
@workspace use .github/prompts/add-feature.prompt.md for adding a user profile feature
```

**Token benefit:** The full prompt is loaded once per task, but it replaces the 5–10 turns of context-setting you'd otherwise do manually. Net saving for complex tasks.

---

### `AGENTS.md` — Copilot Coding Agent guidance

When the **Copilot Coding Agent** (cloud agent, works autonomously on issues/PRs) runs on your repo, it reads `AGENTS.md` to understand:
- How to build, test, and lint the project
- Which directories to touch (and which to avoid)
- Repo-specific workflows

```markdown
<!-- AGENTS.md -->
## Build
- `dotnet build` from repo root
- `dotnet test` for all tests (must pass before committing)

## Code style
- Run `dotnet format` before any commit
- Do not modify files in /legacy/ — read-only

## PR conventions
- One logical change per PR
- Link the GitHub issue in the PR description
```

**Token efficiency angle:**  
The agent reads this file once at the start of a session. A good `AGENTS.md` prevents the agent from making wrong assumptions, exploring dead-end paths, or asking clarifying questions mid-task — all of which consume tokens (and your patience).

---

### Summary: which file for what?

```
Always enforce project-wide rules?   → copilot-instructions.md
Rules only for specific file types?  → instructions/*.instructions.md
Reusable complex task templates?     → prompts/*.prompt.md
Guiding the autonomous cloud agent?  → AGENTS.md
```

---

### 💡 Practical tip: joining an existing codebase?

When onboarding into an unfamiliar repo, let AI *generate* the instruction files by reading the existing code. This is one of the best one-time investments you can make — it pays back on every future Copilot request.

**Why it works:**
- AI detects *actual* patterns from real code, not what someone thinks the conventions are
- Faster than manually reading hundreds of files to reverse-engineer conventions
- One-time token cost → every future request is cheaper (less context-setting per prompt)

**How to do it without wasting tokens:**

Don't analyze the whole codebase at once. Feed AI a *representative sample*:

```
Files to provide:
  ✓ package.json / .csproj / pom.xml       → tech stack & versions
  ✓ 3–4 representative source files        → naming, patterns, structure
  ✓ 2–3 test files                         → test framework & style
  ✓ .editorconfig / .eslintrc / .prettierrc → existing style rules (if any)
```

**Prompt to use:**
```
Based on the files I've shared, generate a .github/copilot-instructions.md 
for this repository. Focus on:
- Tech stack and key libraries (with versions)
- Naming conventions you observe
- Code patterns that appear consistently
- Test patterns and framework
- Any conventions from the config files

Rules for the output:
- Max 400 lines
- Bullet points only, no prose paragraphs
- Imperative tone ("Use X", "Never Y")
- Do not include anything you're uncertain about
```

**Important: always review before committing**

AI sees patterns — it doesn't know *intent*. Common issues to watch for:
- It might pick up **legacy patterns** from old code that the team has since moved away from
- It will **miss undocumented decisions** (architecture choices, security rules, "we never do X here")
- A bloated output file will hurt you — trim aggressively before committing

> The generated file is a first draft, not the final answer. 15 minutes of review makes it genuinely useful.

---

## 9. Strategy 6: Advanced Developer Techniques (6 min)

### 9.0 "I don't know where to look" — navigate first, ask second

When you have a request but don't know which file to look in, the instinct is to reach for `@workspace` and let Copilot scan the entire codebase. This is the most expensive way to navigate.

**The two-phase approach:**

```
Phase 1 — Locate (cheap)   → get file paths, zero code content yet
Phase 2 — Execute (targeted) → use #file: on exactly the right file
```

**Phase 1 options, from cheapest to most expensive:**

| Approach | Token cost | How |
|----------|-----------|-----|
| IDE symbol search (Ctrl+T / F12) | **Free** | Jump to class/method by name |
| Search-in-files (Ctrl+Shift+F) | **Free** | grep for keywords in the IDE |
| Paste folder tree + ask "which file?" | Very cheap | Structure only, no file contents |
| Ask `@workspace` "which file?" | Cheap | Short answer expected — file path only |
| `@workspace` "explain and fix X" | **Expensive** | Scans + generates — last resort |

**The folder tree trick:**

Generate a directory listing (free), paste it, ask for a path — not code:

```bash
# PowerShell
tree /F /A | Select-String -NotMatch "node_modules|.git|bin|obj"

# or just the folder structure (no files)
tree
```

```
"Given this folder structure, which file most likely contains 
the JWT validation logic? Answer with file path only, no explanation."

[paste tree output]
```

Cost: ~100–300 input tokens for the tree, ~10 output tokens for a file path.  
Compare to `@workspace` scanning file contents: potentially 10,000+ tokens.

**Once you have the file path — switch to targeted context:**

```
❌  @workspace how does authentication work?
         → scans many files, large input, large explanation output

✅  Step 1: "Which file handles authentication?" → answer: src/middleware/auth.ts
    Step 2: #file:src/middleware/auth.ts "How does the JWT check work here?"
         → only one file in context, answer is scoped and short
```

**Ask for a "treasure map", not the treasure:**

```
"Based on the folder structure below, give me a reading order — which 
3 files should I read to understand how a user login request flows 
through this system? File paths only."
```

This costs almost nothing and replaces 30 minutes of codebase archaeology — or an expensive `@workspace` deep-dive.

---

### 9.1 Pre-filter with your own tools before prompting

Don't hand the AI a 500-line log file and say "what's wrong?" — scan it yourself first.

```
BAD:  Paste entire stack trace (300 lines) → "What caused this?"
      → You pay for 300 input tokens the AI skims past

GOOD: grep the log yourself → isolate the 5 relevant error lines → paste only those
      → 95% fewer input tokens, same quality answer
```

Use `grep`, `find`, search-in-files, or your IDE before reaching for AI. Your tools are free.

---

### 9.2 Ask for diffs, not full rewrites

When refactoring, a full rewrite of a 100-line function costs ~100 output tokens minimum.  
A diff showing only changed lines might cost 15.

```
❌  "Refactor this function to use async/await"       → full rewrite output
✅  "Show only the lines that change if I refactor    → diff-style output, far fewer tokens
     this to async/await, unified diff format"
```

Also works for code review: `"List only the issues, no need to show the corrected code"`

---

### 9.3 Cap your output length explicitly

The model will fill however much space you give it. Add hard caps:

- `"Max 5 bullet points"`
- `"One paragraph only"`
- `"3 options, each described in one sentence"`
- `"Code only, no explanation, max 20 lines"`

Output tokens are the most expensive — capping them directly cuts your biggest cost driver.

---

### 9.4 Compress context before feeding it

Before pasting long documents, code files, or logs — **summarize them yourself first** using simpler tools, or ask a cheap model to summarize for the expensive one.

```
Step 1: Use GPT-5 mini to summarize a 2000-token document → 200-token summary
        Cost: ~0.5 AIC

Step 2: Use Claude Sonnet with the 200-token summary for the complex reasoning task
        Cost: ~1 AIC (instead of ~5 AIC with the full document)

Total: ~1.5 AIC vs ~5 AIC
```

---

### 9.5 Use `.github/copilot-instructions.md` — set context once, not every time

If you find yourself repeating the same context at the start of every conversation:

```
"We use .NET 8, C#, minimal APIs, xUnit for tests, no MediatR..."
```

Put it in `.github/copilot-instructions.md`. Copilot reads it automatically.  
This file also benefits from **cached token pricing** — repeated context is ~10× cheaper than fresh input.

---

### 9.6 Leverage cached tokens actively

Cached input costs ~10× less than fresh input. The model caches tokens when a prompt **starts the same way** as a previous one in the session.

**Practical pattern:** Keep your system prompt / project context at the **top of your message**, always in the same format. This makes it cache-eligible and drastically reduces effective input cost in multi-turn sessions.

---

### 9.7 Ask for the fix, not the tutorial

```
❌  "Can you explain how connection pooling works and then show me
     how to fix the timeout issue in my code?"
     → Explanation alone: 300–500 output tokens

✅  "Fix the connection timeout in [code]. Add a comment if the fix
     isn't self-explanatory."
     → Answer: 30–80 output tokens
```

Ask for explanations only when you genuinely need to learn. Skip them when you just need working code.

---

### 9.8 Don't use AI as a search engine

AI is not faster or cheaper than a documentation search for known facts.

| Question type | Better tool |
|--------------|-------------|
| "What is the syntax for X in Python?" | Docs / IntelliSense / Stack Overflow |
| "What does this NPM package do?" | README / npmjs.com |
| "What HTTP status code means Y?" | MDN / Google |
| "Why does this specific code behave unexpectedly?" | ✅ AI is ideal here |
| "How do I model this complex domain problem?" | ✅ AI is ideal here |

---

### 9.9 Plan first, execute step by step — and write the plan to a file

For complex tasks, asking for everything at once is one of the most expensive patterns.
A better approach: separate the *thinking* from the *doing*.

**Step 1 — Ask for a plan, not an implementation:**
```
"Create a step-by-step plan to refactor the authentication module to use JWT.
 Output as a markdown checklist. No code yet."
```
This is cheap: the model only outputs a short structured list.

**Step 2 — Review the plan yourself:**
- Remove steps you can do without AI
- Reorder or merge steps if needed
- Catch a wrong direction *before* paying for a full implementation

**Step 3 — Execute each step in a focused prompt:**
```
"Implement step 3 from this plan: [paste only that one step + relevant code]"
```

**Why write the plan to a file (not keep it in chat)?**

If you keep the plan inside the conversation, it gets re-sent as history on *every* follow-up turn — you pay for it repeatedly. Writing it to a `.md` file means:
- It stays out of the context window
- You paste only the one step you need per turn
- You have a durable artifact to track progress against

```
EXPENSIVE pattern:
  Chat history after 5 steps = [plan 400 tokens] + [step 1] + [step 2] + ...
  → You re-pay for the plan on every single turn

EFFICIENT pattern:
  plan.md (external)   → free to reference, not in context
  Each chat turn       → only the current step + relevant code
```

**When to use this pattern:**
- ✅ Multi-file refactoring
- ✅ Building a feature from scratch
- ✅ Any task with 4+ distinct steps
- ❌ Simple, well-scoped tasks — the planning turn overhead isn't worth it

---

### 9.10 Batch related sub-questions into one prompt

Five separate single-question chats about the same feature:
- Each starts a fresh context load (overhead per turn)
- 5 × fixed overhead tokens

One focused prompt asking all five related questions:
- Single context load
- Often shorter combined answer due to shared context

> **But:** unrelated topics should still go in separate chats (see hygiene section).

---

## 10. Bonus: Local LLMs — Offload the Work (5 min)

### Why local LLMs?
- **Zero AI credits consumed** — runs on your own hardware
- Great for: repetitive tasks, sensitive/private code, experimentation, learning
- Latency can be higher, quality lower than cloud — but sufficient for many tasks

---

### Getting started: two easy options

#### Option A — Ollama (CLI-first, developer-friendly)
```bash
# Install: https://ollama.com
ollama run llama3.2        # general purpose
ollama run codellama       # code-focused
ollama run phi4-mini       # small but smart, low resource
ollama run qwen2.5-coder   # strong at code
```

#### Option B — LM Studio (GUI, beginner-friendly)
- Download from https://lmstudio.ai
- Browse, download, and run models from a UI
- Exposes a local OpenAI-compatible API endpoint

---

### Integration with VS Code

| Extension | What it does |
|-----------|-------------|
| **Continue** (continue.dev) | Full Copilot-like experience with local models |
| **Ollama** extension | Basic chat with local models |
| LM Studio local server | Any OpenAI-compatible client can connect |

---

### Recommended models for developer tasks (mid-range hardware, 16GB RAM):

| Model | Size | Best for |
|-------|------|----------|
| Llama 3.2 3B | ~2GB | Quick answers, light tasks |
| Phi-4 Mini | ~2.5GB | Surprisingly capable, low resource |
| Qwen2.5-Coder 7B | ~5GB | Code generation & review |
| Llama 3.1 8B | ~5GB | Good all-rounder |
| Codestral (via LM Studio) | ~12GB | Strong code model, needs more RAM |

---

### Local LLM use case examples:

- **Reviewing boilerplate** PR diffs that don't need cloud-quality reasoning
- **Generating test data / mock data** from a schema
- **Quick regex / SQL / bash snippet generation**
- **Summarizing internal documents** (keeps data on-premise)
- **Learning / experimentation** — iterate freely without credit concerns

---

## Summary: Quick Reference Card

```
┌─────────────────────────────────────────────────────────┐
│           AI Credit Optimization Cheat Sheet            │
├─────────────────────────────────────────────────────────┤
│ PROMPTING                                               │
│  ✓ Be specific — vague prompts = verbose answers        │
│  ✓ Ask for concise/code-only output explicitly          │
│  ✓ Cap output: "max 5 bullets", "one paragraph"         │
│  ✓ Use @file references instead of pasting code         │
│  ✓ One task per prompt                                  │
│  ✓ State constraints upfront (language, version, style) │
├─────────────────────────────────────────────────────────┤
│ PLANNING                                                │
│  ✓ Think before you prompt                              │
│  ✓ Design first, implement second                       │
│  ✓ Break large tasks into small focused prompts         │
│  ✓ Know when not to use AI                              │
├─────────────────────────────────────────────────────────┤
│ CONVERSATION HYGIENE                                    │
│  ✓ Start new chats for new topics                       │
│  ✓ Avoid correction spirals — plan upfront              │
│  ✓ Use #file not @workspace when possible               │
├─────────────────────────────────────────────────────────┤
│ TOOL & MODEL CHOICE                                     │
│  ✓ Use inline completion for simple completions (FREE!) │
│  ✓ Match model size to task complexity                  │
│  ✓ Reserve agent mode for genuinely complex tasks       │
│  ✓ Try cheap model first — escalate only if needed      │
├─────────────────────────────────────────────────────────┤
│ AGENT & INSTRUCTION FILES                               │
│  ✓ copilot-instructions.md: project rules, cached       │
│  ✓ instructions/*.md: path-scoped, only loads on match  │
│  ✓ prompts/*.prompt.md: reusable tasks, zero cost idle  │
│  ✓ AGENTS.md: guide cloud agent, prevent wrong turns    │
├─────────────────────────────────────────────────────────┤
│ ADVANCED TECHNIQUES                                     │
│  ✓ Don't know where to look? Tree first, then #file:    │
│  ✓ grep/search yourself before pasting to AI            │
│  ✓ Ask for diffs, not full rewrites                     │
│  ✓ Summarize/compress long docs before feeding          │
│  ✓ Use copilot-instructions.md for persistent context   │
│  ✓ Ask for the fix, skip the tutorial                   │
│  ✓ Don't use AI as a search engine                      │
│  ✓ Plan first → write to file → execute step by step    │
├─────────────────────────────────────────────────────────┤
│ LOCAL LLMs (zero AIC)                                   │
│  ✓ Ollama + Continue for VS Code integration            │
│  ✓ LM Studio for GUI-based usage                        │
│  ✓ Best for: repetitive, private, or experimental work  │
└─────────────────────────────────────────────────────────┘
```

---

## Resources

- [GitHub Copilot AI Credits documentation](https://docs.github.com/en/copilot/managing-copilot/monitoring-usage-and-spending/about-billing-for-github-copilot)
- [Copilot instructions file docs](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot)
- [Ollama](https://ollama.com) — local model runner
- [LM Studio](https://lmstudio.ai) — local model GUI
- [Continue extension](https://www.continue.dev) — local Copilot alternative for VS Code
- [Prompt Engineering Guide](https://www.promptingguide.ai)
