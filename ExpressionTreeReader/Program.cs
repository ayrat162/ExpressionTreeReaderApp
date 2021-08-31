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
            var exp = new Expression("iif(F11='Housing','No Licence',iif(left(F11,2)='VM','V-','P-') & iif(isnull(myLookup('OS',F8,1,2,'unknown')),'ESX',myLookup('OS',F8,1,2,'unknown')))");
            Console.WriteLine(exp.GetValue());
        }
    }
}