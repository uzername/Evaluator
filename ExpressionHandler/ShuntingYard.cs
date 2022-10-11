using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionHandler
{
    /// <summary>
    /// Taken from https://github.com/kolosovpetro/RpnCalculator/blob/master/RpnCalculator.ShuntingYardAlgorithm/Implementations/ShuntingYard.cs
    /// Generates Reverse Polish Notation string from expression
    /// </summary>
    public static class ShuntingYard
    {
        public static Queue<string> GetPostfixQueue(string input)
        {
            var outputQueue = new Queue<string>();
            var operandStack = new Stack<string>();
            // splitting tokens by space is cheating
            //var inputArray = input.Split(' ');
            var inputArray = myInputSplit(input.Trim());

            foreach (var token in inputArray)
            {
                if (Token.IsNumber(token))
                {
                    outputQueue.Enqueue(token);
                    continue;
                }

                if (Token.IsConstant(token))
                {
                    outputQueue.Enqueue(Token.ReplaceConstant(token));
                    continue;
                }

                if (Token.IsLeftParenthesis(token) || Token.IsFunction(token))
                {
                    operandStack.Push(token);
                    continue;
                }

                if (Token.IsRightParenthesis(token))
                {
                    while (!Token.IsLeftParenthesis(operandStack.Peek()))
                    {
                        outputQueue.Enqueue(operandStack.Pop());
                    }

                    operandStack.Pop();
                    continue;
                }

                while ((operandStack.Count > 0) && Token.IsGreaterPrecedence(operandStack.Peek(), token)
                                          && Token.IsLeftAssociated(token))
                {
                    outputQueue.Enqueue(operandStack.Pop());
                }

                operandStack.Push(token);
            }

            while (operandStack.Count > 0)
                outputQueue.Enqueue(operandStack.Pop());

            return outputQueue;
        }
        /// <summary>
        /// splits line into nice array of tokens
        /// </summary>
        /// <param name="inLine">this should have been passed with Trim() call</param>
        /// <returns></returns>
        private static List<string> myInputSplit(string inLine)
        {
            
            List<string> result = new List<string>();
            string tmpBuffer = "";
            for (int i=0; i<inLine.Length; i++)
            {
                if (Char.IsWhiteSpace( inLine[i]) ) {
                    if (String.IsNullOrEmpty( tmpBuffer) == false) {
                        // found whitespace character, tmpbuffer was not empty - save tmpbuffer
                        // whitespace characters may be between tokens only
                        result.Add(tmpBuffer);
                        tmpBuffer = "";
                    }
                    continue;
                } else
                {
                    if (tmpBuffer.Length==0)
                    {
                        // is this operator or bracket? slip that in
                        if (Token.IsOperator(inLine[i].ToString()) || inLine[i] == '(' || inLine[i] == ')')
                        {
                            result.Add(inLine[i].ToString());
                            continue;
                        }

                        // we added something on previous step, now time to start filling tmpBuffer anew
                        tmpBuffer = tmpBuffer + inLine[i];
                    } else
                    {
                        if ( (Char.IsLetterOrDigit( tmpBuffer[tmpBuffer.Length-1] )|| tmpBuffer[tmpBuffer.Length - 1]=='.')
                            && (Char.IsLetterOrDigit(inLine[i]) || inLine[i]=='.') )
                        {
                            //this is still a literal - function or number, or variable
                            tmpBuffer = tmpBuffer + inLine[i];
                            continue;
                        } else if (Token.IsOperator( inLine[i].ToString()) || inLine[i]=='(' || inLine[i] == ')' || 
                            Char.IsWhiteSpace(inLine[i]) ) {
                            if (string.IsNullOrEmpty(tmpBuffer) == false) {
                                result.Add(tmpBuffer);
                                tmpBuffer = "";
                            }
                            if (Char.IsWhiteSpace(inLine[i]) == false)  {
                                // we always come here
                                result.Add(inLine[i].ToString());
                            }
                            continue;
                        }

                    }
                }
            }
            if (String.IsNullOrEmpty(tmpBuffer) == false)
            {
                result.Add(tmpBuffer);
            }
            return result;
        }

            public static string InfixToPostfixString(string infixString)
            {
                var queue = GetPostfixQueue(infixString);
                var builder = new StringBuilder();

                while (queue.Count > 0)
                    builder.Append(queue.Dequeue() + " ");

                return builder.ToString();
            }

        }
    
}