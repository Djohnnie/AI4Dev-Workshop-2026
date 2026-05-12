# 📋 Developer AI Readiness Survey

> **Purpose:** Help us understand where you are today so we can tailor the workshop to your needs. There are no right or wrong answers — honest responses make for a better day (or two) for everyone.
>
> **Time to complete:** ~10 minutes  
> **Format key:** 🔢 Score 1–5 · ✅ Single choice · ☑️ Multiple choice · ✍️ Open text

---

## Section A — AI Foundations

**Q1 — AI Landscape Literacy** ✅ *Single choice*

When someone says "AI", which description best matches how you currently think about it?

- [ ] A) It's mostly one technology — large language models like ChatGPT
- [ ] B) I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently
- [ ] C) I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies
- [ ] D) I could teach a colleague the full taxonomy, including discriminative vs. generative models and why that distinction matters for tools like Copilot

---

**Q2 — How LLMs Work** 🔢 *Score 1–5*

Rate your confidence in explaining to a colleague how a large language model actually produces its output (tokens, next-token prediction, temperature, hallucination).

| 1 | 2 | 3 | 4 | 5 |
|---|---|---|---|---|
| No idea | Vague sense | Could outline it | Could explain it clearly | Could teach it |

> *Optional — What specifically feels unclear or fuzzy to you about how LLMs work?* ✍️
>
> _______________________________________________

---

**Q3 — Hallucination Awareness** ✅ *Single choice*

Which statement best describes your current understanding of AI hallucination in the context of code generation?

- [ ] A) I've heard the term but I'm not sure how it applies to coding tools
- [ ] B) I know it means the model can produce incorrect output, but I rely on tests and review to catch it
- [ ] C) I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic)
- [ ] D) I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk

---

## Section B — GitHub Copilot Setup & Core Features

**Q4 — Current Copilot Usage** ✅ *Single choice*

How would you describe your current GitHub Copilot usage?

- [ ] A) I've never used it
- [ ] B) I've tried it briefly but don't use it regularly
- [ ] C) I use it occasionally when I remember it's there
- [ ] D) It's part of my daily workflow and I'd notice if it was gone

---

**Q5 — Inline Completions Fluency** ☑️ *Multiple choice — tick all you currently use*

Which ghost-text / inline completion interactions do you use today?

- [ ] `Tab` to accept a full suggestion
- [ ] `Esc` to dismiss without accepting
- [ ] `Ctrl+→` to accept word-by-word (partial accept)
- [ ] `Alt+]` / `Alt+[` to cycle through alternative suggestions
- [ ] The Completions Panel (`Ctrl+Enter`) to see up to 10 alternatives
- [ ] Guiding completions with a specific comment immediately above the cursor
- [ ] Comment-driven development (writing algorithm steps as comments first)
- [ ] None of the above

---

**Q6 — Copilot Chat Commands** ☑️ *Multiple choice — tick all you have used*

Which Copilot Chat slash commands have you used in practice?

- [ ] `/explain` — understand code you didn't write
- [ ] `/fix` — diagnose and repair broken code
- [ ] `/tests` — generate unit tests for selected code
- [ ] `/doc` — generate docstrings or documentation
- [ ] `@workspace` — ask questions grounded in the whole project
- [ ] `@terminal` — explain or generate shell commands
- [ ] `#file` / `#selection` — attach specific context to a prompt
- [ ] None — I haven't used Chat slash commands yet

---

**Q7 — Advanced Features** 🔢 *Score 1–5*

How confident are you using these features? Rate each:

| Feature | 1 Never tried | 2 Tried once | 3 Use occasionally | 4 Use regularly | 5 Power user |
|---|---|---|---|---|---|
| Agent Mode (multi-step autonomous tasks) | ☐ | ☐ | ☐ | ☐ | ☐ |
| Copilot Edits (coordinated multi-file changes) | ☐ | ☐ | ☐ | ☐ | ☐ |
| Custom Instructions (`copilot-instructions.md`) | ☐ | ☐ | ☐ | ☐ | ☐ |
| Prompt files / reusable skill templates | ☐ | ☐ | ☐ | ☐ | ☐ |
| Model switching (choosing GPT-4o vs. Claude vs. Gemini) | ☐ | ☐ | ☐ | ☐ | ☐ |
| GitHub Copilot CLI (`gh copilot suggest` / `explain`) | ☐ | ☐ | ☐ | ☐ | ☐ |

---

## Section C — Prompting Skills

**Q8 — Prompt Quality Self-Assessment** 🔢 *Score 1–5*

Rate the quality of the prompts you currently write when using AI coding tools.

| 1 | 2 | 3 | 4 | 5 |
|---|---|---|---|---|
| Very vague — I mostly type short requests | Simple but inconsistent | I include some context and constraints | Structured prompts with task, context, and constraints | I write precise, reproducible prompts and iterate deliberately |

---

**Q9 — Prompting Framework** ✅ *Single choice*

Which best describes your current approach to writing a Copilot Chat prompt?

- [ ] A) I type whatever comes to mind and refine if the result is wrong
- [ ] B) I try to be specific but don't follow a particular structure
- [ ] C) I consciously include the task (what), context (language/framework), and any constraints
- [ ] D) I consistently apply a structured framework (e.g. Task / Context / Examples / Constraints) and use few-shot examples when the pattern is non-standard

---

**Q10 — Context Management** ✍️ *Open text*

Describe (in 1–3 sentences) how you currently decide what context to give Copilot before generating code. For example: do you open specific files, write comments, paste error messages, or use `#file` references?

> _______________________________________________
> _______________________________________________
> _______________________________________________

---

## Section D — Responsible AI & Security

**Q11 — Data Privacy Practices** ✅ *Single choice*

When working with Copilot (completions or Chat), which statement best describes your current data-handling habits?

- [ ] A) I paste whatever I'm working with and haven't thought much about what gets sent
- [ ] B) I know I shouldn't paste secrets but I'm not always consistent
- [ ] C) I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing
- [ ] D) My team has a written AI usage policy covering what is and isn't safe to share, and I follow it

---

**Q12 — Copilot Data Commitments** ✅ *Single choice*

Which statement best describes what you know about how GitHub stores or uses the code you send to Copilot?

- [ ] A) I assume it's used to train the model — I haven't looked into it
- [ ] B) I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them
- [ ] C) I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model
- [ ] D) I can explain the full data flow (what's assembled locally, what's transmitted, GitHub's contractual commitments by tier) and have used this to inform a team policy

---

**Q13 — Security Risk Awareness** ☑️ *Multiple choice — tick all you actively watch for*

Which categories of security vulnerability do you actively check for when reviewing AI-generated code?

- [ ] SQL injection / command injection
- [ ] Cross-site scripting (XSS) risks
- [ ] Missing or incorrect authentication / authorisation checks
- [ ] Hardcoded credentials or secrets
- [ ] Insecure use of cryptography (weak algorithms, predictable seeds)
- [ ] Missing input validation or boundary checks
- [ ] I review AI-generated code but don't specifically check for security issues
- [ ] I haven't thought about this yet

---

## Section E — AI Across the Development Lifecycle

**Q14 — SDLC Integration** ☑️ *Multiple choice — tick all the SDLC phases where you regularly use AI tools today*

- [ ] Writing new code (features, logic, algorithms)
- [ ] Writing or extending unit / integration tests
- [ ] Debugging — diagnosing errors and fixing failing tests
- [ ] Refactoring existing code
- [ ] Generating or updating documentation (docstrings, READMEs, ADRs)
- [ ] Code review — reviewing others' PRs or getting AI review on your own
- [ ] Writing pull request descriptions and summaries
- [ ] None — I don't use AI tools in my development workflow yet

---

**Q15 — Testing Workflow** ✅ *Single choice*

Which best describes your current approach to generating tests with AI?

- [ ] A) I don't use AI to write tests
- [ ] B) I've tried generating tests but rarely find the output good enough to keep
- [ ] C) I use `/tests` or equivalent as a scaffold and then extend with manual edge cases
- [ ] D) I use a TDD loop — generate failing tests first, then ask Copilot to write a passing implementation, then refactor safely under test coverage

---

## Section F — Workflow & Team Practices

**Q16 — Review Habits for AI Output** 🔢 *Score 1–5*

How rigorously do you review AI-generated code before committing it?

| 1 | 2 | 3 | 4 | 5 |
|---|---|---|---|---|
| I usually accept suggestions without deep review | I skim for obvious errors | I read it as carefully as code from a junior colleague | I read it line-by-line and run it | I read it, run it, and deliberately check for edge cases and security issues |

---

**Q17 — Think → Prompt → Review → Iterate** ✅ *Single choice*

Which step in the AI-assisted development loop do you find most challenging today?

- [ ] A) Think — clarifying what I want before I start prompting
- [ ] B) Prompt — writing a prompt that reliably produces the output I need
- [ ] C) Review — evaluating whether the AI output is correct, secure, and well-structured
- [ ] D) Iterate — knowing when to refine the prompt vs. edit the output manually
- [ ] E) I haven't established a deliberate loop yet — I interact more ad-hoc

---

**Q18 — Team AI Norms** ✅ *Single choice*

Which best describes how your team or organisation currently governs AI tool usage?

- [ ] A) No rules — everyone uses (or doesn't use) AI tools as they see fit
- [ ] B) Informal norms — we have unwritten shared expectations but nothing documented
- [ ] C) A written policy exists but it's not well known or consistently followed
- [ ] D) A clear, documented AI usage policy is in place, communicated, and actively followed by the team

---

## Section G — Looking Ahead

**Q19 — Biggest Blocker** ✍️ *Open text*

What is the single biggest thing that currently prevents you from getting more value from AI coding tools? (e.g. trust in output quality, prompt skills, organisation policy, tool availability, time to learn, security concerns)

> _______________________________________________
> _______________________________________________
> _______________________________________________

---

**Q20 — Workshop Goals** ✍️ *Open text*

What is the one thing you most want to be able to do differently — or more confidently — by the end of this workshop?

> _______________________________________________
> _______________________________________________
> _______________________________________________

---

*Thank you for taking the time to complete this survey. Your responses will help shape the focus of today's (and tomorrow's) sessions.*
