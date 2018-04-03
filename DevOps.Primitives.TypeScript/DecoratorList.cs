using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    /// <remarks>Each instance represents a collection of attribute lists. Each attribute list contains a single attribute</remarks>
    [ProtoContract]
    [Table("DecoratorLists", Schema = nameof(TypeScript))]
    public class DecoratorList : IUniqueList<Decorator, DecoratorListAssociation>
    {
        public DecoratorList() { }
        public DecoratorList(List<DecoratorListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            DecoratorListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public DecoratorList(DecoratorListAssociation attributeList, AsciiStringReference listIdentifier = null)
            : this(new List<DecoratorListAssociation> { attributeList }, listIdentifier)
        {
        }
        public DecoratorList(Identifier decorator, AsciiStringReference listIdentifier = null)
            : this(new DecoratorListAssociation(decorator), listIdentifier)
        {
        }
        public DecoratorList(string decorator, AsciiStringReference listIdentifier = null)
            : this(new Identifier(decorator), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int AttributeListCollectionId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<DecoratorListAssociation> DecoratorListAssociations { get; set; }

        public List<DecoratorListAssociation> GetAssociations() => DecoratorListAssociations;

        public void SetRecords(List<Decorator> records)
        {
            DecoratorListAssociations = UniqueListAssociationsFactory<Decorator, DecoratorListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Decorator>.Create(records, r => r.DecoratorId));
        }
    }
}
