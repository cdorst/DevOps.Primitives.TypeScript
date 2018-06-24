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
        public DecoratorListAssociation(
            in Decorator decorator,
            in DecoratorList decoratorList = default)
        {
            Decorator = decorator;
            DecoratorList = decoratorList;
        }
        public DecoratorListAssociation(
            in Identifier decorator,
            in DecoratorList decoratorList = default)
            : this(new Decorator(in decorator), in decoratorList)
        {
        }
        public DecoratorListAssociation(
            in string decorator,
            in DecoratorList decoratorList = default)
            : this(new Identifier(in decorator), in decoratorList)
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

        public void SetRecord(in Decorator record)
        {
            Decorator = record;
            DecoratorId = record.DecoratorId;
        }
    }
}
