using ExpressionEvaluatorWorkshop;
using Reqnroll;

namespace ExpressionEvaluatorWorkshop.Tests;

[Binding]
public sealed class ExpressionEvaluatorFeatureSteps
{
    private string _expression = string.Empty;
    private EvaluationResult? _result;
    private Exception? _exception;

    [Given("the infix expression {string}")]
    public void GivenTheInfixExpression(string expression)
    {
        _expression = expression;
        _result = null;
        _exception = null;
    }

    [When("the evaluator runs")]
    public void WhenTheEvaluatorRuns()
    {
        try
        {
            _result = ExpressionEvaluator.Evaluate(_expression);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then("the postfix expression should be {string}")]
    public void ThenThePostfixExpressionShouldBe(string expectedPostfix)
    {
        Assert.Null(_exception);
        Assert.True(_result.HasValue);
        Assert.Equal(expectedPostfix, _result.Value.PostfixExpression);
    }

    [Then("the numeric result should be {int}")]
    public void ThenTheNumericResultShouldBe(int expectedValue)
    {
        Assert.Null(_exception);
        Assert.True(_result.HasValue);
        Assert.Equal(expectedValue, _result.Value.Value);
    }

    [Then("evaluation should fail with an argument error")]
    public void ThenEvaluationShouldFailWithAnArgumentError()
    {
        Assert.NotNull(_exception);
        Assert.IsType<ArgumentException>(_exception);
    }
}
