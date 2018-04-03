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
        public TypeArgumentListAssociation(TypeArgument typeArgument, TypeArgumentList typeArgumentList = null)
        {
            TypeArgument = typeArgument;
            TypeArgumentList = typeArgumentList;
        }
        public TypeArgumentListAssociation(Identifier typeArgument, TypeArgumentList typeArgumentList = null)
            : this(new TypeArgument(typeArgument), typeArgumentList)
        {
        }
        public TypeArgumentListAssociation(string typeArgument, TypeArgumentList typeArgumentList = null)
            : this(new Identifier(typeArgument), typeArgumentList)
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

        public void SetRecord(TypeArgument record)
        {
            TypeArgument = record;
            TypeArgumentId = TypeArgument.TypeArgumentId;
        }
    }
}
