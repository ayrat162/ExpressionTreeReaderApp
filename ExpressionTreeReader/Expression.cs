using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NCalc.Domain;
using static System.Double;

namespace ExpressionTreeReader
{
    public class Expression
    {
        private List<Expression> Children { get; set; } = new List<Expression>();

        private List<MathsOperator> MathOperators { get; set; } = new List<MathsOperator>();

        private Operator Operator { get; set; } = Operator.Default;
        private string TextValue { get; set; }


        public Expression(string text)
        {
            TextValue = text.Trim();
            if (string.IsNullOrEmpty(TextValue)) return;


            if (TextValue == "Row")
            {
                Operator = Operator.Row;
                return;
            }

            if (Regex.IsMatch(TextValue, @"^-?(\d*)(\.)?(\d*)?$"))
            {
                Operator = Operator.Number;
                return;
            }

            // COMMA-SEPARATED EXPRESSIONS
            var level = 0;
            var start = 0;
            var end = 0;
            var isQuote = false;
            var isDoubleQuote = false;
            var isHashtag = false;

            for (var i = 0; i < TextValue.Length; i++)
            {
                var c = TextValue[i];
                var prevC = i - 1 >= 0 ? TextValue[i - 1] : '☺';

                if (c == '\'' && !isDoubleQuote && !isHashtag && prevC != '\\')
                {
                    isQuote = !isQuote;
                }
                else if (c == '"' && !isQuote && !isHashtag && prevC != '\\')
                {
                    isDoubleQuote = !isDoubleQuote;
                }
                else if (c == '#' && !isQuote && !isDoubleQuote && prevC != '\\')
                {
                    isHashtag = !isHashtag;
                }
                else if (c == '(' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    level++;
                }
                else if (c == ')' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    level--;
                }
                else if (c == ',' && level == 0 && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    Operator = Operator.Comma;
                    Children.Add(new Expression(TextValue.Substring(start, i - start)));
                    start = i + 1;
                }

                if (i == TextValue.Length - 1 && Children.Any())
                {
                    Children.Add(new Expression(TextValue.Substring(start, i - start + 1)));
                }
            }

            if (Operator != Operator.Default) return;


            level = 0;
            start = 0;
            isQuote = false;
            isDoubleQuote = false;
            isHashtag = false;
            // Parse operator-separated expressions
            for (var i = 0; i < TextValue.Length; i++)
            {
                var c = TextValue[i];
                var prevC = i - 1 >= 0 ? TextValue[i - 1] : '☺';
                if (c == '\'' && !isDoubleQuote && !isHashtag && prevC != '\\')
                {
                    isQuote = !isQuote;
                }
                else if (c == '"' && !isQuote && !isHashtag && prevC != '\\')
                {
                    isDoubleQuote = !isDoubleQuote;
                }
                else if (c == '#' && !isQuote && !isDoubleQuote && prevC != '\\')
                {
                    isHashtag = !isHashtag;
                }
                else if (c == '(' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    level++;
                }
                else if (c == ')' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    level--;
                }
                else if (c.IsMathsOperator() && !isQuote && !isDoubleQuote && !isHashtag && level == 0)
                {
                    Operator = Operator.Maths;

                    Children.Add(new Expression(TextValue.Substring(start, i - start)));

                    switch (c)
                    {
                        case '+':
                            MathOperators.Add(MathsOperator.Plus);
                            break;
                        case '-':
                            MathOperators.Add(MathsOperator.Minus);
                            break;
                        case '*':
                            MathOperators.Add(MathsOperator.Multi);
                            break;
                        case '/':
                            MathOperators.Add(MathsOperator.Div);
                            break;
                        case '&':
                            MathOperators.Add(MathsOperator.Amp);
                            break;
                    } //TODO: Add new cases

                    start = i + 1;
                }

                if (i == TextValue.Length - 1 && Children.Any())
                {
                    Children.Add(new Expression(TextValue.Substring(start, i - start + 1)));
                }
            }

            if (Operator != Operator.Default) return;

            level = 0;
            start = 0;
            isQuote = false;
            isDoubleQuote = false;
            isHashtag = false;
            // Parse F(...)-like formula
            for (var i = 0; i < TextValue.Length; i++)
            {
                var c = TextValue[i];
                var prevC = i - 1 >= 0 ? TextValue[i - 1] : '☺';
                if (c == '\'' && !isDoubleQuote && !isHashtag && prevC != '\\')
                {
                    isQuote = !isQuote;
                }
                else if (c == '"' && !isQuote && !isHashtag && prevC != '\\')
                {
                    isDoubleQuote = !isDoubleQuote;
                }
                else if (c == '#' && !isQuote && !isDoubleQuote && prevC != '\\')
                {
                    isHashtag = !isHashtag;
                }
                else if (c == '(' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    if (level == 0)
                    {
                        start = i + 1;
                        var formula = TextValue.Substring(0, i);

                        switch (formula.ToLower())
                        {
                            case "":
                                Operator = Operator.Empty;
                                break;
                            case "xfind":
                                Operator = Operator.myFind;
                                break;
                            case "lookup":
                                Operator = Operator.myLookup;
                                break;
                            case "mylookup":
                                Operator = Operator.myLookup;
                                break;
                            case "iif":
                                Operator = Operator.Iif;
                                break;
                            case "ucase":
                                Operator = Operator.Iif;
                                break;
                            case "mid":
                                Operator = Operator.Mid;
                                break;
                            case "cdate":
                                Operator = Operator.CDate;
                                break;
                            case "replace":
                                Operator = Operator.Replace;
                                break;
                            default:
                                File.AppendAllText("unknown.formulas.txt", formula + "\n");
                                Operator = Operator.NA;
                                break;
                        }
                    }


                    level++;
                }
                else if (c == ')' && !isQuote && !isDoubleQuote && !isHashtag)
                {
                    if (level == 1)
                    {
                        var newExpression = new Expression(TextValue.Substring(start, i - start));
                        Children = newExpression.Children;
                        MathOperators = newExpression.MathOperators;
                        if (Operator == Operator.Empty)
                        {
                            Operator = newExpression.Operator;
                        }
                    }

                    level--;
                }
            }

            if (Operator != Operator.Default) return;

            isQuote = false;
            isDoubleQuote = false;
            // Parse Quotes and Double Quotes
            if (TextValue[0] == TextValue[^1] && (TextValue[0] == '"' || TextValue[0] == '\''))
            {
                var isContainsNonQuote = false;
                for (var i = 0; i < TextValue.Length; i++)
                {
                    var c = TextValue[i];
                    var prevC = i - 1 >= 0 ? TextValue[i - 1] : '☺';
                    if (c == '\'' && !isDoubleQuote && prevC != '\\')
                    {
                        isQuote = !isQuote;
                    }
                    else if (c == '"' && !isQuote && prevC != '\\')
                    {
                        isDoubleQuote = !isDoubleQuote;
                    }
                    else if (c != '"' && c != '\'' && !isQuote && !isDoubleQuote)
                    {
                        isContainsNonQuote = true;
                    }
                }

                if (!isContainsNonQuote) Operator = Operator.QuoteText;
            }

            if (Operator != Operator.Default) return;


            if (TextValue.Count(c => c == '#') == 2)
            {
                start = TextValue.IndexOf('#');
                var oldFormula = TextValue[(start + 1)..];
                end = oldFormula.IndexOf('#');
                oldFormula = oldFormula.Substring(0, end);

                var newFormula = oldFormula //TODO: Replace with right ones 
                    .Replace("d", DateTime.Now.Day.ToString()) //DAY(@ImportDate))
                    .Replace("m", DateTime.Now.Month.ToString()) //MONTH(@DeliveryDate))
                    .Replace("n", DateTime.Now.Month.ToString()) //MONTH(@ImportDate))
                    .Replace("y", DateTime.Now.Year.ToString()) //YEAR(@DeliveryDate))
                    .Replace("d", DateTime.Now.Day.ToString()) //DAY(@ImportDate))
                    .Replace("M", DateTime.Now.Month.ToString()) //MONTH(@FiscalDeliveryDate)
                    .Replace("N", DateTime.Now.Month.ToString()) //MONTH(@FiscalDate))
                    .Replace("Y", DateTime.Now.Year.ToString()); //YEAR(@FiscalDeliveryDate))

                TryParse(new NCalc.Expression(newFormula).Evaluate().ToString(), out var d);
                var num = Convert.ToInt32(d);
                TextValue = TextValue.Replace("#" + oldFormula + "#", num.ToString());
            } //TODO: Add evaluation of DateTime in Access SQL Format


            if (Regex.IsMatch(TextValue, @"^[Ff](\d{1,})$"))
            {
                Operator = Operator.Fxxx;
                return;
            }

            // Unknown type of data
            Operator = Operator.NA;
            File.AppendAllText("unknown.formulas.txt", TextValue + "\n");
        }

        public string GetValue()
        {
            var rowFunctions = new RowFunctions();
            var groupByFunctions = new GroupByFunctions();
            switch (Operator)
            {
                case Operator.Concat:
                    return StaticFunctions.Concat(Children.Select(e => e.GetValue()));
                case Operator.Count:
                    return groupByFunctions.Count(Children.Select(e => e.GetValue()));
                case Operator.Day:
                    return StaticFunctions.Day(Children.FirstOrDefault().GetValue());
                case Operator.Empty:
                    return "";
                case Operator.Exact:
                    return StaticFunctions.Exact(Children.Select(e => e.GetValue()));
                case Operator.Fxxx:
                    return rowFunctions.Fxxx(int.Parse(TextValue.Substring(1)));
                case Operator.Iif:
                    return StaticFunctions.Iif(Children.Select(e => e.GetValue()));
                case Operator.Int:
                    return StaticFunctions.Int(Children.FirstOrDefault().GetValue());
                case Operator.Left:
                    return StaticFunctions.Left(Children.Select(e => e.GetValue()));
                case Operator.Len:
                    return StaticFunctions.Len(Children.FirstOrDefault().GetValue());
                case Operator.Maths:
                    return StaticFunctions.Maths(Children.Select(e => e.GetValue()));
                case Operator.Mid:
                    return StaticFunctions.Mid(Children.Select(e => e.GetValue()));
                case Operator.Month:
                    return StaticFunctions.Month(Children.FirstOrDefault().GetValue());
                case Operator.myCount:
                    return rowFunctions.myCount(Children.Select(e => e.GetValue()));
                case Operator.myFind:
                    return rowFunctions.myFind(Children.Select(e => e.GetValue()));
                case Operator.myHistory:
                    return rowFunctions.myHistory(Children.Select(e => e.GetValue()));
                case Operator.myLookup:
                    return rowFunctions.myLookup(Children.Select(e => e.GetValue()));
                case Operator.myMin:
                    return StaticFunctions.Min(Children.Select(e => e.GetValue()));
                case Operator.myMax:
                    return StaticFunctions.Max(Children.Select(e => e.GetValue()));
                case Operator.myMonth:
                    return StaticFunctions.Month(Children.FirstOrDefault().GetValue());
                case Operator.myParent:
                    return rowFunctions.myParent(Children.Select(e => e.GetValue()));
                case Operator.mySearch:
                    return rowFunctions.mySearch(Children.Select(e => e.GetValue()));
                case Operator.myYear:
                    return StaticFunctions.Year(Children.FirstOrDefault().GetValue());
                case Operator.Now:
                    return StaticFunctions.Now(Children.FirstOrDefault().GetValue());
                case Operator.Number:
                    return StaticFunctions.Number(Children.Select(e => e.GetValue()));
                case Operator.Replace:
                    return StaticFunctions.Replace(Children.Select(e => e.GetValue()));
                case Operator.Right:
                    return StaticFunctions.Right(Children.Select(e => e.GetValue()));
                case Operator.Round:
                    return StaticFunctions.Round(Children.Select(e => e.GetValue()));
                case Operator.Row:
                    return rowFunctions.Row();
                case Operator.Sum:
                    return groupByFunctions.Sum(Children.Select(e => e.GetValue()));
                case Operator.Trim:
                    return StaticFunctions.Trim(Children.FirstOrDefault().GetValue());
                case Operator.Ucase:
                    return StaticFunctions.Ucase(Children.FirstOrDefault().GetValue());
                case Operator.xFind:
                    return rowFunctions.myFind(Children.Select(e => e.GetValue()));
                case Operator.Year:
                    return StaticFunctions.Year(Children.FirstOrDefault().GetValue());
                case Operator.CDate:
                    return StaticFunctions.CDate(Children.FirstOrDefault().GetValue());
                case Operator.CDbl:
                    return StaticFunctions.CDbl(Children.FirstOrDefault().GetValue());
                case Operator.DateAdd:
                    return StaticFunctions.DateAdd(Children.Select(e => e.GetValue()));
                case Operator.DateSerial:
                    return StaticFunctions.DateSerial(Children.Select(e => e.GetValue()));
                case Operator.DLookup:
                    return rowFunctions.myLookup(Children.Select(e => e.GetValue()));
                case Operator.InStr:
                    return StaticFunctions.InStr(Children.Select(e => e.GetValue()));
                case Operator.InStrRev:
                    return StaticFunctions.InStrRev(Children.Select(e => e.GetValue()));
                case Operator.IsNull:
                    return StaticFunctions.IsNull(Children.Select(e => e.GetValue()));
                case Operator.IsNumeric:
                    return StaticFunctions.IsNumeric(Children.Select(e => e.GetValue()));
                case Operator.QuoteText:
                    return TextValue[1..^1];
                default:
                    return "error!!!";
            }
        }
    }
}