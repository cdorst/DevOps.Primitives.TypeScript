using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("MethodLists", Schema = nameof(TypeScript))]
    public class MethodList : IUniqueList<Method, MethodListAssociation>
    {
        public MethodList() { }
        public MethodList(
            in List<MethodListAssociation> constraintListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            MethodListAssociations = constraintListAssociations;
            ListIdentifier = listIdentifier;
        }
        public MethodList(
            in MethodListAssociation argumentListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MethodListAssociation> { argumentListAssociation }, in listIdentifier)
        {
        }
        public MethodList(
            in Method constraint,
            in AsciiStringReference listIdentifier = default)
            : this(new MethodListAssociation(in constraint), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MethodListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MethodListAssociation> MethodListAssociations { get; set; }

        public List<MethodListAssociation> GetAssociations() => MethodListAssociations;

        public void SetRecords(in List<Method> records)
        {
            MethodListAssociations = UniqueListAssociationsFactory<Method, MethodListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Method>.Create(in records, r => r.MethodId));
        }
    }
}
