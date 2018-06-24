using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Decorators", Schema = nameof(TypeScript))]
    public class Decorator : IUniqueListRecord
    {
        public Decorator() { }
        public Decorator(
            in Identifier identifier,
            in DecoratorArgumentListExpression argumentListExpression = default)
        {
            Identifier = identifier;
            DecoratorArgumentListExpression = argumentListExpression;
        }
        public Decorator(
            in string identifier,
            in string argumentListExpression = default)
            : this(new Identifier(identifier), IsNullOrEmpty(argumentListExpression) ? null : new DecoratorArgumentListExpression(argumentListExpression))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DecoratorId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        [ProtoMember(4)]
        public DecoratorArgumentListExpression DecoratorArgumentListExpression { get; set; }
        [ProtoMember(5)]
        public int? DecoratorArgumentListExpressionId { get; set; }

        public string GetDecoratorSyntax()
            => Concat("@", Identifier, DecoratorArgumentListExpression?.GetDecoratorArgumentListExpressionSyntax() ?? Empty);
    }
}
