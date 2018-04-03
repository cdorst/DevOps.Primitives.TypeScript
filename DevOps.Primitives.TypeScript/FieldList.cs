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
    [Table("FieldLists", Schema = nameof(TypeScript))]
    public class FieldList : IUniqueList<Field, FieldListAssociation>
    {
        public FieldList() { }
        public FieldList(List<FieldListAssociation> fieldListAssociations, AsciiStringReference listIdentifier = null)
        {
            FieldListAssociations = fieldListAssociations;
            ListIdentifier = listIdentifier;
        }
        public FieldList(FieldListAssociation fieldListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<FieldListAssociation> { fieldListAssociation }, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int FieldListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<FieldListAssociation> FieldListAssociations { get; set; }

        public List<FieldListAssociation> GetAssociations() => FieldListAssociations;

        public void SetRecords(List<Field> records)
        {
            FieldListAssociations = UniqueListAssociationsFactory<Field, FieldListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Field>.Create(records, r => r.FieldId));
        }
    }
}
