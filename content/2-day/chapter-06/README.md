[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 5 — Speak AI's Language: Mastering Prompts & Context](../chapter-05/README.md) | [Chapter 7 — Level Up: Best Practices for AI-Powered Development →](../chapter-07/README.md)

---

# Chapter 6— AI Across the Entire Software Lifecycle

> **Duration:** 90 minutes | Day 2, 10:45 – 12:15

GitHub Copilot isn't just for writing new code. This chapter shows how Copilot accelerates every phase of the software development lifecycle — from requirements to deployment to incident response.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Use Copilot to generate, review, and improve tests (unit, integration, edge cases)
- Generate and maintain documentation with Copilot
- Use Copilot for debugging and root-cause analysis
- Apply Copilot to refactoring, code review, and security remediation
- Leverage Copilot on GitHub.com throughout the PR and issue lifecycle

---

## 📋 Content Outline

### 1. Copilot for Testing (25 min)
- Generating unit tests with `/tests` slash command
  - **How it works:** select a function or class in the editor, open inline Chat or the Chat panel, and run `/tests` — Copilot reads the selected code and generates a test file using your project's detected testing framework; it analyses the function signature, infers expected behaviours from the implementation, and writes assertions
  - **What it produces by default:** a test file with one `describe` block per class or module, one `it`/`test` per logical behaviour, setup/teardown boilerplate, and basic assertions — the quality floor is "tests that run and pass", not "tests that catch bugs"
  - **When to add the `#file` variable:** if Copilot picks the wrong framework (e.g., it generates Jest tests for a Vitest project), attach `#file:vitest.config.ts` to the prompt — seeing the config file makes Copilot switch to the correct framework and syntax
  - **Iterating from the initial output:** the first `/tests` output is a starting point, not a final product; follow up with "Add tests for error cases" or "Add a test for the case where `userId` is null" to expand coverage; the iterative loop is faster than trying to write a perfect prompt upfront
- Specifying the testing framework in the prompt or custom instructions
  - **In the prompt:** explicitly name the framework — "Generate Vitest tests using `vi.fn()` for mocking and `@testing-library/react` for component tests" — prevents framework mismatches, especially in projects that have migrated from one framework to another (legacy Jest config still present)
  - **In custom instructions (preferred):** add to `.github/copilot-instructions.md`: "All tests use Vitest. Mock functions use `vi.fn()`. HTTP calls are mocked with `msw`. Component tests use `@testing-library/react` with the `render` helper from `src/test-utils`" — this applies to every future test generation without repeating it in every prompt
  - **Framework-specific patterns to specify:** assertion style (`expect(x).toBe(y)` vs `assert.equal(x, y)`), the test runner command, file naming (`*.test.ts` vs `*.spec.ts`), and the location of test files (co-located vs `__tests__` directory) — all of these affect generated test structure
- Writing edge-case and boundary tests: "Add tests for null inputs, empty arrays, and integer overflow"
  - **Why Copilot defaults to happy-path tests:** the training data contains far more examples of happy-path tests than edge-case tests — `/tests` alone will typically generate one or two "it works" tests and miss all the failure modes; you must explicitly ask for boundary and error cases
  - **Effective edge-case prompts:** "Add tests for: null input, undefined input, empty string, empty array, negative numbers, numbers larger than `Number.MAX_SAFE_INTEGER`, and invalid date strings" — providing an explicit list produces more comprehensive output than asking generically for "edge cases"
  - **The boundary value checklist:** for any numeric input: test 0, -1, 1, `MAX_VALUE`, `MAX_VALUE + 1`; for strings: empty string, whitespace-only, max length, max length + 1, special characters; for arrays: empty array, single-element array, very large array; for nullable inputs: `null`, `undefined`, and the zero value of the type
  - **Error path tests:** "Add tests that verify the function throws a `ValidationError` for invalid inputs" — explicitly requesting tests that assert on exceptions / error states covers the code paths most likely to contain bugs in production
- Test-driven development with Copilot:
  1. Write a failing test (or ask Copilot to)
     - **Copilot-written test as spec:** describe the desired behaviour in a Chat prompt as a series of "should" statements, then ask Copilot to generate a failing test that encodes those behaviours — the test *becomes* the specification before the implementation exists
     - **The discipline:** write the test first, verify it fails (the implementation doesn't exist yet), only then move to step 2 — skipping the "verify it fails" step is the most common TDD mistake; a test that passes before the code is written is not testing what you think it is
  2. Ask Copilot to write code that passes it
     - **Attaching the test as context:** use `#file:user.test.ts` to include the test file in the prompt — "Write an implementation of `UserValidator` that makes all the tests in `#file:user.test.ts` pass" — Copilot reads each assertion and reverse-engineers a compliant implementation
     - **The AI advantage:** Copilot can hold all the test cases in context simultaneously and generate an implementation that satisfies all of them at once — more accurate than writing the implementation manually and checking test by test
  3. Iterate
     - **The refactoring step:** once tests are green, ask Copilot to refactor the implementation for readability or performance — "The tests pass. Now refactor the implementation to eliminate the nested ternary and extract the validation logic into a separate helper function" — tests act as a safety net for the refactor
     - **Adding tests for new requirements:** as requirements evolve, add new failing tests first, then ask Copilot to update the implementation to pass them — the Copilot-assisted TDD loop makes the "red-green-refactor" cycle faster without skipping any steps
- Generating integration and end-to-end test scaffolding
  - **The scope difference:** integration tests verify that multiple units work together correctly (e.g., a service + database layer); E2E tests verify that the whole system behaves correctly from a user's perspective (e.g., a browser-driven Playwright test); the prompts and context you provide differ significantly
  - **For integration tests:** attach the service file and the dependency file ("Generate an integration test for `UserService` that tests it against a real SQLite in-memory database using Prisma") — Copilot generates setup/teardown for the database, realistic test data, and assertions on the database state after each operation
  - **For E2E test scaffolding:** "Generate a Playwright test that covers the user registration flow: navigate to /register, fill in name/email/password, submit the form, and assert that the user is redirected to /dashboard" — Copilot generates page object patterns, selector strategies, and wait conditions
  - **Scaffolding vs. complete tests:** for integration and E2E tests, Copilot is more useful for *scaffolding* (structure, boilerplate, page objects) than for *complete* tests — you'll still need to adjust selectors, test data, and environment config; expect to spend 20–30% of the time the manual approach would take
- Improving test coverage: "What code paths aren't covered by these tests?"
  - **Using `@workspace` or `#file` to analyse coverage gaps:** attach both the implementation file and the test file — "Looking at `#file:userService.ts` and `#file:user.test.ts`, what code paths, branches, and edge cases are not covered by the existing tests?"
  - **Copilot's coverage analysis:** Copilot will identify uncovered `if` branches, uncovered `catch` blocks, code paths that are only reachable with specific input combinations, and any functions that have no tests at all — this is not a substitute for a coverage tool but is faster for targeted gap analysis
  - **Coverage tool integration:** if your project uses Istanbul/nyc, Vitest's `--coverage` flag, or `go test -cover`, run the coverage report first and paste the output — "The coverage report shows `utils/dateParser.ts` has 42% coverage. Looking at `#file:utils/dateParser.ts`, what tests would increase coverage to above 90%?"
  - **Quality vs. quantity:** emphasise to participants that 100% line coverage with only happy-path assertions is worse than 70% line coverage with meaningful edge-case tests — prompt Copilot for meaningful tests, not just coverage numbers
- Mocking dependencies with Copilot
  - **What Copilot needs to generate mocks:** the interface or type of the dependency being mocked, the testing framework's mocking API (Jest `jest.fn()`, Vitest `vi.fn()`, Python `unittest.mock.patch`), and the specific methods that are called in the code under test
  - **The prompt pattern:** "Generate a mock for `EmailService` using `vi.fn()`. The mock should implement the `EmailService` interface from `#file:emailService.ts` and return a resolved Promise for `sendEmail`" — Copilot generates a correctly-typed mock object
  - **Module-level mocking:** "Mock the entire `nodemailer` module for tests in `emailService.test.ts` using Vitest's `vi.mock()`" — Copilot generates the module-level mock setup at the top of the test file and sets up spy assertions
  - **Copilot + msw:** for HTTP mocking, "Set up an msw handler that intercepts `POST /api/users` and returns a 201 with `{ id: '123', name: 'Alice' }`" — Copilot generates the handler, the server setup, and the `beforeEach`/`afterEach` wiring
- Demo: going from 0% to 80% test coverage on a small module in 10 minutes
  - **Setup:** a 120-line `orderProcessor.ts` module with zero tests — has 4 public functions, several branches, error handling, and one async operation
  - **Step 1 (2 min):** select the whole file, run `/tests` → Copilot generates 6–8 basic tests; run coverage → shows ~40%
  - **Step 2 (3 min):** paste the coverage output into Chat, ask "What branches are not covered? Write tests for them" → Copilot adds 5–6 more tests targeting uncovered branches; run coverage → shows ~65%
  - **Step 3 (3 min):** "Add edge-case tests for: null order, empty items array, negative quantity, and failed payment gateway response" → 4 more tests; coverage → ~80%+
  - **Debrief (2 min):** total time ~8 minutes of prompting; a developer would typically spend 60–90 minutes writing these tests manually — highlight the quality of the generated tests, not just the speed

### 2. Copilot for Documentation (15 min)
- Generating JSDoc / Javadoc / docstrings with `/doc`
  - **How it works:** select a function, class, or method; open inline Chat; run `/doc` — Copilot reads the selected code and generates a documentation block in the correct format for the language (JSDoc for JavaScript/TypeScript, Javadoc for Java, docstrings for Python, XML doc comments for C#)
  - **What Copilot infers automatically:** the parameter names and types (from the function signature), the return type and what it represents, the purpose of the function (from its name, body logic, and surrounding code), and any exceptions or error conditions that might be thrown
  - **The quality ceiling:** Copilot documents what the code *does*, not what the developer *intended* — if the function name is misleading or the logic is unclear, the generated doc will faithfully describe the wrong thing; always review generated docs for semantic accuracy, not just format
  - **Prompting for richer docs:** `/doc` alone produces minimal correct docs; follow up with "Also document: when callers should prefer this function over `formatDateShort`, what the performance characteristics are for large inputs, and include a usage example" — the follow-up transforms a skeleton into useful documentation
- Writing README files: "Create a README for this project based on its code and structure"
  - **The prompt pattern:** "`@workspace` Create a README.md for this project. Include: a one-paragraph description of what it does, installation instructions, quickstart usage example, a description of the main configuration options, and links to the test and deployment docs" — `@workspace` gathers the project structure, `package.json`, entry point, and existing docs to inform the generation
  - **What Copilot gets right:** project name, tech stack, file structure overview, command-line flags and environment variables (from the code), and basic usage patterns — all inferred from the codebase without you providing any extra input
  - **What you still need to supply:** the "why" (motivation, the problem it solves), the audience (who should use this and who shouldn't), production deployment considerations, known limitations, and the contribution guidelines — these aren't derivable from code and must be added manually or via a follow-up prompt
  - **Regenerating vs. patching:** for projects with existing READMEs, prefer "Here is the current README (`#file:README.md`). The API has changed — update the API reference section to reflect the new route signatures in `#file:routes.ts`" rather than regenerating the whole file, which would discard the human-written sections
- Generating API documentation from route handlers
  - **The pattern:** attach the router file — "Looking at the Express routes in `#file:routes/users.ts`, generate OpenAPI 3.0 YAML documentation for each route. Include the request body schema, path parameters, query parameters, and all possible response codes with example response bodies"
  - **Why it works well:** route handlers have highly structured, consistent patterns — HTTP method, path, middleware, request body parsing, response — Copilot has seen thousands of these in training and can reliably infer the OpenAPI schema from the implementation
  - **Docusaurus / Swagger UI integration:** follow up with "Format the output as a `swagger.yaml` file compatible with Swagger UI" or "Generate an MDX page for Docusaurus that renders this API reference" — Copilot will adjust the output format accordingly
  - **Limitations:** Copilot infers schema from the *code*, not from intended business rules — if validation is loose in the handler, the generated schema will be loose too; use this as a first draft and tighten the schema manually for public-facing APIs
- Keeping docs in sync: "This function has changed — update the docstring to match"
  - **The documentation rot problem:** in most codebases, inline documentation is written once and never updated — the implementation evolves but the docs stay describing the old behaviour; Copilot makes it easy to eliminate this debt incrementally
  - **The workflow:** whenever you refactor a function, select it + its docstring, use inline Chat: "This function has changed. The old docstring says it always returns a string, but now it can return null. Update the docstring to accurately describe the new behaviour"
  - **Copilot as documentation CI:** in Agent Mode or as a pre-commit hook trigger, ask "Compare the docstrings in `#file:userService.ts` to the actual function implementations. Identify any docstrings that are inaccurate or outdated and suggest corrections" — a regular audit prevents documentation rot from accumulating
  - **The "what changed" prompt:** after merging a PR, "`@workspace` Based on the changes in this PR, which existing docstrings need to be updated? List them and suggest updated text" — keeps docs in sync with code changes as they land
- Generating CHANGELOG entries from a diff or commit history
  - **The prompt pattern:** paste a git diff or a list of commit messages — "Here are the commits from the last release cycle. Generate a CHANGELOG entry in Keep a Changelog format, categorising changes as Added, Changed, Deprecated, Removed, Fixed, or Security"
  - **Using `@terminal`:** "`@terminal` I just ran `git log v1.2.0..HEAD --oneline`. Based on this commit history, generate a CHANGELOG entry for v1.3.0"
  - **From a diff:** for a single feature PR, select the diff text and ask "Summarise this diff as a single CHANGELOG entry written from the perspective of a user, not a developer — describe the *impact*, not the *implementation*"
  - **Quality tip:** raw commit messages are often written for developers ("fix null pointer in user service") — instruct Copilot to translate them into user-facing language ("Fixed a crash that occurred when users with incomplete profiles tried to update their settings")
- Demo: documenting an undocumented open-source utility with Copilot
  - **Setup:** a real undocumented utility (e.g., a 200-line `csvParser.ts` with no comments, no JSDoc, no README) — participants have 5 minutes to explore it manually; note how long it takes to understand just the public API
  - **Step 1:** select the entire file, run `/doc` — 30 seconds produces JSDoc for every function; compare quality to what a developer would write manually in 30 minutes
  - **Step 2:** "`@workspace` Create a README for this utility. Include a quickstart, a description of each public function, and a usage example for each" — 60 seconds for a complete README
  - **Step 3:** "The README doesn't mention error handling. Add a section describing what errors each function throws and how callers should handle them" — targeted follow-up adds the missing section
  - **Debrief:** the utility went from "impossible to use without reading all the source" to "fully documented with examples" in under 3 minutes of prompting — discuss: when is Copilot-generated documentation good enough to ship, and when does it need human review?

### 3. Copilot for Debugging & Root Cause Analysis (15 min)
- Pasting error messages directly into Copilot Chat
  - **The direct approach:** copy the full error message (including the stack trace) and paste it into Copilot Chat with a one-line description of what you were doing — "I got this error when calling `createUser` with an empty email string: [paste]" — Copilot identifies the error type, traces it to the likely source, and suggests a fix
  - **Include context, not just the error:** an error message alone tells Copilot less than an error message + the relevant code snippet + "I expect X to happen but Y happens instead" — the "expected vs. actual" framing is the most efficient way to communicate a bug
  - **For cryptic errors:** some errors (especially from compiled languages, native modules, or misconfigured environments) produce messages that are hard to Google — paste the full output verbatim without editing; Copilot handles compiler output, linker errors, Python tracebacks, JVM stack traces, and shell error codes with equal ability
  - **Asking for root cause vs. fix:** "What is causing this error?" and "How do I fix this error?" often produce different, complementary responses — ask for root cause first to understand the problem, then ask for the fix; combining them in one prompt sometimes produces a surface-level fix that doesn't address the underlying issue
- Using `@terminal` to explain a stack trace in the integrated terminal
  - **The workflow:** run the failing command in the VS Code integrated terminal → open Copilot Chat → type "`@terminal` Why did this fail and how do I fix it?" — Copilot reads the terminal output automatically (no copy-paste needed) and responds in the context of your project
  - **Why `@terminal` is better than copy-paste:** the terminal participant also knows your OS, your shell, your working directory, your recently run commands, and can sometimes infer your project type from the command — this context produces more targeted, environment-aware explanations
  - **For long stack traces:** VS Code's terminal may truncate very long output; if the trace is cut off, paste the relevant sections manually; the most important parts are usually the first line (error type) and the first few frames that reference your own code (not library internals)
  - **Shell command generation:** "`@terminal` Write a command that finds all `.log` files older than 7 days in the `/var/log` directory and deletes them" — Copilot generates a command tuned to your detected shell and OS, reducing the chance of syntax errors from cross-platform command differences
- "Rubber duck debugging" with Copilot: explain the problem, get a second opinion
  - **What rubber duck debugging is:** the classic technique of explaining a problem out loud to an inanimate object — the act of articulating the problem often reveals the solution; Copilot is a rubber duck that talks back
  - **The workflow:** open Chat and describe the bug as if explaining it to a senior colleague who hasn't seen your code — what the code is supposed to do, what it actually does, what you've already tried, and what you suspect — Copilot will often identify the issue from the description alone, before you've pasted any code
  - **When it's most valuable:** when you've been staring at a problem for too long and need a fresh perspective; when the bug is in your mental model of the code rather than the code itself; when you need to articulate assumptions before testing them
  - **The follow-up pattern:** start with a description, let Copilot ask clarifying questions or propose hypotheses, then paste the relevant code to confirm or refute each hypothesis — this structured debugging conversation is faster than randomly adding print statements
- Analysing logs: pasting log output and asking "what is wrong here?"
  - **The prompt:** paste a representative section of the log (not necessarily the whole log — 50–100 lines centred around the error) and ask "What does this log output indicate about what went wrong? What is the root cause?"
  - **Structured log formats:** if your logs are JSON (structured logging), Copilot can parse and analyse them more reliably than human-readable logs — "Here are 10 log entries in JSON format. Identify the pattern that precedes every `level: error` entry"
  - **Correlating events:** paste logs from multiple services around the same time window — "Here are logs from the API server and the background worker. The payment failed at 14:23:07. What happened in the 5 seconds before the failure?" — Copilot performs timeline correlation
  - **Generating log queries:** if your logs are in a query system (Kibana, Splunk, CloudWatch Insights, Datadog), ask "Write a KQL query that finds all requests where latency exceeded 2000ms and the HTTP status was not 5xx" — translates a human question into a query language
- Using `/fix` on code with a known failure mode
  - **How it works:** select a code block that has a bug (or that a test is failing against), open inline Chat, run `/fix` — Copilot analyses the selected code and suggests a corrected version as a diff; you accept, reject, or modify the suggestion
  - **Make the failure mode explicit:** `/fix` alone gives Copilot no information about what's wrong — add a brief description: "`/fix` This function crashes when `items` is an empty array because it calls `.reduce()` without an initial value" — the description dramatically improves fix accuracy
  - **When `/fix` works best:** specific, well-scoped bugs with a clear symptom (null pointer, off-by-one error, missing await, incorrect type coercion) — it's less effective for architectural problems or bugs that span multiple files
  - **Combining with test output:** copy a failing test's error output, then run "`/fix` The following test is failing: [paste test name and error]. Here is the implementation: [selected code]" — Copilot uses both the test expectation and the implementation to generate the minimal fix
- Demo: debugging a subtle async race condition with Copilot Chat
  - **Setup:** a Node.js function that occasionally (not always) sends duplicate notifications when two requests arrive within 100ms of each other — the bug is non-deterministic and doesn't appear in unit tests
  - **Step 1:** describe the symptom to Copilot Chat — "This function sends duplicate notifications intermittently when called concurrently. What could cause this?" — Copilot identifies: no mutex/lock, shared mutable state, async operation without atomicity guarantee
  - **Step 2:** paste the implementation — Copilot confirms: "The check-then-act on line 24 is a race condition. Between the `if (!sent)` check and the `sent = true` assignment, another call can pass the check before the flag is set"
  - **Step 3:** "How do I fix this? We're in Node.js and can't use threading primitives" — Copilot suggests an async mutex pattern or a Map-based deduplication with a TTL
  - **Step 4:** apply the fix, write a concurrent stress test to verify — Copilot helps write the test too
  - **Debrief:** the root cause was identified in 2 minutes of conversation — a human debugger might spend hours adding logs and trying to reproduce the race condition reliably; Copilot's value here is pattern recognition across thousands of similar race conditions in its training data

### 4. Copilot for Refactoring (15 min)
- Common refactoring requests:
  - "Extract this logic into a separate function"
    - **When to use it:** any block of code that has a meaningful name, does one thing, or appears more than once — Copilot will propose a name, signature, and placement for the extracted function; it also updates the call site automatically
    - **The prompt:** select the block to extract, use inline Chat: "Extract this block into a separate named function. Place it above the current function. Use a descriptive name based on what it does"
    - **Follow-up:** "Now add a JSDoc comment to the extracted function explaining its purpose and parameters"
  - "Convert this callback to async/await"
    - **Why it's a great Copilot task:** callback-to-promise-to-async/await migrations are mechanical and highly repetitive — Copilot handles the nesting correctly, preserves error handling (translating `.catch()` to `try/catch`), and avoids the common mistake of forgetting `await` inside a callback
    - **The prompt:** select the callback-heavy function, use inline Chat: "Convert this function to use async/await. Preserve all error handling. Do not change the function's public signature"
    - **Gotcha to watch for:** Copilot may not correctly handle `Promise.all` for parallel operations that were previously run concurrently in a callback pattern — review that the async version doesn't accidentally serialise operations that were previously parallel
  - "Replace this switch statement with a strategy pattern"
    - **When it makes sense:** switch statements that dispatch on a type or string value and where new cases are added regularly — the strategy pattern makes adding new cases additive rather than requiring changes to the dispatcher
    - **The prompt:** select the switch statement, inline Chat: "Replace this switch statement with a Strategy pattern. Create a separate handler object or class for each case. Register them in a map keyed by the switch value"
    - **Copilot's output:** generates a handler interface/type, one class per case, a registry map, and the dispatcher that looks up the handler — then removes the switch statement
  - "Reduce the cognitive complexity of this function"
    - **What cognitive complexity is:** a metric (used by SonarQube, ESLint, etc.) that measures how difficult a function is to understand based on nesting depth, boolean operators, and control flow jumps — a function with cognitive complexity > 15 is a maintenance risk
    - **The prompt:** "This function has a cognitive complexity score of 22. Refactor it to reduce the complexity below 10 without changing its behaviour. Consider: extracting nested conditions into named functions, replacing nested ternaries with early returns, and simplifying complex boolean expressions"
    - **Verify before and after:** run the complexity analysis tool before and after to confirm the improvement — Copilot's refactor may look simpler but may not actually reduce the metric if it moves complexity to another form
- Using Copilot Edits for large-scale refactors across multiple files
  - **When to use Edits mode over inline Chat:** any refactoring that touches more than 2–3 files — renaming a function used in 15 places, changing a shared type definition that propagates through the entire codebase, migrating a pattern (e.g., class components → function components) across a whole directory
  - **The workflow:** open Copilot Edits (Ctrl+Shift+I or ⌘+Shift+I), add all relevant files to the Edits file list, describe the refactor — "Rename `getUserById` to `fetchUserById` everywhere it appears. Update all call sites, the interface definition, and the test file" — Copilot generates diffs for all files simultaneously
  - **Staging and reviewing multi-file changes:** Copilot Edits presents each file's changes as a diff — review each file before accepting; use the "Accept All" only when you've verified that the changes across all files are consistent; reject individual file changes if Copilot has over-applied the refactor
  - **Agent Mode for complex refactors:** for refactors that require running tests to verify correctness (e.g., "Extract this service into a separate package and update all imports"), use Agent Mode — it can run the test suite after each change and iterate until all tests pass
- Refactoring for performance: "Rewrite this to avoid the N+1 query"
  - **What the N+1 problem is:** a loop that issues one database query per iteration (N queries for N items) instead of a single batch query — extremely common in ORM-heavy code; can turn a page load from 50ms to 5000ms as data grows
  - **The prompt:** select the problematic loop, inline Chat: "This loop issues a separate database query for each user in the array (N+1 problem). Rewrite it to use a single batch query using `findMany` with an `id` `IN` clause, then map the results back to the original array"
  - **Other performance refactoring patterns Copilot handles well:** replacing sequential `await` chains with `Promise.all` for parallel execution, adding memoisation to expensive pure functions, replacing O(n²) nested loops with O(n log n) approaches using sorting or hash maps, lazy-loading expensive computations
  - **Always benchmark:** performance refactors should be verified with actual measurements — ask Copilot to also "Add a benchmark test that measures the execution time for 1000 items before and after the refactor"
- Modernising legacy code: "Convert this to ES2024 syntax"
  - **What Copilot knows about:** ES2024 features (groupBy, Promise.withResolvers), TypeScript 5.x features (const type parameters, `satisfies` operator, decorator metadata), Python 3.12+ features, Java records, and most major language version transitions
  - **The prompt:** select a section of legacy code, inline Chat: "Convert this to modern TypeScript 5.4 syntax. Replace `var` with `const`/`let`, use optional chaining (`?.`) instead of manual null checks, use nullish coalescing (`??`) instead of `||` for defaults, and use template literals instead of string concatenation"
  - **Batch modernisation with Edits:** for a whole codebase migration, use Copilot Edits with all files added: "Update all files to use ES2024 syntax. Focus on: replacing `.then()/.catch()` chains with async/await, replacing manual `Object.assign` with spread operators, and using `Array.at()` instead of `arr[arr.length - 1]`"
  - **The safety net:** modernisation refactors should always be followed by running the full test suite — Copilot may modernise syntax in a way that subtly changes semantics (e.g., `||` vs `??` behaves differently for falsy-but-not-nullish values like `0` or `""`) — tests catch these semantic changes before they reach production

### 5. Copilot in the Pull Request Lifecycle (15 min)
- **Copilot-generated PR descriptions:** automatic summaries from the diff
  - **How to trigger it:** on GitHub.com, when creating or editing a PR, click the Copilot sparkle icon next to the description field — Copilot reads the full diff and generates a structured summary with sections for "What changed", "Why it changed", and "How to test it"
  - **What makes a good generated description:** Copilot's description quality is proportional to the quality of the commit messages and the scope of the PR — a focused PR with clear commits produces a clear description; a sprawling PR with "various fixes" commits produces a vague description; use this as a forcing function to keep PRs small and well-described
  - **Editing the generated description:** treat the generated description as a first draft — add context that can't be inferred from the code (business rationale, screenshots, links to the issue, known limitations, deployment notes); remove sections that are obvious from reading the diff
  - **Consistency at scale:** when all PRs use Copilot-generated descriptions as a starting point, the team converges on a consistent description format automatically — valuable for organisations with distributed teams or high PR volume
- **Copilot code review:**
  - How to request a Copilot review on a PR
    - **Requesting in the UI:** on the PR page on GitHub.com, in the "Reviewers" panel, type "Copilot" and select it — Copilot is listed as a reviewer alongside human reviewers; it begins the review immediately without waiting for a human to accept the request
    - **Who can use it:** available on GitHub.com for repositories where the Copilot Code Review feature is enabled by the org admin; individual users need a Copilot Individual, Business, or Enterprise subscription; Copilot appears as a reviewer only if the feature is enabled at the org level
    - **Review timing:** Copilot completes its review within 1–2 minutes for most PRs — faster than a human reviewer, which means developers can address obvious issues before a human reviews the same code, compressing the review cycle
  - Line-level suggestions with inline explanations
    - **What Copilot comments on:** logic errors (off-by-one, missing null checks, incorrect boolean logic), security anti-patterns (SQL injection risk, XSS risk, missing auth checks, exposed secrets), performance concerns (N+1 queries, unbounded loops), error handling gaps, and deviations from common best practices
    - **The explanation format:** every Copilot comment includes an explanation of *why* the change is suggested, not just *what* to change — this is more educational than traditional linter warnings and helps developers understand the principle, not just fix the instance
    - **What Copilot does NOT reliably catch:** business logic correctness (it can't know if the algorithm is solving the right problem), architectural concerns (whether this code belongs in this layer), product decisions (whether the feature behaviour is correct per spec), and team-specific conventions not covered by the instructions file
  - Accepting suggestions directly in the browser
    - **One-click apply:** Copilot review suggestions appear as code diff blocks in the PR comments; there is an "Apply suggestion" button that applies the change directly to the PR branch from the browser without needing to pull the branch locally
    - **Batching suggestions:** you can queue multiple suggestions and apply them all at once in a single commit — more efficient than applying one at a time; the batch commit message is generated automatically
    - **Dismissing suggestions:** use the "Dismiss" option with a comment explaining why you're not applying it — this feedback helps maintain a record of intentional decisions and may inform future Copilot review behaviour
- **Copilot Autofix:** security fixes generated automatically for CodeQL alerts
  - **What CodeQL is:** GitHub's semantic code analysis engine that identifies security vulnerabilities (SQL injection, path traversal, XSS, insecure deserialization, etc.) — runs as part of GitHub Advanced Security in CI; Autofix is the AI-powered fix suggestion layer on top of CodeQL results
  - **The workflow:** CodeQL finds a security alert → Copilot Autofix generates a suggested fix → the fix appears as a draft PR comment or code suggestion on the alert detail page → the developer reviews and applies it
  - **Why Autofix is more than just a text replacement:** security fixes often require understanding the dataflow through the code — Autofix uses CodeQL's semantic analysis + Copilot's code generation to produce fixes that correctly break the tainted data flow, not just add a surface-level sanitisation call
  - **Review is non-negotiable:** Autofix suggestions should always be reviewed by a developer before merging — Copilot may fix the specific vulnerability while inadvertently changing the behaviour of surrounding code, or may suggest a fix that passes the CodeQL check but is semantically incorrect for the business logic
  - **Coverage:** Autofix supports a growing set of CodeQL query types; not every CodeQL alert has an Autofix suggestion — the feature is most reliable for injection-class vulnerabilities and common CWEs in supported languages (JavaScript/TypeScript, Python, Java, C#, Ruby)
- **Issue-to-code:** describe a feature in an issue → ask Copilot to implement it (Copilot Workspace preview)
  - **What Copilot Workspace is:** a GitHub Next preview feature that takes a GitHub issue as input and produces a complete implementation plan, a set of file changes, and a branch-ready PR — the entire flow from "issue description" to "draft PR" without opening an IDE
  - **The workflow:** open a GitHub issue → click "Open in Workspace" → Copilot reads the issue and generates a step-by-step implementation plan → you review and approve the plan → Copilot generates the code → you review the diff → create a PR from the workspace
  - **What it does well:** well-defined, scoped features with clear acceptance criteria in the issue — "Add a `/health` endpoint that returns `{ status: 'ok', version: '1.2.3' }` with a 200 status code" produces a complete, correct implementation in most cases
  - **Current limitations:** Copilot Workspace is a preview feature with changing capability boundaries; it is best suited for greenfield features and struggles with refactoring tasks that require deep understanding of existing business logic; always treat output as a first draft requiring human review
  - **The workflow shift this enables:** issue authors can write richer, more detailed issues knowing that Copilot will implement them more faithfully — incentivises better requirements documentation upstream; developers shift from "writing code" to "reviewing AI-generated code" for routine features
- Demo: full PR lifecycle — write feature → open PR → Copilot describes it → Copilot reviews it → merge
  - **Setup:** a simple feature to add — e.g., a new API endpoint that returns paginated results for a list resource; the feature is complete but intentionally has 2 issues: a missing null check and an N+1 query
  - **Step 1 (IDE):** implement the feature with Copilot assistance; open the PR on GitHub.com
  - **Step 2 (GitHub.com):** click the Copilot sparkle to generate the PR description — show the structured output; add one missing business context sentence manually
  - **Step 3:** request Copilot as a reviewer — wait 60–90 seconds; show the inline comments; note that both intentional issues (null check + N+1) appear as review comments
  - **Step 4:** apply the null-check suggestion with one click; for the N+1 suggestion, show the explanation, then apply
  - **Step 5:** approve and merge; highlight the total time for a review cycle that would normally require scheduling time with a human reviewer
  - **Debrief:** Copilot didn't replace the human reviewer — it removed the easy issues before a human ever saw the PR, so the human review can focus on business logic correctness and architectural concerns that Copilot can't evaluate

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Test Coverage Sprint (20 min)
Participants receive a 150-line module with zero tests. Goal: reach as high test coverage as possible in 15 minutes using Copilot. Measure coverage with the language's standard tool (Jest, pytest, go test -cover). Share results — who got highest coverage?

### Exercise: Documentation Makeover (10 min)
Participants take their most poorly-documented function (from their own work or a provided sample) and use Copilot to generate full documentation. Peer review: is the generated doc accurate? Did it miss anything?

### Exercise: Debug This! (10 min)
Instructor presents a live bug (or uses a pre-written buggy snippet). The room collaborates to write the best Copilot prompt to identify and fix it. First correct answer wins.

### Exercise: PR Review Role-Play (10 min)
Participants open a sample PR on GitHub. They request a Copilot review. Then they role-play as the PR author: accept, reject, or ask for clarification on each Copilot suggestion. Discuss: how does Copilot's review differ from a human's?

---

## 🔗 Resources & References
- [Asking Copilot to generate tests](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide#generating-tests)
- [Using Copilot to write documentation](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide#generating-documentation)
- [Copilot code review on PRs](https://docs.github.com/en/copilot/using-github-copilot/code-review/using-copilot-code-review)
- [GitHub Copilot Autofix for CodeQL](https://docs.github.com/en/code-security/code-scanning/managing-code-scanning-alerts/about-autofix-for-codeql-code-scanning)
- [Copilot Workspace — issue to code](https://githubnext.com/projects/copilot-workspace)

---

## 🗒️ Facilitator Notes
- The test coverage sprint is a fan favourite — consider extending it if time allows
- For the PR demo, use a public repository so all participants can follow along in their browser
- Emphasise: Copilot across the lifecycle means *fewer context switches*, not just faster typing

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 5 — Speak AI's Language: Mastering Prompts & Context](../chapter-05/README.md) | [Chapter 7 — Level Up: Best Practices for AI-Powered Development →](../chapter-07/README.md)