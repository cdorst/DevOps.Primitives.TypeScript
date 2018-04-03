using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationCommentListAssociations", Schema = nameof(TypeScript))]
    public class DocumentationCommentListAssociation : IUniqueListAssociation<DocumentationComment>
    {
        public DocumentationCommentListAssociation() { }
        public DocumentationCommentListAssociation(DocumentationComment documentationComment, DocumentationCommentList documentationCommentList = null)
        {
            DocumentationComment = documentationComment;
            DocumentationCommentList = documentationCommentList;
        }
        public DocumentationCommentListAssociation(Identifier identifier, AsciiMaxStringReference text, bool includeNewLine = false, byte indentLevel = byte.MinValue, DocumentationCommentList documentationCommentList = null)
            : this(new DocumentationComment(identifier, text, includeNewLine, indentLevel), documentationCommentList)
        {
        }
        public DocumentationCommentListAssociation(string identifier, string text, bool includeNewLine = false, byte indentLevel = byte.MinValue, DocumentationCommentList documentationCommentList = null)
            : this(new Identifier(identifier), new AsciiMaxStringReference(text), includeNewLine, indentLevel, documentationCommentList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentListAssociationId { get; set; }

        [ProtoMember(2)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(3)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(4)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(5)]
        public int DocumentationCommentListId { get; set; }

        public DocumentationComment GetRecord() => DocumentationComment;

        public void SetRecord(DocumentationComment record)
        {
            DocumentationComment = record;
            DocumentationCommentId = DocumentationComment.DocumentationCommentId;
        }
    }
}
