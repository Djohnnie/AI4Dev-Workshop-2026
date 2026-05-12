[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)

---

# Chapter 1— Welcome to the AI Revolution!

> **Duration:** 90 minutes | Day 1, 09:00 – 10:30

Set the stage for the entire workshop. Participants leave with a clear mental model of AI, Generative AI, and Large Language Models — and understand exactly where GitHub Copilot fits in the modern developer's world.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain the difference between traditional AI, Generative AI, and LLMs in plain language
- Describe how LLMs are trained and why they produce probabilistic output
- Position GitHub Copilot within the broader AI ecosystem
- Articulate what GitHub Copilot *is* — and what it is *not*
- Feel excited and curious rather than intimidated about AI-assisted development

---

## 📋 Content Outline

### 1. The AI Landscape — A 10-Minute History (10 min)
- From rules-based systems → machine learning → deep learning → generative AI
  - **Rules-based / expert systems:** hand-coded if/then logic; fast and predictable but brittle — any scenario not explicitly programmed breaks the system
  - **Machine learning:** models that *learn patterns from data* rather than following explicit rules; examples include spam filters, recommendation engines, fraud detection
  - **Deep learning:** neural networks with many layers, made practical by GPUs and large datasets; breakthrough in image recognition (ImageNet 2012), then NLP
  - **Generative AI:** models that produce *new content* (text, code, images, audio) rather than just classifying or predicting; a qualitative shift from prior AI — the model creates rather than labels
  - Key point: each wave didn't replace the previous — they coexist; your IDE's spell-check is still rules-based; Netflix recommendations are ML; Copilot is generative AI
- Key milestones: ImageNet, GPT series, Codex, GitHub Copilot (2021 → today)
  - **2012 — AlexNet / ImageNet:** deep learning proved viable at scale; error rates halved overnight; the moment the research community took neural networks seriously
  - **2017 — Transformer architecture** ("Attention Is All You Need", Vaswani et al.): the architectural foundation for all modern LLMs; replaced recurrent networks with self-attention
  - **2018–2020 — GPT-1, GPT-2, GPT-3:** scaling transformers on internet text produced surprisingly capable language models; GPT-3 (175B parameters) shocked researchers with emergent capabilities
  - **2021 — Codex & GitHub Copilot Technical Preview:** OpenAI Codex (GPT-3 fine-tuned on public GitHub code) became the first model to make AI code completion practical; Copilot launched to limited beta
  - **2022 — Copilot GA + ChatGPT:** Copilot became generally available; ChatGPT (Nov 2022) brought generative AI to mainstream public consciousness; 100M users in 2 months
  - **2023–2024 — Multi-model era:** GPT-4, Claude, Gemini; Copilot Chat added; Copilot Business and Enterprise tiers; GitHub Copilot opens to multiple model providers
  - **2025–2026 — Agentic coding:** Agent Mode, MCP, Copilot Workspace; Copilot evolves from autocomplete tool to autonomous coding agent
- "AI" is not one thing — a quick taxonomy
  - **Narrow AI vs. AGI:** all tools in use today (including Copilot) are *narrow AI* — extraordinarily capable in a specific domain, zero common sense outside it; AGI (general human-level AI) remains theoretical
  - **Discriminative AI:** models that classify or predict from existing data — "is this a cat?", "will this customer churn?"; most traditional ML falls here
  - **Generative AI:** models that produce novel outputs — text, code, images, audio, video; trained to understand and recreate patterns, not just recognise them
  - **Large Language Models (LLMs):** a specific type of generative AI trained on massive text/code corpora; the family that powers Copilot, ChatGPT, Claude, Gemini
  - **Other AI categories to name-check:** Computer Vision, Speech Recognition, Recommender Systems, Reinforcement Learning — participants have used all of these without realising it
  - **Where Copilot fits:** an LLM-based developer tool — generative AI, specifically a code-focused LLM accessed via IDE, browser, and CLI interfaces

### 2. How LLMs Actually Work (20 min)
- Tokens, embeddings, and next-token prediction — explained visually
  - **Tokens:** LLMs don't read characters or whole words — they read *tokens* (roughly 3–4 characters on average); "GitHub Copilot" is 3–4 tokens; a 1,000-word file is ~1,300 tokens
  - **Tokenisation demo:** use [platform.openai.com/tokenizer](https://platform.openai.com/tokenizer) to show how code is split; highlight that whitespace, punctuation, and camelCase boundaries all create separate tokens
  - **Embeddings:** each token is mapped to a high-dimensional vector (e.g., 12,288 numbers); tokens with similar meaning cluster together in this vector space — the model "understands" relationships between concepts mathematically
  - **Next-token prediction:** the model's only job is to predict *the single most plausible next token* given all previous tokens; code generation is this loop running hundreds of times
  - **Temperature & sampling:** the model outputs a probability distribution over all possible next tokens; temperature controls randomness — low = deterministic/predictable, high = creative/variable; same prompt can yield different outputs
  - **Visual suggestion:** draw the loop on a whiteboard: `[token 1][token 2]...[token N] → predict token N+1 → append → repeat`
- Why LLMs "hallucinate" and what that means for code generation
  - **Definition:** the model generates output that is fluent and confident but factually wrong, invented, or logically broken — not because it is "lying" but because it is *completing a pattern*, not retrieving facts
  - **Root cause:** there is no "knowledge database" being queried; the model learned statistical associations from training data and reproduces the most plausible-looking continuation
  - **Code-specific hallucination examples:**
    - Calling a method that doesn't exist on a library object (e.g., `array.flattenDeep()` in a language that doesn't have it)
    - Generating a correct-looking but subtly wrong algorithm (off-by-one, wrong edge case)
    - Inventing API signatures, parameter names, or return types that look plausible
    - Producing code that compiles but does the wrong thing
  - **Why this matters more in code than in prose:** broken prose is obvious; broken code can silently pass review, run in production, and cause real incidents
  - **Mitigation:** always run generated code; write tests before accepting; use `/explain` to verify you understand what the code does; treat Copilot output as a *first draft*, not a finished product
- The training pipeline: pre-training on public code, fine-tuning for code tasks
  - **Pre-training:** the base model is trained on billions of tokens of public code from GitHub repos, documentation, Stack Overflow, technical books — it learns syntax, idioms, API patterns, and documentation styles across hundreds of languages
  - **What "pre-trained" means practically:** Copilot has seen enormous quantities of React, Python, Rust, SQL, shell scripts — it has strong statistical intuitions about what "looks right" in each
  - **Fine-tuning:** the pre-trained model is further trained on curated code-completion examples to improve task-specific performance (completing functions, fixing bugs, writing tests)
  - **RLHF (Reinforcement Learning from Human Feedback):** human raters evaluate model outputs; the model is trained to prefer outputs that humans rated highly — this is how "helpfulness" and "safety" are shaped
  - **Training cutoff:** the model's knowledge of libraries, APIs, and frameworks is frozen at its training cutoff date; it will not know about a package released after that date — always verify against current docs
  - **GitHub-specific fine-tuning:** GitHub's models are specifically tuned on code-completion tasks at scale, giving them an advantage over general-purpose LLMs for programming tasks
- Context windows: why "how much Copilot can see" matters
  - **Definition:** the context window is the maximum number of tokens the model can process in a single inference call — everything Copilot "reads" before generating a suggestion must fit within this limit
  - **Practical sizes:** models in 2025–2026 range from ~8K tokens (older/faster models) to 200K+ tokens (long-context models); a 200K token window can hold roughly 10,000–15,000 lines of code
  - **What fills the context window:** open editor tabs, the current file, chat history, `#file` and `#selection` references, `copilot-instructions.md`, and Copilot's own system prompt all compete for space
  - **When context is too small:** Copilot can't "see" the whole codebase simultaneously; it only knows what fits in the window — distant files, past conversations, and unused tabs are invisible to it
  - **The "lost in the middle" problem:** research shows LLMs pay less attention to content in the *middle* of a long context; place the most critical information near the beginning or end of your prompt
  - **Practical tip:** deliberately open the most relevant files in tabs before asking for help; use `#file` to explicitly attach key files rather than hoping Copilot finds them on its own

### 3. GitHub Copilot — The Big Picture (20 min)
- What is GitHub Copilot? (AI pair programmer, not an autonomous agent)
  - **Core metaphor:** Copilot is a knowledgeable colleague who types fast, has read every Stack Overflow answer, and is always available — but needs your direction and benefits from your review
  - **It suggests; you decide:** every completion is a *proposal*; the developer retains full authorship, accountability, and judgment about whether to accept, modify, or reject
  - **What it is NOT:** not a search engine, not a documentation lookup tool, not a guaranteed correct answer, not an autonomous system that acts without your approval
  - **The "pair programmer" dynamic:** like a good pair-programming partner, Copilot proposes ideas, catches patterns, fills in boilerplate — and occasionally goes down the wrong path that you redirect
  - **Accountability stays with you:** the moment you accept a suggestion and commit it, you own it — Copilot doesn't appear in `git blame`
- History: GitHub + OpenAI Codex → now powered by multiple models (GPT-4o, Claude, Gemini, o3)
  - **2021:** GitHub Copilot Technical Preview launched, exclusively powered by OpenAI Codex (GPT-3 fine-tuned on public GitHub code); limited beta access
  - **June 2022:** General Availability — Copilot Individual at $10/month; became the fastest-adopted GitHub product ever
  - **2023:** Copilot Chat added (conversational AI in the IDE); Copilot for Business launched with org-level controls; Copilot starts appearing on GitHub.com
  - **2024:** Multi-model support introduced — users can choose GPT-4o, Claude Sonnet, Gemini; Copilot Enterprise with Knowledge Bases; Agent Mode preview
  - **2025–2026:** Copilot is now a *platform* — MCP integration, Extensions ecosystem, Copilot Workspace, and a growing agentic capability set
  - **Implication for today:** Copilot is no longer "one model" — it's a suite of AI capabilities across the entire GitHub platform, pluggable and extensible
- GitHub Copilot product landscape:
  - **Copilot in the IDE (completions + Chat):** the original product — ghost text completions as you type, plus a Chat panel for conversational assistance, inline chat for in-place edits, and Agent Mode for autonomous multi-step tasks; available in VS Code, JetBrains, Visual Studio, Neovim, and more
  - **Copilot on GitHub.com (PR summaries, Copilot Chat in browser):** Chat available directly on github.com; automatic PR description generation from diffs; AI-powered code review with inline suggestions; Copilot-powered issue summarisation and triage
  - **Copilot CLI:** the `gh copilot` extension for the GitHub CLI; `suggest` generates shell commands from natural language, `explain` demystifies cryptic commands; works in bash, zsh, PowerShell, fish
  - **Copilot Workspace (agentic task completion):** starts from a GitHub Issue and autonomously plans, writes code, runs tests, and opens a PR; preview feature representing the agentic future of Copilot
  - **Copilot Extensions & MCP:** first- and third-party extensions that add new tools to Copilot Chat (e.g., Jira, Sentry, internal APIs); MCP (Model Context Protocol) is an open standard for connecting AI models to external data sources and tools
- Tiers: Copilot Free, Individual, Business, Enterprise
  - **Copilot Free:** available to all GitHub users — 2,000 code completions/month, 50 chat messages/month, access to GPT-4o and Claude Sonnet 3.5; great for experimentation, limited for daily use
  - **Copilot Individual ($10/month):** unlimited completions and chat messages, all available models, no training on your code (opt-out), personalised suggestions based on your coding patterns
  - **Copilot Business ($19/user/month):** everything in Individual plus: organisation-level policy management, content exclusions, audit logs, no training on code by default, IP indemnity, usage metrics API
  - **Copilot Enterprise ($39/user/month):** everything in Business plus: Copilot Knowledge Bases (RAG over internal docs/repos), organisation-level custom instructions, Copilot pull request summaries at scale, fine-tuned model options, Copilot Workspace access

### 4. AI is Changing How We Build Software (15 min)
- Developer productivity data (GitHub's research: 55% faster task completion)
  - **The 2022 GitHub study:** developers using Copilot completed a representative coding task 55% faster than the control group; this was a controlled experiment, not a survey
  - **The 2023 follow-up:** 88% of Copilot users reported feeling more focused; 74% said it helped them stay in the flow state longer by reducing context switches to documentation/search
  - **McKinsey research (2023):** estimated up to 45% of developer time is spent on tasks where AI can provide meaningful assistance (boilerplate, testing, documentation, code review)
  - **Important caveats:** gains vary significantly by task type (strongest for boilerplate, weakest for novel business logic), developer experience level, and domain; raw "lines of code" is a poor proxy for productivity
  - **The flip side:** some studies show increased time spent on *review* of AI suggestions — the net benefit depends on how critically developers evaluate output
- The shift from *writing* code to *reviewing and directing* code
  - **The emerging role:** developers increasingly act as "code directors" — specifying intent, evaluating proposals, integrating outputs — rather than typing every character themselves
  - **Natural language as a programming interface:** the ability to describe *what* you want in plain English and evaluate whether you got it is becoming as important as knowing *how* to write it
  - **Analogy:** senior developers have always directed junior developers this way — AI makes that leverage available to everyone, at every experience level
  - **What this requires:** you must understand the code well enough to judge whether Copilot's output is correct — reading and evaluating code is a distinct skill from writing it, and arguably harder
  - **Risk:** developers who accept suggestions without understanding them accumulate invisible technical debt and security vulnerabilities; critical evaluation is non-negotiable
- New skills that matter more, skills that matter less
  - **More important:** system design and architecture thinking; code review and critical evaluation; prompt crafting and AI communication; domain knowledge and business logic; debugging complex distributed systems; security awareness; requirements decomposition
  - **Less central (but still needed):** memorising language syntax and library APIs; writing boilerplate scaffolding; generating first drafts of unit tests and documentation; looking up common algorithmic patterns
  - **The rise of "AI literacy":** understanding what AI can and can't do, when to trust it, how to guide it, and when to override it is becoming a core engineering competency — not a speciality
  - **Soft skills amplified:** clear written communication, requirements definition, and problem decomposition — historically undervalued in engineering — become differentiators in an AI-assisted world
- The "centaur" model: human + AI, not human vs. AI
  - **Origin of the analogy:** after chess computers surpassed grandmasters, "freestyle chess" tournaments allowed human + computer teams — they consistently beat both pure humans *and* pure computers; the combination was stronger than either alone
  - **Applied to software development:** human judgment (context, ethics, requirements, business knowledge, creativity) + AI speed (pattern matching, boilerplate, recall of APIs) = better outcomes than either alone
  - **What the human brings that AI cannot:** understanding of *why* this software exists, who uses it, what failure looks like, what the organisation needs, and what the right tradeoff is
  - **Why "AI will replace developers" misses the point:** the centaur model suggests the developers who thrive will be those who learn to work *with* AI effectively — the threat is not AI replacing developers, it's AI-fluent developers replacing AI-unfluent developers
  - **Reframe for participants:** this workshop is about becoming the human half of a very capable centaur

### 5. Workshop Roadmap & Setup (10 min)
- Walk through the 2-day agenda
  - **Day 1 arc:** foundations (Ch 1) → hands-on Copilot in the IDE (Ch 2) → responsible and secure use (Ch 3) → advanced features and agentic coding (Ch 4)
  - **Day 2 arc:** prompt engineering mastery (Ch 5) → Copilot across the full SDLC (Ch 6) → team practices and measuring impact (Ch 7) → capstone build (Ch 8)
  - **How chapters build on each other:** each session assumes the prior one; participants who miss a session should review the README before the next
  - **What participants will be able to do by end of Day 2:** complete a full feature cycle (design → code → test → document → PR) using Copilot at every step; apply prompt engineering; configure Copilot for their team
- Confirm GitHub Copilot access and IDE setup (VS Code recommended)
  - **GitHub account:** must be signed in to github.com; Copilot Free is available to all accounts — verify at [github.com/features/copilot](https://github.com/features/copilot)
  - **VS Code setup:** install the *GitHub Copilot* and *GitHub Copilot Chat* extensions; sign in with GitHub account; look for the Copilot icon in the status bar (bottom right)
  - **JetBrains alternative:** GitHub Copilot plugin available for IntelliJ IDEA, PyCharm, GoLand, WebStorm, etc. — install via Plugins marketplace and sign in; same features, slightly different keyboard shortcuts
  - **GitHub Codespaces fallback:** if local setup fails, participants can use a pre-configured Codespace — Copilot works in the browser-based VS Code; provide the Codespace URL in advance
  - **Internet connectivity check:** Copilot requires outbound HTTPS to `copilot-proxy.githubusercontent.com`; corporate proxies/firewalls are the #1 setup issue — verify before the session starts
- Point to prerequisites and troubleshooting guide
  - **Prerequisites checklist:** GitHub account, Copilot access confirmed, VS Code or JetBrains installed, extensions installed and signed in, `git` installed, language runtime for the workshop exercises (Node.js recommended)
  - **Common setup issues to pre-empt:** extension shows error state (re-sign-in usually fixes it), proxy blocking Copilot requests (IT needs to whitelist the domain), free tier quota exhausted (upgrade or use Codespaces), JetBrains plugin not activating (restart IDE after install)
  - **Verification test:** type a comment in a new file (`// function that returns the current date`) — if Copilot is working, ghost text should appear within a second or two
  - **Where to get help during the workshop:** Slack/Teams channel for the workshop (post the link), or raise your hand — facilitators will circulate during exercises

---

## 💡 Ideas for Exercises & Interactivity

### Opening Quiz (5 min — run during intro)
Use Mentimeter or Slido for live polling:
- "Have you used GitHub Copilot before?" (Yes / No / Tried it briefly)
- "What do you think Copilot is actually doing when it suggests code?" (multiple choice)
- "How worried are you about AI replacing your job?" (scale 1–5)
Revisit the last question at the end of Day 2.

### LLM Vocabulary Bingo (5 min)
Give each participant a bingo card with AI buzzwords (token, hallucination, fine-tuning, context window, RAG, embedding, prompt, model, inference, agent…). Mark them off as they come up during the chapter.

### "Is This AI?" Gallery Walk (10 min — optional, good for larger groups)
Post printed examples around the room: autocomplete in Gmail, Copilot suggestion, Google Translate, chess engine. Teams discuss and vote: "Is this Generative AI?"

### Live Demo: Copilot's First Impression (10 min)
Instructor opens a blank file, types a comment describing a simple function, and lets Copilot complete it. Then deliberately gives a vague prompt to show a hallucination. Debrief: what did we learn?

### Token Visualizer Exercise (5 min)
Direct participants to [platform.openai.com/tokenizer](https://platform.openai.com/tokenizer). Paste in a snippet of code and count tokens together. Builds intuition for context windows.

---

## 🔗 Resources & References
- [GitHub Copilot Docs](https://docs.github.com/en/copilot)
- [GitHub Blog: Research on Copilot productivity](https://github.blog/2022-09-07-research-quantifying-github-copilots-impact-on-developer-productivity-and-happiness/)
- [How GitHub Copilot is getting better at understanding your code](https://github.blog/2023-05-17-how-github-copilot-is-getting-better-at-understanding-your-code/)
- [GitHub Copilot model choice](https://docs.github.com/en/copilot/using-github-copilot/ai-models/changing-the-ai-model-for-copilot-chat)

---

## 🗒️ Facilitator Notes
- Keep energy high — this is the first session after arrival
- Don't get lost in deep ML theory; surface-level intuition is enough
- Emphasize: Copilot is a *tool*, not a replacement. The theme of the whole workshop.
- Bring backup: have the demo pre-recorded in case of internet issues

---

[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)