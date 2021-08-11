namespace ExpressionTreeReader
{
    public static class Extensions
    {
        public static bool IsMathsOperator(this char c)
        {
            return c is '+' or '-' or '/' or '*' or '&' or '=';
        }
    }
}