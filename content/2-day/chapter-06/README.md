[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 5 — Speak AI's Language: Mastering Prompts & Context](../chapter-05/README.md) | [Chapter 7 — Level Up: Best Practices for AI-Powered Development →](../chapter-07/README.md)

---

# Chapter 6— AI Across the Entire Software Lifecycle

> **Duration:** 90 minutes | Day 2, 10:45 – 12:15

GitHub Copilot isn't just for writing new code. This chapter shows how Copilot accelerates every phase of the software development lifecycle — from requirements to deployment to incident response.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Use Copilot during the analysis phase to turn ideas, requirements, and unknown codebases into a structured plan
- Use Copilot to explore greenfield and brownfield options before implementation begins
- Use Copilot to generate, review, and improve tests (unit, integration, edge cases)
- Generate and maintain documentation with Copilot
- Use Copilot for debugging and root-cause analysis
- Apply Copilot to refactoring, code review, and security remediation
- Leverage Copilot on GitHub.com throughout the PR and issue lifecycle

---

## 📋 Content Outline

> The slide deck introduces seven lifecycle sections. The detailed notes below keep that same opening analysis focus, then continue into the deeper hands-on topics used in the workshop delivery.

### 1. Copilot for Analysis (15 min)
- Starting from scratch: turn a rough idea into structured requirements
  - **Prompt for clarity first:** "Act like a senior product engineer. Based on this product idea, list the main actors, workflows, constraints, assumptions, and open questions before we write code."
  - **Generate delivery-ready artifacts:** ask for user stories, acceptance criteria, non-functional requirements, error cases, and a first-cut backlog from a plain-language problem statement
  - **Turn ambiguity into decisions:** "What are the biggest unanswered questions in this brief?" and "What decisions should we make now vs. later?" help teams separate knowns from unknowns early
- Explore solution options before a line of code exists
  - **Architecture exploration:** ask Copilot to compare a monolith vs. modular monolith vs. microservice approach for the same requirements, including trade-offs, team fit, and operational complexity
  - **Design-first prompts:** generate API contracts, data models, event flows, and architecture decision records from the requirements draft so the team can review structure before implementation
  - **Risk discovery:** ask for likely failure modes, security concerns, scaling constraints, and integration risks; Copilot is strongest here as a brainstorming amplifier, not a source of final truth
- Working with existing code during analysis
  - **Repo orientation:** use `@workspace` to explain the system boundaries, key modules, entry points, and data flow in an unfamiliar codebase
  - **Hotspot discovery:** ask Copilot which parts of the code are tightly coupled, poorly documented, risky to change, or likely to block a feature or migration
  - **Modernization and migration analysis:** use prompts such as "What would it take to move this app to a layered architecture?" or "What are the likely breaking points if we upgrade this dependency?"
- Ask for outputs that are useful in real planning meetings
  - **Good output shapes:** decision tables, risk registers, migration plans, dependency maps, architecture options, and "questions we still need to answer"
  - **Healthy framing:** treat Copilot's output as a draft for discussion, then validate with the codebase, domain experts, and architecture constraints
- Demo: same analysis goal, two contexts
  - **Greenfield demo:** start with a two-paragraph idea and ask Copilot for requirements, architecture options, and an implementation outline
  - **Brownfield demo:** start with an unfamiliar repository and ask Copilot for a system summary, risk hotspots, and the safest path to add a new feature

### 2. Copilot for Development (10 min)
- Choose the right mode for the task: inline completions for momentum, Chat for guided generation, Agent Mode for multi-file execution
- Turn stories, tickets, or acceptance criteria into scaffolding, first-pass implementations, and implementation plans
- Ask Copilot to propose project structure, service boundaries, and task breakdowns before coding so the team starts from an agreed direction
- Contrast the same task across completion, chat, and agent workflows to show how context depth changes the result
- Start from the smallest useful artifact you already have
  - **Good starting points:** a user story, issue, acceptance criteria, API contract, UI mock-up, failing test, or even a code comment describing intent
  - **What to ask first:** "Which files are likely involved?", "What assumptions am I making?", and "Outline the smallest vertical slice that would satisfy this ticket"
  - **Why this matters:** Copilot gets better when the prompt is grounded in a concrete artifact instead of a vague feature request
- Ask for a plan before asking for code
  - **Prompt pattern:** "Based on this ticket and `#file:OrdersController.cs`, list the touched files, proposed changes, and risks before writing implementation code"
  - **Benefit:** this exposes missing context, reduces broad speculative changes, and gives you a reviewable blueprint before Copilot starts editing
  - **Talk track:** in development, the biggest win is often not code generation itself - it is reducing false starts
- Use context deliberately during feature work
  - **`#file` for local precision:** attach the interface, controller, test file, or existing service you want Copilot to imitate
  - **`@workspace` for broader navigation:** use it when you need to know where a concern already exists elsewhere in the codebase
  - **Working set discipline:** the more precisely you anchor the prompt, the less generic and more convention-aligned the result becomes
- Keep the human in the loop where it matters most
  - **Let Copilot accelerate:** scaffolding, repetitive wiring, boilerplate, CRUD surfaces, DTOs, basic tests, and pattern-following edits
  - **Slow down for human review:** business rules, naming, security boundaries, public API shape, error handling, data migrations, and edge cases
  - **A strong rule of thumb:** ask Copilot for the first 70-80%, then deliberately review the parts where a wrong assumption would be expensive
- Demo: from ticket to feature slice
  - **Step 1:** paste a short ticket or acceptance criteria and ask Copilot for a plan and touched files
  - **Step 2:** ask for a scaffold only - endpoint, service, types, and test skeletons following the existing project pattern
  - **Step 3:** review assumptions, tighten the prompt, and then let Copilot fill in the implementation with a smaller, safer scope

### 3. Copilot for Testing (25 min)
- Cover the full testing spectrum, not just unit tests
  - **Unit tests:** fastest feedback, narrow scope, ideal for pure logic and validation rules
  - **Integration tests:** verify multiple components working together - service + database, API + repository, or controller + dependency graph
  - **Automatic UI tests:** browser or UI automation for critical user journeys, usually via Playwright, Selenium, or similar tools
  - **Teaching point:** Copilot helps across all three, but the kind of help differs - strongest for scaffolding, strongest of all for repetitive setup
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
- BDD with Copilot and Reqnroll
  - **Why BDD belongs here:** BDD turns expected behaviour into shared language that product people, testers, and developers can all read
  - **Reqnroll example:** ask Copilot to draft a `.feature` file from acceptance criteria - "Write a Reqnroll feature for password reset with happy path, invalid token, and expired token scenarios"
  - **Step definitions:** once the feature file exists, ask Copilot to scaffold Reqnroll step definitions and connect them to page objects, APIs, or test doubles
  - **Best use of Copilot:** generating the repetitive glue code, scenario outlines, and missing step implementations - while humans still decide whether the scenarios are meaningful
- Generating integration and end-to-end test scaffolding
  - **The scope difference:** integration tests verify that multiple units work together correctly (e.g., a service + database layer); E2E tests verify that the whole system behaves correctly from a user's perspective (e.g., a browser-driven Playwright test); the prompts and context you provide differ significantly
  - **For integration tests:** attach the service file and the dependency file ("Generate an integration test for `UserService` that tests it against a real SQLite in-memory database using Prisma") — Copilot generates setup/teardown for the database, realistic test data, and assertions on the database state after each operation
  - **For E2E test scaffolding:** "Generate a Playwright test that covers the user registration flow: navigate to /register, fill in name/email/password, submit the form, and assert that the user is redirected to /dashboard" — Copilot generates page object patterns, selector strategies, and wait conditions
  - **Scaffolding vs. complete tests:** for integration and E2E tests, Copilot is more useful for *scaffolding* (structure, boilerplate, page objects) than for *complete* tests — you'll still need to adjust selectors, test data, and environment config; expect to spend 20–30% of the time the manual approach would take
- Talk track: where Copilot helps most in testing
  - **Fastest wins:** setup boilerplate, arranging fixtures, generating edge-case matrices, mocking dependencies, and expanding coverage gaps
  - **Places to slow down:** assertion quality, whether the behaviour matters, whether the test is too coupled to implementation details, and whether UI tests are worth their maintenance cost
  - **Practical rule:** let Copilot write the first draft of the test suite; let humans decide whether it protects the right thing
- Improving test coverage: "What code paths aren't covered by these tests?"
  - **Using `@workspace` or `#file` to analyse coverage gaps:** attach both the implementation file and the test file — "Looking at `#file:userService.ts` and `#file:user.test.ts`, what code paths, branches, and edge cases are not covered by the existing tests?"
  - **Copilot's coverage analysis:** Copilot will identify uncovered `if` branches, uncovered `catch` blocks, code paths that are only reachable with specific input combinations, and any functions that have no tests at all — this is not a substitute for a coverage tool but is faster for targeted gap analysis
  - **Coverage tool integration:** if your project uses Istanbul/nyc, Vitest's `--coverage` flag, or `go test -cover`, run the coverage report first and paste the output — "The coverage report shows `utils/dateParser.ts` has 42% coverage. Looking at `#file:utils/dateParser.ts`, what tests would increase coverage to above 90%?"
  - **Quality vs. quantity:** emphasise to participants that 100% line coverage with only happy-path assertions is worse than 70% line coverage with meaningful edge-case tests — prompt Copilot for meaningful tests, not just coverage numbers
- Demo: use the coverage report as the prompt
  - **Step 1:** run the coverage tool first (`vitest --coverage`, `dotnet test --collect:\"XPlat Code Coverage\"`, `go test -cover`, or similar) so you know where the real gaps are
  - **Step 2:** paste the uncovered file or summary into Copilot and ask for gap-closing tests rather than generic new tests
  - **Prompt pattern:** "The coverage report shows `OrderProcessor.cs` has 46% branch coverage. Based on `#file:OrderProcessor.cs`, suggest tests that would cover the missing branches and explain why each matters."
  - **Teaching point:** the report tells you where coverage is low; Copilot helps convert that signal into concrete test cases faster
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

### 4. Copilot for Documentation (15 min)
- Generating JSDoc / Javadoc / docstrings with `/doc`
  - **How it works:** select a function, class, or method; open inline Chat; run `/doc` — Copilot reads the selected code and generates a documentation block in the correct format for the language (JSDoc for JavaScript/TypeScript, Javadoc for Java, docstrings for Python, XML doc comments for C#)
  - **What Copilot infers automatically:** the parameter names and types (from the function signature), the return type and what it represents, the purpose of the function (from its name, body logic, and surrounding code), and any exceptions or error conditions that might be thrown
  - **The quality ceiling:** Copilot documents what the code *does*, not what the developer *intended* — if the function name is misleading or the logic is unclear, the generated doc will faithfully describe the wrong thing; always review generated docs for semantic accuracy, not just format
  - **Prompting for richer docs:** `/doc` alone produces minimal correct docs; follow up with "Also document: when callers should prefer this function over `formatDateShort`, what the performance characteristics are for large inputs, and include a usage example" — the follow-up transforms a skeleton into useful documentation
- Documenting for maintainers, not just for the compiler
  - **Explain the why:** ask Copilot to describe intent, assumptions, invariants, side effects, failure modes, and "when should I use this?" - the details future maintainers actually need
  - **Prompt pattern:** "Document this method for maintainers. Explain the business purpose, important side effects, error cases, and include a short usage example. Keep it accurate to the code in `#file:OrderService.cs`"
  - **Teaching point:** good inline documentation reduces rediscovery work; it should answer the questions a teammate will have six months from now, not just restate the type signature
- Creating Markdown documentation that lives inside the repository
  - **Docs as code:** ask Copilot to draft `README.md`, `/docs/*.md`, ADRs, onboarding guides, runbooks, troubleshooting notes, or feature-specific docs that are reviewed in the same PR as the code
  - **Prompt pattern:** "`@workspace` Create `docs/order-import.md` for new developers. Include the business flow, important files, configuration, failure modes, and a step-by-step local test path"
  - **Why repo-local docs matter:** Markdown in the repo is versioned, reviewed, branched, tagged with releases, and easier to keep aligned with the implementation than docs stored in a separate system
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
- Creating editable diagrams with draw.io and `.drawio.png`
  - **Why this format is useful:** a `.drawio.png` file renders directly inside Markdown like a normal image, but it still contains the draw.io source so the same file can be reopened and edited later
  - **Great documentation targets:** architecture overviews, sequence flows, integration boundaries, deployment diagrams, data flow maps, and "how this feature works" visuals beside the surrounding Markdown
  - **How Copilot helps:** ask it to draft the structure, labels, components, relationships, and the surrounding documentation section - for example, "Based on `@workspace`, outline the nodes and connections for a draw.io system context diagram of this app"
  - **Practical repo pattern:** store the diagram next to the Markdown and reference it as `![Architecture](architecture.drawio.png)` so the diagram previews in GitHub and remains editable in draw.io
- Keeping docs in sync: "This function has changed — update the docstring to match"
  - **The documentation rot problem:** in most codebases, inline documentation is written once and never updated — the implementation evolves but the docs stay describing the old behaviour; Copilot makes it easy to eliminate this debt incrementally
  - **The workflow:** whenever you refactor a function, select it + its docstring, use inline Chat: "This function has changed. The old docstring says it always returns a string, but now it can return null. Update the docstring to accurately describe the new behaviour"
  - **Copilot as documentation CI:** in Agent Mode or as a pre-commit hook trigger, ask "Compare the docstrings in `#file:userService.ts` to the actual function implementations. Identify any docstrings that are inaccurate or outdated and suggest corrections" — a regular audit prevents documentation rot from accumulating
  - **The "what changed" prompt:** after merging a PR, "`@workspace` Based on the changes in this PR, which existing docstrings need to be updated? List them and suggest updated text" — keeps docs in sync with code changes as they land
- Broaden "documentation" beyond API references
  - **Operational knowledge matters too:** use Copilot to turn incident notes, support tickets, and recurring setup questions into runbooks, troubleshooting guides, FAQ pages, and onboarding checklists
  - **Decision records are high-value docs:** ask Copilot to draft an ADR from a design discussion - problem, options considered, chosen approach, trade-offs, and consequences - then edit the final rationale with the team
  - **Release-facing docs:** changelog entries, release notes, migration notes, and "what changed for users" summaries are all good Copilot tasks because the AI can translate diffs into clearer language faster
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

### 5. Copilot for Debugging & Root Cause Analysis (15 min)
- Pasting error messages directly into Copilot Chat
  - **The direct approach:** copy the full error message (including the stack trace) and paste it into Copilot Chat with a one-line description of what you were doing — "I got this error when calling `createUser` with an empty email string: [paste]" — Copilot identifies the error type, traces it to the likely source, and suggests a fix
  - **Include context, not just the error:** an error message alone tells Copilot less than an error message + the relevant code snippet + "I expect X to happen but Y happens instead" — the "expected vs. actual" framing is the most efficient way to communicate a bug
  - **For cryptic errors:** some errors (especially from compiled languages, native modules, or misconfigured environments) produce messages that are hard to Google — paste the full output verbatim without editing; Copilot handles compiler output, linker errors, Python tracebacks, JVM stack traces, and shell error codes with equal ability
  - **Asking for root cause vs. fix:** "What is causing this error?" and "How do I fix this error?" often produce different, complementary responses — ask for root cause first to understand the problem, then ask for the fix; combining them in one prompt sometimes produces a surface-level fix that doesn't address the underlying issue
- Exceptions and stack traces: start from the first meaningful frame
  - **The debugging habit to teach:** look for the exception type, the first frame in your own code, the inputs that triggered it, and what the application was trying to do at that moment
  - **Prompt pattern:** "Here is the exception, stack trace, and the method on the first application frame. Explain the likely root cause, what assumptions failed, and what data I should inspect next."
  - **Why this works well with Copilot:** stack traces are structured evidence; Copilot is good at recognizing recurring failure shapes such as null dereferences, invalid state transitions, missing awaits, serialization mismatches, and dependency misconfiguration
- Using `@terminal` to explain a stack trace in the integrated terminal
  - **The workflow:** run the failing command in the VS Code integrated terminal → open Copilot Chat → type "`@terminal` Why did this fail and how do I fix it?" — Copilot reads the terminal output automatically (no copy-paste needed) and responds in the context of your project
  - **Why `@terminal` is better than copy-paste:** the terminal participant also knows your OS, your shell, your working directory, your recently run commands, and can sometimes infer your project type from the command — this context produces more targeted, environment-aware explanations
  - **For long stack traces:** VS Code's terminal may truncate very long output; if the trace is cut off, paste the relevant sections manually; the most important parts are usually the first line (error type) and the first few frames that reference your own code (not library internals)
  - **Shell command generation:** "`@terminal` Write a command that finds all `.log` files older than 7 days in the `/var/log` directory and deletes them" — Copilot generates a command tuned to your detected shell and OS, reducing the chance of syntax errors from cross-platform command differences
- Give your Copilot agent eyes
  - **Browser automation as evidence collection:** when the bug is visible in the UI, let the agent drive the app with Playwright and inspect what the user would see - clicks, typed input, page state, accessibility tree, screenshots, console errors, and network requests
  - **Why this matters:** many defects are easier to understand from behaviour than from source alone - broken navigation, missing UI state, validation messages that never appear, elements hidden by CSS, failed API calls, or client-side exceptions
  - **Practical prompt:** "Use Playwright to reproduce the checkout bug. Capture the failing step, inspect console errors, note the network request that fails, and summarize what the UI did versus what it should have done."
  - **Other 'eyes' to feed it:** screenshots, DOM snapshots, HAR/network traces, browser console logs, performance traces, and recordings from synthetic or manual repro sessions
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
- Logs, traces, and metrics together beat any one signal alone
  - **Logs tell you what happened:** error messages, inputs, branch decisions, and local context
  - **Traces tell you where time went:** which service, dependency, or downstream call dominated the request path
  - **Metrics tell you how bad and how widespread it is:** spike patterns, error rate, latency percentiles, throughput drops, or queue growth
  - **Prompt pattern:** "Here are the logs, a trace summary, and the metric spike window for the same incident. Correlate them and tell me the most likely cause, the weakest hypothesis, and what I should verify next."
- Using `/fix` on code with a known failure mode
  - **How it works:** select a code block that has a bug (or that a test is failing against), open inline Chat, run `/fix` — Copilot analyses the selected code and suggests a corrected version as a diff; you accept, reject, or modify the suggestion
  - **Make the failure mode explicit:** `/fix` alone gives Copilot no information about what's wrong — add a brief description: "`/fix` This function crashes when `items` is an empty array because it calls `.reduce()` without an initial value" — the description dramatically improves fix accuracy
  - **When `/fix` works best:** specific, well-scoped bugs with a clear symptom (null pointer, off-by-one error, missing await, incorrect type coercion) — it's less effective for architectural problems or bugs that span multiple files
  - **Combining with test output:** copy a failing test's error output, then run "`/fix` The following test is failing: [paste test name and error]. Here is the implementation: [selected code]" — Copilot uses both the test expectation and the implementation to generate the minimal fix
- Create a debugging loop, not just a one-off prompt
  - **Step 1 - reproduce:** define the scenario precisely - user action, input data, environment, and expected vs actual behaviour
  - **Step 2 - collect evidence:** capture the exception, stack trace, logs, traces, metrics, screenshots, network calls, or a Playwright reproduction
  - **Step 3 - narrow scope:** ask Copilot which component, code path, or assumption is most likely wrong and what smaller repro would isolate it
  - **Step 4 - verify the fix:** ask for a regression test, a better log, or a targeted metric/query so the same issue becomes easier to catch next time
- More debugging ideas worth mentioning
  - **Compare a good run to a bad run:** ask Copilot to identify the first divergence in logs, network calls, or UI state
  - **Generate observability queries:** KQL, Splunk, Datadog, App Insights, or SQL queries that isolate the failure population faster
  - **Write a minimal repro harness:** a unit test, integration test, script, or Playwright scenario that turns a flaky bug into something repeatable
  - **Improve the app for future debugging:** better error messages, structured logging, correlation IDs, feature flags, health endpoints, and timings are all valid AI-assisted debugging outcomes
- Demo: debugging a subtle async race condition with Copilot Chat
  - **Setup:** a Node.js function that occasionally (not always) sends duplicate notifications when two requests arrive within 100ms of each other — the bug is non-deterministic and doesn't appear in unit tests
  - **Step 1:** describe the symptom to Copilot Chat — "This function sends duplicate notifications intermittently when called concurrently. What could cause this?" — Copilot identifies: no mutex/lock, shared mutable state, async operation without atomicity guarantee
  - **Step 2:** paste the implementation — Copilot confirms: "The check-then-act on line 24 is a race condition. Between the `if (!sent)` check and the `sent = true` assignment, another call can pass the check before the flag is set"
  - **Step 3:** "How do I fix this? We're in Node.js and can't use threading primitives" — Copilot suggests an async mutex pattern or a Map-based deduplication with a TTL
  - **Step 4:** apply the fix, write a concurrent stress test to verify — Copilot helps write the test too
  - **Debrief:** the root cause was identified in 2 minutes of conversation — a human debugger might spend hours adding logs and trying to reproduce the race condition reliably; Copilot's value here is pattern recognition across thousands of similar race conditions in its training data

### 6. Copilot for Refactoring (15 min)
- Ask for architectural improvements that make sense in the current system
  - **Good architectural prompts are constrained:** "Given `#file:OrdersController.cs` and `#file:OrderService.cs`, suggest architectural refactors that reduce coupling without turning this into a full rewrite"
  - **What to ask for:** clearer boundaries, extracted orchestration, better dependency direction, smaller responsibilities, fewer hidden side effects, and safer extension points
  - **What to avoid:** vague prompts like "make the architecture better" often produce over-engineered answers; ask Copilot to respect the current app size, team size, and deployment model
- Use evidence to drive refactoring priorities
  - **Feed real signals in:** logs, metrics, traces, flame graphs, profiler output, and performance test results give Copilot a factual starting point instead of guesswork
  - **Prompt pattern:** "These traces show p95 latency spikes in `OrderService`. Based on `#file:OrderService.cs` and this trace summary, what refactors would most likely reduce the hot path cost?"
  - **Why this is powerful:** Copilot can correlate hotspots in telemetry with the code structures likely causing them - repeated DB access, serialization churn, synchronous blocking, excessive allocation, or retry storms
- Improve readability, not just runtime behaviour
  - **Readability refactors are high-leverage:** better names, flatter control flow, extracted intention-revealing helpers, smaller functions, and reduced branching make the next change cheaper
  - **Prompt pattern:** "Refactor this method for readability. Preserve behaviour, keep the public API unchanged, and prioritise early returns, better naming, and smaller helper methods"
  - **Teaching point:** readability is not cosmetic; readable code lowers bug risk and makes future AI suggestions better because the intent is clearer in the code itself
- Create seams before large changes
  - **What a seam is:** a place where you can change behaviour without changing everything at once - an interface, adapter, wrapper, gateway, or extracted boundary around messy legacy code
  - **Why seams matter:** they let you refactor incrementally, introduce tests around a legacy area, and migrate toward a better design without freezing delivery
  - **Prompt pattern:** "Show me how to introduce a seam around this payment provider integration so we can refactor it safely in smaller steps"
- Refactor for observability and maintainability too
  - **Not every refactor is about cleaner code:** sometimes the right move is adding structured logging, clearer error boundaries, correlation IDs, or timing hooks so the next problem becomes diagnosable
  - **Useful prompt:** "Refactor this flow so errors are surfaced more clearly and the code emits logs and timings at the points we need for diagnosis"
- Talk track: more refactoring ideas worth mentioning
  - **Reduce duplication:** consolidate repeated logic into shared abstractions only when the duplication is truly the same behaviour
  - **Split bloated classes or services:** ask Copilot to identify responsibilities that should move apart
  - **Untangle dependency graphs:** ask where a class depends on too many collaborators or where the dependency direction is wrong
  - **Prepare for migration:** use Copilot to sketch a strangler pattern, anti-corruption layer, or adapter-based transition when replacing legacy components
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

### 7. Copilot in the Pull Request Lifecycle (15 min)
- **Copilot-generated PR descriptions:** automatic summaries from the diff
  - **How to trigger it:** on GitHub.com, when creating or editing a PR, click the Copilot sparkle icon next to the description field — Copilot reads the full diff and generates a structured summary with sections for "What changed", "Why it changed", and "How to test it"
  - **What makes a good generated description:** Copilot's description quality is proportional to the quality of the commit messages and the scope of the PR — a focused PR with clear commits produces a clear description; a sprawling PR with "various fixes" commits produces a vague description; use this as a forcing function to keep PRs small and well-described
  - **Editing the generated description:** treat the generated description as a first draft — add context that can't be inferred from the code (business rationale, screenshots, links to the issue, known limitations, deployment notes); remove sections that are obvious from reading the diff
  - **Consistency at scale:** when all PRs use Copilot-generated descriptions as a starting point, the team converges on a consistent description format automatically — valuable for organisations with distributed teams or high PR volume
- Reviewable pull requests start before you click "Create PR"
  - **Use Copilot before the PR exists:** ask it for a touched-file plan, likely reviewer concerns, missing tests, rollback risk, and whether the change should be split before you open the branch for review
  - **Prompt pattern:** "Based on this diff and the ticket, what would make this PR easier to review? Suggest missing tests, unclear naming, hidden risks, and whether the scope should be split."
  - **Teaching point:** the best PR workflow win is often not faster review comments - it is opening a smaller, clearer PR in the first place
- Ask Copilot to help write the review checklist
  - **Good checklist topics:** business rule changes, security-sensitive paths, migrations, feature flags, observability, backward compatibility, performance impact, and how to test
  - **Why this helps:** reviewers miss fewer important concerns when the PR body already frames what changed and what deserves extra scrutiny
  - **Practical prompt:** "Draft a PR checklist for this change. Include what a reviewer should verify manually and what should already be covered by tests."
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
- Copilot review and human review should complement each other
  - **What Copilot is great at:** obvious null checks, risky string building, suspicious loops, missing awaits, inconsistent conditions, and other pattern-level issues
  - **What humans should focus on:** product correctness, architecture fit, data migration risk, naming in domain language, and whether the change solves the right problem
  - **Talk track:** Copilot removes easy-to-miss technical issues early so human reviewers can spend their limited attention on the expensive questions
- **Copilot Autofix:** security fixes generated automatically for CodeQL alerts
  - **What CodeQL is:** GitHub's semantic code analysis engine that identifies security vulnerabilities (SQL injection, path traversal, XSS, insecure deserialization, etc.) — runs as part of GitHub Advanced Security in CI; Autofix is the AI-powered fix suggestion layer on top of CodeQL results
  - **The workflow:** CodeQL finds a security alert → Copilot Autofix generates a suggested fix → the fix appears as a draft PR comment or code suggestion on the alert detail page → the developer reviews and applies it
  - **Why Autofix is more than just a text replacement:** security fixes often require understanding the dataflow through the code — Autofix uses CodeQL's semantic analysis + Copilot's code generation to produce fixes that correctly break the tainted data flow, not just add a surface-level sanitisation call
  - **Review is non-negotiable:** Autofix suggestions should always be reviewed by a developer before merging — Copilot may fix the specific vulnerability while inadvertently changing the behaviour of surrounding code, or may suggest a fix that passes the CodeQL check but is semantically incorrect for the business logic
  - **Coverage:** Autofix supports a growing set of CodeQL query types; not every CodeQL alert has an Autofix suggestion — the feature is most reliable for injection-class vulnerabilities and common CWEs in supported languages (JavaScript/TypeScript, Python, Java, C#, Ruby)
- Security review content worth calling out
  - **Ask for a plain-language explanation:** "Explain this CodeQL alert and the Autofix suggestion in terms a reviewer can understand before approving it."
  - **Use it as a teaching moment:** Autofix is not only a patch generator - it is a way to show why the vulnerability exists and what safe coding pattern replaces it
  - **Broaden beyond the single alert:** ask Copilot whether similar patterns exist elsewhere in the diff or codebase so the review addresses the class of problem, not only one instance
- **Issue-to-code:** describe a feature in an issue → ask Copilot to implement it (Copilot Workspace preview)
  - **What Copilot Workspace is:** a GitHub Next preview feature that takes a GitHub issue as input and produces a complete implementation plan, a set of file changes, and a branch-ready PR — the entire flow from "issue description" to "draft PR" without opening an IDE
  - **The workflow:** open a GitHub issue → click "Open in Workspace" → Copilot reads the issue and generates a step-by-step implementation plan → you review and approve the plan → Copilot generates the code → you review the diff → create a PR from the workspace
  - **What it does well:** well-defined, scoped features with clear acceptance criteria in the issue — "Add a `/health` endpoint that returns `{ status: 'ok', version: '1.2.3' }` with a 200 status code" produces a complete, correct implementation in most cases
  - **Current limitations:** Copilot Workspace is a preview feature with changing capability boundaries; it is best suited for greenfield features and struggles with refactoring tasks that require deep understanding of existing business logic; always treat output as a first draft requiring human review
  - **The workflow shift this enables:** issue authors can write richer, more detailed issues knowing that Copilot will implement them more faithfully — incentivises better requirements documentation upstream; developers shift from "writing code" to "reviewing AI-generated code" for routine features
- Turn the whole issue-to-PR flow into a conversation
  - **Upstream quality matters:** richer issues produce better plans, better diffs, and better PR descriptions
  - **Good prompts here:** ask Copilot to convert an issue into acceptance criteria, a task list, a risk list, and then later a PR summary and reviewer checklist
  - **Useful end-of-flow prompt:** "Summarize this PR for three audiences: the reviewer, the release notes, and the support team."
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

### Exercise 601: Short URL Discovery Sprint (15 min)
Participants receive a greenfield brief for a .NET 10 short URL service that must generate aliases, redirect users, and track usage statistics. Their job is to use Copilot to turn that into actors, requirements, architecture options, risks, ADRs, and an implementation plan before they vibe code a first solution.

### Exercise 602: Expression Evaluator Test Lab (20 min)
Participants use an existing .NET 10 infix/postfix expression evaluator to experiment with four test workflows: writing tests on existing code, TDD, BDD with Reqnroll, and getting code coverage. The emphasis is comparing how Copilot helps with each testing style instead of treating testing as one monolithic activity.

### Exercise 603: Optimize Edit Distance (20 min)
Participants start from a deliberately slow .NET 10 Levenshtein edit-distance implementation, measure it with logs, metrics, tests, and BenchmarkDotNet, then use Copilot to refactor the algorithm into something faster without changing the result.

### Exercise 605: Document GuildOps Like It Will Outlive You (15 min)
Participants use Copilot to draft inline docs, repo-local Markdown docs, an ADR, and a draw.io diagram saved as `.drawio.png` for a .NET 10 solution. Peer review focuses on accuracy, usefulness, and whether the docs would help the next developer.

### Exercise 606: Hunt the Phantom Checkout Bug (20 min)
Participants debug a flaky .NET 10 web flow using exceptions, stack traces, logs, traces, network evidence, and Playwright-driven reproduction. The exercise teaches them to give Copilot eyes and to leave behind a regression test or stronger observability signal.

### Exercise 607: The Pull Request Gauntlet (15 min)
Participants turn an issue into a reviewable PR package: better PR body, reviewer checklist, Copilot review, and CodeQL/Autofix discussion. They compare what Copilot should catch automatically versus what still needs human judgment.

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