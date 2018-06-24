using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Statements", Schema = nameof(TypeScript))]
    public class Statement : IUniqueListRecord
    {
        public Statement() { }
        public Statement(in AsciiMaxStringReference text) => Text = text;
        public Statement(in string text) : this(new AsciiMaxStringReference(in text)) { }

        [Key]
        [ProtoMember(1)]
        public int StatementId { get; set; }

        [ProtoMember(2)]
        public AsciiMaxStringReference Text { get; set; }
        [ProtoMember(3)]
        public int TextId { get; set; }

        public override string ToString() => Text.Value;
    }
}
