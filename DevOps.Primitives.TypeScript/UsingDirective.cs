using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("UsingDirectives", Schema = nameof(TypeScript))]
    public class UsingDirective : IUniqueListRecord
    {
        public UsingDirective() { }
        public UsingDirective(Identifier identifier) { Identifier = identifier; }
        public UsingDirective(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int UsingDirectiveId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }
    }
}
