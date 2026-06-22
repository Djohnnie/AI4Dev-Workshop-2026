# Exercise 501 — Context Window Copilot Clone

> **Chapter:** Chapter 5, Exercise 1  
> **Skill focus:** Building a tiny Visual Studio Copilot clone and comparing no context, FIM context, and open tabs context with GPT-4o  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise builds a small **Visual Studio 2026 extension** that acts like a tiny self-written GitHub Copilot clone. The command opens a dialog where you enter a prompt and choose one of three context levels:

- **No context**
- **FIM context**
- **Open tabs context**

The extension sends the prompt to a **GPT-4o** model and shows how much better the model responds when it can see code around the cursor or the files you already have open.

---

## 🗂️ Project Structure

```
exercise-501/
├── ContextPromptExtension.csproj
├── ContextPromptPackage.cs
├── ContextPromptCommand.cs
├── ContextPromptDialog.cs
├── ContextPromptOrchestrator.cs
├── VisualStudioContextCollector.cs
├── AzureOpenAiChatService.cs
├── ContextPromptExtension.vsct
├── source.extension.vsixmanifest
└── README.md
```

---

## ▶️ Build, Run & Debug

This is a **Visual Studio extension (VSIX)**, not a console app. It **must be built and run from Visual Studio 2026** — it cannot be built with `dotnet build` (the .NET SDK CLI does not run the VSSDK packaging targets).

1. Open `ContextPromptExtension.csproj` directly in **Visual Studio 2026**.
2. Make sure the **Visual Studio extension development** workload is installed.
3. Press **F5**. Visual Studio builds the `.vsix`, deploys it to the **experimental instance**, and launches a second Visual Studio (`devenv.exe /rootsuffix Exp`) with the extension loaded.
4. In that second Visual Studio, open any code file, **right-click in the editor**, and choose **"Ask with Context..."**.

> The first Visual Studio stays attached as the debugger, so you can set breakpoints in the extension code.

If the menu item does not appear, reset the experimental instance and rebuild:

```powershell
# Adjust the path to your VS 2026 install if needed
& "C:\Program Files\Microsoft Visual Studio\18\Insiders\Common7\IDE\CreateExpInstance.exe" /Reset /VSInstance=18.0_Insiders /RootSuffix=Exp
```

---


### No context
Sends only the prompt.

### FIM context
Sends a fixed number of lines before and after the cursor, plus the active line split at the caret position.

### Open tabs context
Sends the active file and the full contents of the other open tabs with their file names, so the model can follow the same relationships your editor already shows you.

The dialog also shows a **Composed prompt preview** pane so you can inspect the full prompt/context payload. It refreshes when you change the context mode.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Prompt a VSIX scaffold | Ask Copilot to create the package, command, and dialog |
| Add cursor-aware context | Ask Copilot to include code around the caret |
| Add open-tabs context | Ask Copilot to gather the active file and related open tabs |
| Compare prompts | Run the same prompt with different context levels |

---

## 🏁 Stretch Goals

1. Add a live response streaming pane in the dialog.
2. Add a fourth context mode for only the active selection.
3. Replace the hardcoded Azure OpenAI settings with options later in the workshop.

---

## Notes

- The GPT-4o endpoint, key, and model are intentionally hardcoded for the demo.
- Install the extension in Visual Studio, then right-click inside a code file to launch it.

---

← Back to [Exercise Index](../../README.md)
