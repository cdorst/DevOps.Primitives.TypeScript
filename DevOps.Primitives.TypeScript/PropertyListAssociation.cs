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
        public PropertyListAssociation(
            in Property property,
            in PropertyList propertyList = default)
        {
            Property = property;
            PropertyList = propertyList;
        }
        public PropertyListAssociation(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment documentationComment,
            in AccessModifiers? accessModifier = default,
            in bool isStatic = default,
            in bool isReadonly = default,
            in DecoratorList decoratorList = default,
            in Expression defaultValue = default,
            in PropertyList propertyList = default)
            : this(new Property(in identifier, in type, in documentationComment, in accessModifier, in isStatic, in isReadonly, in decoratorList, in defaultValue), in propertyList)
        {
        }
        public PropertyListAssociation(
            string identifier,
            string type,
            string comment,
            AccessModifiers? accessModifier = default,
            bool isStatic = default,
            bool isReadonly = default,
            DecoratorList decoratorList = default,
            Expression defaultValue = default,
            PropertyList propertyList = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in accessModifier,
                  in isStatic,
                  in isReadonly,
                  in decoratorList,
                  in defaultValue,
                  in propertyList)
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

        public void SetRecord(in Property record)
        {
            Property = record;
            PropertyId = record.PropertyId;
        }
    }
}
