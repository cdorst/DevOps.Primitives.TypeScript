using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DecoratorArgumentListExpressions", Schema = nameof(TypeScript))]
    public class DecoratorArgumentListExpression
    {
        public DecoratorArgumentListExpression() { }
        public DecoratorArgumentListExpression(in AsciiMaxStringReference expression) => Expression = expression;
        public DecoratorArgumentListExpression(in string expression) : this(new AsciiMaxStringReference(in expression)) { }

        [Key]
        [ProtoMember(1)]
        public int DecoratorArgumentListExpressionId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Expression { get; set; }
        [ProtoMember(3)]
        public int ExpressionId { get; set; }

        public string GetDecoratorArgumentListExpressionSyntax()
        {
            var expression = Expression.Value;
            if (!expression.StartsWith(OpenParenthesis)) expression = Concat(OpenParenthesis, expression);
            if (!expression.EndsWith(CloseParenthesis)) expression = Concat(expression, CloseParenthesis);
            return expression;
        }
    }
}
