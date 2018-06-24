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
        public TypeParameterListAssociation(
            in TypeParameter typeParameter,
            in TypeParameterList typeParameterList = default)
        {
            TypeParameter = typeParameter;
            TypeParameterList = typeParameterList;
        }
        public TypeParameterListAssociation(
            in Identifier typeParameter,
            in TypeParameterList typeParameterList = default)
            : this(new TypeParameter(in typeParameter), in typeParameterList)
        {
        }
        public TypeParameterListAssociation(
            in string typeParameter,
            in TypeParameterList typeParameterList = default)
            : this(new Identifier(in typeParameter), in typeParameterList)
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

        public void SetRecord(in TypeParameter record)
        {
            TypeParameter = record;
            TypeParameterId = record.TypeParameterId;
        }
    }
}
