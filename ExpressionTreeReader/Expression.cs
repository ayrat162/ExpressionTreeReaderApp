using System;
using System.Collections.Generic;

namespace ExpressionTreeReader
{
    public class Expression
    {
        public List<Expression> Child { get; set; }
        public string Value { get; set; }

        public Expression(string text)
        {
            Value = text;
            Child = new List<Expression>();
            var level = 0;
            var levelStart = 0;
            var levelEnd = 0;
            var isQuote = false;
            var isDoubleQuote = false;

            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var prevC = i - 1 >= 0 ? text[i - 1] : '☺';
                if (c == '\'' && !isDoubleQuote && prevC != '\\')
                {
                    isQuote = !isQuote;
                }
                else if (c == '"' && !isQuote && prevC != '\\')
                {
                    isDoubleQuote = !isDoubleQuote;
                }
                else if (c == '(' && !isQuote && !isDoubleQuote)
                {
                    if (level == 0) levelStart = i + 1;
                    level++;
                }
                else if (c == ')' && !isQuote && !isDoubleQuote)
                {
                    level--;
                    if (level == 0)
                    {
                        levelEnd = i - 1;
                        Child.Add(new Expression(text.Substring(levelStart, levelEnd - levelStart + 1)));
                    }
                }
                else if (text[i] == ',' && level == 1 && !isQuote && !isDoubleQuote)
                {
                    levelEnd = i - 1;
                    Child.Add(new Expression(text.Substring(levelStart, levelEnd - levelStart + 1)));
                    levelStart = i + 1;
                }
            }
        }

        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
    }
}