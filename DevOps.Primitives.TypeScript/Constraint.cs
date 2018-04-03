using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Constraints", Schema = nameof(TypeScript))]
    public class Constraint : IUniqueListRecord
    {
        public Constraint() { }
        public Constraint(Identifier identifier) { Identifier = identifier; }
        public Constraint(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int ConstraintId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }
    }
}
