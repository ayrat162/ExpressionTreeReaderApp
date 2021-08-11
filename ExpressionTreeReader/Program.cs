using System;
using System.IO;

namespace ExpressionTreeReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            var rows = File.ReadAllLines("input.txt");
            foreach (var row in rows)
            {
                new Expression(row);
            }
            Console.WriteLine(DateTime.Now);
            // var text = "F#18+(2*N)#\n";
            // // var text = "Lookup(xFind('((())))((()))test1'),Next(',next')+'test2',\"HELLO\")";
            // var expression = new Expression(text);
        }
    }
}