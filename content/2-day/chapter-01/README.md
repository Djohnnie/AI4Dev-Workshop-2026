[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)

---

# Chapter 1— Welcome to the AI Revolution!

> **Duration:** 90 minutes | Day 1, 09:00 – 10:30

Set the stage for the entire workshop. Starting from a few live polls, participants build a clear mental model of how AI evolved, the different flavours of machine learning, and how Generative AI and Large Language Models actually work — then put that theory into practice across six hands-on exercises that grow a simple chat app into a multi-agent system.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Trace the evolution of AI from rules-based systems through machine learning, deep learning, and generative AI
- Distinguish supervised, unsupervised, and reinforcement learning, and explain the adversarial idea behind GANs and RLHF
- Place AI Models, Generative AI, and LLMs in a clear taxonomy — and explain why every LLM is a GenAI model but not vice versa
- Describe how LLMs work under the hood: tokens, embeddings, next-token prediction, and `temperature`
- Explain why LLMs hallucinate, what a context window is, and how RAG grounds a model with external knowledge
- Understand how tool calls, MCP, and agent orchestration extend an LLM beyond a stateless chatbot

---

## 📋 Content Outline

### 1. Opening — Live Polls
- Three interactive polls open the workshop and gauge the room:
  - **Poll A — What AI tools are you using?** (answering questions, writing text, generating images, coding, …)
  - **Poll B — How happy are you with GenAI-related tools?** focused on coding tools like GitHub Copilot or Claude Code
  - **Poll C — How do you feel about the impact GenAI may have on your job?** — deliberately polarizing, with nuances discussed afterwards

### 2. The Evolution of AI
- Four waves, each building on the last:
  1. **Rules-Based Systems** — hand-coded if/then logic; fast and predictable but brittle, breaking on any unplanned scenario
  2. **Machine Learning** — learns patterns from data; powers spam filters, recommendation engines, and fraud detection
  3. **Deep Learning** — neural networks with many layers, made practical by GPUs; the breakthrough behind ImageNet 2012
  4. **Generative AI** — produces new content (text, code, images); the model creates rather than classifies — Copilot lives here

### 3. Types of Machine Learning
- **Supervised Learning** — trains on labelled examples (input → correct output); spam detection, image classification, house-price prediction; needs a large labelled dataset
- **Unsupervised Learning** — finds hidden patterns in unlabelled data; customer segmentation, anomaly detection, topic modelling; needs data only, no labels
- **Reinforcement Learning** — an agent learns by trial and error via rewards; game-playing AI (AlphaGo), robot locomotion, RLHF for LLMs; needs an environment and a reward signal

### 4. The Reinforcement Learning Loop
- The cycle of **Agent → Environment → Interpreter → Model Improvement**: the agent chooses actions from its policy, the environment reacts and produces a new state, the interpreter evaluates the outcome and assigns a reward or penalty, and the reward updates the model weights
- Over time the agent gradually learns better strategies

### 5. Generative AI — The GAN Idea
- A **Generative Adversarial Network** pits two neural networks against each other: a *generator* tries to fool a *discriminator*, and the discriminator tries not to be fooled
- The generator starts from random noise and learns to produce realistic images; the discriminator, trained on real photos, judges whether each result looks real and returns a reward or penalty
- Over thousands of rounds the generator's output becomes indistinguishable from real — the same adversarial principle behind modern image generation and behind **RLHF** for aligning language models

### 6. AI Is Not One Thing — A Quick Taxonomy
- **Narrow AI vs. AGI** — everything in use today (including Copilot) is narrow AI; AGI remains theoretical
- **Discriminative AI** — classifies or predicts from existing data (spam detection, churn prediction, most traditional ML)
- **Generative AI** — produces new content: text, code, images, audio
- **Large Language Models (LLMs)** — generative AI trained on text and code; powers Copilot, ChatGPT, Claude, Gemini
- **Other categories** — Computer Vision, OCR, Speech Recognition, Recommender Systems — all already in everyday use
- **Where Copilot fits** — LLM-based generative AI, a code-focused model accessed via IDE, browser, and CLI

### 7. What Is an AI Model?
- An **AI Model** is a trained mathematical system that maps inputs to outputs using patterns learned from data (spam filter, recommender, fraud detector, OCR)
- A **GenAI Model** is built to generate new content rather than only classify or score — outputs include text, code, images, audio, video, and design variations
- An **LLM** is a generative AI model focused on language (text and code) — chat, summarization, search assistants, code copilots
- The nesting: every LLM is a GenAI model and an AI model, but not every AI model is an LLM

### 8. How LLMs Actually Work
- **Tokens** — LLMs read tokens, not characters or words (roughly 3–4 chars each; ~1,300 tokens per 1,000 words)
- **Tokenisation** — whitespace, punctuation, and camelCase boundaries all create separate tokens
- **Embeddings** — each token maps to a high-dimensional vector; similar meanings cluster together mathematically
- **Next-Token Prediction** — the model's only job is predicting the next token; code generation runs this loop hundreds of times
- **Temperature & Sampling** — low is deterministic, high is creative; the same prompt can yield different results

### 9. LLM Temperature — Choosing the Right Setting
- **0.0 – 0.3 — Factual & Analytical** — best for coding, data extraction, math, technical docs, customer support; consistent and deterministic (still a next-word predictor — always verify)
- **0.3 – 0.7 — Standard Conversational** — best for general chat, summarization, translation, business writing; natural and coherent
- **0.7 – 1.0+ — Creative** — best for brainstorming, creative writing, and poetry; diverse and imaginative
- ⚠ **Past 1.0** the model may produce gibberish or lose context

### 10. Hands-On — First Exercises (101–103)
- **Exercise 101 — Token Visualizer:** configure an Azure OpenAI endpoint, run the terminal app, and compare token counts for English, C#, SQL, and Copilot prompts
- **Exercise 102 — Stateless LLM Chat:** build a .NET terminal chat app with the Microsoft Agent Framework SDK that omits history, showing the LLM is stateless by default
- **Exercise 103 — Chat History & Roles:** extend the app to store turns and separate user, assistant, and system roles, illustrating how apps add memory

### 11. Why LLMs for Coding?
- **Programming languages are languages** — syntax, grammar, and semantics are exactly what LLMs are built on
- **Code is pattern-dense** — CRUD endpoints, design patterns, and boilerplate repeat across millions of repos
- **Trained on vast public codebases** — GitHub, Stack Overflow, and docs offer highly structured examples
- **Intent lives in the code** — function names, variable names, and comments describe what code should do
- **Context is bidirectional** — code above and below the cursor both constrain the next token
- **Errors have structure too** — stack traces and compiler messages follow predictable patterns

### 12. Why LLMs "Hallucinate"
- LLMs predict statistically likely tokens, they don't recall facts — there is no internal fact-checker
- Training data contains noise (outdated APIs, deprecated packages, wrong answers), and the more specific or obscure the request, the riskier the output
- The context window is the only truth; outside it the model fills gaps with learned patterns
- Hallucinations feel trustworthy because the model is confidently wrong — always review what it generates

### 13. The Training Pipeline
- **Pre-training on raw text** — billions of tokens from web, books, and code teach language structure
- **Self-supervised learning** — no labels needed; predicting the next token is the signal, and the text itself is the label
- **Fine-tuning on curated data** — adapts the base model to follow instructions and write useful code
- **RLHF** — human raters rank outputs, teaching the model what "helpful and correct" looks like
- **The knowledge cutoff** — anything after the fixed training date is unknown
- **Scale changes everything** — more parameters, data, and compute unlock emergent abilities

### 14. Context Windows
- The model's working memory — everything it can see at once: prompt, history, and injected context
- Measured in tokens, not characters (a word is ~1.3 tokens; code is more token-dense)
- What goes in the window matters; relevant files and clear instructions beat vague prompts
- Nothing persists between sessions — every new chat starts blank
- Bigger isn't always better — models can lose focus on content buried in the middle
- GitHub Copilot manages context automatically, selecting open files, cursor position, and related code

### 15. RAG — Grounding an LLM
- **Retrieval-Augmented Generation** grounds a model with external knowledge at runtime so it answers from evidence rather than memory alone:
  1. **Retrieve Evidence** — find the most relevant facts from an external knowledge source
  2. **Ground the Prompt** — insert that evidence into the prompt and context window
  3. **Generate** — answer using both general training and the retrieved grounding
- **Still limited** — RAG adds context at inference time; it does not retrain the model, fetch live state itself, or perform external actions — which motivates the next topic: tools

### 16. Hands-On — Tools & MCP (104–105)
- **Exercise 104 — Tool Calls:** build on 103, expose `getDateTime` as an in-process function call, and watch tools give the LLM access to real-time data
- **Exercise 105 — MCP Tool Calls:** move `getDate` and `getTime` out-of-process behind an MCP server and wire the chat app to it, understanding MCP as a reusable tool boundary

### 17. Beyond Chatbots — What Makes an AI Agent?
- **Autonomy & Action** — takes initiative and performs tasks, not just responds
- **Memory & State** — maintains history and context across interactions
- **Tool Integration** — uses APIs, functions, and external systems to get things done
- **Planning & Reasoning** — makes decisions, chains actions, solves multi-step problems
- **Reactive vs. Proactive** — chatbots wait for input; agents can trigger actions on conditions

### 18. Agent Orchestration — Coordinating Intelligence
- **Multi-Agent Systems** — specialized agents working together, each with its own expertise
- **Workflow Automation** — agents pass work between each other to form pipelines
- **Delegation & Specialization** — the right agent for the right job (code, data, planning)
- **Shared Context** — agents share state with no silos
- **Orchestration Layer** — routing, prioritization, and conflict resolution
- **Emergent Capabilities** — orchestrated agents achieve complex goals together

### 19. Hands-On — Agent Orchestration (106)
- **Exercise 106 — Agent Orchestration:** use a summary agent to restate the latest question with context, route it to the right specialist agents (home, energy, car, photos), fan out the calls in parallel, then merge the results — inspecting routing with `/debug`

### 20. Interactive Quizzes
- **Quiz 1** — which temperature range is most consistent/deterministic for coding? (Answer: **0.0 – 0.3**)
- **Quiz 2** — AlphaGo learning Go via a reward signal is which type of ML? (Answer: **Reinforcement Learning**)
- **Quiz 3** — why does Copilot confidently call methods that don't exist? (Answer: **LLMs predict statistically likely tokens, not factually verified answers**)

---

## 🧪 Chapter 1 Exercises

- [Exercise 101 — Token Visualizer](../../../exercises/chapter-01/exercise-101/README.md) — explore how prompts and code are split into tokens using an Azure OpenAI-backed terminal app
- [Exercise 102 — Stateless LLM Chat](../../../exercises/chapter-01/exercise-102/README.md) — build a first console chat app with the Microsoft Agent Framework SDK and see why an LLM is stateless by default
- [Exercise 103 — Chat History & Roles](../../../exercises/chapter-01/exercise-103/README.md) — add explicit state, stored turns, and user/assistant/system roles to the conversation
- [Exercise 104 — Tool Calls](../../../exercises/chapter-01/exercise-104/README.md) — expose `getDateTime` as an in-process function so the model can access real-time data
- [Exercise 105 — MCP Tool Calls](../../../exercises/chapter-01/exercise-105/README.md) — move the date/time tools behind an MCP server and connect the chat app to it
- [Exercise 106 — Agent Orchestration](../../../exercises/chapter-01/exercise-106/README.md) — route one question through a coordinator and specialist agents, then merge the result

---

## 🔗 Resources & References
- [GitHub Copilot Docs](https://docs.github.com/en/copilot)
- [Azure OpenAI Service](https://learn.microsoft.com/en-us/azure/ai-services/openai/)
- [Microsoft Agent Framework](https://learn.microsoft.com/en-us/agent-framework/)
- [Model Context Protocol (MCP)](https://modelcontextprotocol.io/)
- [OpenAI Tokenizer](https://platform.openai.com/tokenizer) — visualise how text is split into tokens

---

## 🗒️ Facilitator Notes
- Open with the three polls to get the room talking; revisit Poll C at the end of the workshop to compare sentiment
- Keep the ML theory at intuition level — the GAN and RL-loop diagrams are there to make adversarial training and rewards tangible, not to teach the math
- Use the temperature table as a recurring reference; Quiz 1 calls back to it directly
- The six exercises build on each other (102 → 103 → 104 → 105), so make sure participants finish each before moving on
- Tie hallucinations, context windows, and RAG together as one story: the model only knows what's in the window, so grounding and tools are how we feed it the truth

---

[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 0 — Introduction](../chapter-00/README.md) | [Chapter 2 — Meet Your New Best Friend: GitHub Copilot →](../chapter-02/README.md)