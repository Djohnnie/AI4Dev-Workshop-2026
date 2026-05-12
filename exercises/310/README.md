# Exercise 310 — Code Obfuscator MCP Tool

> **Chapter:** Chapter 3, Exercise 10  
> **Skill focus:** Building and using a Model Context Protocol (MCP) tool; combining multiple workshop skills in a real deliverable  
> **Difficulty:** ⭐⭐⭐⭐

← Back to [Exercise Index](../README.md)

---

## 🎯 Overview

This is the **capstone exercise**. Rather than implementing an isolated algorithm, you are working on a production-style service: an **ASP.NET Core web application** that exposes a tool to AI assistants via the **Model Context Protocol (MCP)**.

The tool itself performs code obfuscation — the same technique you reversed-engineered in [Exercise 203](../203/README.md). Understanding how obfuscation works (from the inside this time) and then wrapping it in a standard protocol that any AI assistant can call is the perfect synthesis of the week's themes: AI foundations, responsible use, prompting, the full SDLC, and agentic tool-building.

---

## 📚 Background: Model Context Protocol (MCP)

### What Is MCP?

The **Model Context Protocol** is an open standard (developed by Anthropic, adopted across the industry) that defines how AI assistants can call external **tools** in a structured, type-safe way. Think of it as a contract between an AI model and the world outside it:

```
AI assistant  ──(calls)──►  MCP Server  ──(executes)──►  Your Tool
              ◄──(returns)──            ◄──(result)────
```

When an AI is given access to an MCP server, it can:
- Discover what tools are available (via the `tools/list` endpoint)
- Call a tool with structured parameters (via `tools/call`)
- Receive a structured result it can reason about and incorporate into its response

### Why Does This Matter for Developers?

MCP turns your code into **AI-callable functions**. Anything you can write as a function — searching a database, calling an internal API, running a linter, deploying to staging — can be exposed as an MCP tool and invoked by an AI assistant working autonomously in Agent Mode. This is the foundation of **agentic AI development**.

### MCP in the .NET Ecosystem

The `ModelContextProtocol.AspNetCore` NuGet package integrates MCP into ASP.NET Core with a clean attribute-based API:

```csharp
[McpServerToolType]
public class MyTool
{
    [McpServerTool, Description("Does something useful")]
    public static string DoWork(
        [Description("The input to process")] string input)
    {
        return Process(input);
    }
}
```

ASP.NET Core handles serialisation, HTTP transport, and tool discovery automatically.

---

## 📚 Background: The Obfuscation Pipeline

The `CodeObfuscatorTool` applies four transformations to C# source code, in order:

### Step 1 — Strip Block Comments

Removes `/* ... */` style comments, including XML documentation blocks (`/** */`).

```csharp
// Regex: /\*[\s\S]*?\*/
```

### Step 2 — Strip Line Comments

Removes `//` and `///` style comments to the end of each line.

```csharp
// Regex: //[^\r\n]*
```

### Step 3 — Rename Identifiers

This is the most significant step. Local variable names and method parameters are renamed to short generated names (`v0`, `v1`, `v2`, ...).

Rules:
- Only identifiers of **3 or more characters** are renamed (very short names are assumed intentional).
- **PascalCase** identifiers are preserved (these are typically class names, method names, or type names that are part of the public API).
- **C# reserved keywords** (`int`, `string`, `return`, `if`, `for`, etc.) are never renamed.

The result is that a method like:

```csharp
public int CalculateDistance(string source, string target)
{
    int sourceLength = source.Length;
    int targetLength = target.Length;
    ...
}
```

becomes something like:

```csharp
public int CalculateDistance(string v0, string v1)
{
    int v2 = v0.Length;
    int v3 = v1.Length;
    ...
}
```

### Step 4 — Collapse Whitespace

Removes unnecessary blank lines and reduces multiple consecutive spaces/newlines to single ones, making the code denser and harder to scan.

---

## 🗂️ Project Structure

```
310/
└── CodeObfuscator/
    ├── Program.cs                  ← ASP.NET Core entry point; registers MCP
    ├── CodeObfuscator.csproj       ← Web project (.NET 10)
    └── Tools/
        └── CodeObfuscatorTool.cs   ← The MCP tool implementation
```

### `Program.cs`

Minimal ASP.NET Core startup. Registers the MCP server with `AddMcpServer()`, maps the HTTP transport endpoint, and adds the `CodeObfuscatorTool` to the tool registry.

### `CodeObfuscatorTool.cs`

The meat of the exercise. Contains:

- The `[McpServerToolType]` class `CodeObfuscatorTool`
- The `[McpServerTool]` method `ObfuscateCode(string code)` — the tool callable by AI assistants
- Four private helper methods, one per obfuscation step, each using `Regex`
- A static list of C# reserved keywords to preserve during identifier renaming
- Source-generated `Regex` instances (using `[GeneratedRegex]`) for performance

---

## ✅ Your Task

This is an open-ended capstone exercise. Choose one or more of the following tracks based on your interests and the time available.

### Track A — Explore and Understand

1. Start the MCP server:

```bash
cd 310/CodeObfuscator
dotnet run
```

2. Configure an MCP client (Claude Desktop, or Copilot with MCP support) to connect to the local server.
3. Ask the AI to call the `ObfuscateCode` tool with a C# snippet of your choice.
4. Examine the output — compare it with [Exercise 203's](../203/README.md) `C0.cs`.

### Track B — Extend the Tool

Using Copilot's Agent Mode, add one or more new capabilities:

- **`DeobfuscateCode` tool** — attempt to reverse the obfuscation by generating meaningful names using an LLM (hint: call the Azure OpenAI API from inside the tool).
- **`ObfuscateWithLevel` parameter** — add an `int level` parameter (1–3) where level 1 only strips comments, level 2 also renames, level 3 also collapses whitespace.
- **Support for additional languages** — add basic obfuscation for TypeScript or Python.

### Track C — Build Your Own MCP Tool

Design and implement an entirely new MCP tool from scratch. Ideas:

- A tool that analyses C# code and returns a complexity score (cyclomatic complexity)
- A tool that checks for common security anti-patterns in code snippets
- A tool that formats a commit message from a diff summary
- A tool that searches the exercises solution for implementations of a named algorithm

Use the `CodeObfuscatorTool` as your template for the MCP attribute pattern.

---

## 🤖 Copilot Skills That All Come Together Here

| Skill from earlier exercises | How it applies |
|------------------------------|---------------|
| Understanding obfuscated code (Ex. 203) | You now know what the tool produces — and why |
| Prompting with context (Ex. 204) | Agent Mode needs precise prompts to generate correct ASP.NET Core code |
| Test generation (Ex. 205) | Write tests for the obfuscation pipeline |
| Documentation (Ex. 206) | The MCP `[Description]` attributes ARE the documentation — they are shown to the AI |
| Token awareness (Ex. 101) | Large code files sent through the tool consume tokens — be mindful of context window use |

---

## 💡 Tips

- **`[Description]` attributes are prompts.** The text in each `[Description]` on your tool parameters is what the AI sees when deciding how to call your tool. Write them as you would write a good prompt — specific, clear, with examples if useful.
- **Stateless tools are simpler.** MCP tools work best when they are pure functions: same input always produces the same output. Avoid storing state in the tool class.
- **Test the tool as a library first.** Before worrying about MCP, unit-test `ObfuscateCode` directly. Write a test that obfuscates a known snippet and asserts the output.
- **Use Agent Mode to build.** When adding new features, use Copilot Agent Mode with `@workspace` context — it can see the existing tool and follow the same patterns automatically.

---

## 🏁 Completion Criteria

You have completed the exercise when:

- [ ] The MCP server starts without errors.
- [ ] An AI client can discover the `ObfuscateCode` tool via the `tools/list` endpoint.
- [ ] Calling the tool with a C# snippet returns obfuscated code with comments stripped and variables renamed.
- [ ] You can explain how the identifier renaming regex works — including why PascalCase is preserved.
- [ ] (Stretch) At least one additional tool or capability has been added and tested.

---

← Back to [Exercise Index](../README.md) | Previous: [Exercise 206](../206/README.md)
