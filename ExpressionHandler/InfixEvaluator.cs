using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionHandler
{
    /// <summary>
    /// taken from https://github.com/kolosovpetro/RpnCalculator/blob/master/RpnCalculator.Evaluator/Implementations/InfixEvaluator.cs
    /// uses ShuntingYard.cs algorithm internally to get reverse polish notation string and then calculates it.
    /// </summary>
    public static class InfixEvaluator
        {
            public static double EvaluateInfix(Queue<string> postfix)
            {
                var resultStack = new Stack<double>();

                while (postfix.Any())
                {
                    var currentToken = postfix.Dequeue();

                    if (Token.IsNumber(currentToken))
                    {
                        resultStack.Push(double.Parse(currentToken));
                        continue;
                    }

                    if (Token.IsOperator(currentToken))
                    {
                        var val1 = resultStack.Pop();
                        var val2 = resultStack.Pop();
                        var output = Token.Evaluate(val2, val1, currentToken);
                        resultStack.Push(output);
                        continue;
                    }

                    if (Token.IsFunction(currentToken))
                    {
                        var value = resultStack.Pop();
                        var result = Token.Evaluate(value, currentToken);
                        resultStack.Push(result);
                    }
                }

                return resultStack.Pop();
            }

            public static double EvaluateInfix(string infixString)
            {
                var resultStack = new Stack<double>();
                var postfixQueue = ShuntingYard.GetPostfixQueue(infixString);

                while (postfixQueue.Any())
                {
                    var currentToken = postfixQueue.Dequeue();

                    if (Token.IsNumber(currentToken))
                    {
                        resultStack.Push(double.Parse(currentToken));
                        continue;
                    }

                    if (Token.IsOperator(currentToken))
                    {
                        var val1 = resultStack.Pop();
                        var val2 = resultStack.Pop();
                        var output = Token.Evaluate(val2, val1, currentToken);
                        resultStack.Push(output);
                        continue;
                    }

                    if (Token.IsFunction(currentToken))
                    {
                        var value = resultStack.Pop();
                        var result = Token.Evaluate(value, currentToken);
                        resultStack.Push(result);
                    }
                }

                return resultStack.Pop();
            }
        }
    
}
