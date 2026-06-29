namespace ExpressionEvaluatorWorkshop;

public static class ExpressionEvaluator
{
    public static EvaluationResult Evaluate(string infixExpression)
    {
        if (string.IsNullOrWhiteSpace(infixExpression))
        {
            throw new ArgumentException("Expression is required.", nameof(infixExpression));
        }

        var tokens = Tokenize(infixExpression);
        var postfixTokens = ConvertToPostfix(tokens);
        var value = EvaluatePostfix(postfixTokens);

        return new EvaluationResult(string.Join(' ', postfixTokens), value);
    }

    private static IReadOnlyList<string> Tokenize(string expression)
    {
        var tokens = new List<string>();
        var index = 0;

        while (index < expression.Length)
        {
            var current = expression[index];

            if (char.IsWhiteSpace(current))
            {
                index++;
                continue;
            }

            if (char.IsDigit(current))
            {
                var start = index;
                while (index < expression.Length && char.IsDigit(expression[index]))
                {
                    index++;
                }

                tokens.Add(expression[start..index]);
                continue;
            }

            if (IsOperator(current) || current is '(' or ')')
            {
                tokens.Add(current.ToString());
                index++;
                continue;
            }

            throw new ArgumentException($"Unsupported character '{current}'.", nameof(expression));
        }

        return tokens;
    }

    private static IReadOnlyList<string> ConvertToPostfix(IReadOnlyList<string> tokens)
    {
        var output = new List<string>();
        var operators = new Stack<char>();

        foreach (var token in tokens)
        {
            if (int.TryParse(token, out _))
            {
                output.Add(token);
                continue;
            }

            var symbol = token[0];

            if (symbol == '(')
            {
                operators.Push(symbol);
                continue;
            }

            if (symbol == ')')
            {
                while (operators.Count > 0 && operators.Peek() != '(')
                {
                    output.Add(operators.Pop().ToString());
                }

                if (operators.Count == 0 || operators.Pop() != '(')
                {
                    throw new InvalidOperationException("Mismatched parentheses.");
                }

                continue;
            }

            while (operators.Count > 0 &&
                   operators.Peek() != '(' &&
                   Precedence(operators.Peek()) >= Precedence(symbol))
            {
                output.Add(operators.Pop().ToString());
            }

            operators.Push(symbol);
        }

        while (operators.Count > 0)
        {
            var symbol = operators.Pop();
            if (symbol is '(' or ')')
            {
                throw new InvalidOperationException("Mismatched parentheses.");
            }

            output.Add(symbol.ToString());
        }

        return output;
    }

    private static int EvaluatePostfix(IReadOnlyList<string> postfixTokens)
    {
        var values = new Stack<int>();

        foreach (var token in postfixTokens)
        {
            if (int.TryParse(token, out var number))
            {
                values.Push(number);
                continue;
            }

            if (values.Count < 2)
            {
                throw new InvalidOperationException("The expression does not contain enough operands.");
            }

            var right = values.Pop();
            var left = values.Pop();
            values.Push(ApplyOperator(token[0], left, right));
        }

        if (values.Count != 1)
        {
            throw new InvalidOperationException("The expression could not be evaluated cleanly.");
        }

        return values.Pop();
    }

    private static int ApplyOperator(char op, int left, int right)
    {
        return op switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => right == 0
                ? throw new DivideByZeroException("Division by zero is not allowed.")
                : left / right,
            _ => throw new InvalidOperationException($"Unsupported operator '{op}'.")
        };
    }

    private static bool IsOperator(char symbol) => symbol is '+' or '-' or '*' or '/';

    private static int Precedence(char symbol) => symbol is '*' or '/' ? 2 : 1;
}

public readonly record struct EvaluationResult(string PostfixExpression, int Value);
