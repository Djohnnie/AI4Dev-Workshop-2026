# Exercise 604 — Draw.io Playground with MCP and *.drawio.png

> **Chapter:** Chapter 6, Exercise 4  
> **Skill focus:** Exploring Draw.io, the DrawIO MCP server, and editable `*.drawio.png` files  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is a **diagram playground**.

Participants use Copilot to brainstorm a diagram, use the **DrawIO MCP server** to refine the structure, and save the result as a `*.drawio.png` file that previews like a normal image in Markdown while staying easy to iterate on in draw.io.

The goal is not perfect architecture. The goal is to get comfortable with:

- asking Copilot for diagram structure
- experimenting with Draw.io quickly
- understanding why `*.drawio.png` is a practical repo artifact
- reviewing a diagram as part of normal documentation work

---

## ✅ What Participants Should Do

### Phase 1 — Inspect the starter idea

Open the sample solution folder:

`..\exercise-604-solution\`

Look at the committed `*.drawio.png` file and talk about:

- what the diagram is trying to explain
- which labels are helpful
- what would be easier to understand with one more iteration

### Phase 2 — Prompt for a first diagram

Ask Copilot for a fun but structured diagram idea.

Good themes:

- a pizza-delivery robot fleet
- an intergalactic bug triage station
- a haunted build pipeline
- a multiplayer duck-debugging arcade

Have Copilot produce:

1. the main nodes
2. the relationships between them
3. suggested labels and notes
4. a short Markdown section that explains the picture

### Phase 3 — Use DrawIO MCP and Draw.io

Now play with the diagram directly.

Try prompts that ask Copilot or the DrawIO MCP server to:

- outline a flow or architecture
- simplify an overcrowded diagram
- rename shapes so the picture reads better
- turn a rough sketch into something reviewable

Save the result as a `*.drawio.png` file and confirm it still works well as a repo artifact.

### Phase 4 — Review the artifact like documentation

Treat the diagram as if it were part of a real pull request.

Review questions:

- can a teammate understand the system in under a minute?
- are the node names specific enough?
- does the Markdown around the diagram explain why it exists?
- what would you change before committing it?

### Phase 5 — Compare with the sample solution

After participants finish, compare their result with the sample artifact in:

`..\exercise-604-solution\`

---

## 🤖 Prompt Starters

```text
Act as a technical writer and system designer. Propose a fun diagram idea for a workshop demo, list the nodes and connections, and suggest a title plus short Markdown caption.
```

```text
Use the DrawIO MCP server to outline a diagram for this scenario. Keep it simple enough to fit on one slide but detailed enough that a teammate could explain the flow.
```

```text
I want to save this as a *.drawio.png file. What layout, labels, and grouping would make the diagram readable both in draw.io and in a Markdown preview?
```

```text
Review this diagram description like a PR reviewer. Which labels are vague, which connections are missing, and what should the surrounding README text explain?
```

---

## 🏁 Stretch Goals

1. Create a second version of the same diagram for a different audience.
2. Add a short ADR-style note explaining why the diagram belongs in the repo.
3. Turn the whimsical diagram into a realistic architecture diagram for an internal tool.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-06/SLIDES.md)
