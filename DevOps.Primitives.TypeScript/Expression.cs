using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Expressions", Schema = nameof(TypeScript))]
    public class Expression
    {
        public Expression() { }
        public Expression(in AsciiMaxStringReference text) => Text = text;
        public Expression(in string text) :this(new AsciiMaxStringReference(in text)) { }

        [Key]
        [ProtoMember(1)]
        public int ExpressionId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(3)]
        public int TextId { get; set; }

        public string GetDefaultValueAssignmentSyntax() => Concat(" = ", ToString());

        public override string ToString() => Text.Value;
    }
}
