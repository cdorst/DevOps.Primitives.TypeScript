using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Blocks", Schema = nameof(TypeScript))]
    public class Block
    {
        public Block() { }
        public Block(StatementList statementList) { StatementList = statementList; }

        [Key]
        [ProtoMember(1)]
        public int BlockId { get; set; }

        [ProtoMember(2)]
        public StatementList StatementList { get; set; }
        [ProtoMember(3)]
        public int? StatementListId { get; set; }
    }
}
