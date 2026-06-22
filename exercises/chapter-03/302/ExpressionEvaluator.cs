namespace ExpressionEvaluatorLab;

public static class ExpressionEvaluator
{
    /// <summary>
    /// Converts a simple infix expression to postfix notation and evaluates it.
    /// Support single-digit integers, +, -, *, /, parentheses, and optional spaces.
    /// Return the postfix expression as space-separated tokens together with the result.
    /// </summary>
    public static EvaluationResult Evaluate(string infixExpression)
    {
        // TODO: Use Copilot Ask mode to implement infix-to-postfix conversion
        // followed by postfix evaluation.
        // Before you accept any code, make Copilot explain:
        // 1. why * and / must stay above + and - on the operator stack
        // 2. what happens when you see '(' and ')'
        // 3. why postfix evaluation can be done with a value stack
        throw new NotImplementedException();
    }
}

public readonly record struct EvaluationResult(string PostfixExpression, int Value);
