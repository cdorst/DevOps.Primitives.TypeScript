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
            DocumentationComment documentationComment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null,
            PropertyList propertyList = null)
            : this(new Property(identifier, type, documentationComment, accessModifier, isStatic, isReadonly, decoratorList, defaultValue), propertyList)
        {
        }
        public PropertyListAssociation(
            string identifier,
            string type,
            string comment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null,
            PropertyList propertyList = null)
            : this(new Identifier(identifier), new Identifier(type), new DocumentationComment(comment), accessModifier, isStatic, isReadonly, decoratorList, defaultValue, propertyList)
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
