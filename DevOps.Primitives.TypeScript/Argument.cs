using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Arguments", Schema = nameof(TypeScript))]
    public class Argument : IUniqueListRecord
    {
        public Argument() { }
        public Argument(Identifier identifier) { Identifier = identifier; }
        public Argument(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int ArgumentId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }
    }
}
