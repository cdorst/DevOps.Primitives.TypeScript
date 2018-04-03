using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ArgumentLists", Schema = nameof(TypeScript))]
    public class ArgumentList : IUniqueList<Argument, ArgumentListAssociation>
    {
        public ArgumentList() { }
        public ArgumentList(List<ArgumentListAssociation> argumentListAssociations, AsciiStringReference listIdentifier = null)
        {
            ArgumentListAssociations = argumentListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ArgumentList(ArgumentListAssociation argumentListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ArgumentListAssociation> { argumentListAssociation }, listIdentifier)
        {
        }
        public ArgumentList(Argument argument, AsciiStringReference listIdentifier = null)
            : this(new ArgumentListAssociation(argument), listIdentifier)
        {
        }
        public ArgumentList(Identifier argument, AsciiStringReference listIdentifier = null)
            : this(new Argument(argument), listIdentifier)
        {
        }
        public ArgumentList(string argument, AsciiStringReference listIdentifier = null)
            : this(new Identifier(argument), listIdentifier)
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

        public void SetRecords(List<Argument> records)
        {
            ArgumentListAssociations = UniqueListAssociationsFactory<Argument, ArgumentListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Argument>.Create(records, r => r.ArgumentId));
        }
    }
}
