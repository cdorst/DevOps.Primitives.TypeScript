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
        public TypeParameter(Identifier identifier, Identifier extendsConstraint = null)
        {
            Identifier = identifier;
            ExtendsConstraint = extendsConstraint;
        }
        public TypeParameter(string identifier, string extendsConstraint = null)
            : this(new Identifier(identifier), new Identifier(extendsConstraint))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeParameterId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        [ProtoMember(4)]
        public Identifier ExtendsConstraint { get; set; }
        [ProtoMember(5)]
        public int? ExtendsConstraintId { get; set; }

        public string GetTypeParameterSyntax()
            => ExtendsConstraint == null
                ? Identifier.ToString()
                : $"{Identifier} extends {ExtendsConstraint}";
    }
}
