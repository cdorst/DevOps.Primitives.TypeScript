using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("PropertyListAssociations", Schema = nameof(TypeScript))]
    public class PropertyListAssociation : IUniqueListAssociation<Property>
    {
        public PropertyListAssociation() { }
        public PropertyListAssociation(Property property, PropertyList propertyList = null)
        {
            Property = property;
            PropertyList = propertyList;
        }
        public PropertyListAssociation(
            Identifier identifier,
            Identifier type,
            AccessorList accessorList,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            PropertyList propertyList = null)
            : this(new Property(identifier, type, accessorList, modifierList, documentationCommentList, attributeListCollection), propertyList)
        {
        }
        public PropertyListAssociation(
            string identifier,
            string type,
            AccessorList accessorList,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            PropertyList propertyList = null)
            : this(new Identifier(identifier), new Identifier(type), accessorList, modifierList, documentationCommentList, attributeListCollection, propertyList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int PropertyListAssociationId { get; set; }

        [ProtoMember(2)]
        public Property Property { get; set; }
        [ProtoMember(3)]
        public int PropertyId { get; set; }

        [ProtoMember(4)]
        public PropertyList PropertyList { get; set; }
        [ProtoMember(5)]
        public int PropertyListId { get; set; }

        public Property GetRecord() => Property;

        public void SetRecord(Property record)
        {
            Property = record;
            PropertyId = Property.PropertyId;
        }
    }
}
