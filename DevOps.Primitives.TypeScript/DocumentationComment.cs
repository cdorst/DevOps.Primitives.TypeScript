using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationComments", Schema = nameof(TypeScript))]
    public class DocumentationComment : IUniqueListRecord
    {
        public DocumentationComment() { }
        public DocumentationComment(AsciiMaxStringReference text)
        {
            Text = text;
        }
        public DocumentationComment(string text)
            : this(new AsciiMaxStringReference(text))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(3)]
        public int TextId { get; set; }

        public override string ToString() => Text.Value;
    }
}
