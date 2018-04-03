using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("SyntaxTokens", Schema = nameof(TypeScript))]
    public class SyntaxToken : IUniqueListRecord
    {
        public SyntaxToken() { }
        public SyntaxToken(AsciiStringReference syntaxKind) { SyntaxKind = syntaxKind; }

        [Key]
        [ProtoMember(1)]
        public short SyntaxTokenId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference SyntaxKind { get; set; }
        [ProtoMember(3)]
        public int SyntaxKindId { get; set; }
    }
}
