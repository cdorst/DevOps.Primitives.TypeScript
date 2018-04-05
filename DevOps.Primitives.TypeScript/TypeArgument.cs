using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeArguments", Schema = nameof(TypeScript))]
    public class TypeArgument : IUniqueListRecord
    {
        public TypeArgument() { }
        public TypeArgument(Identifier identifier) { Identifier = identifier; }
        public TypeArgument(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int TypeArgumentId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        public string GetTypeArgumentSyntax()
            => Identifier.ToString();
    }
}
