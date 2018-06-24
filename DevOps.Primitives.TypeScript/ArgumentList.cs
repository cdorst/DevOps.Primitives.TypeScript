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
    [Table("ArgumentLists", Schema = nameof(TypeScript))]
    public class ArgumentList : IUniqueList<Argument, ArgumentListAssociation>
    {
        public ArgumentList() { }
        public ArgumentList(
            in List<ArgumentListAssociation> argumentListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            ArgumentListAssociations = argumentListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ArgumentList(
            in ArgumentListAssociation argumentListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<ArgumentListAssociation> { argumentListAssociation }, in listIdentifier)
        {
        }
        public ArgumentList(
            in Argument argument,
            in AsciiStringReference listIdentifier = default)
            : this(new ArgumentListAssociation(in argument), in listIdentifier)
        {
        }
        public ArgumentList(
            in Identifier argument,
            in AsciiStringReference listIdentifier = default)
            : this(new Argument(in argument), in listIdentifier)
        {
        }
        public ArgumentList(
            in string argument,
            in AsciiStringReference listIdentifier = default)
            : this(new Identifier(in argument), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ArgumentListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ArgumentListAssociation> ArgumentListAssociations { get; set; }

        public List<ArgumentListAssociation> GetAssociations() => ArgumentListAssociations;

        public void SetRecords(in List<Argument> records)
        {
            ArgumentListAssociations = UniqueListAssociationsFactory<Argument, ArgumentListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Argument>.Create(in records, r => r.ArgumentId));
        }
    }
}
