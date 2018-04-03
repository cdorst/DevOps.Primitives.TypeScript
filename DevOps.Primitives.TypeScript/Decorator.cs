using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Decorators", Schema = nameof(TypeScript))]
    public class Decorator : IUniqueListRecord
    {
        public Decorator() { }
        public Decorator(Identifier identifier, DecoratorArgumentListExpression argumentListExpression = null)
        {
            Identifier = identifier;
            DecoratorArgumentListExpression = argumentListExpression;
        }
        public Decorator(string identifier, string argumentListExpression = null)
            : this(new Identifier(identifier), string.IsNullOrEmpty(argumentListExpression) ? null : new DecoratorArgumentListExpression(argumentListExpression))
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
    }
}
