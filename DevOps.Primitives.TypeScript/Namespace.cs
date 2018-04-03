using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Namespaces", Schema = nameof(TypeScript))]
    public class Namespace
    {
        public Namespace() { }
        public Namespace(Identifier identifier) { Identifier = identifier; }
        public Namespace(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int NamespaceId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }
    }
}
