[🏠 Workshop Home](../../../README.md) | [← Chapter 7 — Level Up: Best Practices for AI-Powered Development](../chapter-07/README.md)

---

# Chapter 8 — Get Your Hands Dirty: Real-World AI in Action

> **Duration:** 90 minutes | Day 2, 15:00 – 16:30

The culminating session. Participants apply everything learned over two days to build a real feature end-to-end, using GitHub Copilot at every step. The session closes with reflection, next steps, and celebration.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Complete a full feature development cycle using Copilot (design → code → test → document → PR)
- Apply prompt engineering, context variables, and custom instructions in a realistic scenario
- Demonstrate confident, critical use of Copilot — accepting, rejecting, and refining suggestions
- Articulate a personal action plan for adopting Copilot in their daily work
- Leave with a working code artifact they built with AI assistance

---

## 📋 Content Outline

### 1. The Challenge Brief (10 min)
- Introduce the capstone project: a small but realistic feature to build from scratch
  - **Suggested project: "Mini REST API with authentication, validation, and tests"**
    - **Why this works well as a capstone:** it touches every Copilot skill covered in the workshop — scaffolding (Phase 1), completions and Agent Mode (Phase 2), `/tests` with edge cases (Phase 3), `/doc` and PR description (Phase 4); it also produces a portable artifact participants can show to colleagues or include in a portfolio
    - **Scope guidance:** the API should have 2–3 endpoints maximum (e.g., `POST /register`, `POST /login`, `GET /me`) — small enough to complete in 20 minutes with Copilot, rich enough to require authentication middleware, input validation, error responses, and at least one database interaction (in-memory is fine)
    - **Starter repo:** provide a pre-configured repository with the runtime and framework installed but no application code — participants clone it and start from an empty `src/` directory; avoids wasting time on environment setup; offer TypeScript/Express, Python/FastAPI, and Go/Chi variants
  - **Alternative: "Data processing CLI tool with error handling and documentation"**
    - **Good for:** participants from data engineering, DevOps, or platform engineering backgrounds who don't typically build REST APIs; the CLI context also exercises Copilot CLI skills from Chapter 4
    - **Scope guidance:** a CLI tool that reads a CSV file, applies a transformation (filter, aggregate, or format), and writes the output to stdout or a file — with proper argument parsing, error handling for missing/malformed files, `--help` text, and a README; achievable in 20 minutes with Copilot
    - **Differentiating value:** this alternative specifically exercises comment-driven development (writing the algorithm as comments first) and test-first prompting for data transformation logic — reinforces Chapter 5's advanced prompting patterns
  - **Alternative: "Front-end feature component with unit tests and accessibility"**
    - **Good for:** participants from front-end or full-stack backgrounds; exercises Copilot's React/Vue/Angular component generation, `@testing-library` test generation, and accessibility-focused prompting ("Add ARIA attributes to this form and ensure it is navigable by keyboard")
    - **Scope guidance:** a search-with-filters component — a text input, 2–3 filter toggles, and a results list — with controlled state, loading/error states, unit tests using Testing Library, and a Storybook story; deliberate inclusion of accessibility requirements tests Copilot's ability to handle non-functional requirements
- Walk through the requirements (deliberately written like a real ticket/issue)
  - **Why "like a real ticket":** real-world requirements are ambiguous, incomplete, and written for a product manager's audience, not a developer's — the capstone requirements should reflect this; participants must ask Copilot to clarify ambiguities, fill in gaps, and make reasonable assumptions, which mirrors actual daily work
  - **Requirement format:** write the brief as a GitHub Issue with an overview paragraph, a bulleted list of acceptance criteria, a "notes" section with deliberate ambiguities, and a "definition of done" checklist — this format lets participants use `@workspace` to reference the requirements document during the build
  - **Built-in ambiguities to resolve with Copilot:** at least one requirement should be vague ("the API should handle errors gracefully" — what does that mean for the response format?); participants use Copilot Chat to propose reasonable interpretations before writing code, modelling the "clarify before coding" habit from Chapter 5
  - **Facilitator role during brief walkthrough:** read each requirement aloud and pause at ambiguities — ask "How would you interpret this?" before revealing the intended interpretation; establishes that requirements analysis is part of the capstone task, not just implementation
- Participants choose their preferred language/framework (Copilot supports them all)
  - **Why choice matters:** participants are most productive in the language they know best — forcing everyone into JavaScript/TypeScript would disadvantage Python, Go, and Java developers and undercut the "Copilot works in your stack" message; diversity of languages in the room also demonstrates Copilot's breadth
  - **Supported with high quality:** TypeScript, Python, Go, Java, C#, Ruby, Rust — all have excellent Copilot completion quality and `/tests`, `/doc`, and Agent Mode support; reassure participants that whichever they choose, Copilot will be a capable partner
  - **Starter repos per language:** pre-provision at least TypeScript/Express, Python/FastAPI, and Go/chi variants so participants don't spend time on dependency installation; have the starter repos in GitHub Codespaces for zero-setup option
  - **Framework-agnostic instructions:** remind participants to add their chosen framework to their `copilot-instructions.md` as the first step — this ensures Copilot generates idiomatic code for the specific framework rather than generic code that needs adjustment
- Optional: participants use their *own* real project for maximum relevance
  - **The case for real projects:** participants who work on their actual codebase during the capstone get the highest-value experience — the context is real, the business rules are real, the impact of Copilot's output is immediately tangible; they leave with real work done, not a toy exercise
  - **What to warn them about:** real projects may have sensitive code — remind participants of the data handling principles from Chapter 3 (don't paste PII, credentials, or proprietary algorithms into Chat); they should treat the capstone session like a normal Copilot workday, not as an exception to their data handling norms
  - **The guidance for real project participants:** pick a well-scoped, self-contained feature — not a multi-sprint epic; if the feature is too large, ask Copilot to help scope it down to a 50-minute deliverable; the capstone structure (design → implement → test → document → PR) still applies even for real projects
  - **The facilitator's challenge:** participants on real projects need more facilitation attention because their context is unique — check in with them early in Phase 1 to make sure they've scoped appropriately and set up their instructions file correctly

### 2. Capstone Build: Feature Development Sprint (50 min)
Participants work individually or in pairs, with facilitator circulating. Structured in phases:

#### Phase 1 — Design & Scaffold (10 min)
- Create a `copilot-instructions.md` for the project
  - **What to write first:** open the file and start with tech stack declarations — language, framework, test library, and any key conventions — so that every subsequent Copilot interaction in the project is anchored to the correct context from the very first suggestion
  - **Minimum viable instructions for the capstone:** (1) tech stack one-liner, (2) file naming convention, (3) test framework and file location, (4) error handling style ("throw typed errors, never return error objects") — four lines is enough to meaningfully improve suggestion quality for a 50-minute project
  - **Facilitator tip:** circulate during this step and check that participants are actually creating the file, not skipping it — the instructions file is the single highest-leverage action in the sprint; teams that skip it will produce less consistent Copilot output and may not see why
- Use Copilot Chat to turn the requirements into a file/function structure
  - **The planning prompt:** paste the requirements into Chat and ask "Based on these requirements, what files and functions do I need to create? Output a folder structure and a brief description of each file's responsibility" — this produces a concrete plan to react to rather than having to design from scratch
  - **Evaluating the plan:** read the proposed structure critically — does it make sense for your chosen framework? Does it reflect the patterns already in the codebase (or the starter repo)? Does it separate concerns appropriately? Adjust before starting implementation — changing the structure after you've written 10 files is expensive
  - **Saving the plan as context:** paste the approved structure into a comment at the top of the main entry file or into a `ARCHITECTURE.md` — this gives Copilot a persistent reference to the intended structure throughout the sprint; subsequent completions will respect the declared responsibilities
- Scaffold the project: "Create the folder structure and boilerplate files for a REST API with Express and TypeScript"
  - **Using Agent Mode for scaffolding:** this is an ideal Agent Mode task — "Create the folder structure described above and generate boilerplate files for each. Include the imports, empty function stubs, and JSDoc stubs but no implementation yet" — Agent Mode creates all files in one coordinated action
  - **What good scaffolding looks like:** each file exists, imports are correct, function signatures match the planned interface, placeholder comments indicate what each function should do — the scaffold compiles (or at least parses) without errors before implementation begins
  - **The "scaffold first, implement second" discipline:** participants who jump straight into implementation (skipping scaffolding) typically spend time reorganising later; scaffolding takes 3 minutes with Copilot and saves 15 minutes of structural cleanup during implementation
  - **Checking the scaffold:** ask participants to open each generated file and verify it looks reasonable before starting Phase 2 — a 60-second sanity check per file is faster than debugging a structural mistake mid-implementation

#### Phase 2 — Implementation (20 min)
- Write code using completions and inline chat
  - **The flow state goal:** Phase 2 is the core coding sprint — the aim is to stay in a flow state where Copilot is generating 60–80% of the code and the developer is reviewing, accepting, and occasionally correcting; context switching (switching to a browser, searching docs) should be minimised
  - **Completions for function bodies:** position the cursor inside each stub function and let completions fill the body — the JSDoc stub from Phase 1 acts as a specification; if the completion is close but not right, use inline chat to adjust rather than dismissing and reprompting from scratch
  - **Inline chat for specific fixes:** when a completion misses the mark, select the generated code and use inline chat — "This doesn't handle the case where the user already exists — add a check that throws a `UserAlreadyExistsError`"; targeted inline corrections are faster than full regeneration
  - **Staying in the editor:** if a question arises about a library API, ask Copilot Chat before opening a browser — "In Express 5, how does error-handling middleware signature differ from regular middleware?" is faster to answer via Chat than via Google, and the answer is scoped to your framework version
- Use Agent Mode for multi-file generation if comfortable
  - **When to escalate to Agent Mode:** if implementing a feature requires coordinated changes across 3+ files (e.g., adding an endpoint requires changes to the router, the controller, the service, and the test file), Agent Mode handles the coordination automatically rather than requiring the developer to manually keep all files consistent
  - **The agent prompt for Phase 2:** "Implement the `POST /register` endpoint end-to-end. Create: the route handler in `routes/auth.ts`, the business logic in `services/authService.ts`, the Zod validation schema in `schemas/userSchema.ts`, and update `app.ts` to mount the new router. Follow the patterns already established in the scaffold"
  - **Monitoring the agent:** watch what files Agent Mode creates or modifies — if it starts editing files outside the intended scope, use the pause/stop control to redirect it; Agent Mode has no implicit understanding of scope boundaries unless explicitly stated in the prompt
  - **Fallback to Edits:** if participants aren't comfortable with Agent Mode, Copilot Edits (Ctrl+Shift+I) achieves similar multi-file coordination with more explicit per-file control — add the relevant files to the Edits set and describe the change; Copilot generates diffs for each file without running any commands
- Use `@workspace` to query what's already been built
  - **Mid-sprint orientation:** as the codebase grows during the sprint, use `@workspace` to re-orient — "What authentication middleware have I already written?" or "Does my current implementation handle the case where the database is unavailable?" — treats Copilot as a living documentation of the emerging codebase
  - **Avoiding duplication:** before implementing a utility function, ask "`@workspace` Is there already a function in this project that validates email addresses?" — prevents the common mistake of implementing the same utility twice in different places because you forgot what was already built
  - **Cross-referencing with requirements:** "`@workspace` Looking at the requirements and what I've built so far, which acceptance criteria have I not yet implemented?" — a rapid gap analysis without manually reading through both the requirements and the codebase
- Implement at least one non-trivial function (authentication middleware, data transformation, etc.)
  - **Why "non-trivial" matters:** the capstone should push participants beyond what completions can handle alone — a function that requires understanding the security model, handling multiple failure modes, or coordinating multiple dependencies exercises the full prompting skill set developed over two days
  - **Good candidates:** JWT middleware that validates the token, extracts the user, and handles expiry separately from invalid signatures; a pagination function that handles cursor-based navigation with total count; an input sanitisation pipeline that chains multiple validators with specific error messages per field
  - **The deliberate struggle:** if participants find the non-trivial function easy with Copilot, encourage them to add complexity (add rate limiting, add logging, add caching) — the goal is to experience the full Copilot workflow including iteration and refinement, not just one-shot generation

#### Phase 3 — Testing (10 min)
- Generate tests with `/tests`
  - **The starting point:** select each non-trivial function implemented in Phase 2, run `/tests` — accept the initial output as a starting scaffold; expect 3–5 basic tests per function; at this stage quantity matters less than getting a runnable test file in place
  - **Framework verification:** before running the tests, quickly scan the generated test file — is it using the correct test runner (`describe`/`it` for Vitest, `def test_` for pytest)? Are imports correct? Catching a framework mismatch before running saves a confusing failure loop
  - **Running the tests:** `npm test`, `pytest`, `go test ./...` — whichever is appropriate; tests should be runnable at this point even if some fail; failures in Phase 3 are expected and valuable (they identify where Copilot's implementation had gaps)
- Add at least 3 meaningful edge case tests
  - **The edge case brainstorm prompt:** "Looking at the `register` function I just tested, what are 5 edge cases that the happy-path tests don't cover? List them, then write the tests" — Copilot identifies its own blind spots when explicitly asked
  - **Mandatory edge cases for the REST API:** empty string for required fields; malformed email (missing `@`); password that is technically valid but extremely short; duplicate registration (same email twice); request body that is valid JSON but missing required fields entirely
  - **Making the edge cases meaningful:** an edge case test is only meaningful if it asserts on the *correct* error — not just that an error occurred, but that the right HTTP status code (422 vs 400 vs 409) and the right error message were returned; Copilot sometimes generates edge case tests that only assert `expect(response.status).not.toBe(200)` — push for specific assertions
- Run the tests and fix failures with Copilot's help
  - **The debugging loop:** copy the failing test output (test name + error message + stack trace), paste into Chat — "This test is failing with the following error. Looking at the implementation and the test, what is wrong and how do I fix it?"
  - **Expect some failures:** it's normal for Copilot-generated tests to catch real bugs in Copilot-generated implementation — this is the system working as intended; the implementation had a gap, the test found it, Copilot fixes it; this virtuous cycle is the core TDD value proposition
  - **Time management:** if more than 2 tests are failing at 5 minutes into Phase 3, triage — fix the failures that reveal real implementation bugs and skip test-setup issues (incorrect mocks, missing test utilities) that can be fixed later; the goal is demonstrating the workflow, not 100% green tests

#### Phase 4 — Documentation & PR (10 min)
- Generate docstrings/JSDoc for public functions with `/doc`
  - **Selective `/doc` application:** don't run `/doc` on every function — focus on the public API surface (route handlers, service methods, exported utilities); internal helper functions can remain undocumented for now; this mirrors real-world documentation prioritisation
  - **Enriching the generated docs:** immediately after `/doc` generates a block, add one sentence of context that Copilot couldn't infer — the *why* behind the design decision, a known limitation, or a usage example; the combination of generated structure + human insight produces documentation that is both fast and valuable
  - **Accessibility check for front-end alternative:** for the front-end capstone, use `/doc` on component props and then ask "Does this component meet WCAG 2.1 AA requirements? What is missing?" — Copilot performs a basic accessibility audit as a bonus documentation step
- Write a README section for the feature
  - **The prompt:** "`@workspace` Write a README section for the feature I just built. Include: what the feature does in one sentence, how to use the API endpoints (with example `curl` commands), the expected request/response format, and the error responses" — `@workspace` gathers the actual implementation to ground the documentation
  - **The curl example pattern:** ask Copilot to generate example `curl` commands for each endpoint — these double as smoke tests (developers can run them manually to verify the API is working) and as documentation; Copilot generates correct curl syntax for the implemented request/response shapes
  - **Keeping it concise:** a README section for a capstone feature should be 20–40 lines — not a book; use the "Respond concisely" instruction or add length guidance to the prompt: "Keep the README section under 30 lines and use a table for the endpoint reference"
- Draft a PR description using Copilot Chat: "Write a PR description for the following changes: [paste diff or summary]"
  - **The full prompt:** "Write a GitHub PR description for the feature I just built. Use this structure: ## What Changed (one paragraph), ## Why (one sentence linking to the capstone requirements), ## How to Test (numbered steps to verify the feature works), ## Notes (any known limitations or follow-up work)" — the explicit structure ensures the PR description is reviewable, not just a summary
  - **Using the GitHub.com sparkle button instead:** if participants have pushed their branch to GitHub, show them the Copilot sparkle button in the PR creation form — it generates the same output from the diff without requiring any prompt; compare the auto-generated description to the Chat-generated one and discuss which is better for this PR
  - **The capstone PR as a portfolio artifact:** encourage participants to keep this PR as a genuine portfolio piece — a well-documented PR that shows the full Copilot-assisted workflow (scaffold, implement, test, document) is a compelling demonstration of AI-augmented development skills for job applications or internal career discussions

### 3. Show & Tell (15 min)
- 3–4 volunteers share their screen and walk through what they built
  - **How to select volunteers:** ask for volunteers 5 minutes before the sprint ends so they can prepare; aim for diversity — different languages, different capstone options, different experience levels; a junior developer's experience is often more relatable and instructive to the room than an expert's
  - **What to show:** not just the final code — the journey; encourage volunteers to show their Chat history (what prompts they used), at least one moment where Copilot surprised them (positively or negatively), and the test results; the process is more educational than the artifact
  - **Screen share setup:** ensure the screen share shows VS Code at a font size readable by the whole room (minimum 18pt); participants on projectors may need to zoom in on specific code blocks; having volunteers demo from a Codespace URL is more reliable than local screen sharing
  - **Facilitator's role:** guide the volunteer with the structured questions below — keep each demo to 4–5 minutes maximum; celebrate what was built without ranking or comparing quality; the goal is diverse perspectives, not a competition
- Facilitator asks:
  - "What was the best prompt you used today?"
    - **Why this question:** it surfaces concrete, reusable prompt patterns for the room — a great prompt someone discovered during the sprint is more memorable than any example from the slides; write the best prompts on a shared whiteboard or Miro board for participants to photograph
    - **What to listen for:** specificity (did they add context, examples, and constraints?), iteration (did they start with a basic prompt and refine it?), creative use of context variables (did they combine `@workspace` with `#file` or use `@terminal`?); highlight what made the prompt effective when they share it
    - **Follow-up prompt for the facilitator:** "Would you have known to write that prompt on Day 1, before this workshop? What's different about how you're thinking about prompting now?" — connects the exercise back to the learning arc
  - "Where did Copilot surprise you — positively or negatively?"
    - **Why this question:** it normalises both success and failure; the room needs to hear that Copilot gets things wrong (so they maintain critical evaluation habits) and that it can do things they didn't expect (so they expand their mental model of what Copilot can do)
    - **Common positive surprises:** Copilot generated a complete test suite faster than expected; it suggested an error handling pattern that was better than what the participant would have written; it inferred the correct business rule from surrounding code without being told
    - **Common negative surprises:** Copilot generated code that looked correct but had a subtle logic error (reinforce: always review); it used a library API that doesn't exist or is deprecated; it generated tests that passed but didn't actually test the intended behaviour
    - **The facilitator's reframe:** for negative surprises — "This is exactly why human review is the non-negotiable step; Copilot gave you a head start but the correctness is your responsibility; the skill is knowing what to verify, not trusting everything"
  - "What would you do differently?"
    - **Why this question:** reflection on process — not just on the code produced — is the highest-value learning in a capstone; participants who articulate what they'd change are more likely to actually change it in their daily work
    - **Common answers:** "I'd write the `copilot-instructions.md` before writing any code" (they skipped it and got worse suggestions); "I'd use Agent Mode earlier for the scaffolding" (they typed manually what Agent Mode would have done in seconds); "I'd write the test prompts more specifically" (the edge case tests weren't as meaningful as they hoped)
    - **Write the answers down:** capture "what I'd do differently" responses on a shared whiteboard under the heading "Habits to Build" — this becomes the input for the Action Plan exercise in Section 4
- Celebrate creative approaches and honest failures equally
  - **Why failures deserve celebration:** teams that share and laugh at failed prompts ("I asked Copilot to write a sort function and it generated 200 lines of... something") build psychological safety around AI experimentation; the willingness to try unconventional prompts is what produces the biggest productivity wins
  - **The creative approach highlight:** if a participant used a prompting pattern not covered in the slides (perhaps a diff-driven prompt, a chain-of-thought approach, or an unusual context variable combination), give it specific recognition — "That's actually a technique called [name] and it's one of the most effective advanced patterns"; this validates experimentation
  - **The failure reframe:** every failed Copilot interaction is a data point — "Copilot doesn't handle this type of problem well, so I should use a different approach next time" is a valuable learning, not a reason to reject Copilot; end the Show & Tell with "The developers who get the most from Copilot are the ones who are unafraid to try things and learn from what doesn't work"

### 4. Closing: Your Copilot Action Plan (10 min)
- Each participant writes down 3 things on a card:
  1. One Copilot feature they'll use starting tomorrow
     - **Facilitation prompt:** "Not 'use Copilot more' — be specific. Is it Agent Mode for a particular type of task? Is it `@workspace` for codebase questions? Is it writing a `copilot-instructions.md` for your main project tonight?" — specificity is what converts workshop insight into durable habit change
     - **The "tomorrow" constraint is deliberate:** asking for action tomorrow (not "soon" or "this sprint") creates accountability and prevents the common workshop outcome of good intentions that never materialise; the friction of starting is lowest in the 48 hours after an energising learning experience
     - **Common good answers:** "Set up `copilot-instructions.md` for my main project"; "Use Agent Mode the next time I need to add a new endpoint"; "Try the `/tests` command on the untested module I've been avoiding"; "Use `@terminal` next time I get a confusing error message instead of Googling it"
  2. One habit they'll change to work better with AI
     - **Facilitation prompt:** "Based on today — what's one thing you currently do that you'll do differently when Copilot is involved? Maybe it's opening related files in tabs before starting, or reviewing generated code more carefully, or writing a comment before asking for a completion" — habits are harder to form than feature knowledge; name the specific habit explicitly
     - **Connecting to the Show & Tell:** if the "what I'd do differently" answers from Section 3 were captured on the whiteboard, invite participants to pick one of those as their habit to change — the peer-generated list is more credible and memorable than facilitator suggestions
     - **The smallest viable habit:** encourage the smallest habit that would have a meaningful impact — one extra minute of tab setup before coding, one extra pass of review on AI-generated code, one comment written before triggering a completion; small habits that stick beat ambitious habits that don't
  3. One thing they want to learn more about
     - **Why this question closes the workshop:** it reframes the end of the workshop as a beginning — participants leave with a specific next learning goal, not just a feeling of completion; this is the most important question for long-term Copilot adoption
     - **Common answers and resources to have ready:** "Copilot Extensions" → point to the Extensions docs and the GitHub Marketplace; "Knowledge Bases" → point to the Enterprise docs and suggest building a pilot with internal docs; "MCP and custom tools for Agent Mode" → point to the MCP protocol docs and GitHub Next; "Measuring my team's Copilot usage" → point to the Metrics API and the Copilot dashboard
     - **The learning plan follow-through:** suggest participants add the "one thing to learn" as a calendar block in the next 2 weeks — not just a note; the time investment commitment is what separates learning intentions from learning outcomes
- Optional: share anonymously via Mentimeter word cloud
  - **Why anonymous sharing works:** participants often have more honest answers when not identified — especially for "what I want to learn more about" (which may expose knowledge gaps) and "one habit I'll change" (which may imply current habits that feel embarrassing to admit publicly)
  - **Setup:** pre-configure a Mentimeter open-text word cloud question for each of the three prompts; participants submit from their phones while writing on their cards; display the word cloud live — the visual immediately shows where there's consensus and where there's diversity of thinking
  - **The facilitator insight:** the word cloud is also feedback for future workshop iterations — if "Agent Mode" appears in almost every "one thing to learn more about" cloud, it signals that the workshop's Agent Mode coverage wasn't sufficient for participants to feel confident; use this to calibrate future workshop depth
- Facilitator summary: key takeaways from the 2 days
  - **The narrative arc to reinforce:** Day 1 moved from "what is AI / what is Copilot" to "here's what it can do and how to use it safely"; Day 2 moved from "how to prompt well" to "Copilot across your whole workflow" to "you just built something real with it" — participants completed a full journey from curiosity to capability
  - **The three non-negotiables to repeat:** (1) always review AI-generated code — you own everything you commit; (2) context is everything — the quality of your input determines the quality of Copilot's output; (3) Copilot amplifies skill, it doesn't replace it — invest in your fundamentals alongside your AI tool proficiency
  - **The emotional close:** acknowledge that learning a new way of working is genuinely difficult — habits are hard to change, the tooling is moving fast, and scepticism is healthy; the developers in this room are now meaningfully ahead of the curve and that is a real competitive advantage; end with energy, not caution
- Resources for continued learning (Copilot docs, GitHub Skills, YouTube channel)
  - **GitHub Copilot docs (docs.github.com/copilot):** the authoritative reference — encourage bookmarking the "What's new" page for the Copilot changelog; new features ship every 2–4 weeks and the docs reflect changes within days; reading the changelog monthly keeps participants current without requiring continuous deep research
  - **GitHub Skills (skills.github.com):** free, interactive, in-browser exercises for Copilot — the "Code with GitHub Copilot" exercise runs entirely in GitHub Codespaces and takes 45–60 minutes; ideal for reinforcing workshop learning in the week after the workshop while muscle memory is still forming
  - **GitHub YouTube channel and Copilot playlist:** short (5–15 minute) demo videos of new features as they ship — easier to absorb than documentation for visual learners; the "GitHub Copilot in Minutes" series is specifically designed for busy developers who want to stay current without long study sessions
  - **GitHub Next (githubnext.com):** experimental projects that preview where Copilot and AI-assisted development are heading — Copilot Workspace, AI-native IDEs, multi-agent development; following GitHub Next gives participants a 6–12 month preview of what will become mainstream Copilot features
  - **GitHub Copilot community discussions:** the GitHub Community forum has a dedicated Copilot category — real developers sharing real prompts, workarounds, and feature requests; the most upvoted threads reveal what experienced Copilot users find most valuable and most frustrating
- Revisit the Day 1 poll: "How worried are you about AI replacing your job?" — has the number changed?
  - **Why revisit it:** the closing poll creates a narrative bookend — participants can see how 2 days of hands-on experience changed (or reinforced) their initial perspective; it demonstrates that informed, skilled use of AI tools changes the relationship with AI from threat to partnership
  - **Expected movement:** most participants move from moderate concern to low concern — not because AI has become less capable, but because they now see themselves as skilled AI users rather than passive observers of an AI wave; the "AI replaces jobs" fear is largely driven by unfamiliarity, and this workshop directly addresses that
  - **When the number doesn't change:** some participants will still be concerned after 2 days — acknowledge this honestly; the appropriate response is "stay curious, stay skilled, and stay current" not "don't worry about it"; the developers most at risk from AI disruption are the ones who refuse to engage with it, not the ones who engage thoughtfully and critically
  - **The final message:** "You started today uncertain about AI. You're leaving having built something real with it, having reviewed AI-generated code critically, having understood its limits, and having a specific plan for how to use it better tomorrow. That is the AI-augmented developer. That is you."

---

## 💡 Ideas for Exercises & Interactivity

### Bonus Challenge: The Unprompted Refactor
Once participants complete the capstone, challenge them: swap their code with a neighbour. Use Copilot to review, refactor, and improve the other person's code — without talking to them. Then debrief: what did you change? Do they agree?

### The "Worst Prompt" Competition
During the sprint, participants note their worst/funniest prompt that produced a terrible result. Share at the end — prizes for most creative failure. Normalises experimentation.

### Leaderboard: Acceptance Rate Race
If using VS Code with metrics visible, track whose suggestion acceptance rate is highest during the sprint. Debrief: does higher acceptance rate mean better use of Copilot, or less critical thinking?

### Retrospective: 2-Day Workshop in One Tweet
Each participant summarises the 2-day workshop in 280 characters. Read aloud. Captures key takeaways and provides feedback for the facilitator.

---

## 🔗 Resources & References
- [GitHub Skills: Code with GitHub Copilot](https://skills.github.com/exercises/copilot-codespaces-vscode)
- [GitHub Copilot documentation](https://docs.github.com/en/copilot)
- [GitHub Copilot changelog](https://github.blog/changelog/label/copilot/)
- [GitHub YouTube: Copilot playlist](https://www.youtube.com/@GitHub/search?query=copilot)
- [GitHub Next: Experimental Copilot projects](https://githubnext.com/)
- [GitHub Copilot community discussions](https://github.com/orgs/community/discussions/categories/copilot)

---

## 🗒️ Facilitator Notes
- Pre-provision a sample repository for participants who don't bring their own project — keep it language-agnostic or provide multiple language variants
- The "Show & Tell" is the emotional highlight of the workshop — give it full time even if the sprint runs short
- End on an energising note: AI is moving fast, and these participants are now ahead of the curve
- Consider providing a digital "Certificate of Completion" or GitHub badge to reinforce the achievement

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 7 — Level Up: Best Practices for AI-Powered Development](../chapter-07/README.md)