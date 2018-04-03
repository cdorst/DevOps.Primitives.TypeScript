using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ConstraintClauseListAssociations", Schema = nameof(TypeScript))]
    public class ConstraintClauseListAssociation : IUniqueListAssociation<ConstraintClause>
    {
        public ConstraintClauseListAssociation() { }
        public ConstraintClauseListAssociation(ConstraintClause constraintClause, ConstraintClauseList constraintClauseList = null)
        {
            ConstraintClause = constraintClause;
            ConstraintClauseList = constraintClauseList;
        }

        [Key]
        [ProtoMember(1)]
        public int ConstraintClauseListAssociationId { get; set; }

        [ProtoMember(2)]
        public ConstraintClause ConstraintClause { get; set; }
        [ProtoMember(3)]
        public int ConstraintClauseId { get; set; }

        [ProtoMember(4)]
        public ConstraintClauseList ConstraintClauseList { get; set; }
        [ProtoMember(5)]
        public int ConstraintClauseListId { get; set; }

        public ConstraintClause GetRecord() => ConstraintClause;

        public void SetRecord(ConstraintClause record)
        {
            ConstraintClause = record;
            ConstraintClauseId = ConstraintClause.ConstraintClauseId;
        }
    }
}
