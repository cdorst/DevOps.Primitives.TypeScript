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
        public MethodList(List<MethodListAssociation> constraintListAssociations, AsciiStringReference listIdentifier = null)
        {
            MethodListAssociations = constraintListAssociations;
            ListIdentifier = listIdentifier;
        }
        public MethodList(MethodListAssociation argumentListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<MethodListAssociation> { argumentListAssociation }, listIdentifier)
        {
        }
        public MethodList(Method constraint, AsciiStringReference listIdentifier = null)
            : this(new MethodListAssociation(constraint), listIdentifier)
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

        public void SetRecords(List<Method> records)
        {
            MethodListAssociations = UniqueListAssociationsFactory<Method, MethodListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Method>.Create(records, r => r.MethodId));
        }
    }
}
