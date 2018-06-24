using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeParameterLists", Schema = nameof(TypeScript))]
    public class TypeParameterList : IUniqueList<TypeParameter, TypeParameterListAssociation>
    {
        public TypeParameterList() { }
        public TypeParameterList(
            in List<TypeParameterListAssociation> typeParameterListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            TypeParameterListAssociations = typeParameterListAssociations;
            ListIdentifier = listIdentifier;
        }
        public TypeParameterList(
            in TypeParameterListAssociation typeParameterListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<TypeParameterListAssociation> { typeParameterListAssociation }, in listIdentifier)
        {
        }
        public TypeParameterList(
            in Identifier typeParameter,
            in AsciiStringReference listIdentifier = default)
            : this(new TypeParameterListAssociation(in typeParameter), in listIdentifier)
        {
        }
        public TypeParameterList(
            in string typeParameter,
            in AsciiStringReference listIdentifier = default)
            : this(new Identifier(in typeParameter), in listIdentifier)
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

        public void SetRecords(in List<TypeParameter> records)
        {
            TypeParameterListAssociations = UniqueListAssociationsFactory<TypeParameter, TypeParameterListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<TypeParameter>.Create(in records, r => r.TypeParameterId));
        }

        public string GetTypeParameterListSyntax()
            => Concat("<", Join(CommaSpace, this.GetRecords().Select(arg => arg.GetTypeParameterSyntax())), ">");
    }
}
