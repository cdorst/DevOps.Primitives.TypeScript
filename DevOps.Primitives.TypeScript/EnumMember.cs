using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("EnumMembers", Schema = nameof(TypeScript))]
    public class EnumMember : IUniqueListRecord
    {
        public EnumMember() { }
        public EnumMember(Identifier identifier, DocumentationComment comment, UnicodeStringReference equalsValue = null)
        {
            Identifier = identifier;
            DocumentationComment = comment;
            EqualsValue = equalsValue;
        }
        public EnumMember(string identifier, string comment, string equalsValue = null)
            : this(new Identifier(identifier), new DocumentationComment(comment), new UnicodeStringReference(equalsValue))
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
            var assignment = EqualsValue == null ? string.Empty : $" = {EqualsValue.Value}";
            return $"{DocumentationComment.ToSelfClosingJsDoc()}{NewLine}{Identifier}{assignment}";
        }
    }
}
