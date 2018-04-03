using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeParameterLists", Schema = nameof(TypeScript))]
    public class TypeParameterList : IUniqueList<TypeParameter, TypeParameterListAssociation>
    {
        public TypeParameterList() { }
        public TypeParameterList(List<TypeParameterListAssociation> typeParameterListAssociations, AsciiStringReference listIdentifier = null)
        {
            TypeParameterListAssociations = typeParameterListAssociations;
            ListIdentifier = listIdentifier;
        }
        public TypeParameterList(TypeParameterListAssociation typeParameterListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<TypeParameterListAssociation> { typeParameterListAssociation }, listIdentifier)
        {
        }
        public TypeParameterList(Identifier typeParameter, AsciiStringReference listIdentifier = null)
            : this(new TypeParameterListAssociation(typeParameter), listIdentifier)
        {
        }
        public TypeParameterList(string typeParameter, AsciiStringReference listIdentifier = null)
            : this(new Identifier(typeParameter), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeParameterListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<TypeParameterListAssociation> TypeParameterListAssociations { get; set; }

        public List<TypeParameterListAssociation> GetAssociations() => TypeParameterListAssociations;

        public void SetRecords(List<TypeParameter> records)
        {
            TypeParameterListAssociations = UniqueListAssociationsFactory<TypeParameter, TypeParameterListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<TypeParameter>.Create(records, r => r.TypeParameterId));
        }
    }
}
