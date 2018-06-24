using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("EnumMembers", Schema = nameof(TypeScript))]
    public class EnumMember : IUniqueListRecord
    {
        public EnumMember() { }
        public EnumMember(
            in Identifier identifier,
            in DocumentationComment comment,
            in UnicodeStringReference equalsValue = default)
        {
            Identifier = identifier;
            DocumentationComment = comment;
            EqualsValue = equalsValue;
        }
        public EnumMember(
            in string identifier,
            in string comment,
            in string equalsValue = default)
            : this(new Identifier(in identifier), new DocumentationComment(in comment), new UnicodeStringReference(in equalsValue))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int EnumMemberId { get; set; }

        [ProtoMember(2)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(3)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(4)]
        public UnicodeStringReference EqualsValue { get; set; }
        [ProtoMember(5)]
        public int? EqualsValueId { get; set; }

        [ProtoMember(6)]
        public Identifier Identifier { get; set; }
        [ProtoMember(7)]
        public int IdentifierId { get; set; }

        public string GetEnumMemberSyntax()
        {
            var assignment = EqualsValue == null ? Empty : Concat(" = ", EqualsValue.Value);
            return Concat(DocumentationComment.ToSelfClosingJsDoc(), NewLine, Identifier, assignment);
        }
    }
}
