[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 2 — Meet Your New Best Friend: GitHub Copilot](../chapter-02/README.md) | [Chapter 4 — Let Your AI Co-Pilot Take the Wheel →](../chapter-04/README.md)

---

# Chapter 3— Power with Purpose: Using AI Responsibly

> **Duration:** 90 minutes | Day 1, 13:15 – 14:45

This chapter walks through Microsoft and GitHub's **six principles of Responsible AI** — Fairness, Reliability & Safety, Privacy & Security, Inclusiveness, Transparency, and Accountability — one by one, and grounds each in how GitHub Copilot actually behaves. The goal is to develop smarter with AI **without losing control**: understanding what data is sent, why suggestions vary in quality, where the human stays accountable, and how to extend AI assistance to everyone.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Name the six Microsoft/GitHub Responsible AI principles and what each means for Copilot
- Explain why Copilot's suggestions reflect both the strengths and the biases of the public code it was trained on
- Describe what data Copilot actually sends to the model — and what it does *not* (no keystroke streaming)
- Distinguish training use from short-term retention, and tell which plan tiers offer a no-training guarantee
- Explain how the secret-blocking filter and the public-code (duplication) filter work, and what they do not cover
- Recognise that AI assistance must stay inclusive — across abilities, languages, and connectivity — including local models with Ollama
- Treat Copilot like a fast junior developer: review, test, and own every suggestion you accept

---

## 📋 Content Outline

### 1. The Six Principles & Principle 1 — Fairness
- **Six Principles of Responsible AI** — Microsoft and GitHub's shared foundation: Fairness, Reliability & Safety, Privacy & Security, Inclusiveness, Transparency, Accountability; each is explored in turn alongside the Copilot topics that bring it to life
- **Fairness — treat all people fairly, never amplify bias**
  - No differential impact on similar groups; consistent outcomes for similar cases
  - Detect bias, mitigate unfair impact, and train on diverse, balanced data
  - For Copilot: watch for weaker output in some languages or ecosystems; review generated user-facing logic for discriminatory criteria; bias in training data can resurface
- **Trained on a world of public code**
  - Copilot is built on large language models — what they learned shapes what they suggest
  - Trained on a mountain of public code (GitHub repos, Q&A sites like Stack Overflow, countless languages and frameworks — great and not-so-great patterns alike) and on broad general-purpose reasoning (natural language, explaining, planning, refactoring, "rubber ducking")
  - It learned from the crowd — including the crowd's mistakes; suggestion quality varies with the training data
- **Exercise 301 — Who Does Copilot Picture?** Let Copilot autocomplete name lists, pronouns and sample data, run it, and spot the gender/ethnicity/locale defaults it quietly reaches for — then rework the code to stop baking in stereotyped defaults

### 2. Principle 2 — Reliability & Safety
- **Reliability & Safety — perform reliably and safely, especially at the edges**
  - Perform consistently as designed; respond safely to unexpected input; resist harmful manipulation; minimize unintended harm
  - For Copilot: suggestions are not guaranteed correct — always review, test and validate; beware prompt injection in context; check edge cases and error handling
- **Treat it like Stack Overflow** — apply the same judgement you would to a web answer
  - **Think:** does this fit my problem and context? Do I understand it?
  - **Implement:** adapt it to your conventions and needs — never blind paste
  - **Test:** verify it works and is correct & secure before you trust it
  - Critical thinking is non-negotiable: if you cannot explain a suggestion line by line, do not accept it — you own the code you commit, not Copilot
- **Exercise 302 — Infix/Postfix with Ask Mode** Use Copilot Ask mode to implement a tricky algorithm and make it explain every line: understand precedence, draft infix-to-postfix conversion and postfix evaluation, have Copilot teach back both stacks, then verify and own it

### 3. Principle 3 — Privacy & Security
- **Privacy & Security — be secure and respect user privacy**
  - Get consent before collecting data; collect only what is needed; anonymize personal data; encrypt in transit and at rest
  - For Copilot: no training on Business/Enterprise code; content exclusion keeps files out; secret-blocking filter on completions; keep sensitive data out of context
- **What data is sent to the model?** Copilot assembles a request locally first — the prompt plus the most relevant context
  - Your prompt (chat input, pasted code and errors), editor context (code around the cursor, imports, types, selection), and extra files (attached files, open tabs, instructions, repo context)
  - Typical payload: `prompt + nearby code + referenced files + IDE metadata` (language, file type, path context, diagnostics, repository info)
  - Key idea: the model sees a **ranked slice** of context, not your whole project
- **No keystrokes, only context** — requests are event-based, sent when you ask for help
  - *Not* sent: every raw keystroke, a live mirror of your screen, constant background uploads
  - *Sent per request:* the prompt/chat message, surrounding code and included files, IDE metadata and relevant diagnostics — think request payload, not keystroke logging (the data can still be sensitive)
- **Watch for sensitive data** — if it is visible in your prompt context, assume it can be sent
  - Common leak paths: hardcoded secrets/tokens, sensitive business logic, connection strings and keys, internal URLs/hostnames, nearby open tabs, variable names, sample credentials, copied stack traces, `.env` values, customer data, TODO comments with real details
  - Best practice: replace secrets with placeholders and keep sensitive files out of Copilot context
- **Is your data used to train future models?** Depends on plan and policy
  - **Individual:** check your GitHub settings — product-improvement data use can depend on that opt-in choice
  - **Business:** prompts and code are *not* used to train GitHub foundation models — the main enterprise assurance, managed at the org level
  - **Enterprise:** same no-training guarantee, plus more admin/compliance controls; still cloud-hosted inference
  - Training use and short-term retention are different topics: even with training off, requests may be kept briefly for security, abuse detection, and reliability
- **Secret scanning integration** — two layers keep leaked secrets out
  - **Copilot-side blocking:** filters completions that would emit a known secret pattern (e.g. `ghp_` GitHub PAT, `AKIA` AWS key) before it reaches your editor — stops the model reproducing secrets
  - **Repository-side scanning:** GitHub secret scanning runs on the repo; push protection can block a commit containing a detected secret
  - What it does *not* cover: it catches patterns in completions, not semantics — it will not protect a secret *you* paste into Copilot Chat; use placeholders, never real secrets
- **Exercise 303 — Malicious Repo Prompt Trap** Review a hostile repo setup flow, find the trust break (the repo controls a script the agent may run as routine setup), use Copilot to threat-model — not execute — the instructions, and set the rule: never let AI tooling run repo-local setup commands until a human has inspected them
- **Exercise 304 — Malicious MCP "Obfuscator" Demo** A tool can look legitimate ("obfuscate my code"), ask for too much (secrets/files it never needed), behave well to build trust then pull the rug later — keep the human in charge by reviewing tool descriptions, requested context, and every prompt before approving access

### 4. Principle 4 — Inclusiveness
- **Inclusiveness — empower everyone; design for all abilities and regions**
  - Empower diverse users; accessible regardless of ability; available worldwide, even offline; diverse input into the design
  - For Copilot: supports many languages and skill levels; lowers the barrier for newcomers; chat helps non-native speakers; accessible tooling in the IDE
- **Lowering the barrier to entry** — expertise on tap for everyone
  - Helps beginners, career-changers, non-native English speakers, returning developers, and solo devs with no senior to ask
  - Explains code in plain language, suggests idiomatic patterns to learn from, answers without judgment, turns docs into runnable examples
- **Works in your language — both kinds**
  - Programming languages: broad coverage, strongest on popular ecosystems, quality varies by representation (ties back to fairness)
  - Natural language: chat and prompts in many human languages — ask and read answers in your own tongue, lowering the language barrier
- **Accessible by design** — AI tooling should work for every developer regardless of ability
  - Chat as an alternative to mouse-heavy navigation; keyboard-driven, conversational flows; less repetitive typing; voice interaction where supported
  - Keep in mind: accessibility depends on the host IDE; verify with your own screen reader and assistive tooling; it is not a substitute for building accessible products yourself
- **Available where you are** — connectivity, cost and data-residency rules should not lock people out
  - The challenge: cloud Copilot needs a reliable connection; some regions/networks restrict it; data-residency rules can block cloud inference entirely
  - Local models help: run open models locally with **Ollama** — works offline and on slow networks, keeping tooling reachable for more developers
- **Using your own models with Ollama** — Copilot is hosted, but the same app patterns can target a local model server
  - Why local: keep experiments on your machine, use open local coding models, useful for demos and labs
  - Typical stack: `Your app → Ollama → local model → tool call → response` — same tool-calling pattern, different backend
  - Tradeoffs: usually smaller context windows, setup and hardware are on you, quality may differ from frontier models (Copilot is the product; Ollama is one way to build your own local alternative)
- **Exercise 305 — Tool Calls with Ollama** Rebuild Exercise 104 against a local Ollama-backed model: keep the same chat and tool-call structure, swap the backend from the hosted model, reuse `GetDate` and `GetTime`, then compare privacy, setup effort, speed, and answer quality against Exercise 104

### 5. Principle 5 — Transparency
- **Transparency — be understandable and honest**
  - Explain how the system operates; justify design choices; be honest about capabilities and limits; enable logging and auditing
  - For Copilot: model cards document behavior; you can see which model is in use; public-code matching is disclosed; data collection is documented
- **What telemetry is tracked?** — product usage and quality signals, not full code content
  - Usage metrics (feature used, request count, accept/reject/dismiss rates), service quality (latency, error rates, safety/filter outcomes), admin insight (org-level usage reporting, audit and compliance views)
  - Important distinction: telemetry for statistics is not the same as sending full code content for analytics dashboards — ask what is measured, where it is stored, and who in the org can see it
- **GitHub Copilot architecture** — local prompt assembly → GitHub service controls → hosted model inference
  - *On your computer:* the VS Code / IDE extension gathers prompt and context and renders suggestions and chat UI
  - *GitHub proxy (control plane):* auth, policy, routing, filtering and safety controls, logging, telemetry, abuse checks
  - *Hosted cloud models:* perform the inference and send the result back — prompt goes right, answer comes back left
- **The "public code" filter** — Copilot checks completions against an index of known public code
  - **Generate → Compare → Decide:** the model produces a candidate (not yet shown), it is matched against the public-code index with a similarity threshold, and above-threshold matches are blocked (an alternative or nothing is returned)
  - Targets near-verbatim reproduction of substantial blocks; short snippets (under ~150 chars) are usually exempt; it does not flag merely inspired-by or structurally similar code, and runs at suggestion time, not training time
  - Opt-in for Individual; built into the IP indemnity terms for Business and Enterprise
- **Duplication detection & the setting** — one setting: "Suggestions matching public code" → Allowed or Blocked
  - *Allowed* (default for Individual): matching suggestions are shown, carrying more IP exposure
  - *Blocked* (recommended, safer): matching completions are silently skipped (you may see fewer)
  - Set it at GitHub.com → Settings → Copilot → "Suggestions matching public code" → Blocked; on Business/Enterprise an admin can enforce "Blocked" for everyone, with no user override
  - The matching index is built from GitHub's public code search, updated periodically — not in real time

### 6. Principle 6 — Accountability & Quizzes
- **Accountability — a human is always responsible, not the model**
  - People, not models, are responsible; creators own how systems operate; continuously monitor performance; mitigate risks and harms
  - For Copilot: you own the code you commit; review before accepting; humans make the final decision; orgs set and enforce usage policy
- **Think of it as a fast junior developer** — eager and quick, but missing the bigger picture
  - **Great at:** typing and coding very fast, boilerplate and repetitive code, recalling common syntax and APIs, offering a quick first draft
  - **Lacks:** your architectural requirements, functional and business context, the "why" behind the system, accountability for the result
  - You are the senior reviewer — guide it, review it, decide; the architecture, requirements and final judgement stay with you
- **Interactive Quiz 7** — Which scenario is NOT protected by Copilot's secret-blocking filter? *(Answer: C — a real API key you paste directly into Copilot Chat yourself)*
- **Interactive Quiz 8** — What fairness risk does training on public GitHub repositories introduce? *(Answer: B — training data over-represents certain demographics, embedding their assumptions)*
- **Interactive Quiz 9** — A developer accepts a Copilot suggestion with a security bug and commits it; who is accountable in production? *(Answer: C — the developer who accepted and committed it)*

---

## 🧪 Chapter 3 Exercises

- **[Exercise 301 — Who Does Copilot Picture?](../../../exercises/chapter-03/exercise-301/README.md)** — Let Copilot autocomplete names, pronouns and sample data, then spot and mitigate the stereotyped defaults it reaches for (Fairness).
- **[Exercise 302 — Infix/Postfix with Ask Mode](../../../exercises/chapter-03/exercise-302/README.md)** — Implement infix-to-postfix conversion and postfix evaluation with Ask mode, making Copilot explain both stacks before you accept anything (Reliability & Safety).
- **[Exercise 303 — Malicious Repo Prompt Trap](../../../exercises/chapter-03/exercise-303/README.md)** — Threat-model a hostile repository that tries to get an AI tool to run a repo-local setup command, and set the rule never to execute it unreviewed (Privacy & Security).
- **[Exercise 304 — Code Obfuscator MCP Tool](../../../exercises/chapter-03/exercise-304/README.md)** — Examine a seemingly harmless MCP "obfuscator" that asks for too much and can turn malicious after earning trust — keep a human in the approval loop (Privacy & Security).
- **[Exercise 305 — Tool Calls with Ollama](../../../exercises/chapter-03/exercise-305/README.md)** — Rebuild Exercise 104 against a local Ollama model, reusing `GetDate`/`GetTime`, and compare privacy, setup, speed and quality with the hosted version (Inclusiveness).

---

## 🔗 Resources & References
- [Microsoft Responsible AI principles](https://www.microsoft.com/en-us/ai/responsible-ai)
- [GitHub Copilot Trust Center](https://resources.github.com/copilot-trust-center/)
- [Content exclusions for GitHub Copilot](https://docs.github.com/en/copilot/managing-copilot/configuring-and-auditing-content-exclusion)
- [Excluding suggestions matching public code](https://docs.github.com/en/copilot/managing-copilot/managing-github-copilot-in-your-organization/setting-policies-for-copilot-in-your-organization/managing-policies-for-copilot-in-your-organization)
- [GitHub secret scanning & push protection](https://docs.github.com/en/code-security/secret-scanning/about-secret-scanning)
- [Ollama — run open models locally](https://ollama.com/)

---

## 🗒️ Facilitator Notes
- Frame the chapter around the **six principles in order** — each principle pairs an abstract definition with a concrete Copilot behaviour; keep that pairing explicit.
- Stress the recurring theme: Copilot learned from the crowd *and its mistakes* — quality and bias both flow from the training data (this ties Fairness, Reliability, and the public-code filter together).
- For the privacy slides, hammer the distinction between **training use** and **short-term retention**, and between **telemetry** and **sending full code** — these are commonly confused.
- The secret-blocking filter's blind spot (pasting secrets into Chat yourself) is the basis of Quiz 7 — demo or discuss it before running the quiz.
- Land the accountability message with the "fast junior developer" framing; Quiz 9 reinforces that the human who commits the code owns it.
- Exercises 303 and 304 are review/discussion-style security exercises — analyse the threat, do not execute the malicious flows.

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 2 — Meet Your New Best Friend: GitHub Copilot](../chapter-02/README.md) | [Chapter 4 — Let Your AI Co-Pilot Take the Wheel →](../chapter-04/README.md)
