using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Properties", Schema = nameof(TypeScript))]
    public class Property : IUniqueListRecord
    {
        public Property() { }
        public Property(
            Identifier identifier,
            Identifier type,
            AccessorList accessorList,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null)
        {
            Identifier = identifier;
            Type = type;
            AccessorList = accessorList;
            ModifierList = modifierList;
            DocumentationCommentList = documentationCommentList;
            AttributeListCollection = attributeListCollection;
        }
        public Property(
            string identifier,
            string type,
            AccessorList accessorList,
            ModifierList modifierList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null)
            : this(new Identifier(identifier), new Identifier(type), accessorList, modifierList, documentationCommentList, attributeListCollection)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int PropertyId { get; set; }

        [ProtoMember(2)]
        public AccessorList AccessorList { get; set; }
        [ProtoMember(3)]
        public int AccessorListId { get; set; }

        [ProtoMember(4)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(5)]
        public int? AttributeListCollectionId { get; set; }

        [ProtoMember(6)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(7)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(8)]
        public Identifier Identifier { get; set; }
        [ProtoMember(9)]
        public int IdentifierId { get; set; }

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
