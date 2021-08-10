using System.Collections.Generic;

namespace ExpressionTreeReader
{
    public interface IExpression
    {
        public List<IExpression> Child { get; set; }
        public Operators Operator { get; set; }
        public string GetValue();
    }
}