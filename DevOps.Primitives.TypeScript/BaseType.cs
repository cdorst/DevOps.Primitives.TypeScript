using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("BaseTypes", Schema = nameof(TypeScript))]
    public class BaseType : IUniqueListRecord
    {
        public BaseType() { }
        public BaseType(
            in Identifier identifier,
            in TypeArgumentList typeArgumentList = default)
        {
            Identifier = identifier;
            TypeArgumentList = typeArgumentList;
        }
        public BaseType(
            in string identifier,
            in TypeArgumentList typeArgumentList = default)
            : this(new Identifier(in identifier), in typeArgumentList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int BaseTypeId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        [ProtoMember(4)]
        public TypeArgumentList TypeArgumentList { get; set; }
        [ProtoMember(5)]
        public int? TypeArgumentListId { get; set; }

        public string GetBaseTypeSyntax()
            => Concat(Identifier.ToString(), TypeArgumentList?.GetTypeArgumentListSyntax() ?? Empty);
    }
}
