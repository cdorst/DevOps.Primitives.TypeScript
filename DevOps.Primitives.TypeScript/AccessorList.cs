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
    [Table("AccessorLists", Schema = nameof(TypeScript))]
    public class AccessorList : IUniqueList<Accessor, AccessorListAssociation>
    {
        public AccessorList() { }
        public AccessorList(List<AccessorListAssociation> accessorListAssociations, AsciiStringReference listIdentifier = null)
        {
            AccessorListAssociations = accessorListAssociations;
            ListIdentifier = listIdentifier;
        }
        public AccessorList(AccessorListAssociation accessorListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<AccessorListAssociation> { accessorListAssociation }, listIdentifier)
        {
        }
        public AccessorList(Accessor accessor, AsciiStringReference listIdentifier = null)
            : this(new AccessorListAssociation(accessor), listIdentifier)
        {
        }
        public AccessorList(SyntaxToken syntaxToken, AsciiStringReference listIdentifier = null)
            : this(new Accessor(syntaxToken), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int AccessorListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<AccessorListAssociation> AccessorListAssociations { get; set; }

        public List<AccessorListAssociation> GetAssociations() => AccessorListAssociations;

        public void SetRecords(List<Accessor> records)
        {
            AccessorListAssociations = UniqueListAssociationsFactory<Accessor, AccessorListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Accessor>.Create(records, r => r.AccessorId));
        }
    }
}
