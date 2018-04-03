using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationComments", Schema = nameof(TypeScript))]
    public class DocumentationComment : IUniqueListRecord
    {
        public const string SummaryElement = "summary";

        public DocumentationComment() { }
        public DocumentationComment(Identifier identifier, AsciiMaxStringReference text, bool includeNewLine = false, byte indentLevel = byte.MinValue)
        {
            Identifier = identifier;
            Text = text;
            IncludeNewLine = includeNewLine;
            IndentLevel = indentLevel;
        }
        public DocumentationComment(string text, string identifier = SummaryElement, bool includeNewLine = false, byte indentLevel = byte.MinValue)
            : this(new Identifier(identifier), new AsciiMaxStringReference(text), includeNewLine, indentLevel)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(2)]
        public bool IncludeNewLine { get; set; }

        [ProtoMember(3)]
        public byte IndentLevel { get; set; }

        [ProtoMember(4)]
        public Identifier Identifier { get; set; }
        [ProtoMember(5)]
        public int IdentifierId { get; set; }

        [ProtoMember(6)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(7)]
        public int TextId { get; set; }

        private string Indent()
            => string.Join(string.Empty, indent());

        private IEnumerable<string> indent()
        {
            for (int i = 0; i < IndentLevel; i++)
            {
                yield return "    ";
            }
        }
    }
}
