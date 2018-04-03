using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Accessors", Schema = nameof(TypeScript))]
    public class Accessor : IUniqueListRecord
    {
        public Accessor() { }
        public Accessor(SyntaxToken syntaxToken, Block block = null)
        {
            SyntaxToken = syntaxToken;
            Body = block;
        }

        [Key]
        [ProtoMember(1)]
        public int AccessorId { get; set; }

        [ProtoMember(2)]
        public Block Body { get; set; }
        [ProtoMember(3)]
        public int? BodyId { get; set; }

        [ProtoMember(4)]
        public SyntaxToken SyntaxToken { get; set; }
        [ProtoMember(5)]
        public short SyntaxTokenId { get; set; }
    }
}
