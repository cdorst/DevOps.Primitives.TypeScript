using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ConstraintClauses", Schema = nameof(TypeScript))]
    public class ConstraintClause : IUniqueListRecord
    {
        public ConstraintClause() { }
        public ConstraintClause(Identifier identifier, ConstraintList constraintList)
        {
            Identifier = identifier;
            ConstraintList = constraintList;
        }
        public ConstraintClause(string identifier, ConstraintList constraintList)
            : this(new Identifier(identifier), constraintList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstraintClauseId { get; set; }

        [ProtoMember(2)]
        public ConstraintList ConstraintList { get; set; }
        [ProtoMember(3)]
        public int ConstraintListId { get; set; }

        [ProtoMember(4)]
        public Identifier Identifier { get; set; }
        [ProtoMember(5)]
        public int IdentifierId { get; set; }
    }
}
