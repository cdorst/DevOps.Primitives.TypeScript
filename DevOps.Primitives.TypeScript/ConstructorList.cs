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
    [Table("ConstructorLists", Schema = nameof(TypeScript))]
    public class ConstructorList : IUniqueList<Constructor, ConstructorListAssociation>
    {
        public ConstructorList() { }
        public ConstructorList(List<ConstructorListAssociation> constructorListAssociations, AsciiStringReference listIdentifier = null)
        {
            ConstructorListAssociations = constructorListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ConstructorList(ConstructorListAssociation constructorListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ConstructorListAssociation> { constructorListAssociation }, listIdentifier)
        {
        }
        public ConstructorList(Constructor constructor, AsciiStringReference listIdentifier = null)
            : this(new ConstructorListAssociation(constructor), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstructorListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ConstructorListAssociation> ConstructorListAssociations { get; set; }

        public List<ConstructorListAssociation> GetAssociations() => ConstructorListAssociations;

        public void SetRecords(List<Constructor> records)
        {
            ConstructorListAssociations = UniqueListAssociationsFactory<Constructor, ConstructorListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Constructor>.Create(records, r => r.ConstructorId));
        }
    }
}
