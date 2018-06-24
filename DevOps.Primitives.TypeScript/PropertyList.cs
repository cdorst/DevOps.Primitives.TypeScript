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
    [Table("PropertyLists", Schema = nameof(TypeScript))]
    public class PropertyList : IUniqueList<Property, PropertyListAssociation>
    {
        public PropertyList() { }
        public PropertyList(
            in List<PropertyListAssociation> propertyListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            PropertyListAssociations = propertyListAssociations;
            ListIdentifier = listIdentifier;
        }
        public PropertyList(
            in PropertyListAssociation propertyListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<PropertyListAssociation> { propertyListAssociation }, in listIdentifier)
        {
        }
        public PropertyList(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment documentationComment,
            in AccessModifiers? accessModifier = default,
            in bool isStatic = default,
            in bool isReadonly = default,
            in DecoratorList decoratorList = default,
            in Expression defaultValue = default,
            in AsciiStringReference listIdentifier = default)
            : this(
                  new PropertyListAssociation(in identifier, in type, in documentationComment, in accessModifier, in isStatic, in isReadonly, in decoratorList, in defaultValue),
                  in listIdentifier)
        {
        }
        public PropertyList(
            in string identifier,
            in string type,
            in string comment,
            in AccessModifiers? accessModifier = default,
            in bool isStatic = default,
            in bool isReadonly = default,
            in DecoratorList decoratorList = default,
            in Expression defaultValue = default,
            in AsciiStringReference listIdentifier = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in accessModifier,
                  in isStatic,
                  in isReadonly,
                  in decoratorList,
                  in defaultValue,
                  in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int PropertyListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<PropertyListAssociation> PropertyListAssociations { get; set; }

        public List<PropertyListAssociation> GetAssociations() => PropertyListAssociations;

        public void SetRecords(in List<Property> records)
        {
            PropertyListAssociations = UniqueListAssociationsFactory<Property, PropertyListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Property>.Create(in records, r => r.PropertyId));
        }
    }
}
