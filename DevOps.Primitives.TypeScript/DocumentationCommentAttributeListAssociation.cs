using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationCommentAttributeListAssociations", Schema = nameof(TypeScript))]
    public class DocumentationCommentAttributeListAssociation : IUniqueListAssociation<DocumentationCommentAttribute>
    {
        public DocumentationCommentAttributeListAssociation() { }
        public DocumentationCommentAttributeListAssociation(DocumentationCommentAttribute documentationCommentAttribute, DocumentationCommentAttributeList documentationCommentAttributeList = null)
        {
            DocumentationCommentAttribute = documentationCommentAttribute;
            DocumentationCommentAttributeList = documentationCommentAttributeList;
        }
        public DocumentationCommentAttributeListAssociation(Identifier attribute, Identifier value, DocumentationCommentAttributeList documentationCommentAttributeList = null)
            : this(new DocumentationCommentAttribute(attribute, value), documentationCommentAttributeList)
        {
        }
        public DocumentationCommentAttributeListAssociation(string attribute, string value, DocumentationCommentAttributeList documentationCommentAttributeList = null)
            : this(new Identifier(attribute), new Identifier(value), documentationCommentAttributeList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentAttributeListAssociationId { get; set; }

        [ProtoMember(2)]
        public DocumentationCommentAttribute DocumentationCommentAttribute { get; set; }
        [ProtoMember(3)]
        public int DocumentationCommentAttributeId { get; set; }

        [ProtoMember(4)]
        public DocumentationCommentAttributeList DocumentationCommentAttributeList { get; set; }
        [ProtoMember(5)]
        public int DocumentationCommentAttributeListId { get; set; }

        public DocumentationCommentAttribute GetRecord() => DocumentationCommentAttribute;

        public void SetRecord(DocumentationCommentAttribute record)
        {
            DocumentationCommentAttribute = record;
            DocumentationCommentAttributeId = DocumentationCommentAttribute.DocumentationCommentAttributeId;
        }
    }
}
