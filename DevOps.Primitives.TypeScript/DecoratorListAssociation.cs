using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DecoratorListAssociations", Schema = nameof(TypeScript))]
    public class DecoratorListAssociation : IUniqueListAssociation<Decorator>
    {
        public DecoratorListAssociation() { }
        public DecoratorListAssociation(Decorator decorator, DecoratorList decoratorList = null)
        {
            Decorator = decorator;
            DecoratorList = decoratorList;
        }
        public DecoratorListAssociation(Identifier decorator, DecoratorList decoratorList = null)
            : this(new Decorator(decorator), decoratorList)
        {
        }
        public DecoratorListAssociation(string decorator, DecoratorList decoratorList = null)
            : this(new Identifier(decorator), decoratorList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DecoratorListAssociationId { get; set; }

        [ProtoMember(2)]
        public Decorator Decorator { get; set; }
        [ProtoMember(3)]
        public int DecoratorId { get; set; }

        [ProtoMember(4)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(5)]
        public int DecoratorListId { get; set; }

        public Decorator GetRecord() => Decorator;

        public void SetRecord(Decorator record)
        {
            Decorator = record;
            DecoratorId = Decorator.DecoratorId;
        }
    }
}
