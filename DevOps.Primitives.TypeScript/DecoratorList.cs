using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DecoratorLists", Schema = nameof(TypeScript))]
    public class DecoratorList : IUniqueList<Decorator, DecoratorListAssociation>
    {
        public DecoratorList() { }
        public DecoratorList(
            in List<DecoratorListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            DecoratorListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public DecoratorList(
            in DecoratorListAssociation attributeList,
            in AsciiStringReference listIdentifier = default)
            : this(new List<DecoratorListAssociation> { attributeList }, in listIdentifier)
        {
        }
        public DecoratorList(
            in Identifier decorator,
            in AsciiStringReference listIdentifier = default)
            : this(new DecoratorListAssociation(in decorator), in listIdentifier)
        {
        }
        public DecoratorList(
            in string decorator,
            in AsciiStringReference listIdentifier = default)
            : this(new Identifier(in decorator), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DecoratorListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<DecoratorListAssociation> DecoratorListAssociations { get; set; }

        public List<DecoratorListAssociation> GetAssociations() => DecoratorListAssociations;

        public void SetRecords(in List<Decorator> records)
        {
            DecoratorListAssociations = UniqueListAssociationsFactory<Decorator, DecoratorListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Decorator>.Create(in records, r => r.DecoratorId));
        }

        public string GetInlineDecoratorListSyntax()
            => string.Join(Space, this.GetRecords().Select(decorator => decorator.GetDecoratorSyntax()));
    }
}
