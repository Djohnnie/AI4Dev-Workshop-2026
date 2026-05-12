# 📊 AI4Dev Workshop 2026 — Survey Results Report

**Participants:** 28  
**Source:** `Survey results.xlsx`  
**Survey:** [SURVEY.md](SURVEY.md)

> Single-choice questions are scored A=1 → D=4 (Q17 is categorical, A–E). Scale questions are 1–5. Multi-choice questions report the number of items ticked out of the available options.


---

## 🧮 How the Familiarity Score Is Calculated

Each participant gets a **0–100 familiarity score** that blends conceptual knowledge, tool proficiency, prompting skill, and workflow integration:

| Component | Weight | Source |
|---|---|---|
| AI taxonomy literacy | 4 | Q1 |
| Hallucination understanding | 4 | Q3 |
| LLM mechanics confidence | 6 | Q2 |
| Copilot usage frequency | 10 | Q4 |
| Inline-completion breadth | 5 | Q5 |
| Chat-command breadth | 5 | Q6 |
| Advanced features (Agent / Edits / Custom Instructions / Prompt files / Model switching / CLI) | 20 | Q7 |
| Prompt quality self-assessment | 8 | Q8 |
| Prompting framework discipline | 7 | Q9 |
| Privacy hygiene | 4 | Q11 |
| Copilot data-flow understanding | 6 | Q12 |
| Security review breadth | 5 | Q13 |
| SDLC integration breadth | 5 | Q14 |
| Testing workflow maturity | 4 | Q15 |
| Review rigor | 3 | Q16 |
| Team AI governance | 2 | Q18 |
| **Total** | **100** | |

---

## 📈 Group Averages (All 28 Participants)

### Section A — AI Foundations
| Metric | Average | Notes |
|---|---|---|
| Q1 — AI Landscape Literacy (1–4) | **2.14** | A=1 'LLMs only' → D=4 'full taxonomy teacher' |
| Q2 — LLM mechanics confidence (1–5) | **2.46** | tokens, next-token, temperature, hallucination |
| Q3 — Hallucination awareness (1–4) | **2.57** | A=1 → D=4 'actively mitigates' |

### Section B — Copilot Setup & Core Features
| Metric | Average |
|---|---|
| Q4 — Copilot usage frequency (1–4) | **3.18** |
| Q5 — Inline-completion interactions used (out of 7) | **1.71** |
| Q6 — Chat slash/@/# commands used (out of 7) | **1.61** |

#### Q7 — Advanced features (1 Never tried → 5 Power user)
| Feature | Average |
|---|---|
| Agent Mode | **2.32** |
| Copilot Edits | **2.07** |
| Custom Instructions | **2.07** |
| Prompt Files | **1.86** |
| Model Switching | **3.04** |
| Copilot CLI | **1.54** |
| **Q7 overall mean** | **2.15** |

### Section C — Prompting Skills
| Metric | Average |
|---|---|
| Q8 — Prompt quality self-rating (1–5) | **2.75** |
| Q9 — Prompting framework discipline (1–4) | **2.46** |

### Section D — Responsible AI & Security
| Metric | Average |
|---|---|
| Q11 — Data-privacy practice (1–4) | **2.75** |
| Q12 — Copilot data-flow understanding (1–4) | **2.21** |
| Q13 — Security categories actively reviewed (out of 6) | **1.82** |

### Section E & F — SDLC, Testing, Review, Governance
| Metric | Average |
|---|---|
| Q14 — SDLC phases where AI is used (out of 7) | **2.96** |
| Q15 — Testing workflow maturity (1–4) | **2** |
| Q16 — Review rigor of AI output (1–5) | **3.64** |
| Q18 — Team AI governance maturity (1–4) | **1.64** |

#### Q17 — Most challenging step in the AI loop
| Step | Count |
|---|---|
| Think | 3 |
| Prompt | 3 |
| Review | 6 |
| Iterate | 3 |
| No deliberate loop | 13 |

---

## 👤 Detailed Results — All Participants

Score key: **Familiarity** is the 0–100 weighted score described above.

### Participant #1 — Familiarity score: **58.33 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (1/7) | Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 3.5/5) | Agent Mode: 5 — Power user · Copilot Edits: 5 — Power user · Custom Instructions: 3 — Use occasionally · Prompt Files: 3 — Use occasionally · Model Switching: 3 — Use occasionally · Copilot CLI: 2 — Tried once |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `use #file, #folder, whenever needed, give project context and feature context, ask to challenge the request itself and rewrite it for any forgotten gaps by the requester himself. ask to understand first, suggest request enhancement, propose several approaches and recommend the best one and why, then implement in mode agent. use plan mode whenever it makes sense.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (3/6) | Hardcoded credentials or secrets, Missing input validation or boundary checks, Missing or incorrect authentication / authorisation checks |
| Q14 | SDLC phases using AI (6/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs), Writing pull request descriptions and summaries |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 3 — Read like a junior colleague's code (3/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Organisation policy and tool availability. Security is primordial to the usage of generative AI to avoid leaking sensitive data out there, but I sometimes feel like there's too much assumptions & blockers "just to make sure". As an external consultant, I hope there will be some platform for me to be able to use the tools and llms I'm most productive with, while fully complying to GroupS' security obligations. I'm thinking of Cursor and my own Anthropic subscription as an example.` |
| Q20✍️ | Workshop goal | `Understand the internal functioning of generative AI, token generation, inference, token usage, thinking models, etc..` |

### Participant #2 — Familiarity score: **52.46 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | C — I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies (3/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q2✍️ | What feels unclear about LLMs | `I wouldn't be able to explain to someone in detail how an LLM is trained, how it consumes data, etc...` |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (1/7) | Esc to dismiss without accepting |
| Q6  | Chat commands used (3/7) | @workspace — ask questions grounded in the whole project, /fix — diagnose and repair broken code, /explain — understand code you didn't write |
| Q7  | Advanced features (avg 2/5) | Agent Mode: 3 — Use occasionally · Copilot Edits: 2 — Tried once · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 4 — Use regularly · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `Sometimes I copy/paste error messages for him so that he can figure out why. If I need to make a somewhat specific change, I use @workspace so that it can have more context. I tell him exactly what change I'd like to make, the constraints, and I explain roughly how I want it done.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (3/6) | Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Missing input validation or boundary checks, I review AI-generated code but don't specifically check for security issues |
| Q14 | SDLC phases using AI (4/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | B — I've tried generating tests but rarely find the output good enough to keep (2/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | D — A clear, documented AI usage policy is in place, communicated, and actively followed by the team (4/4) |
| Q19✍️ | Biggest blocker | `trust in output quality` |
| Q20✍️ | Workshop goal | `Generating more relevant test classes would save me time. Knowing how to prompt more optimally in order to consume fewer tokens unnecessarily.` |

### Participant #3 — Familiarity score: **41.04 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `Token what are they exactly, how are they consumed and counted, what is consumed exactly` |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (1/7) | @workspace — ask questions grounded in the whole project |
| Q7  | Advanced features (avg 1.83/5) | Agent Mode: 3 — Use occasionally · Copilot Edits: 3 — Use occasionally · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 2 — Tried once · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `Paste error messages. Tell him at what runtime action it happens (context)` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (6/6) | SQL injection / command injection, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds), Missing input validation or boundary checks, Cross-site scripting (XSS) risks |
| Q14 | SDLC phases using AI (2/7) | Debugging — diagnosing errors and fixing failing tests, Writing new code (features, logic, algorithms) |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | B — Prompt — writing a prompt that reliably produces the output I need |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Prompt skills and how to use it correctly to get the most out of it. Confidence in the output and potential hallucinations or not getting the context right or specifically how I meant it` |
| Q20✍️ | Workshop goal | `Be able to get my point across for the model to understand what I want specifically, and make it get the right context for each task when needed. Be sure that it has all the information for a better answer, so I don't have to give it additional info in other prompts.` |

### Participant #4 — Familiarity score: **41.22 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (2/7) | Tab to accept a full suggestion, Esc to dismiss without accepting |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 2.33/5) | Agent Mode: 3 — Use occasionally · Copilot Edits: 3 — Use occasionally · Custom Instructions: 3 — Use occasionally · Prompt Files: 1 — Never tried · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I try to explain briefly what I want: write unit test for x class. look at that stacktrace. I got this but was expeting that, ...` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (0/6) | I review AI-generated code but don't specifically check for security issues |
| Q14 | SDLC phases using AI (3/7) | Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `trust in output quality, prompt skills, environmental and social cost.` |
| Q20✍️ | Workshop goal | `I'd like to know how to use the most effectively the LLM tools` |

### Participant #5 — Familiarity score: **7.6 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | A — It's mostly one technology — large language models like ChatGPT (1/4) |
| Q2  | LLM mechanics confidence | 1 — No idea (1/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | B — I've tried it briefly but don't use it regularly (2/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1.17/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 2 — Tried once · Model Switching: 1 — Never tried · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | A — I type whatever comes to mind and refine if the result is wrong (1/4) |
| Q10✍️ | Context management approach | `write comment` |
| Q11 | Data-privacy practice | B — I know I shouldn't paste secrets but I'm not always consistent (2/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (0/7) | None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 2 — Skim for obvious errors (2/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `skills` |
| Q20✍️ | Workshop goal | `generate documentation, debugging, refactoring existing code` |

### Participant #6 — Familiarity score: **75.9 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | D — I could teach a colleague the full taxonomy, including discriminative vs. generative models and why that distinction matters for tools like Copilot (4/4) |
| Q2  | LLM mechanics confidence | 4 — Could explain it clearly (4/5) |
| Q2✍️ | What feels unclear about LLMs | `Profound knowledge is still out of reach (maths behind transformers) and how the LLM is finetuned is clearly not in scope right now.` |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (4/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Guiding completions with a specific comment immediately above the cursor, The Completions Panel (Ctrl+Enter) to see up to 10 alternatives |
| Q6  | Chat commands used (6/7) | #file / #selection — attach specific context to a prompt, @workspace — ask questions grounded in the whole project, /doc — generate docstrings or documentation, /tests — generate unit tests for selected code, /fix — diagnose and repair broken code, /explain — understand code you didn't write |
| Q7  | Advanced features (avg 3.67/5) | Agent Mode: 5 — Power user · Copilot Edits: 2 — Tried once · Custom Instructions: 4 — Use regularly · Prompt Files: 3 — Use occasionally · Model Switching: 5 — Power user · Copilot CLI: 3 — Use occasionally |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | D — I consistently apply a structured framework (e.g. Task / Context / Examples / Constraints) and use few-shot examples when the pattern is non-standard (4/4) |
| Q10✍️ | Context management approach | `Depends on the use case. Most of the time, I paste or link the files or code snippets that I find the more fitting to my case. Pasting the error blindly rarely works and explaining it is often way more reliable (you might still want to paste a part of the error sometimes). Referencing files or entire folders is often required to narrow the behavior of the LLM.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (4/6) | Missing input validation or boundary checks, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds) |
| Q14 | SDLC phases using AI (5/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `Trust in the output quality is certainly one of the biggest concerns. But honestly, right now the biggest blocker is that I find myself using less powerful LLM's to make sure I don't run out of tokens for the rest of my cycle.` |
| Q20✍️ | Workshop goal | `Create a full AI assisted SDLC that would be reliable (currently unachievable for the industry).` |

### Participant #7 — Familiarity score: **55.28 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | C — I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies (3/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (4/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Guiding completions with a specific comment immediately above the cursor, Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 2.67/5) | Agent Mode: 4 — Use regularly · Copilot Edits: 4 — Use regularly · Custom Instructions: 2 — Tried once · Prompt Files: 1 — Never tried · Model Switching: 4 — Use regularly · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I spent more time on telling it what not to do (make assumptions, add emotions to its response, ..) I paste previous output and tried to paint the bigger picture while detailing the task at hand. I tried to guide it beforehand when it comes to generalities dealing with code quality, security and performance.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (1/6) | Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (5/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `prompt skills, output quality differs immensely between tasks` |
| Q20✍️ | Workshop goal | `better prompting` |

### Participant #8 — Familiarity score: **33.19 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (1/7) | #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 2/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 3 — Use occasionally · Prompt Files: 3 — Use occasionally · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 2 — Simple but inconsistent (2/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I try to feed it as much info as possible, for ex. logs, to help it generate an answer for me.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (3/7) | Debugging — diagnosing errors and fixing failing tests, Generating or updating documentation (docstrings, READMEs, ADRs), Writing pull request descriptions and summaries |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 3 — Read like a junior colleague's code (3/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `Trust in output quality, prompt skills, time to learn` |
| Q20✍️ | Workshop goal | `Be more confident in using the AI and get the right results for my questions.` |

### Participant #9 — Familiarity score: **72.0 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | D — I could teach a colleague the full taxonomy, including discriminative vs. generative models and why that distinction matters for tools like Copilot (4/4) |
| Q2  | LLM mechanics confidence | 4 — Could explain it clearly (4/5) |
| Q2✍️ | What feels unclear about LLMs | `(topics I still - want to - learning about )   - vector search, vector databases, - retrieval-augmented generation, - inference optimization : model quantization, knowledge distillation (training a LLM model) and pruning (removing unimportant/redundant connections) - overall about how the orchestration frameworks work : prompt engineering - api interaction - data retrieval and state management` |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (4/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Guiding completions with a specific comment immediately above the cursor, Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (7/7) | #file / #selection — attach specific context to a prompt, @terminal — explain or generate shell commands, @workspace — ask questions grounded in the whole project, /doc — generate docstrings or documentation, /tests — generate unit tests for selected code, /fix — diagnose and repair broken code, /explain — understand code you didn't write |
| Q7  | Advanced features (avg 4.33/5) | Agent Mode: 4 — Use regularly · Copilot Edits: 5 — Power user · Custom Instructions: 4 — Use regularly · Prompt Files: 5 — Power user · Model Switching: 5 — Power user · Copilot CLI: 3 — Use occasionally |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `- give directly the concerned files / folders / functions (@ or # ), I let and order sometimes the LLM to explore further if needed to establish new connections or highlight missing spots with the current work or ongoing task. - I write sometimes simple (ephemeral) comments in the code itself to guide the LLM; or it will read the already present JsDoc ( a commenting standard : /** */ , in Typescript Normally these and indeed the error messages are also done by default by a bigger (but very costly LLM such Claude Opus 4.x / Gemini 3.x / GPT 5.x)` |
| Q11 | Data-privacy practice | B — I know I shouldn't paste secrets but I'm not always consistent (2/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (4/6) | Cross-site scripting (XSS) risks, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Missing input validation or boundary checks, I review AI-generated code but don't specifically check for security issues |
| Q14 | SDLC phases using AI (5/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 2 — Skim for obvious errors (2/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `organisation policy, transparency over costs, dependance of Microsoft contracting, flexibility with tools and providers ... my organization is currently a follower in the general Gen AI topic and we lag behind of several months/even a year or two (depending of topic/approach) ; but we're still in the right direction I guess we need urgently to have a proper evolutive 'stack' or 'tooling' about gen ai , maybe local LLM or a concurrent contract/provider (Mistral AI? ), so we can properly anticipate evolutions and costs and be ahead , thus also about agentic AI, new products & services to offer to our clients ; dependancy of 1 big provider (= Microsoft) is our bigger bottleneck, we will always feature restricted or cost-inflated in this way. this is not only about a single all-in-one fit product to get ROI, it's about a broader thinking in our AI usage and tooling to transform the business` |
| Q20✍️ | Workshop goal | `having a full stack monitoring and control of the LLM/AI implmentation, and knowing where to intervene in order to have a right correction loop and enhance workflow accross the time,; then share it with my team and other colleagues` |

### Participant #10 — Familiarity score: **39.36 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | A — It's mostly one technology — large language models like ChatGPT (1/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q2✍️ | What feels unclear about LLMs | `La façon dont ça fonctionne, j'ai un macbook pro, et j'utilise beaucoup le LLM, surtout en l'installant sur Ollama, mais je me sens beaucoup moins cultivé que Corneliu, je ne sais pas part exemple comment rajouter le model sur github copilot, part contre je l'ai déjà utilisé sur des script python` |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 2.5/5) | Agent Mode: 4 — Use regularly · Copilot Edits: 2 — Tried once · Custom Instructions: 1 — Never tried · Prompt Files: 2 — Tried once · Model Switching: 5 — Power user · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 2 — Simple but inconsistent (2/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `i open a specific file and write comments, but sometimes it could be not sufficient for to have a good solution .. I guess ?` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (1/6) | Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (4/7) | Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Code review — reviewing others' PRs or getting AI review on your own, Writing pull request descriptions and summaries |
| Q15 | Testing workflow | D — I use a TDD loop — generate failing tests first, then ask Copilot to write a passing implementation, then refactor safely under test coverage (4/4) |
| Q16 | Review rigor | 3 — Read like a junior colleague's code (3/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `La limite de tokens, et le fait que je puisse pas installer ollama sur mon ordinateur, ce qui aurait été très éfficace car j'aurais pu entrainer un modèle à faire des tests unitaires` |
| Q20✍️ | Workshop goal | `Trouver un moyen d'effectuer des prompts sans cramer mes tokens en 1 semaine ? être plus économe et savoir comment faire ?` |

### Participant #11 — Familiarity score: **57.76 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q2✍️ | What feels unclear about LLMs | `token splitting and also how to make a model grow ( make it learn new entries )` |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (4/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Guiding completions with a specific comment immediately above the cursor, Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (3/7) | /doc — generate docstrings or documentation, @workspace — ask questions grounded in the whole project, #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 2.83/5) | Agent Mode: 3 — Use occasionally · Copilot Edits: 4 — Use regularly · Custom Instructions: 3 — Use occasionally · Prompt Files: 2 — Tried once · Model Switching: 4 — Use regularly · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `I provide files references, provide specific filenames and/or functions, comments I also try to focus the AI on the files I want and tell it to ignore a specific scope if needed I also sometimes simply use it with an error message` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (5/6) | SQL injection / command injection, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds), Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (4/7) | Writing new code (features, logic, algorithms), Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | B — I've tried generating tests but rarely find the output good enough to keep (2/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `the single biggest blocker right now is time to learn more and try` |
| Q20✍️ | Workshop goal | `Know how to correctly prompt an AI to limit the token use while also being able to generate from scratch bigger projects ( with skills.md )` |

### Participant #12 — Familiarity score: **47.22 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q2✍️ | What feels unclear about LLMs | `Les diverses possibilités et la manière la plus précise d'obtenir un résultat rapidement, c'est souvent une suite d'essai erreur. Connaitre les outils pour mieux les utilisés` |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 2.17/5) | Agent Mode: 2 — Tried once · Copilot Edits: 4 — Use regularly · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 4 — Use regularly · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `je réfléchis au préalable afin de déterminer les objectifs et ensuite j'écris une première salve, je relis et je vois ce qui doit etre adapté avant de prompter. Une fois que je considère cela complet j'envoie le prompt` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (3/6) | Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds), Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (6/7) | Writing new code (features, logic, algorithms), Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs), Code review — reviewing others' PRs or getting AI review on your own, Writing pull request descriptions and summaries |
| Q15 | Testing workflow | B — I've tried generating tests but rarely find the output good enough to keep (2/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `la méconnaissance de toutes les possibilités concrètes.` |
| Q20✍️ | Workshop goal | `Mieux maitriser les tenants et les oboutissants, comprendre quoi demander et la façon la plus structurée de le faire, avec une veritable stratégie derrière cela` |

### Participant #13 — Familiarity score: **42.56 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `I don't know how the tokens work or how they are billed to Group S. So, I don't really dare to use them.` |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (1/7) | Esc to dismiss without accepting |
| Q6  | Chat commands used (4/7) | /explain — understand code you didn't write, /fix — diagnose and repair broken code, @workspace — ask questions grounded in the whole project, #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 2.17/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 2 — Tried once · Prompt Files: 1 — Never tried · Model Switching: 4 — Use regularly · Copilot CLI: 4 — Use regularly |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I open a file (Cobol or PL/SQL)  /explain to see if it picks up the code If it's a bug, I give him the error message. Otherwise, I ask him if he can optimize the code, especially for the PL/SQL` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (1/6) | Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (3/7) | Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | B — I've tried generating tests but rarely find the output good enough to keep (2/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | A — Think — clarifying what I want before I start prompting |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `prompt skills, organisation policy, time to learn, security concerns` |
| Q20✍️ | Workshop goal | `I would like to know the commands and usage guidelines for improving prompts with AI.` |

### Participant #14 — Familiarity score: **26.01 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | A — It's mostly one technology — large language models like ChatGPT (1/4) |
| Q2  | LLM mechanics confidence | 1 — No idea (1/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (1/7) | #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 1.5/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 2 — Tried once · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 2 — Simple but inconsistent (2/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `Ik laad een business analyse op met #file references` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (1/6) | Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (1/7) | Writing new code (features, logic, algorithms) |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | C — Review — evaluating whether the AI output is correct, secure, and well-structured |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `trust in output quality` |
| Q20✍️ | Workshop goal | `AI kunnen aansturen om correcte output te krijgen` |

### Participant #15 — Familiarity score: **33.86 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | B — I've tried it briefly but don't use it regularly (2/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (1/7) | #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 2/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 3 — Use occasionally · Prompt Files: 3 — Use occasionally · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `I don't ask copilot to generate code for me in cobol yet. I don't feel the need to in my current work. I mostly use it to help me in my research in large cobol programs and generate documentation.` |
| Q11 | Data-privacy practice | B — I know I shouldn't paste secrets but I'm not always consistent (2/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (3/7) | Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | B — Prompt — writing a prompt that reliably produces the output I need |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `trust in output quality and my prompt skills` |
| Q20✍️ | Workshop goal | `better prompts` |

### Participant #16 — Familiarity score: **24.57 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q3  | Hallucination awareness | A — I've heard the term but I'm not sure how it applies to coding tools (1/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (2/7) | Tab to accept a full suggestion, Esc to dismiss without accepting |
| Q6  | Chat commands used (2/7) | /fix — diagnose and repair broken code, /doc — generate docstrings or documentation |
| Q7  | Advanced features (avg 2/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 3 — Use occasionally · Prompt Files: 1 — Never tried · Model Switching: 3 — Use occasionally · Copilot CLI: 3 — Use occasionally |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `Ik open relevante bestanden en geef indien nodig bijkomende context mee, zoals een wettekst of een concrete foutmelding. Daarnaast open ik soms een specifiek bestand om documentatie of een analyse te laten genereren.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (0/6) | I review AI-generated code but don't specifically check for security issues |
| Q14 | SDLC phases using AI (1/7) | Generating or updating documentation (docstrings, READMEs, ADRs), None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 3 — Read like a junior colleague's code (3/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `De grootste belemmering is dat ik momenteel onvoldoende promptvaardigheden heb en te weinig tijd om hiermee te experimenteren en te leren. Ik zou geholpen zijn met een duidelijke aanzet of begeleiding om AI‑tools efficiënter en gerichter in te zetten.` |
| Q20✍️ | Workshop goal | `Tegen het einde van de training wil ik meer vertrouwen hebben in het formuleren van goede prompts en beter begrijpen hoe ik AI‑codingtools doelgericht kan inzetten in mijn dagelijkse werk. Ik wil weten hoe ik efficiënt begin, zonder veel trial‑and‑error en hoe ik AI als echte ondersteuning kan gebruiken.` |

### Participant #17 — Familiarity score: **17.75 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | A — It's mostly one technology — large language models like ChatGPT (1/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `J'ai des notions, mais je ne saurais pas expliquer comment ça fonctionne en interne` |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | A — I've never used it (1/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 1 — Never tried · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | D — I consistently apply a structured framework (e.g. Task / Context / Examples / Constraints) and use few-shot examples when the pattern is non-standard (4/4) |
| Q10✍️ | Context management approach | `Je n'utilise pas encore Copilot (développements en PowerBuilder)` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (2/6) | SQL injection / command injection, Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (0/7) | None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | A — Think — clarifying what I want before I start prompting |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Je l'utilise actuellement uniquement pour du SQL et du PHP Je ne sais pas comment l'utiliser pour PowerBuilder` |
| Q20✍️ | Workshop goal | `apprende à utiliser l'IA pour découvrir de nouvelles fonctionnalité dans les langages de programmation Egalement vérifier et tester les scripts que j'écris` |

### Participant #18 — Familiarity score: **26.31 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `A vrai dire "pas grand chose" ...` |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (3/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, The Completions Panel (Ctrl+Enter) to see up to 10 alternatives |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 1 — Never tried · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 2 — Simple but inconsistent (2/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `Never` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (0/7) | None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 1 — Accept without deep review (1/5) |
| Q17 | Most challenging loop step | D — Iterate — knowing when to refine the prompt vs. edit the output manually |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Confiance ...` |
| Q20✍️ | Workshop goal | `Tout` |

### Participant #19 — Familiarity score: **29.02 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (3/7) | Tab to accept a full suggestion, Ctrl+→ to accept word-by-word (partial accept), Esc to dismiss without accepting |
| Q6  | Chat commands used (2/7) | #file / #selection — attach specific context to a prompt, @workspace — ask questions grounded in the whole project |
| Q7  | Advanced features (avg 1.83/5) | Agent Mode: 1 — Never tried · Copilot Edits: 2 — Tried once · Custom Instructions: 2 — Tried once · Prompt Files: 3 — Use occasionally · Model Switching: 1 — Never tried · Copilot CLI: 2 — Tried once |
| Q8  | Prompt quality self-rating | 2 — Simple but inconsistent (2/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `Je n'ai pas encore vraiment fait créer des scripts par copilot` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (1/7) | Refactoring existing code |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 2 — Skim for obvious errors (2/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Je n'ai pas encore eu beaucoup le temps pour entrer dans cette matière.` |
| Q20✍️ | Workshop goal | `Pouvoir faire créer des scripts et faire confiance à l'IA` |

### Participant #20 — Familiarity score: **40.9 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | C — I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies (3/4) |
| Q2  | LLM mechanics confidence | 4 — Could explain it clearly (4/5) |
| Q2✍️ | What feels unclear about LLMs | `a bit unclear on how attention mechanisms translate into reasoning.` |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (4/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Guiding completions with a specific comment immediately above the cursor, Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1.33/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I usually provide context by describing task or past errors when needed, try to guide the expected behavior. I don't follow a strict structure` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (3/6) | Missing or incorrect authentication / authorisation checks, Missing input validation or boundary checks, Hardcoded credentials or secrets |
| Q14 | SDLC phases using AI (3/7) | Refactoring existing code, Writing new code (features, logic, algorithms), Debugging — diagnosing errors and fixing failing tests |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `provide the best level of context and structure in prompts` |
| Q20✍️ | Workshop goal | `Improve how I structure prompts and integrate AI more effectively into my workflow to increase code quality` |

### Participant #21 — Familiarity score: **8.35 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 1 — No idea (1/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | A — I've never used it (1/4) |
| Q5  | Inline completions used (2/7) | Esc to dismiss without accepting, Tab to accept a full suggestion |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 1 — Never tried · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | A — I type whatever comes to mind and refine if the result is wrong (1/4) |
| Q10✍️ | Context management approach | `??` |
| Q11 | Data-privacy practice | A — I paste whatever I'm working with and haven't thought much about what gets sent (1/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (0/7) | None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 4 — Read line-by-line and run it (4/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `?` |
| Q20✍️ | Workshop goal | `?` |

### Participant #22 — Familiarity score: **16.98 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `Vocabulary and practice` |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | B — I've tried it briefly but don't use it regularly (2/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1.67/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 2 — Tried once · Prompt Files: 2 — Tried once · Model Switching: 2 — Tried once · Copilot CLI: 2 — Tried once |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | A — I type whatever comes to mind and refine if the result is wrong (1/4) |
| Q10✍️ | Context management approach | `The only exercise I have done is to get documentation based on skill and instructions defined by a colleague to view the result in a 'md' file. I use the M365 Copilot.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (1/7) | Writing new code (features, logic, algorithms) |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 2 — Skim for obvious errors (2/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | D — A clear, documented AI usage policy is in place, communicated, and actively followed by the team (4/4) |
| Q19✍️ | Biggest blocker | `time to learn` |
| Q20✍️ | Workshop goal | `To understand more easily and quickly programs I know little about => Before testing, improving quality code, may be converting in another code, ...` |

### Participant #23 — Familiarity score: **52.66 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | C — I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies (3/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (2/7) | Tab to accept a full suggestion, Guiding completions with a specific comment immediately above the cursor |
| Q6  | Chat commands used (3/7) | /tests — generate unit tests for selected code, /explain — understand code you didn't write, /doc — generate docstrings or documentation |
| Q7  | Advanced features (avg 3.17/5) | Agent Mode: 2 — Tried once · Copilot Edits: 3 — Use occasionally · Custom Instructions: 3 — Use occasionally · Prompt Files: 3 — Use occasionally · Model Switching: 4 — Use regularly · Copilot CLI: 4 — Use regularly |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I open the relevant class to modify, related interfaces/entities/repositories, and similar tests. Then I give Copilot the exact error message, sometimes with #file references or a short comment explaining the expected behavior.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (2/6) | Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds) |
| Q14 | SDLC phases using AI (5/7) | Writing or extending unit / integration tests, Code review — reviewing others' PRs or getting AI review on your own, Writing pull request descriptions and summaries, Debugging — diagnosing errors and fixing failing tests, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | D — Iterate — knowing when to refine the prompt vs. edit the output manually |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `My biggest blocker is trust in output quality.` |
| Q20✍️ | Workshop goal | `By the end of the training, I want to be more confident in evaluating their output, especially debugging and writing tests.` |

### Participant #24 — Familiarity score: **71.98 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 3 — Could outline it (3/5) |
| Q2✍️ | What feels unclear about LLMs | `Temperature and how to adjust it` |
| Q3  | Hallucination awareness | D — I actively adjust my workflow — prompt structure, review habits, test coverage — specifically to mitigate hallucination risk (4/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (5/7) | Tab to accept a full suggestion, Esc to dismiss without accepting, Ctrl+→ to accept word-by-word (partial accept), Guiding completions with a specific comment immediately above the cursor, Comment-driven development (writing algorithm steps as comments first) |
| Q6  | Chat commands used (7/7) | /explain — understand code you didn't write, /fix — diagnose and repair broken code, /tests — generate unit tests for selected code, /doc — generate docstrings or documentation, @workspace — ask questions grounded in the whole project, @terminal — explain or generate shell commands, #file / #selection — attach specific context to a prompt |
| Q7  | Advanced features (avg 3.5/5) | Agent Mode: 5 — Power user · Copilot Edits: 2 — Tried once · Custom Instructions: 4 — Use regularly · Prompt Files: 4 — Use regularly · Model Switching: 5 — Power user · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | D — I consistently apply a structured framework (e.g. Task / Context / Examples / Constraints) and use few-shot examples when the pattern is non-standard (4/4) |
| Q10✍️ | Context management approach | `I open a complete folder which becomes the workspace. I use the ‘Add File to Chat’ option for an entire project, the ‘Add #file to the context’ option, and the ‘#file’ tag to add source files, instructions (Markdown) or documentation (usually converted to or created in Markdown). I usually create the source file first using a template and optionally add comments to it.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (2/6) | SQL injection / command injection, Hardcoded credentials or secrets, I review AI-generated code but don't specifically check for security issues |
| Q14 | SDLC phases using AI (5/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | D — I use a TDD loop — generate failing tests first, then ask Copilot to write a passing implementation, then refactor safely under test coverage (4/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | A — Think — clarifying what I want before I start prompting |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `tool availability - lightweight software suite, subscription limited in usage (requests)` |
| Q20✍️ | Workshop goal | `creation/use of SKILL for development` |

### Participant #25 — Familiarity score: **47.83 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q2✍️ | What feels unclear about LLMs | `1) How does an LLM maintain conversation context during long conversations? What are the practical limitations? 2) Training Process: What is the detailed training workflow? How do models learn patterns? 3) Developer Knowledge Requirement: Do I need to understand LLM mechanics in depth, or should I focus on best practices for using existing models? As in most cases, we are using pre-trained models anyway ? 4) Output Generation Details: I understand LLMs generate tokens sequentially, it would be nice to have a concrete example of how the model calculates/predicts the next token?` |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (3/7) | The Completions Panel (Ctrl+Enter) to see up to 10 alternatives, Esc to dismiss without accepting, Tab to accept a full suggestion |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1.5/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 4 — Use regularly · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 4 — Structured prompts (4/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `I indicate the files or methods that need to be changed. I specify clear instructions with constraints and technical explanations in detailsto guide code generation. I also sometimes copy-paste existing code to show the current structure or request refactoring. I generally chat` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (3/6) | SQL injection / command injection, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets |
| Q14 | SDLC phases using AI (4/7) | Writing or extending unit / integration tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs), Code review — reviewing others' PRs or getting AI review on your own |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | B — Prompt — writing a prompt that reliably produces the output I need |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `Lack of clear team policy on AI tool usage. I know not to share passwords,  but we lack documentation on company guidelines for data handling with AI tools. What is our organization's official policy on data security with AI? What are the compliance and security risks I should be aware of? It would be great to have clear guidance to use AI tools securely and confidently.` |
| Q20✍️ | Workshop goal | `Productivity & Shortcuts  Learn all the tricks, shortcuts, and best practices to code faster with Copilot. Optimized Prompts, build effective, reusable prompt templates for common tasks (unit tests, integration tests, code generation) that I can launch and adapt quickly. Explore creating autonomous agents that detect and fix issues automatically, with developer review and approval. For example: an agent monitors error logs in Elasticsearch, identifies issues, attempts fixes, and presents results to the developer. The developer then validates the fix either approving it to save time, or correcting it if needed.` |

### Participant #26 — Familiarity score: **5.67 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | A — It's mostly one technology — large language models like ChatGPT (1/4) |
| Q2  | LLM mechanics confidence | 1 — No idea (1/5) |
| Q3  | Hallucination awareness | A — I've heard the term but I'm not sure how it applies to coding tools (1/4) |
| Q4  | Current Copilot usage | B — I've tried it briefly but don't use it regularly (2/4) |
| Q5  | Inline completions used (0/7) | None of the above |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 1/5) | Agent Mode: 1 — Never tried · Copilot Edits: 1 — Never tried · Custom Instructions: 1 — Never tried · Prompt Files: 1 — Never tried · Model Switching: 1 — Never tried · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 1 — Very vague (1/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `Aucune idée` |
| Q11 | Data-privacy practice | A — I paste whatever I'm working with and haven't thought much about what gets sent (1/4) |
| Q12 | Copilot data-flow understanding | A — I assume it's used to train the model — I haven't looked into it (1/4) |
| Q13 | Security checks (0/6) | I haven't thought about this yet |
| Q14 | SDLC phases using AI (0/7) | None — I don't use AI tools in my development workflow yet |
| Q15 | Testing workflow | A — I don't use AI to write tests (1/4) |
| Q16 | Review rigor | 1 — Accept without deep review (1/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `Je hais l'IA` |
| Q20✍️ | Workshop goal | `Je hais l'IA` |

### Participant #27 — Familiarity score: **50.27 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | C — I can clearly distinguish narrow AI, machine learning, deep learning, and generative AI and explain when each applies (3/4) |
| Q2  | LLM mechanics confidence | 4 — Could explain it clearly (4/5) |
| Q3  | Hallucination awareness | C — I understand hallucination at the token/probability level and can explain specific code-generation failure modes (invented APIs, plausible-but-wrong logic) (3/4) |
| Q4  | Current Copilot usage | C — I use it occasionally when I remember it's there (3/4) |
| Q5  | Inline completions used (2/7) | Tab to accept a full suggestion, Esc to dismiss without accepting |
| Q6  | Chat commands used (0/7) | None — I haven't used Chat slash commands yet |
| Q7  | Advanced features (avg 2.33/5) | Agent Mode: 4 — Use regularly · Copilot Edits: 3 — Use occasionally · Custom Instructions: 2 — Tried once · Prompt Files: 1 — Never tried · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 3 — Some context and constraints (3/5) |
| Q9  | Prompting framework | B — I try to be specific but don't follow a particular structure (2/4) |
| Q10✍️ | Context management approach | `I provide context to Copilot by attaching specific files and by relying on the workspace structure that is automatically shared.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | C — I know that Copilot Business/Enterprise does not train on user code by default, and I understand what data travels to the model (3/4) |
| Q13 | Security checks (5/6) | SQL injection / command injection, Missing or incorrect authentication / authorisation checks, Hardcoded credentials or secrets, Insecure use of cryptography (weak algorithms, predictable seeds), Missing input validation or boundary checks |
| Q14 | SDLC phases using AI (4/7) | Writing or extending unit / integration tests, Code review — reviewing others' PRs or getting AI review on your own, Refactoring existing code, Debugging — diagnosing errors and fixing failing tests |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 5 — Read, run, and check edge cases and security (5/5) |
| Q17 | Most challenging loop step | E — I haven't established a deliberate loop yet — I interact more ad-hoc |
| Q18 | Team AI governance | A — No rules — everyone uses (or doesn't use) AI tools as they see fit (1/4) |
| Q19✍️ | Biggest blocker | `-` |
| Q20✍️ | Workshop goal | `-` |

### Participant #28 — Familiarity score: **50.99 / 100**

| Q | Question | Answer |
|---|---|---|
| Q1  | AI Landscape Literacy | B — I know it covers several categories (rules-based, ML, generative AI) but I couldn't explain the differences confidently (2/4) |
| Q2  | LLM mechanics confidence | 2 — Vague sense (2/5) |
| Q3  | Hallucination awareness | B — I know it means the model can produce incorrect output, but I rely on tests and review to catch it (2/4) |
| Q4  | Current Copilot usage | D — It's part of my daily workflow and I'd notice if it was gone (4/4) |
| Q5  | Inline completions used (1/7) | Tab to accept a full suggestion |
| Q6  | Chat commands used (4/7) | /explain — understand code you didn't write, /fix — diagnose and repair broken code, /tests — generate unit tests for selected code, /doc — generate docstrings or documentation |
| Q7  | Advanced features (avg 2.17/5) | Agent Mode: 4 — Use regularly · Copilot Edits: 1 — Never tried · Custom Instructions: 3 — Use occasionally · Prompt Files: 1 — Never tried · Model Switching: 3 — Use occasionally · Copilot CLI: 1 — Never tried |
| Q8  | Prompt quality self-rating | 5 — Precise and deliberate (5/5) |
| Q9  | Prompting framework | C — I consciously include the task (what), context (language/framework), and any constraints (3/4) |
| Q10✍️ | Context management approach | `Based on the tasks, I either give individual files or entire modules as context. I give specific constraints, and in case of large requests, always ask whether its clear for the AI agent or whether it needs additional information.` |
| Q11 | Data-privacy practice | C — I avoid pasting secrets, PII, or sensitive data and replace them with placeholders before sharing (3/4) |
| Q12 | Copilot data-flow understanding | B — I've heard there are different rules for Individual vs. Business tiers but I couldn't explain them (2/4) |
| Q13 | Security checks (2/6) | Missing input validation or boundary checks, SQL injection / command injection |
| Q14 | SDLC phases using AI (5/7) | Writing new code (features, logic, algorithms), Writing or extending unit / integration tests, Debugging — diagnosing errors and fixing failing tests, Refactoring existing code, Generating or updating documentation (docstrings, READMEs, ADRs) |
| Q15 | Testing workflow | C — I use `/tests` or equivalent as a scaffold and then extend with manual edge cases (3/4) |
| Q16 | Review rigor | 3 — Read like a junior colleague's code (3/5) |
| Q17 | Most challenging loop step | D — Iterate — knowing when to refine the prompt vs. edit the output manually |
| Q18 | Team AI governance | B — Informal norms — we have unwritten shared expectations but nothing documented (2/4) |
| Q19✍️ | Biggest blocker | `A clear and well created instruction file. Now we need to spend most of the time to explain on how we want the code to be structured, or where certain data/classes can be found/retrieved. With a good instruction file, we can focus more on the business request and analysis, rather than to repeat ourselves over and over again.` |
| Q20✍️ | Workshop goal | `Even better and easier AI usage.` |


---

## 🏁 Final Verdict — Three Familiarity Groups

Participants are split into three cohorts strictly by descending familiarity score: **10 → Basics**, **10 → More Advanced**, **8 → Most Advanced**. This grouping is intended to drive workshop pacing, exercise selection, and pairing.


### 🥇 Most Advanced (Top 8)
_Daily Copilot users with hands-on experience in advanced features (Agent Mode, Edits, Custom Instructions). Ready for deep-dive sessions on agentic workflows, custom skills, model selection, and governance leadership._

| Rank | ID | Score | Q4 Copilot use | Q7 avg | Q8 prompt | Chat cmds | Inline | Adv. signals |
|---|---|---|---|---|---|---|---|---|
| 1 | #6 | **75.9** | D | 3.67 | 4/5 | 6/7 | 4/7 | Agent, CustomInstr, ModelSwitch |
| 2 | #9 | **72.0** | D | 4.33 | 4/5 | 7/7 | 4/7 | Agent, Edits, CustomInstr, PromptFiles, ModelSwitch |
| 3 | #24 | **71.98** | D | 3.5 | 4/5 | 7/7 | 5/7 | Agent, CustomInstr, PromptFiles, ModelSwitch |
| 4 | #1 | **58.33** | D | 3.5 | 4/5 | 0/7 | 1/7 | Agent, Edits |
| 5 | #11 | **57.76** | D | 2.83 | 3/5 | 3/7 | 4/7 | Edits, ModelSwitch |
| 6 | #7 | **55.28** | D | 2.67 | 4/5 | 0/7 | 4/7 | Agent, Edits, ModelSwitch |
| 7 | #23 | **52.66** | C | 3.17 | 3/5 | 3/7 | 2/7 | ModelSwitch, CLI |
| 8 | #2 | **52.46** | C | 2 | 4/5 | 3/7 | 1/7 | ModelSwitch |

**Group averages:**  
Familiarity **62.05** · Q4 Copilot use 3.75/4 · Q2 LLM conf 3.12/5 · Q7 advanced features 3.21/5 · Q8 prompt quality 3.75/5 · Q9 framework 3/4 · Q12 data understanding 2.88/4 · Q14 SDLC breadth 4.88/7


### 🥈 More Advanced (Middle 10)
_Regular Copilot users with solid prompting habits but uneven exposure to advanced features. Best served by structured deep-dives on Agent Mode, Custom Instructions, prompt files, and security-aware review._

| Rank | ID | Score | Q4 Copilot use | Q7 avg | Q8 prompt | Chat cmds | Inline | Adv. signals |
|---|---|---|---|---|---|---|---|---|
| 1 | #28 | **50.99** | D | 2.17 | 5/5 | 4/7 | 1/7 | Agent |
| 2 | #27 | **50.27** | C | 2.33 | 3/5 | 0/7 | 2/7 | Agent |
| 3 | #25 | **47.83** | D | 1.5 | 4/5 | 0/7 | 3/7 | ModelSwitch |
| 4 | #12 | **47.22** | D | 2.17 | 4/5 | 0/7 | 0/7 | Edits, ModelSwitch |
| 5 | #13 | **42.56** | C | 2.17 | 3/5 | 4/7 | 1/7 | ModelSwitch, CLI |
| 6 | #4 | **41.22** | C | 2.33 | 3/5 | 0/7 | 2/7 | — |
| 7 | #3 | **41.04** | D | 1.83 | 3/5 | 1/7 | 0/7 | — |
| 8 | #20 | **40.9** | C | 1.33 | 3/5 | 0/7 | 4/7 | — |
| 9 | #10 | **39.36** | D | 2.5 | 2/5 | 0/7 | 0/7 | Agent, ModelSwitch |
| 10 | #15 | **33.86** | B | 2 | 3/5 | 1/7 | 0/7 | — |

**Group averages:**  
Familiarity **43.52** · Q4 Copilot use 3.4/4 · Q2 LLM conf 2.8/5 · Q7 advanced features 2.03/5 · Q8 prompt quality 3.3/5 · Q9 framework 2.5/4 · Q12 data understanding 2.4/4 · Q14 SDLC breadth 3.7/7


### 🥉 Basics (Bottom 10)
_Occasional or new Copilot users. Need foundations: how LLMs work, inline completion fluency, chat commands, prompt structure, and responsible-AI basics before tackling agentic features._

| Rank | ID | Score | Q4 Copilot use | Q7 avg | Q8 prompt | Chat cmds | Inline | Adv. signals |
|---|---|---|---|---|---|---|---|---|
| 1 | #8 | **33.19** | D | 2 | 2/5 | 1/7 | 0/7 | — |
| 2 | #19 | **29.02** | C | 1.83 | 2/5 | 2/7 | 3/7 | — |
| 3 | #18 | **26.31** | C | 1 | 2/5 | 0/7 | 3/7 | — |
| 4 | #14 | **26.01** | D | 1.5 | 2/5 | 1/7 | 0/7 | — |
| 5 | #16 | **24.57** | C | 2 | 1/5 | 2/7 | 2/7 | — |
| 6 | #17 | **17.75** | A | 1 | 1/5 | 0/7 | 0/7 | — |
| 7 | #22 | **16.98** | B | 1.67 | 1/5 | 0/7 | 0/7 | — |
| 8 | #21 | **8.35** | A | 1 | 1/5 | 0/7 | 2/7 | — |
| 9 | #5 | **7.6** | B | 1.17 | 1/5 | 0/7 | 0/7 | — |
| 10 | #26 | **5.67** | B | 1 | 1/5 | 0/7 | 0/7 | — |

**Group averages:**  
Familiarity **19.54** · Q4 Copilot use 2.5/4 · Q2 LLM conf 1.6/5 · Q7 advanced features 1.42/5 · Q8 prompt quality 1.4/5 · Q9 framework 2/4 · Q12 data understanding 1.5/4 · Q14 SDLC breadth 0.7/7


### 📋 Cohort Roster (quick reference)

| Group | Participant IDs |
|---|---|
| 🥇 Most Advanced (8) | #6, #9, #24, #1, #11, #7, #23, #2 |
| 🥈 More Advanced (10) | #28, #27, #25, #12, #13, #4, #3, #20, #10, #15 |
| 🥉 Basics (10) | #8, #19, #18, #14, #16, #17, #22, #21, #5, #26 |

---

## 🎯 Workshop Recommendations

- **Most Advanced cohort** averages **3.21/5** on advanced features — push them on Agent Mode autonomy, custom instructions design, prompt-file libraries, model selection trade-offs, and team governance.
- **More Advanced cohort** sits between proficient daily users and aspiring power users — focus on filling Q7 gaps (Custom Instructions, Prompt files, Model switching) and tightening prompt frameworks.
- **Basics cohort** averages only **2.5/4** on Copilot usage frequency — start with hands-on inline completion, the core slash commands, and prompt structure before moving to advanced topics.
- Across all cohorts, **Q17** flags **'No deliberate loop'** as the most-cited challenge — allocate explicit workshop time to this loop step.
- Q12 (data-flow understanding) average is **2.21/4** — a short responsible-AI primer benefits every cohort.