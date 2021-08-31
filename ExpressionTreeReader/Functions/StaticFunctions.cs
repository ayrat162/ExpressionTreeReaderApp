using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpressionTreeReader
{
    public class StaticFunctions
    {
        public static string Concat(IEnumerable<string> x)
        {
            var sb = new StringBuilder();
            foreach (var s in x)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }

        public static string Day(string x)
        {
            throw new NotImplementedException();
        }

        public static string Exact(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }


        public static string Iif(IEnumerable<string> x)
        {
            if (x == null || x.Count() != 3) return "";
            var xArray = x.ToArray();
            return xArray[0].ToBoolean() ? xArray[1] : xArray[2];
        }

        public static string Int(string x)
        {
            return x.ToInt().ToString();
        }

        public static string Left(IEnumerable<string> x)
        {
            if (x == null || x.Count() != 2) return "";
            var xArray = x.ToArray();
            var length = xArray[1].ToInt();
            if (string.IsNullOrEmpty(xArray[0])) return xArray[0];
            return xArray[0].Substring(0, Math.Min(xArray[0].Length, length));
        }

        public static string Len(string x)
        {
            throw new NotImplementedException();
        }

        public static string Min(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Max(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Maths(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Mid(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Month(string x)
        {
            throw new NotImplementedException();
        }

        public static string Now(string x)
        {
            return DateTime.Now.ToString("s", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        public static string Number(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Replace(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Right(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Round(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }


        public static string Trim(string x)
        {
            return x.Trim();
        }

        public static string Ucase(string x)
        {
            throw new NotImplementedException();
        }

        public static string xFind(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string Year(string x)
        {
            throw new NotImplementedException();
        }

        public static string CDate(string x)
        {
            throw new NotImplementedException();
        }

        public static string CDbl(string x)
        {
            throw new NotImplementedException();
        }

        public static string DateAdd(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string DateSerial(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public string DLookup(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string InStr(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string InStrRev(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string IsNull(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }

        public static string IsNumeric(IEnumerable<string> x)
        {
            throw new NotImplementedException();
        }
    }
}