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
    [Table("ConstraintClauseLists", Schema = nameof(TypeScript))]
    public class ConstraintClauseList : IUniqueList<ConstraintClause, ConstraintClauseListAssociation>
    {
        public ConstraintClauseList() { }
        public ConstraintClauseList(List<ConstraintClauseListAssociation> constraintClauseListAssociations, AsciiStringReference listIdentifier = null)
        {
            ConstraintClauseListAssociations = constraintClauseListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ConstraintClauseList(ConstraintClauseListAssociation constraintClauseListAssociations, AsciiStringReference listIdentifier = null)
            : this(new List<ConstraintClauseListAssociation> { constraintClauseListAssociations }, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstraintClauseListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ConstraintClauseListAssociation> ConstraintClauseListAssociations { get; set; }

        public List<ConstraintClauseListAssociation> GetAssociations() => ConstraintClauseListAssociations;

        public void SetRecords(List<ConstraintClause> records)
        {
            ConstraintClauseListAssociations = UniqueListAssociationsFactory<ConstraintClause, ConstraintClauseListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<ConstraintClause>.Create(records, r => r.ConstraintClauseId));
        }
    }
}
