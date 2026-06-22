[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 4 — Let Your AI Co-Pilot Take the Wheel](../chapter-04/README.md) | [Chapter 6 — AI Across the Entire Software Lifecycle →](../chapter-06/README.md)

---

# Chapter 5 — Speak AI's Language: Mastering Prompts & Context

> **Duration:** 90 minutes | Day 2, 09:00 – 10:30

The quality of Copilot's output is directly proportional to the quality of the prompt and context it receives. This chapter turns participants into effective prompt engineers for code — turning vague requests into precise, high-quality results.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain how Copilot assembles its prompt from surrounding context
- Apply structured prompting techniques to improve completion quality
- Use all Copilot context variables (`@`, `#`) to provide the right context
- Iterate on prompts using follow-up messages and clarifications
- Craft a custom instructions file that dramatically improves day-to-day Copilot output

---

## 📋 Content Outline

### 1. How Copilot Reads Your Code (15 min)
- The "context window" revisited: what Copilot sees when it generates a suggestion
  - **Not the whole project:** Copilot never sees your entire codebase at once — it sees a carefully assembled slice of the most relevant content that fits within the model's token limit; everything outside that window is invisible to the model
  - **The prompt is assembled by the extension, not by you:** the Copilot extension automatically gathers and ranks context signals before every request — you can influence this assembly but you can't directly control it (except through context variables in Chat)
  - **Token budget allocation:** a typical completion request has a context budget of ~8,000–32,000 tokens depending on the model; the extension fills this budget by ranking context sources and including the highest-priority content first, discarding lower-priority content if the budget runs out
  - **Why this matters for prompt quality:** if the most relevant context doesn't fit in the window — because irrelevant open tabs, a long chat history, or a very large file consumed the budget — Copilot will produce generic suggestions rather than project-aware ones; understanding this helps you actively manage what Copilot sees
- Priority of context signals:
  1. Cursor position and surrounding code
     - **The primary signal:** the code immediately surrounding the cursor (typically 50–200 lines above and below) is the highest-priority context; it tells Copilot the language, indentation style, variable names in scope, and the specific problem being solved
     - **Above and below both matter:** Copilot uses fill-in-the-middle (FIM), so code that appears *after* the cursor constrains the suggestion — if a closing brace or a `return` statement follows the cursor, Copilot will generate code that fits naturally before it
     - **Implication:** position your cursor thoughtfully; placing it in the middle of a well-structured function gives Copilot maximum context about intent; placing it at the bottom of an empty file gives it very little
  2. Open editor tabs (especially similar-named files)
     - **Tab scanning is automatic:** Copilot scans all open editor tabs and extracts relevant snippets — the extension uses a similarity ranking algorithm to decide which tabs contribute the most useful context for the current cursor position
     - **Similar-named files score highest:** if you're editing `userController.ts`, open tabs named `userService.ts`, `userModel.ts`, and `user.test.ts` will score very highly — Copilot infers they are related and includes content from them preferentially
     - **Practical implication:** before asking Copilot for help on a feature, open the related files in tabs (interfaces, related services, test files) — this is one of the highest-leverage things you can do to improve suggestion quality without changing your prompt
     - **Close irrelevant tabs:** tabs from unrelated modules consume token budget without contributing useful context; closing them before a focused Copilot session measurably improves suggestion relevance
  3. Imported modules and type definitions
     - **Imports signal the available vocabulary:** the imports at the top of your file tell Copilot which libraries, types, and utilities are in scope — it uses these to generate method calls, type annotations, and function signatures that match the libraries you're actually using
     - **Type definitions are especially valuable:** if a type file is imported, Copilot can generate correctly typed code — correct field names, correct return types, correct parameter shapes — without you repeating the type structure in a comment
     - **Missing imports lead to hallucinated APIs:** if a library isn't imported, Copilot may suggest methods that don't exist in your installed version, or mix APIs from similar libraries; having the right imports in place before asking for completions prevents this
  4. Comments immediately above the cursor
     - **The highest-signal manual input:** a clear, specific comment written immediately before where you want code generated is the single most reliable way to guide a completion — it is read first, weighted heavily, and directly frames the generation task
     - **Specificity dramatically changes output quality:** `// format date` produces a generic implementation; `// Returns a date string in "DD Mon YYYY" format (e.g. "14 Apr 2026"), or "Unknown" if the input is null` produces a precisely shaped function — the comment is effectively the specification
     - **JSDoc as a completion trigger:** starting a `/** ... */` block above a function and writing the `@param` and `@returns` tags before the function body exists causes Copilot to generate an implementation that matches the declared contract — a powerful pattern for typed languages
  5. `copilot-instructions.md` custom instructions
     - **Always-on project context:** content from `.github/copilot-instructions.md` is injected into every Copilot request in the repository — it is the persistent background context that ensures Copilot always knows your tech stack, conventions, and preferences without you repeating them
     - **Lower priority than immediate cursor context:** custom instructions influence the *style and approach* of suggestions but yield to the specific signals at the cursor; if your instructions say "use async/await" but the surrounding code is all callbacks, Copilot may blend the two — instructions work best when reinforced by the surrounding code style
     - **Session chat instructions:** in Copilot Chat, you can set session-level context with a system message at the start of a conversation ("For this session, act as a senior Rust developer reviewing my code"); these extend the file-level instructions for the duration of the thread
- The "fill-in-the-middle" (FIM) technique: Copilot sees above *and* below the cursor
  - **What FIM is:** a training technique where the model learns to generate text that fits *between* a prefix (code above the cursor) and a suffix (code below the cursor) — rather than only completing from a prefix; this is what allows Copilot to generate code that integrates naturally with what follows
  - **Why it matters in practice:** without FIM, Copilot would only consider the code above the cursor and might generate a body that doesn't naturally lead into the next line; with FIM, if a `return result;` statement already exists below the cursor, Copilot generates code that correctly populates `result`
  - **Demonstrating FIM:** write a function signature and a `return` statement, leave the body empty, place the cursor in the middle — Copilot generates the body that connects the signature to the return; this is FIM working as designed
  - **Implication for workflow:** sketch the structure first (signature at the top, return value at the bottom) then fill in the middle with Copilot — you get much more accurate implementations than generating the whole function top-to-bottom
- Practical implication: open relevant files in tabs before asking Copilot for help
  - **The "tab setup" habit:** before starting a Copilot-assisted feature, spend 60 seconds opening the 3–5 most relevant files in tabs: the interface being implemented, related services, the test file, and any type definition files — this single habit has an outsized impact on suggestion quality
  - **The payoff:** with the right tabs open, Copilot will suggest method calls that match your actual service interfaces, variable names consistent with your codebase, and patterns consistent with your testing approach — without any extra prompting
  - **Demonstrate the difference:** show the same prompt with zero tabs open vs. with four related tabs open; the suggestions in the second case will be noticeably more contextually appropriate — one of the most impactful 2-minute demos in the whole workshop

### 2. The Anatomy of a Good Prompt (20 min)
- The four ingredients of a great prompt:
  1. **Task** — what do you want? (verb-first: "Create", "Refactor", "Explain", "Fix")
     - **Start with a strong action verb:** the verb sets the register for everything that follows — "Create" implies generating from scratch, "Refactor" implies improving existing code without changing behaviour, "Explain" implies prose output, "Fix" implies a known problem to solve; using the wrong verb produces the wrong type of response
     - **Be specific about the scope of the task:** "Create a function" is weaker than "Create a TypeScript function that..."; "Refactor" is weaker than "Refactor this function to reduce cyclomatic complexity without changing its public interface"
     - **One task per prompt:** resist the urge to bundle multiple tasks — "Create the function, add error handling, and write tests" produces lower quality output than three separate focused prompts; Copilot trades depth for breadth when given multiple tasks
     - **Good verbs for code tasks:** Create, Refactor, Extract, Convert, Migrate, Optimise, Simplify, Document, Test, Debug, Review, Explain, Translate, Scaffold — each sets a different expectation for the response
  2. **Context** — what is the environment? (language, framework, constraints)
     - **Language and runtime:** even if Copilot can detect the language from the file, stating it explicitly ("in TypeScript") removes ambiguity and helps when asking in Chat without a file open
     - **Framework and version:** "using Express 5" vs. "using Express 4" produces different middleware syntax; "using React 19 with the new compiler" vs. "using React 18" affects whether hooks, Server Components, or Actions are appropriate suggestions
     - **Environment context:** "for a Node.js serverless function with a 3-second timeout" completely changes the approach to async operations, error handling, and external calls compared to a long-running server process
     - **Who will use this code:** "this function will be called by third-party developers via a public SDK" changes documentation requirements, error handling verbosity, and the need for input validation
  3. **Examples** — show the shape of what you want (input/output examples, similar code)
     - **Input/output examples are the most powerful clarifier:** `// Input: ["alice", "BOB", "Charlie"] → Output: ["Alice", "Bob", "Charlie"]` tells Copilot the exact transformation expected better than any English description
     - **Show a similar existing function:** "Write a function similar to `formatCurrency` already in this file, but for formatting percentages" — Copilot reads `formatCurrency`, infers the expected style, structure, and error handling approach, and applies the same pattern
     - **Negative examples:** "Do NOT return an error object like `{ error: string }` — throw a typed exception instead" — showing the shape of what you *don't* want is as informative as showing what you do
     - **Format examples:** if you want the output in a specific format (table, numbered list, diff, code block only), show an example of that format in the prompt: "Output a table with columns: function name | complexity | suggested refactoring"
  4. **Constraints** — what should it *not* do? (no external libraries, must be < 10 lines)
     - **Dependency constraints:** "Do not introduce any new npm dependencies" prevents Copilot from suggesting a clean solution that adds a library your team hasn't approved; state this explicitly, especially for security-sensitive or compliance-bound projects
     - **Complexity constraints:** "The function must be under 20 lines" or "Use only standard library functions" are legitimate constraints that shape what an acceptable solution looks like
     - **Backward compatibility:** "This function must remain compatible with the existing function signature — do not change parameter names or types" prevents Copilot from producing a breaking change
     - **Performance constraints:** "This function runs in a tight loop processing 1M records — optimise for speed over readability" completely changes the implementation approach compared to the default
     - **When to omit constraints:** don't artificially constrain if you don't have a real requirement — unnecessary constraints reduce the solution space and may produce worse output; only include constraints that genuinely matter
- Prompt anti-patterns to avoid:
  - Vague tasks: "make this better"
    - **Why it fails:** "better" is undefined — better could mean faster, more readable, more secure, shorter, better documented, or better tested; Copilot will guess, and the guess may not match your intent
    - **The fix:** replace subjective adjectives with specific, measurable goals — "Reduce the cyclomatic complexity of this function to below 5", "Improve readability by extracting the nested conditional into a named helper function", "Add input validation to prevent SQL injection"
    - **The pattern:** whenever you write an adjective in a prompt ("better", "cleaner", "nicer", "improved"), ask yourself: "what does that *specifically* mean for this code?" — then use that specific meaning in the prompt
  - Missing context: "write a function" (function for what? in what language?)
    - **Why it fails:** without context, Copilot defaults to the most statistically common interpretation of "a function" — which may be in the wrong language, use the wrong patterns, or solve the wrong problem entirely
    - **The fix:** always answer the implicit questions — what does it do, what are its inputs and outputs, what language and framework, where will it be called from, what should it return when things go wrong
    - **Context can come from the file:** if you're asking via inline chat with a relevant file open, some context is implicit — but it's still better to be explicit; "Write a function that validates this User type's email field" is clearer than "Write a validation function"
  - Conflicting constraints: "make it fast but also use a nested loop"
    - **Why it fails:** the model will attempt to satisfy all constraints simultaneously; when they conflict, the result is typically a compromise that satisfies neither well — code that looks like it has a nested loop but with some confusing micro-optimisations
    - **The fix:** if you have constraints that tension against each other, acknowledge the tradeoff explicitly — "Use a nested loop (O(n²) is acceptable given n < 100) but optimise the inner loop body as much as possible"; Copilot then makes the right choice within the defined tradeoff
    - **Watch for implicit conflicts:** "write a one-liner that handles all edge cases" is a hidden conflict — truly robust edge case handling rarely fits in one line; pick the goal that matters more
- One-shot vs. few-shot prompting in Copilot Chat
  - **One-shot prompting:** a single prompt with no examples — the default mode for most Copilot use; works well for common, well-understood tasks where the model has strong priors (sorting, CRUD, API calls)
  - **Few-shot prompting:** providing 1–3 examples of the pattern you want before asking Copilot to apply it — significantly improves output for non-standard patterns, domain-specific formats, or when you have very specific style requirements
  - **How to apply few-shot in Copilot Chat:** paste 1–2 examples of the output format you want, labelled as "Example:", then write "Now apply the same pattern to: [your actual task]"; the model infers the pattern from examples and replicates it
  - **When few-shot is worth the effort:** custom DSL or config formats, very specific documentation styles, API response shapes that don't match common patterns, or any time one-shot has already failed to produce the right result
  - **The cost:** few-shot prompts are longer, consume more tokens, and take slightly longer to write; for simple tasks the overhead isn't worth it — use one-shot by default, switch to few-shot when quality is insufficient
- Iterative prompting: treating Copilot Chat as a conversation, not a single query
  - **The biggest beginner mistake:** writing one long, complex prompt and expecting a perfect result; expert Copilot users write shorter initial prompts and refine through conversation — faster and more reliable
  - **The iterative loop:** prompt → review output → identify the specific gap → send a targeted follow-up ("The function looks right but it doesn't handle the case where the array is empty — fix that") → repeat until satisfied
  - **Follow-ups are more powerful than re-prompts:** instead of discarding a response and starting over, build on it — "Keep everything the same but change the error type from `Error` to `ValidationError`" is faster than re-asking from scratch with the correction baked in
  - **Naming and referencing previous output:** Copilot Chat maintains conversation history within the thread; you can refer to previous output ("The function you just wrote...") without pasting it again; use this to progressively refine without repeating yourself
  - **When to start a new thread:** if the conversation has gone in a direction that doesn't serve your current goal, start a new Chat — accumulated context from a derailed conversation can mislead subsequent responses as much as it helps them

### 3. Context Variables — Feeding Copilot the Right Information (20 min)
- **`@workspace`** — query the entire codebase; best for "how does X work in this project?"
  - **What it does:** triggers a semantic search across every indexed file in the workspace — Copilot finds the most relevant code snippets across all files and injects them into the prompt automatically; you don't need to know which file contains the code
  - **Best use cases:** architectural questions ("How does this app handle authentication?"), cross-cutting investigations ("Which components use the `useAnalytics` hook?"), onboarding a new team member to a codebase, understanding the data flow through a system
  - **Important caveat:** `@workspace` performs *search*, not *whole-codebase analysis* — it finds relevant snippets, not all code; for very large codebases, the highest-ranked snippets may miss edge cases in rarely-touched files
  - **Combining with a question:** "`@workspace` How is the payment flow tested?" is much better than just `@workspace` alone — the question shapes what snippets are retrieved; the more specific the question, the more targeted the retrieved context
  - **Performance note:** `@workspace` queries take 1–3 seconds longer than regular Chat queries because of the retrieval step; for simple questions where you know which file is relevant, `#file` is faster and more precise
- **`@vscode`** — IDE-specific help ("how do I configure the debugger for Node?")
  - **What it does:** routes the question to VS Code's built-in documentation and settings knowledge — it knows VS Code's settings JSON keys, extension APIs, keyboard shortcuts, and configuration patterns
  - **Best use cases:** configuring `launch.json` for a specific language, understanding what a VS Code setting does, finding the right keybinding or command palette command, troubleshooting extension conflicts
  - **Not for code questions:** `@vscode` is for *IDE configuration*, not for coding tasks — asking it to help with a TypeScript function will produce a worse answer than asking without the `@vscode` prefix; use it only for VS Code itself
  - **Example:** "`@vscode` How do I configure the Node.js debugger to attach to a running process and ignore node_modules in stack traces?" — the answer will cite specific `launch.json` properties and VS Code settings
- **`@terminal`** — paste and explain terminal errors directly
  - **What it does:** reads the current terminal's recent output (or a pasted error) and explains it in the context of your project — it knows your shell, your OS, and the commands you've run
  - **Killer use case:** paste a cryptic npm/pip/compiler error and ask "Why is this happening and how do I fix it?" — `@terminal` provides a more contextualised answer than Googling the error message because it also considers the project context
  - **How to use it:** run a failing command → open Copilot Chat → type "`@terminal` Explain this error and suggest a fix" → Copilot reads the terminal output automatically (in VS Code) or you paste the output
  - **Shell script generation:** "`@terminal` write a bash command that finds all files modified in the last 24 hours and larger than 1MB" — Copilot generates shell commands tuned to your detected shell (bash, zsh, PowerShell)
- **`#file`** — attach a specific file to the prompt
  - **What it does:** explicitly includes the full content of a named file in the Copilot Chat prompt — more reliable than relying on tab scanning because you know exactly what's in context
  - **Syntax:** type `#file:` followed by the filename (Copilot autocompletes from open files and recently opened files in the workspace)
  - **When to prefer it over `@workspace`:** when you know *exactly* which file is relevant — `#file` is precise and fast; `@workspace` is broad and slower; use `#file` for "refactor this function in `userService.ts`", use `@workspace` for "how does user authentication work across this project?"
  - **Multiple files:** you can attach multiple files in one prompt — "`#file:userService.ts` `#file:userModel.ts` Refactor the `createUser` method to use the types from the model file" — both files are included in full
  - **File size limit:** very large files may be truncated when attached; if the file is over ~500 lines, consider attaching a `#selection` from the relevant section instead
- **`#selection`** — reference the current text selection
  - **What it does:** automatically includes the text currently selected in the editor in the Chat prompt — prevents you from copy-pasting code into the Chat window
  - **Workflow:** select the relevant code in the editor → switch to Chat → write your prompt — `#selection` is injected automatically when you use inline Chat; in the panel Chat you can reference it explicitly
  - **Best for:** asking about a specific function or block — "Explain what `#selection` does", "What are the edge cases not handled in `#selection`?", "Rewrite `#selection` using the Strategy pattern"
  - **Combining with other variables:** "`#selection` doesn't match the interface in `#file:userModel.ts` — update it to comply" — very powerful combination for targeted refactoring
- **`#codebase`** — semantic search across the codebase for relevant snippets
  - **What it does:** similar to `@workspace` but used as an inline variable rather than a participant prefix — triggers semantic search and injects the most relevant snippets into the current prompt
  - **Difference from `@workspace`:** `@workspace` makes Copilot act as a "codebase expert" for the whole conversation; `#codebase` pulls in snippets for a single prompt without changing the conversation mode
  - **Use case:** "Show me all the places in `#codebase` where we make HTTP calls without error handling" — retrieves relevant code snippets and Copilot analyses them
- **`#sym`** / `#def`** — reference a specific symbol or its definition
  - **`#sym`:** reference a named symbol (function, class, variable) by name — Copilot finds its definition and usages across the codebase and includes them in context
  - **`#def`:** reference the *definition* of the symbol under the cursor — equivalent to "Go to Definition" but as context for Chat
  - **Best use cases:** "Explain `#sym:processOrder` in detail", "Refactor `#sym:UserRepository` to add caching", "What would break if I changed the signature of `#def`?"
  - **Advantage over `#file`:** when you care about a specific function or class, `#sym` is more targeted than attaching the whole file — it includes the symbol and its direct dependencies, not the entire file
- Demo: solving a real debugging problem using only `@workspace` and `#file` references
  - **Demo scenario:** an unfamiliar bug in a medium-complexity codebase — the goal is to diagnose and fix without manually reading through files
  - **Step 1:** "`@workspace` There's a bug where `processOrder` occasionally sends duplicate emails. Which part of the code handles email sending, and where could a race condition occur?"
  - **Step 2:** based on the response, attach the specific file: "`#file:orderProcessor.ts` Here's the function — explain why the email might be sent twice if two requests arrive simultaneously"
  - **Step 3:** "Suggest a fix using a distributed lock or idempotency key. Assume we have Redis available"
  - **Debrief:** highlight that the developer navigated and diagnosed the bug without opening a single file manually — purely through Copilot Chat + context variables

### 4. Advanced Prompting Patterns for Code (20 min)
- **Comment-driven development:** writing the spec as comments, letting Copilot fill the implementation
  - **The pattern:** instead of writing a function and then adding comments, write all the comments first as if they were documentation for code that doesn't exist yet, then let Copilot generate the implementation comment by comment
  - **Why it works:** comments sit immediately above the code position and have the highest weight in the context window; a detailed comment is effectively a specification that Copilot translates directly into code
  - **How to do it:** write a JSDoc block with `@param`, `@returns`, and `@throws`; add inline comments describing each major step of the algorithm; position the cursor below the last comment and trigger a completion — Copilot generates the full function body
  - **Demo script:** open an empty file; write 5–8 lines of comments describing a rate limiter function; watch Copilot generate the entire implementation; note that you never typed a line of code
  - **The discipline payoff:** forces you to think clearly about what you want before you write it — the comment-writing process reveals ambiguities in your own understanding before they become bugs
- **Test-first prompting:** describe the test → let Copilot write the implementation that passes it
  - **The pattern:** write the test specification or the test code first, then ask Copilot to generate the implementation that would make the test pass — aligns with TDD but using Copilot to accelerate the implementation step
  - **Option A — test-from-spec:** describe the desired behaviour in a Chat prompt as a series of "should" statements ("This function should return null for empty input, return a ValidationError for emails without @, and return the email lowercase for valid input") — ask Copilot to write tests matching this spec, then ask it to write the implementation
  - **Option B — implementation-from-test:** write the test yourself (or have Copilot write it) then say in Chat "Write an implementation of `validateEmail` that would make all these tests pass" — attach the test file with `#file`; Copilot reads the assertions and reverse-engineers the implementation
  - **Why it produces better code:** the test constrains the solution space — Copilot can't take shortcuts that would break the test; you get an implementation that is correct by construction (with respect to the test coverage)
  - **Gotcha:** Copilot may write an implementation that *passes the tests* but is incorrect in other ways (hardcoded values for specific test inputs, missing edge cases not covered by tests) — always review the implementation, don't just check that tests are green
- **Persona prompting:** "You are an expert in database query optimisation. Review this query and suggest improvements."
  - **What it does:** priming Copilot with a role activates the statistical patterns associated with that role in the model's training data — an "expert DBA" prompt produces more domain-specific, technically deeper answers than asking the same question without the persona
  - **Effective persona patterns:** "You are a security auditor reviewing this code for OWASP vulnerabilities", "You are a senior engineer with 10 years of distributed systems experience — review this design for failure modes", "You are a technical writer — rewrite this README so a junior developer can follow it without assistance"
  - **Combine with constraints:** "You are a performance engineer. Review this function and suggest optimisations *without changing its public API and without introducing new dependencies*" — the persona plus constraints focus the output precisely
  - **When persona prompting is most valuable:** for review tasks where you want a specific lens applied (security, performance, accessibility, usability, testability) — the persona tells Copilot *what to look for*, which produces more targeted feedback than "review my code"
  - **Limitation:** personas don't give Copilot new knowledge — they activate existing patterns; if Copilot doesn't have training data on the subject, a persona won't compensate; use with appropriate scepticism on highly specialised topics
- **Stepwise decomposition:** breaking a complex task into steps and prompting each step individually
  - **The problem it solves:** complex tasks (like "Build a REST API for user management with JWT auth, rate limiting, and logging") produce poor results when asked all at once — the model must simultaneously navigate too many design decisions and frequently produces shallow or inconsistent output
  - **The pattern:** decompose the task into 4–6 logical steps before prompting; prompt each step separately using the output of the previous step as context for the next
  - **Example decomposition:** (1) "Define the TypeScript types for a User entity and its DTOs" → (2) "Write the Prisma schema for the User model" → (3) "Write the `UserRepository` class using Prisma" → (4) "Write the `UserService` with business logic" → (5) "Write the Express routes wiring the service to HTTP"
  - **Benefit:** each step produces high-quality, focused output; subsequent steps build on actual concrete code rather than hypothetical code; errors are caught at each step before they propagate
  - **When to let Agent Mode do the decomposition:** if you're using Agent Mode (chapter 4), it does stepwise decomposition automatically — manual stepwise prompting is most valuable in Chat mode where Agent Mode isn't available or appropriate
- **Diff-driven prompting:** "Here is the before and after of a similar refactor. Apply the same pattern to this code."
  - **The pattern:** show Copilot a before/after example of a transformation you've already done (or found in a PR), then ask it to apply the same transformation to different code
  - **Why it's powerful:** showing the transformation is more precise than describing it in words — the model can infer the exact pattern, the edge cases handled, the naming conventions preserved, and the structural changes made from the example
  - **How to structure it:** paste two code blocks labelled "Before:" and "After:", write one sentence explaining the transformation ("This refactoring extracts the error handling into a separate helper function"), then paste the target code and ask "Apply the same refactoring"
  - **Use cases:** enforcing a consistent refactoring pattern across multiple files, migrating from one pattern to another (callbacks → promises → async/await), updating code to match a new design system or API version, applying a security fix consistently across similar functions
  - **Gotcha:** the model may over-apply the pattern — transforming code that shouldn't be changed; always review the output for unintended changes
- **Chain-of-thought:** asking Copilot to "think step by step" before generating code
  - **What it does:** adding "think step by step" (or "explain your reasoning before writing the code") to a prompt causes the model to explicitly reason through the problem before generating the implementation — this produces more accurate and more explainable results for complex tasks
  - **Why it works:** chain-of-thought prompting has been shown to significantly improve accuracy on reasoning tasks; the reasoning process surfaces intermediate results that constrain and focus the final output; the model "checks its work" as it reasons
  - **When to use it:** complex algorithm design, debugging a problem with unclear root cause, any task where correctness matters more than speed, tasks where you want to understand *why* Copilot made the choices it did
  - **Example:** "Think step by step about how to implement a least-recently-used (LRU) cache with O(1) get and put operations. Explain each data structure you'll use and why, then write the implementation" — the reasoning output is itself educational and helps you verify the implementation is correct
  - **Tradeoff:** chain-of-thought responses are longer — sometimes 3–5x longer than direct answers; for simple tasks it's overkill; use it when the task is hard enough that an incorrect answer would cost significant time to debug
- When to use completions vs. when to use Chat
  - **Use completions (inline ghost text) when:** you're writing new code in a flow state, the task is small and local (a single function, a conditional block), you want suggestions based on the immediately surrounding code, you want multiple alternatives to cycle through
  - **Use inline Chat when:** you want to ask a specific question about highlighted code, you want to refactor a selection with a specific instruction, you need a quick explanation of what a block does without leaving the editor flow
  - **Use Chat panel when:** the task requires codebase-wide context (`@workspace`, multiple `#file` references), you're debugging a complex problem through conversation, you want to explore multiple approaches before writing any code, the task is architectural or design-level
  - **Use Agent Mode when:** the task spans multiple files, requires running code or tests to verify correctness, or is complex enough that it would naturally decompose into multiple steps — Agent Mode automates the iteration loop
  - **Rule of thumb:** start with completions for "what to write next", switch to Chat when you need to "figure out how to solve something", escalate to Agent Mode when the solution requires coordinating changes across multiple places

### 5. Custom Instructions — Advanced Configuration (10 min)
- Layered instructions: user-level → repository-level → session context
  - **User-level instructions:** set in VS Code settings (`github.copilot.chat.codeGeneration.instructions`) — apply to every Copilot session regardless of which project is open; ideal for personal preferences that don't vary by project (preferred comment style, response verbosity, preferred language for explanations)
  - **Repository-level instructions:** stored in `.github/copilot-instructions.md` in the repository root — committed to source control, shared with the whole team, scoped to that repository; every developer using Copilot on this repo benefits from the same baseline project context without any individual configuration
  - **Session context:** set at the start of a Chat conversation ("For this session, treat me as a junior developer unfamiliar with this codebase — explain everything in detail") — overrides or extends the persistent instructions for a single conversation thread without changing the files
  - **Resolution order:** when all three levels exist, they are all injected into the prompt; session context is highest priority (most recent), repository-level is next, user-level is baseline; instructions don't cancel each other out — they stack, so keep each level focused and non-redundant
  - **Governance implication:** because repository-level instructions are in source control, they can be code-reviewed, versioned, and audited — organisations can enforce specific Copilot behaviour at scale through pull request review of the instructions file
- Effective patterns:
  - Tech stack declarations: "This project uses React 19, TypeScript 5.4, Tailwind CSS, and Vitest"
    - **Why this matters:** without a tech stack declaration, Copilot may suggest React class components (trained on older code), use Jest syntax (more common in training data), or suggest inline styles — the declaration anchors Copilot to your actual stack from the first token
    - **Be version-specific:** "TypeScript 5.4" vs "TypeScript" matters because newer versions have features (e.g., `satisfies` operator, const type parameters) that older versions don't — specificity prevents suggestions that won't compile in your environment
    - **Include negative declarations:** "We do NOT use Redux — use Zustand for state management" prevents Copilot from defaulting to the most common pattern when the common pattern isn't what you use
  - Style rules: "Always use named exports. Avoid default exports."
    - **Consistency enforcement:** style rules are the most reliable category of instructions — Copilot follows explicit naming and structure conventions well because they are low-ambiguity directives
    - **Good style rules to include:** export style (named vs default), `interface` vs `type` preference, `function` vs arrow function preference, file naming convention, folder structure for new files ("New components go in `src/components/`, each in its own subfolder with an `index.ts` barrel file")
    - **Don't over-prescribe:** rules that are too granular ("Use exactly 2 blank lines between functions") are brittle and may conflict with the formatter; let your linter/formatter handle micro-formatting, let instructions handle architectural style
  - Testing rules: "Every new function must have a corresponding test in the `__tests__` directory"
    - **Automated TDD nudge:** this instruction causes Copilot to suggest test files alongside implementation files — in Agent Mode it may even create the test file automatically; it operationalises your testing policy without requiring a code review comment every time
    - **Include the testing library:** "Tests use Vitest and @testing-library/react for component tests. Mocks use vi.fn(), not jest.fn()" — prevents syntax mismatches that produce code that looks right but fails to run
    - **Coverage guidance:** "Test the happy path, one error path, and the boundary condition for every function" — Copilot will produce tests that are structurally more complete, not just boilerplate `it('should work')` tests
  - Output rules: "Respond concisely. Do not add comments unless I ask."
    - **Controlling verbosity:** by default Copilot may add extensive inline comments, lengthy explanations, and scaffolding placeholders — if your team has a "code is self-documenting" culture, this instruction suppresses unwanted noise
    - **When to include this:** for experienced teams who want Copilot to produce production-ready code without tutorial-style comments; for onboarding scenarios, omit this instruction so Copilot explains its reasoning
    - **Format instructions:** "When producing code, output *only* the code block — no preamble, no explanation, no trailing commentary" — very useful when using Copilot output programmatically or in an agentic pipeline
- Writing instructions that are specific but not brittle
  - **Specific:** use concrete, verifiable directives — "Use `zod` for all input validation" is specific; "Follow best practices for validation" is not
  - **Not brittle:** avoid instructions that break on edge cases — "Every file must have a header comment" becomes a problem if Copilot is asked to generate a JSON file or a Markdown README; prefer "All TypeScript and JavaScript files must have a header comment"
  - **Test your instructions:** after writing or updating the instructions file, ask Copilot a representative question for each rule — check whether the output actually reflects the instruction; if it doesn't, the instruction is ambiguous or conflicting with another
  - **Version your instructions:** treat the instructions file like configuration code — use git blame to understand why a rule was added, use pull requests to propose changes, use commit messages to document the rationale for each update
  - **Short is better than long:** a 15-line instructions file that Copilot follows reliably is worth more than a 100-line file that Copilot partially ignores because the context budget runs out before the end; prioritise the rules that matter most and remove the rest
- Testing and iterating on custom instructions
  - **The validation loop:** (1) add or change an instruction, (2) ask a prompt that should be affected by it, (3) check whether the output changed in the expected direction, (4) if not, refine the instruction and repeat
  - **Common failure modes:** instruction is phrased as a preference ("prefer X") rather than a directive ("always use X") — Copilot treats preferences as suggestions and may ignore them; be directive when you want reliable adherence
  - **Conflicts produce unpredictable blending:** if a user-level instruction says "use async/await" and the repository instruction says "use Promises for all async code", Copilot may blend the two inconsistently; audit all three levels for conflicts when behaviour is surprising
  - **Organise by category:** group related instructions under markdown headings (## Tech Stack, ## Code Style, ## Testing, ## Output Format) — easier to maintain and to spot redundancy or conflict; Copilot reads the headings and uses them as context for the instructions that follow

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Prompt Makeover (15 min)
Display 5 "bad" prompts on screen. Teams compete to rewrite them using the four-ingredient framework. Copilot judges the winner — run both prompts and compare output quality in the room.

Example bad prompts:
- "help with my code"
- "write a sort function"
- "fix the bug"
- "make it work with TypeScript"
- "add error handling"

### Exercise 501: Context Window Copilot Clone (20 min)
Build a small Visual Studio 2026 extension that behaves like a tiny self-written GitHub Copilot clone. Run the same GPT-4o prompt with no context, FIM context, and open tabs context so participants can feel how context windows change the answer.

Example prompt:
- "Create a Visual Studio extension command that opens a dialog with a prompt box and a context selector. Call a GPT-4o model, support no context, FIM context, and open tabs context, and show the composed prompt so we can compare the results."

### Exercise 502: Prompt Arena (20 min)
Build a deployable .NET web app that acts like a prompt-rating game. Participants submit prompts, and a custom GPT-4o chatbot returns a score from 0% to 100% based on the four ingredients, one-shot or few-shot examples, and anti-pattern avoidance.

Example prompt:
- "Create a .NET web app with a textbox, a score panel, and a custom system prompt that judges whether a participant prompt includes task, context, examples, and constraints. Return JSON with a score and feedback."

### Exercise 503: Context Variables Playground (15 min)
Use a prebuilt .NET playground app and compare how Copilot answers when participants ask the same question with no variable, with `@workspace`, and with `#file`. The code is already there; the point is to experiment with context selection, not to build new features.

Example prompts:
- `@workspace map this project and tell me which files matter most for sprint health and incident digests.`
- `#file:Services/AuthGateway.cs explain how dashboard access is validated and whether it looks production-ready.`
- `@workspace are there any TODO comments in this exercise, and which one matters most?`

### Exercise 504: Prompt Pattern Playground (15 min)
Use a second prebuilt .NET playground app to try the advanced prompting patterns from section 4 on existing code. Participants can compare comment-driven, test-first, persona, stepwise, and diff-driven prompts against the same codebase without first inventing a task.

Example prompts:
- `#file:Services/ReleaseSummaryService.cs explain how the leading comment constrains the implementation.`
- `#file:Services/ReleaseWindowCalculator.cs list the tests you would want before changing this calculator.`
- `Act as a security reviewer. #file:Services/SecurityHeadersPolicy.cs what concerns do you see?`
- `Compare #file:Services/LegacyAuditLogger.cs and #file:Services/StructuredAuditLogger.cs. Apply the same logging transform to #file:Services/LegacyBillingLogger.cs.`

### Lab 501: Ultimate Snake with Instructions, Prompt Files, and Skills (25 min)
Build the same Snake game from chapter 4 one more time, but now start from an almost-empty lab folder that already contains a repository instruction file, reusable prompt files, and a skill file. The lab is about feeling how much better Copilot performs when the repo itself carries the prompt engineering.

Example flow:
- open the lab and read `.github/copilot-instructions.md`
- invoke the scaffold prompt file instead of typing the whole request from scratch
- refine the follow-up prompt with task, context, examples, and constraints
- use the skill file to review whether the finished game matches the lab checklist

### Exercise: Personal Custom Instructions (10 min)
Each participant writes or improves their own `copilot-instructions.md` for a real project they work on. Peer review: does the other person's instructions make sense? Would it help Copilot?

### Live Experiment: One-Shot vs. Few-Shot (5 min)
Ask Copilot to generate a function with a one-sentence prompt. Then re-ask with an example input/output. Compare results live. Discuss the difference.

---

## 🔗 Resources & References
- [Prompt engineering for GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/prompt-engineering-for-github-copilot)
- [Copilot Chat context and variables](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/copilot-chat-context)
- [Best practices for using GitHub Copilot](https://docs.github.com/en/copilot/using-github-copilot/best-practices-for-using-github-copilot)
- [Adding custom instructions for Copilot](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
- [GitHub blog: How to use GitHub Copilot: Prompts, tips, and use cases](https://github.blog/developer-skills/github/how-to-use-github-copilot-prompts-tips-and-use-cases/)

---

## 🗒️ Facilitator Notes
- This session is the pivot point of the workshop — it turns casual Copilot users into power users
- The "Prompt Makeover" exercise is consistently the most engaging — give it full time
- Encourage participants to use their *real* codebases for the custom instructions exercise for maximum relevance

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 4 — Let Your AI Co-Pilot Take the Wheel](../chapter-04/README.md) | [Chapter 6 — AI Across the Entire Software Lifecycle →](../chapter-06/README.md)