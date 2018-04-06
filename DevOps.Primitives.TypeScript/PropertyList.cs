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
        public PropertyList(List<PropertyListAssociation> propertyListAssociations, AsciiStringReference listIdentifier = null)
        {
            PropertyListAssociations = propertyListAssociations;
            ListIdentifier = listIdentifier;
        }
        public PropertyList(PropertyListAssociation propertyListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<PropertyListAssociation> { propertyListAssociation }, listIdentifier)
        {
        }
        public PropertyList(
            Identifier identifier,
            Identifier type,
            DocumentationComment documentationComment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null,
            AsciiStringReference listIdentifier = null)
            : this(new PropertyListAssociation(identifier, type, documentationComment, accessModifier, isStatic, isReadonly, decoratorList, defaultValue), listIdentifier)
        {
        }
        public PropertyList(
            string identifier,
            string type,
            string comment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null,
            AsciiStringReference listIdentifier = null)
            : this(new Identifier(identifier), new Identifier(type), new DocumentationComment(comment), accessModifier, isStatic, isReadonly, decoratorList, defaultValue, listIdentifier)
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

        public void SetRecords(List<Property> records)
        {
            PropertyListAssociations = UniqueListAssociationsFactory<Property, PropertyListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Property>.Create(records, r => r.PropertyId));
        }
    }
}
