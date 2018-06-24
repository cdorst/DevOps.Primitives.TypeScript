using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeParameters", Schema = nameof(TypeScript))]
    public class TypeParameter : IUniqueListRecord
    {
        public TypeParameter() { }
        public TypeParameter(
            in Identifier identifier,
            in Identifier extendsConstraint = default)
        {
            Identifier = identifier;
            ExtendsConstraint = extendsConstraint;
        }
        public TypeParameter(
            in string identifier,
            in string extendsConstraint = default)
            : this(new Identifier(in identifier), new Identifier(in extendsConstraint))
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
                : Concat(Identifier.ToString(), " extends ", ExtendsConstraint.ToString());
    }
}
