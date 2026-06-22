Feature: Expression evaluator
  To describe evaluator behaviour in shared language
  As a team discussing expected outcomes
  We want readable Reqnroll scenarios for precedence, grouping, and validation

  Scenario: Operator precedence is respected
    Given the infix expression "1+2*3"
    When the evaluator runs
    Then the postfix expression should be "1 2 3 * +"
    And the numeric result should be 7

  Scenario: Parentheses override default precedence
    Given the infix expression "(1+2)*3"
    When the evaluator runs
    Then the postfix expression should be "1 2 + 3 *"
    And the numeric result should be 9

  Scenario: Unsupported characters are rejected
    Given the infix expression "1+a"
    When the evaluator runs
    Then evaluation should fail with an argument error
