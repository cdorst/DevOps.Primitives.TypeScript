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
        public StatementListAssociation(Statement statement, StatementList statementList = null)
        {
            Statement = statement;
            StatementList = statementList;
        }
        public StatementListAssociation(AsciiMaxStringReference statement, StatementList statementList = null)
            : this(new Statement(statement), statementList)
        {
        }
        public StatementListAssociation(string statement, StatementList statementList = null)
            : this(new AsciiMaxStringReference(statement), statementList)
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

        public void SetRecord(Statement record)
        {
            Statement = record;
            StatementId = Statement.StatementId;
        }
    }
}
