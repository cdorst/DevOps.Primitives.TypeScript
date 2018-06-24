using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("StatementListAssociations", Schema = nameof(TypeScript))]
    public class StatementListAssociation : IUniqueListAssociation<Statement>
    {
        public StatementListAssociation() { }
        public StatementListAssociation(
            in Statement statement,
            in StatementList statementList = default)
        {
            Statement = statement;
            StatementList = statementList;
        }
        public StatementListAssociation(
            in AsciiMaxStringReference statement,
            in StatementList statementList = default)
            : this(new Statement(in statement), in statementList)
        {
        }
        public StatementListAssociation(
            in string statement,
            in StatementList statementList = default)
            : this(new AsciiMaxStringReference(in statement), in statementList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int StatementListAssociationId { get; set; }

        [ProtoMember(2)]
        public Statement Statement { get; set; }
        [ProtoMember(3)]
        public int StatementId { get; set; }

        [ProtoMember(4)]
        public StatementList StatementList { get; set; }
        [ProtoMember(5)]
        public int StatementListId { get; set; }

        public Statement GetRecord() => Statement;

        public void SetRecord(in Statement record)
        {
            Statement = record;
            StatementId = record.StatementId;
        }
    }
}
