using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ImportStatementLists", Schema = nameof(TypeScript))]
    public class ImportStatementList : IUniqueList<ImportStatement, ImportStatementListAssociation>
    {
        public ImportStatementList() { }
        public ImportStatementList(List<ImportStatementListAssociation> importStatementListAssociations, AsciiStringReference listIdentifier = null)
        {
            ImportStatementListAssociations = importStatementListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ImportStatementList(ImportStatementListAssociation ImportStatementListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ImportStatementListAssociation> { ImportStatementListAssociation }, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ImportStatementListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ImportStatementListAssociation> ImportStatementListAssociations { get; set; }

        public List<ImportStatementListAssociation> GetAssociations() => ImportStatementListAssociations;

        public void SetRecords(List<ImportStatement> records)
        {
            ImportStatementListAssociations = UniqueListAssociationsFactory<ImportStatement, ImportStatementListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<ImportStatement>.Create(records, r => r.ImportStatementId));
        }
    }
}
