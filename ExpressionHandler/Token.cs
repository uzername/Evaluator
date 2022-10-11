using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpressionHandler
{
    // taken from https://github.com/kolosovpetro/RpnCalculator
    public static class Token
    {
        private static readonly string[] Operators = { "^", "*", "/", "+", "-" };
        private static readonly string[] Functions = { "sin", "cos", "tan", "ctan", "sqrt", "exp", "ln", "log" };
        private static readonly string[] Constants = { "e", "pi" };

        public static int Precedence(string token)
        {
            int rslt = 0;
             switch (token)
            {
                case "^": { rslt = 4; break; }
                case "sin": { rslt = 4; break; }
                case "cos": { rslt = 4; break; }
                case "tan": { rslt = 4; break; }
                case "ctan": { rslt = 4; break; }
                case "sqrt": { rslt = 4; break; }
                case "exp": { rslt = 4; break;}
                case "ln": { rslt = 4; break;}
                case "log": { rslt = 4; break; }
                case "*": { rslt = 3; break; }
                case "/": { rslt = 3; break; }
                case "+": { rslt = 2; break; }
                case "-": { rslt = 2; break; } 
                default: { rslt = 0; break; }
            };
            return rslt;
        }

        public static string ReplaceConstant(string token)
        {
            string rslt;
            switch ( token )
            {
                case "e": { rslt = Math.E.ToString(CultureInfo.CurrentCulture); break; }
                case "pi": { rslt = Math.PI.ToString(CultureInfo.CurrentCulture); break; }
                default: { rslt = "0"; break; }
            };
            return rslt;
        }

        public static double Evaluate(double value1, double value2, string token)
        {
            double rslt;
            switch (token)
            {
                case "+": { rslt = value1 + value2; break; }
                case "-": { rslt = value1 - value2; break; }
                case "*": { rslt = value1 * value2; break; }
                case "/": { rslt = value1 / value2; break; }
                case "^": { rslt = Math.Pow(value1, value2); break; }
                default: { rslt = 0; break;}
            };
            return rslt;
        }

        public static double Evaluate(double value, string token)
        {
            double rslt;
            switch ( token )
            {
                case "sin": { rslt = Math.Sin(value); break; }
                case "cos": { rslt = Math.Cos(value); break; }
                case "tan": { rslt = Math.Tan(value); break; }
                case "ctan": { rslt = 1 / Math.Tan(value); break; }
                case "sqrt": { rslt = Math.Sqrt(value); break; }
                case "exp": { rslt = Math.Exp(value); break; }
                case "ln": { rslt = Math.Log(value); break; }
                case "log": { rslt = Math.Log10(value); break; }
                default: { rslt = 0; break; }
            };
            return rslt;
        }

        public static string Associativity(string token)
        {
            return token == "^" ? "Right" : "Left";
        }

        public static bool IsOperator(string token)
        {
            return Operators.Contains(token);
        }

        public static bool IsFunction(string token)
        {
            return Functions.Contains(token);
        }
        
        public static bool IsNumber(string token)
        {
            return double.TryParse(token, out _);
        }

        public static bool IsLeftParenthesis(string token)
        {
            return token == "(";
        }

        public static bool IsRightParenthesis(string token)
        {
            return token == ")";
        }

        public static bool IsLeftAssociated(string token)
        {
            return Associativity(token) == "Left";
        }

        public static bool IsGreaterPrecedence(string token1, string token2)
        {
            return Precedence(token1) >= Precedence(token2);
        }

        public static bool IsConstant(string token)
        {
            return Constants.Contains(token);
        }
    }
}
