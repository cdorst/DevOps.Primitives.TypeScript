using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("TypeArgumentLists", Schema = nameof(TypeScript))]
    public class TypeArgumentList : IUniqueList<TypeArgument, TypeArgumentListAssociation>
    {
        public TypeArgumentList() { }
        public TypeArgumentList(List<TypeArgumentListAssociation> typeArgumentListAssociations, AsciiStringReference listIdentifier = null)
        {
            TypeArgumentListAssociations = typeArgumentListAssociations;
            ListIdentifier = listIdentifier;
        }
        public TypeArgumentList(TypeArgumentListAssociation typeArgumentListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<TypeArgumentListAssociation> { typeArgumentListAssociation }, listIdentifier)
        {
        }
        public TypeArgumentList(Identifier typeArgument, AsciiStringReference listIdentifier = null)
            : this(new TypeArgumentListAssociation(typeArgument), listIdentifier)
        {
        }
        public TypeArgumentList(string typeArgument, AsciiStringReference listIdentifier = null)
            : this(new Identifier(typeArgument), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeArgumentListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<TypeArgumentListAssociation> TypeArgumentListAssociations { get; set; }

        public List<TypeArgumentListAssociation> GetAssociations() => TypeArgumentListAssociations;

        public void SetRecords(List<TypeArgument> records)
        {
            TypeArgumentListAssociations = UniqueListAssociationsFactory<TypeArgument, TypeArgumentListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<TypeArgument>.Create(records, r => r.TypeArgumentId));
        }

        public string GetTypeArgumentListSyntax()
            => $"<{string.Join(CommaSpace, this.GetRecords().Select(arg => arg.GetTypeArgumentSyntax()))}>";
    }
}
