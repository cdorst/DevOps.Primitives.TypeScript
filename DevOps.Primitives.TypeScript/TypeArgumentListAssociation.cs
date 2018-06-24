using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeArgumentListAssociations", Schema = nameof(TypeScript))]
    public class TypeArgumentListAssociation : IUniqueListAssociation<TypeArgument>
    {
        public TypeArgumentListAssociation() { }
        public TypeArgumentListAssociation(
            in TypeArgument typeArgument,
            in TypeArgumentList typeArgumentList = default)
        {
            TypeArgument = typeArgument;
            TypeArgumentList = typeArgumentList;
        }
        public TypeArgumentListAssociation(
            in Identifier typeArgument,
            in TypeArgumentList typeArgumentList = default)
            : this(new TypeArgument(in typeArgument), in typeArgumentList)
        {
        }
        public TypeArgumentListAssociation(
            in string typeArgument,
            in TypeArgumentList typeArgumentList = default)
            : this(new Identifier(in typeArgument), in typeArgumentList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeArgumentListAssociationId { get; set; }

        [ProtoMember(2)]
        public TypeArgument TypeArgument { get; set; }
        [ProtoMember(3)]
        public int TypeArgumentId { get; set; }

        [ProtoMember(4)]
        public TypeArgumentList TypeArgumentList { get; set; }
        [ProtoMember(5)]
        public int TypeArgumentListId { get; set; }

        public TypeArgument GetRecord() => TypeArgument;

        public void SetRecord(in TypeArgument record)
        {
            TypeArgument = record;
            TypeArgumentId = record.TypeArgumentId;
        }
    }
}
