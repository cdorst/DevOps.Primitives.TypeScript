using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Fields", Schema = nameof(TypeScript))]
    public class Field : IUniqueListRecord
    {
        public Field() { }
        public Field(
            Identifier identifier,
            Identifier type,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            Expression initializer = null,
            DecoratorList attributeListCollection = null)
        {
            Identifier = identifier;
            Type = type;
            ModifierList = modifierList;
            DocumentationCommentList = documentationCommentList;
            Initializer = initializer;
            AttributeListCollection = attributeListCollection;
        }
        public Field(
            string identifier,
            string type,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            Expression initializer = null,
            DecoratorList attributeListCollection = null)
            : this(new Identifier(identifier), new Identifier(type), modifierList, documentationCommentList, initializer, attributeListCollection)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int FieldId { get; set; }

        [ProtoMember(2)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(3)]
        public int? AttributeListCollectionId { get; set; }

        [ProtoMember(4)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(5)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(6)]
        public Identifier Identifier { get; set; }
        [ProtoMember(7)]
        public int IdentifierId { get; set; }

        [ProtoMember(8)]
        public Expression Initializer { get; set; }
        [ProtoMember(9)]
        public int? InitializerId { get; set; }

        [ProtoMember(10)]
        public ModifierList ModifierList { get; set; }
        [ProtoMember(11)]
        public byte? ModifierListId { get; set; }

        [ProtoMember(12)]
        public Identifier Type { get; set; }
        [ProtoMember(13)]
        public int TypeId { get; set; }
    }
}
