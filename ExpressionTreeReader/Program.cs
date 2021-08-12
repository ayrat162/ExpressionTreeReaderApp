using System;

namespace ExpressionTreeReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // var watch = System.Diagnostics.Stopwatch.StartNew();
            //
            // var rows = File.ReadAllLines("input.txt");
            // foreach (var row in rows)
            // {
                // new Expression(row);
            // }
            //
            // watch.Stop();
            // Console.WriteLine(watch.ElapsedMilliseconds);
            // Console.WriteLine(watch.ElapsedMilliseconds *1.0 / rows.Length);
            var exp = new Expression("F#m+1#");
            Console.WriteLine(exp.GetValue());
        }
    }
}