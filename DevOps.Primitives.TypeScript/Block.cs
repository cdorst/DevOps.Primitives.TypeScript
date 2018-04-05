using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

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

        public string GetBlockSyntax()
        {
            var stringBuilder = new StringBuilder().Append(OpenCurlyBrace);
            foreach (var statement in StatementList.GetRecords()) stringBuilder.AppendLine($"{Indent}{statement}");
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
