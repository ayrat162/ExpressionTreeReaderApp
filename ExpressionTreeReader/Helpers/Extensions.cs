using System;
using System.Threading;

namespace ExpressionTreeReader
{
    public static class Extensions
    {
        public static bool IsMathsOperator(this char c)
        {
            return c is '+' or '-' or '/' or '*' or '&' or '=';
        }

        public static bool ToBoolean(this string s)
        {
            s = s.Trim();

            var result = bool.TryParse(s, out var b);
            if (result) return b;

            result = int.TryParse(s, out var i);
            if (result) return (i > 0);

            return false;
        }

        public static int ToInt(this string s)
        {
            s = s.Trim();
            s = s.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            
            var result = int.TryParse(s, out var i);
            if (result) return i;

            result = bool.TryParse(s, out var b);
            if (result && b) return 1;

            result = float.TryParse(s, out var f);
            if (result) return (int)f;

            return 0;
        }

        public static float ToFloat(this string s)
        {
            s = s.Trim();
            s = s.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            
            var result = float.TryParse(s, out var f);
            if (result) return f;

            return 0;
        }
    }
}