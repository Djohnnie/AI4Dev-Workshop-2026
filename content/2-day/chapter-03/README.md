[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 2 — Meet Your New Best Friend: GitHub Copilot](../chapter-02/README.md) | [Chapter 4 — Let Your AI Co-Pilot Take the Wheel →](../chapter-04/README.md)

---

# Chapter 3— Power with Purpose: Using AI Responsibly

> **Duration:** 90 minutes | Day 1, 13:15 – 14:45

AI assistance comes with real responsibilities. This chapter equips developers with the knowledge to use GitHub Copilot in a way that is secure, legal, ethical, and trustworthy — without being paralysed by fear.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Describe what data GitHub Copilot sends to the cloud and what it retains
- Apply GitHub Copilot's content exclusion features to protect sensitive files
- Identify categories of risk when using AI-generated code (IP, security, correctness)
- Evaluate a Copilot suggestion critically before accepting it
- Explain GitHub's responsible AI principles and how they apply to Copilot

---

## 📋 Content Outline

### 1. How Copilot Handles Your Code (20 min)
- What data is sent to the model
  - **Prompt assembly happens locally first:** the IDE extension builds a request from the prompt, code around your cursor, referenced files, relevant open tabs, imports, instructions, and other nearby context before anything is sent
  - **What actually travels:** your chat prompt or completion request, the surrounding code, attached selections or files, and IDE metadata such as language, file path context, repository context, and sometimes diagnostics
  - **Not your whole machine:** Copilot does not automatically upload your entire repository or continuously mirror your editor session; it sends the context needed for the current request
- No keystrokes are streamed
  - **No raw keylogging:** Copilot does not send every keystroke as you type
  - **Requests are event-based:** data is sent when you ask Chat something or when the editor requests a completion
  - **Important distinction:** the prompt may contain text you just typed, but that is part of a request payload, not a continuous feed of every edit
- Sensitive data risk: treat prompt context as leaving the machine
  - **Anything in prompt context can be transmitted:** code, comments, variable names, config values, open files, repository names, internal hostnames, and copied secrets can all become part of the request
  - **Open tabs matter:** relevant files you have open may be used as context even if you did not explicitly paste them into Chat
  - **Practical rule:** never keep hardcoded secrets, customer data, or sensitive internal details in code or prompts you would not want sent to a hosted service
- Is your data used to train future models?
  - **Copilot Individual:** GitHub may use prompts, suggestions, and related interaction data for product improvement if the user allows it in settings
  - **Copilot Business and Enterprise:** prompts and code are not used to train GitHub's foundation models; this is one of the key enterprise assurances
  - **Retention still exists:** requests may still be retained briefly for abuse detection, security, and service reliability even when training is disabled
- What telemetry is tracked
  - **Usage analytics:** feature usage, request counts, accept/reject rates, latency, failures, and similar product signals
  - **Policy and filter events:** events such as blocked suggestions, public-code matching signals, and quality/safety outcomes may be recorded for service operation
  - **Administrative visibility:** organisations can review Copilot usage metrics and audit information, especially on Business and Enterprise plans
- GitHub Copilot architecture at a high level
  - **Local client:** the IDE extension gathers context, formats the prompt, and shows suggestions
  - **GitHub proxy/control plane:** authentication, policy enforcement, filtering, routing, and safety checks happen in the service layer
  - **Hosted models:** the actual inference is performed by cloud-hosted models, and the result is returned to the IDE
- Using your own models for local workflows
  - **Advanced alternative:** for demos or privacy-sensitive experiments, you can build the same pattern with your own local model instead of a hosted Copilot model
  - **Example:** Ollama can run a local coding model on your own machine and serve it over a local API
  - **Tradeoff:** you gain local control, but often give up quality, context length, speed, or convenience compared with frontier hosted models
- Exercise 301 — Tool Calls with Ollama
  - **Workshop bridge:** reuse the Exercise 104 tool-calling idea, but swap the hosted model for a local Ollama model
  - **Learning goal:** show that tool calling is an application pattern, not something tied to one specific cloud provider
  - **Debrief:** compare local control, setup effort, and model quality against the hosted version

### 2. Intellectual Property & Licensing (15 min)
- The "public code" filter: how Copilot avoids reproducing verbatim licensed snippets
  - **What the filter does:** before returning a completion, Copilot checks whether the suggestion closely matches known public code in GitHub's index; if a match above a similarity threshold is found, the suggestion is blocked and an alternative is offered (or no suggestion is returned)
  - **What "verbatim" means here:** the filter targets near-exact reproduction of substantial code blocks — it does not flag code that is merely *inspired by* or *structurally similar to* public code; short snippets (under ~150 characters) are typically exempt from the check
  - **The filter is opt-in for Individual, on by default for Business/Enterprise:** Individual users must enable "Suggestions matching public code: Blocked" in their GitHub Copilot settings; Business and Enterprise plans have this protection built into their IP indemnity terms
  - **Limitations:** the filter operates at suggestion time, not at training time — it reduces the risk of verbatim reproduction but does not guarantee it; novel synthesis of patterns from multiple copyleft-licensed sources is not detected
  - **How to enable:** GitHub.com → Settings → Copilot → "Suggestions matching public code" → set to "Blocked"; confirm the status bar icon reflects the updated policy
- Duplication detection and the `suggestions matching public code` setting
  - **Where to find the setting:** GitHub account settings → Copilot → "Suggestions matching public code" — options are "Allowed" (default for Individual) and "Blocked"
  - **What "Blocked" does in practice:** when a completion would reproduce code that matches public GitHub content above the similarity threshold, Copilot silently skips that suggestion and either returns an alternative or nothing — you may notice fewer completions in edge cases
  - **Organisation-level enforcement:** on Business/Enterprise, an admin can enforce "Blocked" across all users in the org via the Copilot policy settings — individual users cannot override this
  - **The GitHub.com code search connection:** the matching index is built from GitHub's public code search infrastructure — the same database that powers `github.com/search`; it is updated periodically, not in real-time
  - **What to communicate to legal teams:** this setting is the primary technical control for IP risk mitigation; combined with IP indemnity (Business/Enterprise), it represents GitHub's layered approach to copyright protection
- The ongoing legal landscape — what we know, what's still evolving
  - **Key active cases (as of 2026):** *Doe v. GitHub, Inc.* (class action alleging Copilot violates open-source licenses by reproducing licensed code without attribution) remains in litigation; outcome will have significant implications for the industry
  - **What courts have decided so far:** no definitive ruling on whether training on public code constitutes copyright infringement; early rulings have been mixed; the legal landscape varies significantly by jurisdiction (US, EU, UK diverge)
  - **The "fair use" argument:** GitHub and OpenAI argue that training on publicly available code constitutes transformative fair use; critics argue that reproduction of substantial portions without attribution violates licence terms, particularly copyleft licences (GPL, AGPL)
  - **What's relatively settled:** generating *new* code that is functionally similar but not verbatim is generally considered lower risk; reproducing identifiable, substantial blocks from copyleft-licensed works without attribution is the higher-risk scenario
  - **Facilitator guidance:** acknowledge uncertainty honestly; do not make legal claims; point participants toward their organisation's legal counsel for specific guidance; the goal is awareness, not legal advice
  - **Practical takeaway for participants:** the legal picture is unsettled, which is exactly why the "public code filter" and careful review practices matter — they reduce risk while the courts catch up to the technology
- Practical guidance: treat Copilot suggestions like code from Stack Overflow — review it, don't blindly copy it
  - **The Stack Overflow analogy:** most developers copy-paste from Stack Overflow with varying degrees of scrutiny; the accepted practice is to understand the code, verify it fits the context, and adapt it — Copilot suggestions deserve the same treatment
  - **Understand before accepting:** if you can't explain what a Copilot suggestion does line by line, don't accept it — use `/explain` first; accepting code you don't understand is a liability regardless of its source
  - **Fit for context:** a Copilot suggestion that's technically correct in isolation may be wrong for your specific use case — wrong data types, missing error handling, mismatched conventions, or incompatible library versions
  - **Attribution in practice:** some organisations require noting AI assistance in commit messages, PR descriptions, or code comments; check your organisation's policy; GitHub's Copilot IP indemnity applies regardless of whether you attribute
  - **The "junior developer" mental model:** treat Copilot like a capable junior developer who writes fast but doesn't know your specific codebase, business rules, or quality standards — every contribution needs a review
- Attribution and documentation: noting AI-assisted code in your workflow
  - **Why attribution matters:** even in the absence of a legal requirement, documentation of AI assistance helps teams understand how code was produced, identify sections that may need extra scrutiny, and build institutional knowledge about where AI assistance is most effective
  - **Commit message conventions:** some teams add a `[AI-assisted]` tag or `Co-authored-by: GitHub Copilot` trailer to commits that contain significant AI-generated code; this is optional but increasingly common in open-source projects
  - **PR description disclosure:** Copilot's auto-generated PR descriptions can include a note that the implementation was AI-assisted — some teams make this a PR template requirement
  - **Code comments:** for complex AI-generated algorithms, a comment like `// Generated with Copilot assistance — reviewed and tested` can signal to reviewers that extra scrutiny is warranted
  - **No universal standard yet:** there is no industry-wide convention for AI attribution in code — teams should define their own policy explicitly rather than assuming; the important thing is consistency, not the specific format chosen

### 3. Security Considerations (20 min)
- Common vulnerabilities AI tends to generate: SQL injection, path traversal, insecure defaults, hardcoded credentials
  - **Why AI generates insecure code:** Copilot learns from public code — and public code contains a lot of vulnerable code; the model optimises for "looks correct and compiles", not "is secure"; security rarely appears as a clear pattern in training examples the way syntax does
  - **SQL injection:** Copilot frequently generates string-concatenated queries (`"SELECT * FROM users WHERE id = " + userId`) rather than parameterised queries — especially when the surrounding code doesn't already use an ORM or query builder; always check that user input is never interpolated directly into SQL strings
  - **Path traversal:** file system operations generated by Copilot often lack path sanitisation — `fs.readFile(req.params.filename)` is a classic Copilot output that allows attackers to read arbitrary files; look for any file I/O that uses unsanitised user input
  - **Insecure defaults:** Copilot may generate cryptographic code with weak algorithms (MD5, SHA1 for passwords), insufficient entropy, or disabled SSL verification (`verify=False` in Python requests); it tends to use whatever was common in its training data, which includes a lot of legacy patterns
  - **Hardcoded credentials:** when writing example or test code, Copilot sometimes inserts placeholder credentials that look real (`password = "admin123"`, `api_key = "sk-..."`) — these get committed, found by secret scanning, and occasionally make it to production
  - **Incomplete error handling:** Copilot's happy-path completions often omit error handling entirely, or handle it in ways that leak stack traces and implementation details to end users — a common source of information disclosure vulnerabilities
- **GitHub Copilot Autofix**: automatic vulnerability remediation in PRs (powered by CodeQL)
  - **What Autofix does:** when GitHub's CodeQL static analysis detects a vulnerability in a PR, Copilot Autofix automatically generates a suggested fix — a diff shown inline on the PR — that the author can review and apply with one click
  - **How CodeQL + Copilot work together:** CodeQL identifies the specific vulnerability type and location with high precision; Copilot then generates a contextually appropriate fix (not a generic patch) using the surrounding code as context
  - **Supported vulnerability categories:** SQL injection, XSS, path traversal, SSRF, command injection, insecure deserialization, and many more — the full list mirrors CodeQL's query suite
  - **The review-and-apply workflow:** the Autofix suggestion appears as a code change in the PR's security alerts tab; the author reviews, tests, and either applies the fix (one click) or dismisses it and writes their own; it does not auto-merge
  - **Availability:** Copilot Autofix is available to all organisations with GitHub Advanced Security (GHAS) enabled — which is included in GitHub Enterprise Cloud; it's free for open-source public repositories
  - **Demo opportunity:** create a PR with a deliberate SQL injection vulnerability, trigger a CodeQL scan, and show the Autofix suggestion appearing — one of the most visually impressive Copilot features for security-conscious audiences
- Secret scanning integration — Copilot now blocks secrets in suggestions
  - **The problem:** LLMs trained on public code have seen a lot of accidentally-committed secrets (API keys, tokens, connection strings); they can reproduce patterns that look like real secrets, or even reproduce actual leaked secrets from training data
  - **GitHub's secret scanning layer:** GitHub's push protection and secret scanning run on the repository side — if a secret is committed, it's detected and the push may be blocked; this is separate from Copilot but works in concert
  - **Copilot-side blocking:** Copilot now includes a filter that detects when a completion would contain a token matching the pattern of a known secret type (e.g., `ghp_` for GitHub PATs, `AKIA` for AWS access keys) and suppresses that suggestion
  - **What this doesn't cover:** the filter catches *patterns*, not semantics — a developer who manually types a secret into Copilot Chat is not protected; the filter applies to completions, not to chat responses that include secrets the user pasted in
  - **Training participants:** the key behaviour change is simple — never paste real secrets, tokens, connection strings, or credentials into Copilot Chat prompts, even to "help Copilot understand the context"; use placeholder values or environment variable references instead
- Never paste secrets, keys, or PII into Copilot Chat prompts
  - **Why this matters:** the Copilot Chat prompt is transmitted to an external model API; any data you paste becomes part of the request payload and may be logged temporarily by GitHub's infrastructure
  - **Common mistakes to avoid:** pasting a `.env` file to ask "what's wrong with my config", including a real JWT in a "can you decode this?" question, sharing a database connection string to ask about query optimisation, pasting customer data to demonstrate a data processing bug
  - **Safe alternatives:** replace real values with clearly fake placeholders (`DB_PASSWORD=REPLACE_ME`, `token=<YOUR_TOKEN_HERE>`); use `#file` to reference config file *structure* without pasting contents; describe the secret type without including the value
  - **PII is also off-limits:** pasting customer names, email addresses, health records, or financial data into Chat prompts may violate GDPR, HIPAA, or your organisation's data handling policies regardless of Copilot's data retention commitments
  - **Establish team norms:** include "no secrets or PII in Copilot Chat" in your team's AI usage policy as an explicit written rule, not an assumed convention
- Using Copilot to *find* vulnerabilities with `/fix` and `/explain`
  - **Copilot as a security reviewer:** use Copilot Chat to proactively audit your own code before it reaches PR review; ask "Review this function for security vulnerabilities" or "What could go wrong with this input handling code?" as a fast first pass
  - **`/explain` for security analysis:** select a suspicious function and use `/explain` to get a plain-English walkthrough; then follow up with "Could an attacker exploit any of this?" — Copilot often identifies injection points, missing validation, and insecure patterns when prompted directly
  - **`/fix` on known-bad code:** paste a snippet known to have a vulnerability and ask `/fix` — useful as a training exercise (show before/after) and also useful when you've identified a bug and want a suggested remediation to evaluate
  - **Limitations:** Copilot is not a security scanner — it has no data flow analysis, no taint tracking, and no understanding of your threat model; it will miss context-specific vulnerabilities and can generate false positives; treat it as a "fast, informal first opinion", not a replacement for SAST tools
  - **Combining with CodeQL:** the most robust approach is Copilot Chat for exploratory review during development + CodeQL (via GitHub Actions or VS Code extension) for rigorous automated scanning — defence in depth
- Demo: deliberately insecure code → Copilot Autofix in action
  - **Suggested demo script:** prepare a small Express.js route with a raw SQL query using string concatenation; commit it to a branch; open a PR; trigger a CodeQL scan (either manually or via GitHub Actions); wait for the Autofix suggestion to appear in the PR security tab; walk through the suggested fix
  - **What to highlight:** the specificity of the Autofix suggestion (it's not a generic "use parameterised queries" comment — it's actual corrected code for *this* route); the one-click apply workflow; the explanation of why the original code is vulnerable
  - **Pre-record this:** CodeQL scans can take 5–10 minutes depending on repo size; pre-record the scan completion and Autofix suggestion appearing, and play the recording rather than waiting live — then show the live result of applying the fix

### 4. Content Exclusions & Enterprise Controls (10 min)
- Repository-level content exclusions (`copilot_content_exclusion` in settings)
  - **What content exclusions do:** instruct the Copilot extension to *not* use content from specified files or directories when assembling prompts — excluded file content is never sent to the model, even if the file is open in the editor
  - **How to configure (repository level):** add a `.github/copilot-instructions.md` or configure via repository Settings → Copilot → Content exclusions; specify file glob patterns (e.g., `secrets/**`, `*.pem`, `config/production.yml`)
  - **How to configure (organisation level):** GitHub org admins can set content exclusions that apply across all repositories in the organisation via the organisation's Copilot settings page
  - **What "excluded" means in practice:** the Copilot extension will show the file content in the editor as normal — exclusions only affect Copilot's prompt assembly; the file is still readable, editable, and committable; only AI assistance is disabled for those files
  - **Verification:** after configuring exclusions, open an excluded file — the Copilot status bar icon should change to indicate "Copilot disabled for this file"; test by typing in the file and confirming no ghost text appears
- Excluding files with sensitive logic, proprietary algorithms, or regulated data
  - **Sensitive logic and proprietary algorithms:** if your pricing engine, fraud detection model, or cryptographic implementation represents competitive IP, exclude it from Copilot's context — not because Copilot "leaks" it, but as a defence-in-depth measure and to satisfy legal/compliance teams who may not be comfortable with any external transmission
  - **Regulated data patterns:** files likely to contain PII or regulated data — `migrations/`, `fixtures/`, `test/data/`, seed files — should be excluded as a precaution; even synthetic test data can be mistaken for real data in a prompt
  - **Configuration files with environment-specific values:** `config/production.json`, `.env.production`, `terraform.tfvars` — files where real infrastructure details, endpoints, or credentials may appear even after secret scanning
  - **Recommended default exclusions:** `.env*`, `*.pem`, `*.key`, `*.p12`, `secrets/**`, `credentials/**`, any directory named `private/` or `internal/` — create a standard exclusion template that teams can adopt and extend
  - **Communicate exclusions to the team:** document which files are excluded and why in the repository's contributing guide — developers should know why Copilot isn't helping in certain files rather than assuming it's broken
- Organisation-level policy management (GitHub Copilot Business/Enterprise admin controls)
  - **Copilot seat management:** organisation admins can assign and revoke Copilot seats per user or team; revoked seats immediately disable Copilot for that user across all IDEs and github.com
  - **Feature toggles:** admins can enable or disable specific Copilot features org-wide — for example, disabling Copilot in the CLI, restricting Agent Mode, or limiting which AI models are available to users
  - **Content exclusion inheritance:** org-level exclusions apply to all repositories in the org and cannot be overridden by repository-level settings — allows security teams to enforce baseline exclusions without relying on individual repo owners
  - **Audit logs:** Copilot Business/Enterprise includes audit log events for key Copilot actions — seat assignments, policy changes, content exclusion updates — accessible via the GitHub audit log and exportable to SIEM tools
  - **Policy review cadence:** recommend that organisations review Copilot policies quarterly as the product evolves; new features (Agent Mode, MCP, Extensions) may require new policy decisions that didn't exist when the initial rollout was configured
- Blocking specific topics or domains via system prompt (Enterprise)
  - **What the system prompt is:** on Copilot Enterprise, organisation admins can configure a system-level prompt that is injected into every Copilot Chat request — before the user's message — to shape Copilot's behaviour across the organisation
  - **Use cases for topic blocking:** "Do not provide advice on cryptocurrency or blockchain topics", "Do not generate code that connects to external APIs not in our approved vendor list", "Always recommend our internal logging library over third-party alternatives"
  - **Use cases for positive direction:** the system prompt can also be used to *add* context — "This organisation uses our internal `AcmePay` payment SDK for all payment processing; prefer it over Stripe or Braintree suggestions" — effectively an org-wide custom instruction
  - **Limitations:** system prompts influence but do not absolutely control model output; a determined user could still elicit responses outside the intended scope; the system prompt is a guardrail, not a hard block (that requires content exclusions or seat revocation)
  - **Access:** system prompt configuration is in the organisation's Copilot settings under "Policies" — available to Copilot Enterprise administrators only

### 5. Responsible AI Principles — GitHub's Approach (10 min)
- Microsoft/GitHub Responsible AI framework: fairness, reliability, privacy, inclusivity, transparency, accountability
  - **Fairness:** AI systems should treat all people equitably and not amplify biases present in training data; for Copilot, this means monitoring whether suggestions are consistently worse for code in certain languages, or whether it generates discriminatory patterns in user-facing logic (e.g., biased filtering criteria)
  - **Reliability & safety:** systems should perform as expected, fail safely, and not cause unintended harm; Copilot's reliability principle drives the ongoing investment in reducing hallucinations, improving suggestion accuracy, and ensuring the model doesn't generate dangerous code (e.g., malware, exploit code)
  - **Privacy & security:** user data should be protected and people should have control over how their information is used; this is the direct foundation for Copilot's no-training-on-Business/Enterprise-code commitment, the content exclusion system, and the secret-blocking filter
  - **Inclusivity:** AI should work well for all users, regardless of background, language, or experience level; Copilot's multilingual code support and the investment in supporting a wide range of programming languages (not just the most popular) reflects this principle
  - **Transparency:** users should understand what AI systems are doing and why; Copilot surfaces this through model cards, the ability to see which model is being used, the public code matching disclosure, and documentation of what data is collected
  - **Accountability:** people and organisations should be accountable for AI systems; GitHub's IP indemnity, the Copilot Trust Center, and the ongoing public commitment to the RAI framework are expressions of institutional accountability — but the developer retains personal accountability for the code they accept and ship
- Copilot's model card and known limitations
  - **What a model card is:** a structured document (originally proposed by Google researchers) that describes an AI model's intended use, performance characteristics, known limitations, and ethical considerations — analogous to a data sheet for hardware
  - **Where to find it:** GitHub publishes information about Copilot's capabilities and limitations in the [GitHub Copilot Trust Center](https://resources.github.com/copilot-trust-center/) and in the Copilot documentation; specific model cards for the underlying models (GPT-4o, Claude) are published by OpenAI and Anthropic respectively
  - **Known limitations to surface:** Copilot performs worse on less-represented programming languages; it may reproduce patterns from outdated library versions; it has no real-time knowledge (training cutoff applies); it cannot verify business logic correctness; it may generate culturally biased variable names or comments
  - **Why this matters for the workshop:** participants who understand Copilot's limitations calibrate their trust appropriately — they know when to be skeptical without needing to distrust everything
- Bias in code generation: when Copilot reflects the biases of its training data
  - **What bias looks like in code:** variable names that reflect cultural assumptions (gender-binary role names, Western-centric date formats as defaults), algorithmic patterns that embed historical biases (e.g., using protected characteristics as features in decision trees), comments and docstrings that use exclusionary or non-inclusive language
  - **Representation bias:** code in less common languages, frameworks, or paradigms is underrepresented in training data — Copilot will suggest lower-quality, less idiomatic code for Elixir than for Python, not because Elixir is harder, but because there's less Elixir on GitHub
  - **Recency bias:** security practices, library versions, and API conventions from 2019–2021 (when training data was heavily weighted) may be suggested over more current alternatives; Copilot may suggest deprecated APIs that still appear in older public repos
  - **What developers can do:** be alert to culturally specific assumptions in generated code; use `/explain` to interrogate reasoning; provide explicit constraints in prompts ("use inclusive variable names", "use the latest stable API"); review generated code with the same lens you'd apply to a human contributor
  - **Organisational response:** include bias awareness in your team's AI code review checklist; the same DEI lens applied to hiring and communication should extend to AI-generated code that your team ships
- The developer's obligation: AI is a *tool*, the developer is still accountable
  - **The fundamental principle:** no matter how the code was generated — typed by hand, copied from Stack Overflow, or suggested by Copilot — the developer who accepts, commits, and ships it owns it; there is no "Copilot wrote it" defence for a security incident or a product bug
  - **Professional accountability:** software engineers have a professional obligation to their users, their organisation, and the wider public to ensure the code they ship is correct, secure, and appropriate; AI assistance does not change or diminish this obligation
  - **The "one-click trap":** the convenience of accepting a Copilot suggestion with `Tab` can erode the critical thinking that would accompany manually typing the same code; be intentional — accept when you understand and agree, not simply because the suggestion appeared
  - **Documentation and traceability:** teams that want to maintain accountability should be able to trace who reviewed AI-generated code, when, and what they checked; this is achieved through rigorous PR reviews, clear commit messages, and team norms around AI disclosure
  - **Reframe as empowerment:** accountability is not a burden — it is what makes developers irreplaceable; the human in the loop, exercising judgment and owning outcomes, is exactly what separates AI-assisted software from fully automated code generation, and it is where the professional value lies

### 6. Building Trust: When to Trust Copilot, When to Question It (10 min)
- High-confidence scenarios: boilerplate, well-known patterns, test scaffolding
  - **Boilerplate and scaffolding:** CRUD endpoint structure, class constructors, interface implementations, configuration file templates, standard middleware setup — Copilot has seen these patterns millions of times and produces consistently accurate output; accept with light review
  - **Well-known algorithms:** sorting, searching, string manipulation, date arithmetic, standard data structure operations — these are heavily represented in training data with many correct implementations; still verify edge cases, but the logic is likely sound
  - **Test scaffolding:** generating the structure of a test file, `describe`/`it` blocks, mock setup boilerplate, `beforeEach`/`afterEach` hooks — Copilot is reliable here; the test *assertions* still need your judgment, but the scaffolding can be accepted quickly
  - **Translating between languages:** converting Python to TypeScript, converting a callback pattern to async/await, rewriting a SQL query as an ORM call — well-defined transformations where correctness is verifiable by inspection
  - **Documentation for simple functions:** JSDoc/docstrings for a function with clear inputs and outputs — easy to verify the generated description matches the actual implementation
- Lower-confidence scenarios: business logic, security-sensitive code, novel algorithms
  - **Business logic:** code that encodes your organisation's specific rules — pricing calculations, eligibility criteria, compliance checks, approval workflows — Copilot has no knowledge of these rules and will generate plausible-looking but potentially wrong implementations; always validate against the actual requirements document or business owner
  - **Security-sensitive code:** authentication, authorisation, session management, cryptography, input sanitisation, file system access — any code where a mistake creates a vulnerability rather than just a bug; treat every Copilot suggestion here with the same scrutiny you'd apply to code from an untrusted contributor
  - **Novel or domain-specific algorithms:** algorithms specific to your industry (actuarial calculations, physics simulations, financial models, medical imaging processing) — Copilot's training data may have very few examples of correct implementations; it will generate *something* that looks plausible but may be subtly wrong in ways that aren't obvious
  - **Integrations with internal APIs:** Copilot doesn't know your internal service interfaces, internal conventions, or undocumented behaviours — generated integration code will often use the *shape* of public APIs it has seen, not your actual internal contract; always check against internal documentation
  - **Performance-critical code:** Copilot optimises for correctness and readability, not performance; hot paths, tight loops, and high-throughput data processing code needs measurement-driven optimisation that Copilot cannot provide
- The "trust but verify" mindset — every suggestion is a *proposal*, not a decision
  - **The mental shift:** receiving a Copilot suggestion should trigger the same mental process as receiving a PR from a colleague — "does this look right? Do I understand it? Does it fit the context? Are there edge cases it missed?" — not "great, accepted"
  - **Verification proportional to risk:** light review for boilerplate (does it compile, does it follow our naming conventions?); thorough review for business logic (does it match the spec? what happens with invalid input?); rigorous review plus testing for security code (can I think of any way to exploit this?)
  - **Use the tools available:** `/explain` before accepting unfamiliar code; run the tests after accepting; use a linter or type checker to catch type errors Copilot missed; commit-by-commit review in your PR
  - **The "delete test":** if you deleted the Copilot suggestion and wrote the same thing yourself, would you review it differently? If yes — apply that same scrutiny to the Copilot version; the suggestion's origin doesn't change its risk
  - **Build the habit early:** developers who build the verification habit from the start avoid the "Copilot surprise" — discovering months later that accepted suggestions contained bugs or security issues that rigorous review would have caught
- Code review still matters — arguably more so with AI
  - **The review bottleneck shifts:** with AI assistance, code is written faster — which means *more code reaches review*, not less; code reviewers will see higher volumes of AI-assisted code, making rigorous review skills more critical, not less
  - **New review questions for AI-generated code:** in addition to standard review criteria (correctness, readability, testability), reviewers should ask: does the author understand this code? Are there signs of uncritical AI acceptance (inconsistent style, variable names that don't fit the codebase, unnecessary complexity)?
  - **Reviewers as the last line of defence:** content exclusions, secret scanning, and Autofix reduce *some* risks automatically, but reviewers catch what automated tools miss — subtly wrong business logic, architecture mismatches, and code that is technically correct but doesn't belong in this codebase
  - **Copilot can assist reviewers too:** reviewers can use `@workspace` and `#file` to quickly understand unfamiliar code, use `/explain` on a suspicious function, or ask "what edge cases does this code miss?" — AI assistance accelerates both sides of the review
  - **Team norms to establish:** explicit agreement on what "reviewed" means when AI was used to generate code; whether AI-generated sections require a second reviewer; how to handle suggestions that look correct but weren't verified — these conversations should happen proactively, before an incident forces them

---

## 💡 Ideas for Exercises & Interactivity

### Exercise: Spot the Vulnerability (15 min)
Provide 5 short code snippets — some safe, some with common vulnerabilities (SQL injection, hardcoded API key, XSS). Participants decide: accept or reject? Use Copilot Chat `/fix` and `/explain` to analyse each one. Debrief: did Copilot catch what you missed?

### Exercise: Content Exclusion Setup (10 min)
Participants configure content exclusions on a sample repository. Add a `secrets/` directory and verify Copilot stops suggesting content from those files.

### Scenario Discussion: "Should I use Copilot here?" (10 min)
Small groups are given three scenarios:
1. Writing a GDPR-compliant data anonymisation function
2. Adding a login form with OAuth
3. Implementing a novel proprietary pricing algorithm

Groups debate: should they use Copilot? With what safeguards? Share back.

### Poll: Privacy Comfort Levels (5 min — Mentimeter)
Anonymous poll: "Which of these would you be comfortable pasting into Copilot Chat?" (Code with PII, internal API URLs, hardcoded passwords, customer data samples). Discuss results.

---

## 🔗 Resources & References
- [GitHub Copilot Privacy Statement](https://docs.github.com/en/site-policy/privacy-policies/github-general-privacy-statement)
- [Copilot for Business — data handling](https://docs.github.com/en/copilot/overview-of-github-copilot/about-github-copilot-for-business)
- [Content exclusions for GitHub Copilot](https://docs.github.com/en/copilot/managing-copilot/configuring-and-auditing-content-exclusion)
- [GitHub Copilot Autofix](https://docs.github.com/en/code-security/code-scanning/managing-code-scanning-alerts/about-autofix-for-codeql-code-scanning)
- [Microsoft Responsible AI principles](https://www.microsoft.com/en-us/ai/responsible-ai)

---

## 🗒️ Facilitator Notes
- This session often sparks strong opinions — welcome debate, keep it productive
- Emphasise: responsible use is a *competitive advantage*, not a constraint
- The legal landscape is evolving; acknowledge uncertainty rather than making absolute claims
- Have a printed one-pager of "Copilot Do's and Don'ts" to hand out

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 2 — Meet Your New Best Friend: GitHub Copilot](../chapter-02/README.md) | [Chapter 4 — Let Your AI Co-Pilot Take the Wheel →](../chapter-04/README.md)