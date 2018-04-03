using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ImportStatementListAssociations", Schema = nameof(TypeScript))]
    public class ImportStatementListAssociation : IUniqueListAssociation<ImportStatement>
    {
        public ImportStatementListAssociation() { }
        public ImportStatementListAssociation(ImportStatement importStatement, ImportStatementList importStatementList = null)
        {
            ImportStatement = importStatement;
            ImportStatementList = importStatementList;
        }

        [Key]
        [ProtoMember(1)]
        public int ImportStatementListAssociationId { get; set; }

        [ProtoMember(2)]
        public ImportStatement ImportStatement { get; set; }
        [ProtoMember(3)]
        public int ImportStatementId { get; set; }

        [ProtoMember(4)]
        public ImportStatementList ImportStatementList { get; set; }
        [ProtoMember(5)]
        public int ImportStatementListId { get; set; }

        public ImportStatement GetRecord() => ImportStatement;

        public void SetRecord(ImportStatement record)
        {
            ImportStatement = record;
            ImportStatementId = ImportStatement.ImportStatementId;
        }
    }
}
