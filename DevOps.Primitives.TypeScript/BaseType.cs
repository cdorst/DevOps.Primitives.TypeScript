using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("BaseTypes", Schema = nameof(TypeScript))]
    public class BaseType : IUniqueListRecord
    {
        public BaseType() { }
        public BaseType(Identifier identifier, TypeArgumentList typeArgumentList = null)
        {
            Identifier = identifier;
            TypeArgumentList = typeArgumentList;
        }
        public BaseType(string identifier, TypeArgumentList typeArgumentList = null)
            : this(new Identifier(identifier), typeArgumentList)
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
            => $"{Identifier}{TypeArgumentList?.GetTypeArgumentListSyntax()}";
    }
}
