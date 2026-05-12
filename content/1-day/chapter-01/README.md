[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)

---

# Chapter 1 — Welcome to the AI Revolution & Power with Purpose

> **Duration:** 90 minutes | Day 1, 09:00 – 10:30

Set the stage for the entire day. Participants leave with a clear mental model of what AI and LLMs actually are, where GitHub Copilot fits in the ecosystem, and — critically — how to use it responsibly, securely, and with sound professional judgment. Foundations first, hands-on tools from Chapter 2 onwards.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain the difference between traditional AI, Generative AI, and LLMs in plain language
- Describe how LLMs produce probabilistic output and why hallucination matters for code
- Position GitHub Copilot within the broader AI ecosystem and product landscape
- Describe what data Copilot sends to the model — and what GitHub commits to *not* retaining
- Apply the "public code" IP filter and explain the Stack Overflow analogy for responsible acceptance
- Identify the categories of security vulnerability AI commonly generates and how to catch them
- Calibrate trust in Copilot suggestions by scenario — and explain why code review still matters

---

## 📋 Content Outline

### 1. The AI Landscape — Where Copilot Fits (12 min)
- "AI" is not one thing — a quick taxonomy
  - **Rules-based → ML → deep learning → generative AI:** each wave coexists with the prior — your IDE's spell-check is rules-based, Netflix recommendations are ML, Copilot is generative AI; use this progression to anchor where we are today
  - **Narrow AI vs. AGI:** all tools in use today (including Copilot) are *narrow AI* — extraordinarily capable in a specific domain, zero common sense outside it; AGI remains theoretical; resist framing Copilot as something more general than it is
  - **Discriminative vs. generative AI:** discriminative models classify or predict from existing data ("is this spam?"); generative models produce *new content* — text, code, images; Copilot belongs firmly in the generative camp
  - **Large Language Models:** a specific type of generative AI trained on massive text/code corpora; the family that powers Copilot, ChatGPT, and Claude; the key differentiator is that they model language (including code) at scale
- Key milestones: from Transformers to Copilot today
  - **2017 — Transformer architecture** ("Attention Is All You Need"): the architectural breakthrough that made modern LLMs possible; all current Copilot models descend from this paper
  - **2021 — Codex & GitHub Copilot Technical Preview:** OpenAI Codex (GPT-3 fine-tuned on public GitHub code) made AI code completion practical; Copilot launched to limited beta
  - **2022–2024 — Copilot GA → multi-model era:** Copilot GA, Chat added, Business and Enterprise tiers launched; users now choose between GPT-4o, Claude, and Gemini
  - **2025–2026 — Agentic coding:** Agent Mode, MCP, Copilot Workspace; Copilot evolves from autocomplete to autonomous coding agent — participants today are getting ahead of this shift
- What GitHub Copilot *is* — and what it is *not*
  - **Core metaphor:** a knowledgeable colleague who types fast, has read every Stack Overflow answer, and is always available — but needs your direction and benefits from your review
  - **It suggests; you decide:** every completion is a *proposal*; the developer retains full authorship, accountability, and judgment; `git blame` still points to you
  - **What it is NOT:** not a search engine, not a guaranteed-correct answer, not an autonomous system that acts without approval; the moment you accept and commit, you own it
  - **Product landscape in brief:** IDE completions and Chat (VS Code, JetBrains, Visual Studio), Copilot on GitHub.com (PR summaries, browser Chat), the `gh copilot` CLI extension, and the emerging agentic layer (Workspace, Agent Mode, Extensions)
  - **Tiers in one sentence:** Free (2,000 completions/month), Individual ($10/mo, unlimited), Business ($19/user/mo, org controls + IP indemnity), Enterprise ($39/user/mo, adds Knowledge Bases and fine-tuning options)

### 2. How LLMs Actually Work (13 min)
- Tokens and next-token prediction — the only thing the model does
  - **Tokens, not words:** LLMs read *tokens* (roughly 3–4 characters on average); "GitHub Copilot" is 3–4 tokens; a 1,000-word file is ~1,300 tokens; understanding tokens explains why long files can exhaust context budgets
  - **Tokeniser demo:** use [platform.openai.com/tokenizer](https://platform.openai.com/tokenizer) to show how code is split — whitespace, punctuation, and camelCase boundaries all create separate tokens
  - **Next-token prediction:** the model's only job is to predict the single most plausible next token given all previous tokens; code generation is this loop running hundreds of times — there is no "understanding", only pattern completion
  - **Temperature & sampling:** the model outputs a probability distribution over possible next tokens; temperature controls randomness — low = deterministic, high = creative/variable; same prompt can yield different outputs across sessions
- Why LLMs "hallucinate" and what that means for code
  - **Definition:** the model generates output that is fluent and confident but factually wrong or invented — not because it is "lying" but because it is *completing a pattern*, not retrieving facts from a knowledge base
  - **Code-specific hallucination examples:** calling a method that doesn't exist on a library; generating code that compiles but does the wrong thing; inventing API signatures that look plausible; off-by-one errors that pass visual inspection
  - **Why this matters more in code than prose:** broken prose is obvious; broken code can silently pass review, run in production, and cause real incidents — the "looks correct" surface of AI-generated code is a genuine hazard
  - **Mitigation:** always run generated code; use `/explain` to verify you understand what the code does; treat Copilot output as a *first draft* requiring review, not a finished product
- Context windows: why "how much Copilot can see" matters
  - **Definition:** the context window is the maximum number of tokens the model can process per request — everything Copilot "reads" before generating must fit within this limit
  - **What fills the window:** the current file, open editor tabs, chat history, `#file` references, `copilot-instructions.md`, and Copilot's own system prompt all compete for the same budget
  - **Training cutoff:** the model's knowledge of libraries and APIs is frozen at its training date — it may not know about packages or language features released after that; always verify against current docs for anything recent
  - **Practical tip:** deliberately open the most relevant files in tabs; use `#file` to explicitly attach key files rather than hoping Copilot finds them on its own

### 3. Privacy & Data — What Copilot Sends and Stores (12 min)
- What is sent to the model: the prompt context
  - **The prompt is assembled locally first:** before any network call, the extension gathers context from the current file, open tabs, and custom instructions into a structured prompt — this prompt is what travels to the model, not a live stream of everything you type
  - **What's included:** code surrounding your cursor (above and below), imports and type definitions in scope, other open editor tabs (ranked by relevance), `copilot-instructions.md`, and any `#file` or `#selection` references you've attached
  - **Comments and variable names travel too:** avoid embedding sensitive values (customer names, internal hostnames, API keys) in comments even in "temporary" code — they become part of the prompt
  - **Chat prompts are sent verbatim:** everything you type into Copilot Chat — including pasted code snippets, error messages, or context — is sent as plain text; treat the Chat input like a message to an external service
- What is *not* retained: GitHub's data commitments
  - **Copilot Individual:** GitHub *may* use prompts and suggestions to improve models by default — opt out at [github.com/settings/copilot](https://github.com/settings/copilot) → "Allow GitHub to use my data for product improvements"
  - **Copilot Business:** no training on user code by default — prompts and suggestions are not used to train models; this is a contractual commitment, not a settings toggle
  - **Copilot Enterprise:** same no-training guarantee as Business, plus additional data residency options; prompts processed in Microsoft Azure infrastructure with standard enterprise data handling agreements
  - **Key message:** if your organisation is on Business or Enterprise, your code is not training the model — but it *is* transmitted to cloud infrastructure and should be treated accordingly
- Never paste secrets, PII, or sensitive data into Copilot Chat
  - **Why it matters:** the Chat prompt travels to an external model API; data you paste may be logged temporarily by GitHub's infrastructure — regardless of retention commitments
  - **Common mistakes:** pasting a `.env` file to ask "what's wrong with my config", sharing a database connection string to ask about query optimisation, including a real JWT in "can you decode this?"
  - **Safe alternatives:** replace real values with clearly fake placeholders (`DB_PASSWORD=REPLACE_ME`); use `#file` to reference config file *structure* without pasting contents; describe the secret type without including the value
  - **PII is also off-limits:** customer names, email addresses, health records, or financial data may violate GDPR, HIPAA, or your data handling policies regardless of Copilot's retention commitments
  - **Establish team norms:** include "no secrets or PII in Copilot Chat" in your AI usage policy as an explicit written rule, not an assumed convention

### 4. Intellectual Property & Legal Basics (10 min)
- The "public code" filter: how Copilot avoids reproducing verbatim licensed snippets
  - **What the filter does:** before returning a completion, Copilot checks whether the suggestion closely matches known public code; if a match above a similarity threshold is found, the suggestion is blocked and an alternative is offered
  - **Individual vs. Business/Enterprise:** Individual users must enable "Suggestions matching public code: Blocked" in GitHub Settings → Copilot; Business and Enterprise plans have this protection built in and cannot be weakened by individual users
  - **Limitations:** the filter targets near-exact verbatim reproduction — it does not flag code that is merely structurally similar; novel synthesis of patterns from multiple copyleft-licensed sources is not detected; no filter is a guarantee
  - **IP indemnity (Business/Enterprise):** if a copyright claim arises from a Copilot suggestion, GitHub will defend the customer — but only when the "public code: Blocked" filter is enabled; this is the legal backstop behind the technical control
- The ongoing legal landscape — what we know, what's still evolving
  - **Active litigation:** *Doe v. GitHub, Inc.* (class action alleging Copilot violates open-source licences by reproducing licensed code without attribution) is ongoing; no definitive ruling yet — the legal landscape is actively being written
  - **Jurisdiction variation:** the US, EU, and UK are each approaching training-data copyright differently; do not assume your jurisdiction mirrors another
  - **Practical takeaway:** the legal picture is unsettled — which is exactly why the public code filter and careful review practices matter; they reduce risk while courts catch up to the technology
  - **Facilitator guidance:** acknowledge uncertainty honestly; do not make legal claims; point participants toward their organisation's legal counsel for specific guidance
- Treat Copilot suggestions like code from Stack Overflow — understand it, don't blindly copy it
  - **The analogy:** most developers copy-paste from Stack Overflow with varying scrutiny; the accepted practice is to understand the code, verify it fits the context, and adapt it — Copilot suggestions deserve the same treatment
  - **Understand before accepting:** if you can't explain what a suggestion does line by line, don't accept it — use `/explain` first; accepting code you don't understand is a liability regardless of source
  - **The "junior developer" mental model:** treat Copilot like a capable junior who writes fast but doesn't know your codebase, business rules, or quality standards — every contribution needs a review
  - **Attribution in practice:** some organisations add `[AI-assisted]` tags or `Co-authored-by: GitHub Copilot` trailers to commits; check your organisation's policy; no universal standard exists yet

### 5. Security — Common Vulnerabilities & How to Catch Them (15 min)
- Common vulnerability categories AI tends to generate
  - **Why AI generates insecure code:** Copilot learns from public code — and public code contains a lot of vulnerable code; the model optimises for "looks correct and compiles", not "is secure"; security rarely appears as a clear signal in training examples the way syntax does
  - **SQL injection:** Copilot frequently generates string-concatenated queries (`"SELECT * FROM users WHERE id = " + userId`) rather than parameterised queries — especially when the surrounding code doesn't already use an ORM; always check that user input is never interpolated directly into SQL
  - **Path traversal:** file-system operations often lack path sanitisation — `fs.readFile(req.params.filename)` is a classic Copilot output that allows attackers to read arbitrary files; look for any file I/O using unsanitised user input
  - **Insecure defaults:** cryptographic code may use weak algorithms (MD5, SHA1 for passwords), insufficient entropy, or disabled SSL verification (`verify=False`); Copilot tends to use whatever was common in its training data, including a lot of legacy patterns
  - **Hardcoded credentials:** test and example code sometimes gets placeholder credentials that look real (`password = "admin123"`); these get committed, found by secret scanning, and occasionally reach production
  - **Incomplete error handling:** happy-path completions often omit error handling entirely, or handle it in ways that leak stack traces to end users
- GitHub Copilot Autofix: automatic vulnerability remediation in PRs
  - **What it does:** when CodeQL detects a vulnerability in a PR, Copilot Autofix automatically generates a suggested fix — a diff shown inline — that the author can review and apply with one click
  - **How it works:** CodeQL identifies the vulnerability type and location with precision; Copilot generates a contextually appropriate fix using the surrounding code as context — not a generic patch
  - **Supported categories:** SQL injection, XSS, path traversal, SSRF, command injection, insecure deserialisation, and more — the full CodeQL query suite
  - **Availability:** included for organisations with GitHub Advanced Security (GHAS) enabled; free for public open-source repositories
  - **The one-click workflow:** the Autofix suggestion appears in the PR's security alerts tab; review, test, and accept or dismiss — it never auto-merges
- Using `/fix` and `/explain` as a security first pass
  - **Copilot as a fast security reviewer:** ask "Review this function for security vulnerabilities" or "What could go wrong with this input handling?" in Chat — a useful first-pass before formal review
  - **`/explain` for security analysis:** select a suspicious function, `/explain` it for a plain-English walkthrough, then follow up "Could an attacker exploit any of this?" — Copilot often identifies injection points and missing validation when prompted directly
  - **`/fix` on known-bad code:** paste a vulnerable snippet and ask `/fix` — useful as a training exercise (show before/after) and when you've identified a bug and want a suggested remediation to evaluate
  - **Limitations:** Copilot has no data-flow analysis, no taint tracking, and no understanding of your threat model; treat it as a "fast informal first opinion", not a replacement for CodeQL or dedicated SAST tools
  - **Defence in depth:** Copilot Chat for exploratory review during development + CodeQL for rigorous automated scanning — each catches what the other misses

### 6. Trust & Judgment — Using Copilot Like a Professional (13 min)
- High-confidence scenarios: where Copilot is reliable
  - **Boilerplate and scaffolding:** CRUD endpoint structure, class constructors, interface implementations, config file templates, standard middleware setup — Copilot has seen these patterns millions of times; accept with light review
  - **Well-known algorithms:** sorting, searching, string manipulation, date arithmetic, standard data structure operations — heavily represented in training data; verify edge cases, but the logic is likely sound
  - **Test scaffolding:** generating the structure of a test file, `describe`/`it` blocks, mock setup boilerplate, `beforeEach`/`afterEach` hooks — reliable; the test *assertions* still need your judgment
  - **Translating between languages or patterns:** converting Python to TypeScript, converting callbacks to async/await, rewriting SQL as ORM calls — well-defined transformations where correctness is verifiable by inspection
- Lower-confidence scenarios: where to be sceptical
  - **Business logic:** code encoding your organisation's specific rules — pricing calculations, eligibility criteria, compliance checks, approval workflows — Copilot has no knowledge of these and will generate plausible-looking but potentially wrong implementations; validate against the actual requirements
  - **Security-sensitive code:** authentication, authorisation, session management, cryptography, input sanitisation — any code where a mistake creates a *vulnerability* rather than just a bug; treat every Copilot suggestion here with the scrutiny you'd give an untrusted contributor
  - **Domain-specific algorithms:** actuarial calculations, physics simulations, financial models — Copilot's training data may have very few correct examples; it will generate something that looks plausible but may be subtly wrong in non-obvious ways
  - **Integrations with internal APIs:** Copilot doesn't know your internal service interfaces, conventions, or undocumented behaviours — generated integration code will use the *shape* of public APIs it has seen, not your actual contract
- The "trust but verify" mindset
  - **Every suggestion is a proposal:** receiving a Copilot suggestion should trigger the same mental process as receiving a PR — "does this look right? Do I understand it? Does it fit the context? Are there edge cases it missed?" — not "great, accepted"
  - **Verification proportional to risk:** light review for boilerplate; thorough review for business logic; rigorous review *plus* testing for security code
  - **The "one-click trap":** the convenience of `Tab` to accept can erode the critical thinking that would accompany typing the same code; accept when you understand and agree, not simply because the suggestion appeared
  - **Use the tools available:** `/explain` before accepting unfamiliar code; run tests after accepting; commit-by-commit review in your PR
- Code review still matters — arguably more so with AI
  - **The review bottleneck shifts:** with AI assistance, code is written faster — which means *more code reaches review*, not less; rigorous review skills become more critical, not less
  - **New review questions:** in addition to standard criteria, ask: does the author understand this code? Are there signs of uncritical AI acceptance — inconsistent style, variable names that don't fit the codebase, unnecessary complexity?
  - **Copilot can assist reviewers too:** use `@workspace` to quickly understand unfamiliar code, `/explain` on a suspicious function, or ask "what edge cases does this code miss?"
  - **Team norms to establish now:** explicit agreement on what "reviewed" means when AI was used to generate code; these conversations should happen proactively, before an incident forces them

---

## 💡 Ideas for Exercises & Interactivity

### Opening Poll (5 min — run during intro)
Use Mentimeter or Slido:
- "Have you used GitHub Copilot before?" (Yes / No / Tried it briefly)
- "What do you think Copilot is doing when it suggests code?" (multiple choice — next-token prediction, web search, database lookup, magic)
- "How worried are you about AI replacing your job?" (scale 1–5) — revisit at end of day

### Token Visualiser (5 min — during LLM section)
Direct participants to [platform.openai.com/tokenizer](https://platform.openai.com/tokenizer). Paste a code snippet and count tokens together. Builds intuition for context windows.

### Poll: Privacy Comfort Levels (5 min — during Privacy section)
Anonymous Mentimeter poll: "Which of these would you be comfortable pasting into Copilot Chat?" (Code with PII / internal API URLs / hardcoded passwords / customer data samples). Discuss results — no wrong answers, but surfaces assumptions.

### Exercise: Spot the Vulnerability (15 min — after Security section)
Provide 4–5 short code snippets — some safe, some with common vulnerabilities (SQL injection, hardcoded API key, path traversal). Participants decide: accept or reject? Use Copilot Chat `/fix` and `/explain` to analyse each one. Debrief: did Copilot catch what you missed?

### Scenario Discussion: "Should I use Copilot here?" (10 min — closing discussion)
Small groups debate three scenarios:
1. Writing a GDPR-compliant data anonymisation function
2. Adding a login form with OAuth
3. Implementing a novel proprietary pricing algorithm

Groups discuss: should they use Copilot? With what safeguards? Share back. Perfect bridge into Chapter 2's hands-on work.

---

## 🔗 Resources & References
- [GitHub Copilot Docs](https://docs.github.com/en/copilot)
- [GitHub Copilot Privacy Statement](https://docs.github.com/en/site-policy/privacy-policies/github-general-privacy-statement)
- [Copilot for Business — data handling](https://docs.github.com/en/copilot/overview-of-github-copilot/about-github-copilot-for-business)
- [Content exclusions for GitHub Copilot](https://docs.github.com/en/copilot/managing-copilot/configuring-and-auditing-content-exclusion)
- [GitHub Copilot Autofix](https://docs.github.com/en/code-security/code-scanning/managing-code-scanning-alerts/about-autofix-for-codeql-code-scanning)
- [GitHub blog: Research on Copilot productivity](https://github.blog/2022-09-07-research-quantifying-github-copilots-impact-on-developer-productivity-and-happiness/)
- [Microsoft Responsible AI principles](https://www.microsoft.com/en-us/ai/responsible-ai)

---

## 🗒️ Facilitator Notes
- Keep AI theory tight — surface-level intuition is enough; don't get lost in ML depth
- The responsible use content often sparks strong opinions — welcome debate, keep it productive
- Emphasise: responsible use is a *competitive advantage*, not a constraint
- The legal landscape is evolving — acknowledge uncertainty rather than making absolute claims
- The "Spot the Vulnerability" exercise is the most memorable part of the chapter — make sure it fits
- Have a printed one-pager of "Copilot Do's and Don'ts" to hand out as participants leave for the break

---

[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)
