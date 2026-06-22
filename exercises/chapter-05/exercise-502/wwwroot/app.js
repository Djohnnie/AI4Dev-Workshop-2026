const challengeList = document.getElementById("challenge-list");
const sampleButton = document.getElementById("sample-button");
const scoreButton = document.getElementById("score-button");
const promptInput = document.getElementById("prompt-input");
const statusElement = document.getElementById("status");
const resultPanel = document.getElementById("result-panel");
const scoreValue = document.getElementById("score-value");
const verdict = document.getElementById("verdict");
const styleChip = document.getElementById("style-chip");
const ingredientList = document.getElementById("ingredient-list");
const strengthList = document.getElementById("strength-list");
const antiPatternList = document.getElementById("anti-pattern-list");
const suggestionList = document.getElementById("suggestion-list");
const hintList = document.getElementById("hint-list");

const samplePrompt = `Create a TypeScript function for our Express 5 API that validates a participant prompt before it is sent to the judge.
Use the existing validator pattern, return a typed result, and do not add new dependencies.
Example input: "write a function"
Example output: { valid: false, missing: ["context", "constraints"] }.`;

sampleButton.addEventListener("click", () => {
    promptInput.value = samplePrompt;
    promptInput.focus();
});

scoreButton.addEventListener("click", async () => {
    const prompt = promptInput.value.trim();

    if (!prompt) {
        statusElement.textContent = "Paste a prompt first.";
        return;
    }

    clearResult();
    setBusyState(true, "Prompt Arena Judge is scoring...");

    try {
        const response = await fetch("/api/score", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ prompt })
        });

        const payload = await response.json();

        if (!response.ok) {
            const detail = payload.detail || payload.title || "The judge could not score that prompt.";
            throw new Error(detail);
        }

        renderResult(payload);
        setBusyState(false, "Score ready. Rewrite it and go again.");
    } catch (error) {
        resultPanel.classList.add("hidden");
        setBusyState(false, error.message || "Something went wrong while scoring the prompt.");
    }
});

async function loadChallenges() {
    const response = await fetch("/api/challenges");
    const challenges = await response.json();

    for (const challenge of challenges) {
        const card = document.createElement("article");
        card.className = "challenge-card";
        card.innerHTML = `
            <h3>${challenge.title}</h3>
            <p><strong>Goal:</strong> ${challenge.goal}</p>
            <p><strong>Twist:</strong> ${challenge.twist}</p>
        `;
        challengeList.appendChild(card);
    }
}

function renderResult(result) {
    resultPanel.classList.remove("hidden");
    scoreValue.textContent = `${result.score}%`;
    verdict.textContent = result.verdict;
    styleChip.textContent = result.promptingStyle;

    renderIngredientList(result.ingredients);
    renderSimpleList(strengthList, result.strengths, "No obvious strengths yet.");
    renderSimpleList(antiPatternList, result.antiPatterns, "No anti-patterns detected.");
    renderSimpleList(suggestionList, result.suggestions, "No suggestions returned.");
    renderSimpleList(hintList, result.hints, "No prompt-specific hints returned.");
}

function renderIngredientList(ingredients) {
    ingredientList.innerHTML = "";

    for (const ingredient of ingredients) {
        const item = document.createElement("li");
        item.className = ingredient.present ? "present" : "missing";
        item.innerHTML = `<strong>${ingredient.name}:</strong> ${ingredient.notes}`;
        ingredientList.appendChild(item);
    }
}

function renderSimpleList(target, items, emptyMessage) {
    target.innerHTML = "";

    if (!items || items.length === 0) {
        const item = document.createElement("li");
        item.textContent = emptyMessage;
        target.appendChild(item);
        return;
    }

    for (const value of items) {
        const item = document.createElement("li");
        item.textContent = value;
        target.appendChild(item);
    }
}

function setBusyState(isBusy, message) {
    scoreButton.disabled = isBusy;
    sampleButton.disabled = isBusy;
    statusElement.textContent = message;
}

function clearResult() {
    resultPanel.classList.add("hidden");
    scoreValue.textContent = "0%";
    verdict.textContent = "";
    styleChip.textContent = "none";
    ingredientList.innerHTML = "";
    strengthList.innerHTML = "";
    antiPatternList.innerHTML = "";
    suggestionList.innerHTML = "";
    hintList.innerHTML = "";
}

loadChallenges().catch(() => {
    statusElement.textContent = "Could not load challenge modes.";
});
