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
    [Table("ConstraintLists", Schema = nameof(TypeScript))]
    public class ConstraintList : IUniqueList<Constraint, ConstraintListAssociation>
    {
        public ConstraintList() { }
        public ConstraintList(List<ConstraintListAssociation> constraintListAssociations, AsciiStringReference listIdentifier = null)
        {
            ConstraintListAssociations = constraintListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ConstraintList(ConstraintListAssociation argumentListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ConstraintListAssociation> { argumentListAssociation }, listIdentifier)
        {
        }
        public ConstraintList(Constraint constraint, AsciiStringReference listIdentifier = null)
            : this(new ConstraintListAssociation(constraint), listIdentifier)
        {
        }
        public ConstraintList(Identifier constraint, AsciiStringReference listIdentifier = null)
            : this(new Constraint(constraint), listIdentifier)
        {
        }
        public ConstraintList(string constraint, AsciiStringReference listIdentifier = null)
            : this(new Identifier(constraint), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstraintListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ConstraintListAssociation> ConstraintListAssociations { get; set; }

        public List<ConstraintListAssociation> GetAssociations() => ConstraintListAssociations;

        public void SetRecords(List<Constraint> records)
        {
            ConstraintListAssociations = UniqueListAssociationsFactory<Constraint, ConstraintListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Constraint>.Create(records, r => r.ConstraintId));
        }
    }
}
