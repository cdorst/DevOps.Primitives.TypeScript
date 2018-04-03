using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ConstraintListAssociations", Schema = nameof(TypeScript))]
    public class ConstraintListAssociation : IUniqueListAssociation<Constraint>
    {
        public ConstraintListAssociation() { }
        public ConstraintListAssociation(Constraint constraint, ConstraintList constraintList = null)
        {
            Constraint = constraint;
            ConstraintList = constraintList;
        }
        public ConstraintListAssociation(Identifier constraint, ConstraintList constraintList = null)
            : this(new Constraint(constraint), constraintList)
        {
        }
        public ConstraintListAssociation(string constraint, ConstraintList constraintList = null)
            : this(new Identifier(constraint), constraintList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstraintListAssociationId { get; set; }

        [ProtoMember(2)]
        public Constraint Constraint { get; set; }
        [ProtoMember(3)]
        public int ConstraintId { get; set; }

        [ProtoMember(4)]
        public ConstraintList ConstraintList { get; set; }
        [ProtoMember(5)]
        public int ConstraintListId { get; set; }

        public Constraint GetRecord() => Constraint;

        public void SetRecord(Constraint record)
        {
            Constraint = record;
            ConstraintId = Constraint.ConstraintId;
        }
    }
}
