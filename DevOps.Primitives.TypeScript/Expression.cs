using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Expressions", Schema = nameof(TypeScript))]
    public class Expression
    {
        public Expression() { }
        public Expression(AsciiMaxStringReference text) { Text = text; }
        public Expression(string text) :this(new AsciiMaxStringReference(text)) { }

        [Key]
        [ProtoMember(1)]
        public int ExpressionId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(3)]
        public int TextId { get; set; }
    }
}
