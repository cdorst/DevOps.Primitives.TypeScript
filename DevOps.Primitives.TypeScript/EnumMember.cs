using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("EnumMembers", Schema = nameof(TypeScript))]
    public class EnumMember : IUniqueListRecord
    {
        public EnumMember() { }
        public EnumMember(Identifier identifier, DocumentationCommentList documentationCommentList = null, DecoratorList attributeListCollection = null, int? equalsValue = null)
        {
            Identifier = identifier;
            DocumentationCommentList = documentationCommentList;
            AttributeListCollection = attributeListCollection;
            EqualsValue = equalsValue;
        }
        public EnumMember(string identifier, DocumentationCommentList documentationCommentList = null, DecoratorList attributeListCollection = null, int? equalsValue = null)
            : this(new Identifier(identifier), documentationCommentList, attributeListCollection, equalsValue)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int EnumMemberId { get; set; }

        [ProtoMember(2)]
        public int? EqualsValue { get; set; }

        [ProtoMember(3)]
        public Identifier Identifier { get; set; }
        [ProtoMember(4)]
        public int IdentifierId { get; set; }

        [ProtoMember(5)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(6)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(7)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(8)]
        public int? AttributeListCollectionId { get; set; }
    }
}
