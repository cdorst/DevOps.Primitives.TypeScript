using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Blocks", Schema = nameof(TypeScript))]
    public class Block
    {
        public Block() { }
        public Block(in StatementList statementList) => StatementList = statementList;

        [Key]
        [ProtoMember(1)]
        public int BlockId { get; set; }

        [ProtoMember(2)]
        public StatementList StatementList { get; set; }
        [ProtoMember(3)]
        public int? StatementListId { get; set; }

        public string GetBlockSyntax()
        {
            if (StatementList == null) return "{ }";
            var stringBuilder = new StringBuilder().Append(OpenCurlyBrace);
            foreach (var statement in StatementList.GetRecords()) stringBuilder.AppendLine(Concat(Indent, statement));
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
