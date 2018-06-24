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
    [Table("TypeArgumentLists", Schema = nameof(TypeScript))]
    public class TypeArgumentList : IUniqueList<TypeArgument, TypeArgumentListAssociation>
    {
        public TypeArgumentList() { }
        public TypeArgumentList(
            in List<TypeArgumentListAssociation> typeArgumentListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            TypeArgumentListAssociations = typeArgumentListAssociations;
            ListIdentifier = listIdentifier;
        }
        public TypeArgumentList(
            in TypeArgumentListAssociation typeArgumentListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<TypeArgumentListAssociation> { typeArgumentListAssociation }, in listIdentifier)
        {
        }
        public TypeArgumentList(
            in Identifier typeArgument,
            in AsciiStringReference listIdentifier = default)
            : this(new TypeArgumentListAssociation(in typeArgument), in listIdentifier)
        {
        }
        public TypeArgumentList(
            in string typeArgument,
            in AsciiStringReference listIdentifier = default)
            : this(new Identifier(in typeArgument), in listIdentifier)
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

        public void SetRecords(in List<TypeArgument> records)
        {
            TypeArgumentListAssociations = UniqueListAssociationsFactory<TypeArgument, TypeArgumentListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<TypeArgument>.Create(in records, r => r.TypeArgumentId));
        }

        public string GetTypeArgumentListSyntax()
            => Concat("<", Join(CommaSpace, this.GetRecords().Select(arg => arg.GetTypeArgumentSyntax())), ">");
    }
}
