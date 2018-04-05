using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DecoratorArgumentListExpressions", Schema = nameof(TypeScript))]
    public class DecoratorArgumentListExpression
    {
        public DecoratorArgumentListExpression() { }
        public DecoratorArgumentListExpression(AsciiMaxStringReference expression) { Expression = expression; }
        public DecoratorArgumentListExpression(string expression) : this(new AsciiMaxStringReference(expression)) { }

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
            if (!expression.StartsWith(OpenParenthesis)) expression = $"{OpenParenthesis}{expression}";
            if (!expression.EndsWith(CloseParenthesis)) expression = $"{expression}{CloseParenthesis}";
            return expression;
        }
    }
}
