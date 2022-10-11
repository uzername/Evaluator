using System;
using ExpressionHandler;
namespace Evaluator 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression: ");
            string infixString = Console.ReadLine();
            if (string.IsNullOrEmpty(infixString) == false)
            {
                Console.WriteLine(ShuntingYard.InfixToPostfixString(infixString));    // 2 1 2 / tan 2 ^ + 0 cos +
                Console.WriteLine(InfixEvaluator.EvaluateInfix(infixString));
            }
        }
    }
}