using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeParameterListAssociations", Schema = nameof(TypeScript))]
    public class TypeParameterListAssociation : IUniqueListAssociation<TypeParameter>
    {
        public TypeParameterListAssociation() { }
        public TypeParameterListAssociation(TypeParameter typeParameter, TypeParameterList typeParameterList = null)
        {
            TypeParameter = typeParameter;
            TypeParameterList = typeParameterList;
        }
        public TypeParameterListAssociation(Identifier typeParameter, TypeParameterList typeParameterList = null)
            : this(new TypeParameter(typeParameter), typeParameterList)
        {
        }
        public TypeParameterListAssociation(string typeParameter, TypeParameterList typeParameterList = null)
            : this(new Identifier(typeParameter), typeParameterList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeParameterListAssociationId { get; set; }

        [ProtoMember(2)]
        public TypeParameter TypeParameter { get; set; }
        [ProtoMember(3)]
        public int TypeParameterId { get; set; }

        [ProtoMember(4)]
        public TypeParameterList TypeParameterList { get; set; }
        [ProtoMember(5)]
        public int TypeParameterListId { get; set; }

        public TypeParameter GetRecord() => TypeParameter;

        public void SetRecord(TypeParameter record)
        {
            TypeParameter = record;
            TypeParameterId = TypeParameter.TypeParameterId;
        }
    }
}
