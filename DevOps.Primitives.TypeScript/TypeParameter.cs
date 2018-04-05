using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeParameters", Schema = nameof(TypeScript))]
    public class TypeParameter : IUniqueListRecord
    {
        public TypeParameter() { }
        public TypeParameter(Identifier identifier) { Identifier = identifier; }
        public TypeParameter(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int TypeParameterId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        public string GetTypeParameterSyntax()
            => Identifier.ToString();
    }
}
