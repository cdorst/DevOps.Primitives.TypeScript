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
        public ImportStatementList(
            in List<ImportStatementListAssociation> importStatementListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            ImportStatementListAssociations = importStatementListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ImportStatementList(
            in ImportStatementListAssociation ImportStatementListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<ImportStatementListAssociation> { ImportStatementListAssociation }, in listIdentifier)
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

        public void SetRecords(in List<ImportStatement> records)
        {
            ImportStatementListAssociations = UniqueListAssociationsFactory<ImportStatement, ImportStatementListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<ImportStatement>.Create(in records, r => r.ImportStatementId));
        }
    }
}
