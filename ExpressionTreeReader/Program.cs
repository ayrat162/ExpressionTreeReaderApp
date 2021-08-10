using System;

namespace ExpressionTreeReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "Lookup(xFind('((())))((()))test1'),'test2',\"HELLO\")";
            var expression = new Expression(text);
        }
    }
}