using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationComments", Schema = nameof(TypeScript))]
    public class DocumentationComment : IUniqueListRecord
    {
        public DocumentationComment() { }
        public DocumentationComment(in AsciiMaxStringReference text) => Text = text;
        public DocumentationComment(in string text) : this(new AsciiMaxStringReference(in text)) { }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(3)]
        public int TextId { get; set; }

        public string ToSelfClosingJsDoc() => Concat("/** ", ToString(), " */");

        public override string ToString() => Text.Value;
    }
}
