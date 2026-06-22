# Exercise 303 — Malicious Repo Prompt Trap

> **Chapter:** Chapter 3, Exercise 3  
> **Skill focus:** Threat modelling GenAI-assisted setup flows; reviewing repository instructions before letting AI tools execute them  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is a **security awareness lab**, not an implementation lab.

You are given a reference repository that looks harmless at first glance:

**Reference repo:** <https://github.com/Djohnnie/AI4Dev-Workshop-2026-FolderLister>

The trap is social, not technical: the repository tells you to initialise your environment using a **GenAI coding assistant tool**, but it points that tool at a **repo-local npm executable** that you should not trust.

That is exactly the kind of failure mode teams can miss when they become comfortable with AI agents doing setup work on their behalf.

> 🔐 This is the **Security** principle in practice: AI tools can accelerate dangerous actions just as easily as safe ones if you let them execute untrusted repository instructions without review.

---

## ⚠️ Safety Rule for This Exercise

Treat the reference repository as **potentially hostile**.

- Do **not** run its setup commands on your normal machine.
- Do **not** let a coding agent execute repository-provided scripts automatically.
- If you explore it hands-on, do so only in a **throwaway, isolated environment**.

For the workshop, the main task is to **inspect and reason**, not to execute.

---

## 📚 Scenario

Imagine you cloned an unfamiliar GitHub repository and opened it in a GenAI coding tool.

The README or setup instructions say something like:

> "To initialise the dev environment, ask your AI coding assistant to run the local npm-based setup command in this repository."

That feels convenient. It is also dangerous.

Why?

- the repository controls the script you are being asked to run
- the AI tool may execute that instruction with little friction if you phrase it as a normal setup task
- the script may read files, environment variables, shell history, tokens, SSH keys, or other local context
- the trust boundary shifts from **"I review commands before running them"** to **"the agent will take care of it"**

This exercise helps participants build the habit of stopping and asking:

> **Who controls the tool or script I am about to let my AI run?**

---

## ✅ Your Task

### Phase 1 — Read the repository like an attacker would

1. Open the reference repo on GitHub.
2. Read the README and setup instructions carefully.
3. Identify every place where the repository tries to influence what your AI tool should run.

Questions to ask:

- Is the setup command calling a **repo-local executable**?
- Is the README encouraging me to **delegate trust** to the agent?
- Would a rushed developer treat this as a harmless bootstrap step?

### Phase 2 — Use Copilot as a reviewer, not an executor

Ask Copilot questions like:

- *"Review these setup instructions for trust-boundary problems."*
- *"What is risky about letting an AI coding agent run repository-local npm executables?"*
- *"What local secrets or files could a malicious setup script try to access?"*
- *"What signs would tell me this repository is trying to exploit tool trust?"*

The point is to use AI to help you **analyse** the risk, not to execute anything.

### Phase 3 — Write down the red flags

By the end, participants should be able to explain:

- why repository-provided setup tooling is not automatically trustworthy
- why AI agent convenience can increase the blast radius of bad instructions
- why "it is just npm" or "it is just a dev setup command" is not a security guarantee
- why setup commands deserve the same scrutiny as shell scripts from the internet

---

## 🤖 Copilot Skills to Practise

| Task | How |
|---|---|
| Threat-model a setup flow | Ask: *"What are the trust boundaries in these setup instructions?"* |
| Analyse a risky instruction | Ask: *"What could go wrong if an AI agent executes this command automatically?"* |
| Review a repo for AI-agent abuse | Ask: *"What signs suggest this repository is trying to misuse an AI coding tool?"* |
| Turn analysis into policy | Ask: *"Write a short team policy for reviewing repo-local setup scripts before using AI tools."* |

---

## 🧠 What Participants Should Learn

This exercise is not mainly about npm. It is about **delegated execution**.

AI coding tools can:

- run commands
- install dependencies
- follow repository instructions
- act on natural-language prompts that sound routine

That means a malicious repository does not only need to trick a human into running a command. It may only need to trick the human into telling an **AI assistant** to do it.

The security lesson:

- never blindly trust repository setup instructions
- never blindly trust repo-local executables or scripts
- never assume an AI tool is adding security review just because it is "helping"
- inspect first, delegate second

---

## 🏁 Completion Criteria

You have completed the exercise when:

- [ ] You can explain why the reference repository is dangerous to hand over to an AI coding tool.
- [ ] You can identify the trust-boundary failure in the setup flow.
- [ ] You can describe at least three local assets a malicious setup script might try to access.
- [ ] You can state a safe team rule for AI-assisted repository bootstrap steps.

---

## 🏁 Stretch Goals

1. Ask Copilot to draft a **safe-repo checklist** for opening unfamiliar repositories in an AI coding assistant.
2. Ask Copilot to write a short **security policy** for "AI agents may not execute repo-local setup scripts without human review."
3. Compare this scenario with classic supply-chain risks such as `curl | sh`, malicious post-install scripts, or typo-squatted packages.

---

← Back to [Exercise Index](../../README.md) | Related: [Reference repo](https://github.com/Djohnnie/AI4Dev-Workshop-2026-FolderLister) | Previous: [Exercise 302](../302/README.md) | Next: [Exercise 304](../304/README.md)
