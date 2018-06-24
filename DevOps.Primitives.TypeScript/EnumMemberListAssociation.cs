using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("EnumMemberListAssociations", Schema = nameof(TypeScript))]
    public class EnumMemberListAssociation : IUniqueListAssociation<EnumMember>
    {
        public EnumMemberListAssociation() { }
        public EnumMemberListAssociation(in EnumMember enumMember, in EnumMemberList enumMemberList = default)
        {
            EnumMember = enumMember;
            EnumMemberList = enumMemberList;
        }

        [Key]
        [ProtoMember(1)]
        public int EnumMemberListAssociationId { get; set; }

        [ProtoMember(2)]
        public EnumMember EnumMember { get; set; }
        [ProtoMember(3)]
        public int EnumMemberId { get; set; }

        [ProtoMember(4)]
        public EnumMemberList EnumMemberList { get; set; }
        [ProtoMember(5)]
        public int EnumMemberListId { get; set; }

        public EnumMember GetRecord() => EnumMember;

        public void SetRecord(in EnumMember record)
        {
            EnumMember = record;
            EnumMemberId = record.EnumMemberId;
        }
    }
}
