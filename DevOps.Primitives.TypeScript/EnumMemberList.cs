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
    [Table("EnumMemberLists", Schema = nameof(TypeScript))]
    public class EnumMemberList : IUniqueList<EnumMember, EnumMemberListAssociation>
    {
        public EnumMemberList() { }
        public EnumMemberList(List<EnumMemberListAssociation> enumMemberListAssociations, AsciiStringReference listIdentifier = null)
        {
            EnumMemberListAssociations = enumMemberListAssociations;
            ListIdentifier = listIdentifier;
        }
        public EnumMemberList(EnumMemberListAssociation enumMemberListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<EnumMemberListAssociation> { enumMemberListAssociation }, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int EnumMemberListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<EnumMemberListAssociation> EnumMemberListAssociations { get; set; }

        public List<EnumMemberListAssociation> GetAssociations() => EnumMemberListAssociations;

        public void SetRecords(List<EnumMember> records)
        {
            EnumMemberListAssociations = UniqueListAssociationsFactory<EnumMember, EnumMemberListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<EnumMember>.Create(records, r => r.EnumMemberId));
        }
    }
}
