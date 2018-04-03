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
    [Table("DocumentationCommentLists", Schema = nameof(TypeScript))]
    public class DocumentationCommentList : IUniqueList<DocumentationComment, DocumentationCommentListAssociation>
    {
        public DocumentationCommentList() { }
        public DocumentationCommentList(List<DocumentationCommentListAssociation> documentationCommentListAssociations, bool includeNewLine = false, AsciiStringReference listIdentifier = null)
        {
            DocumentationComments = documentationCommentListAssociations;
            IncludeNewLine = includeNewLine;
            ListIdentifier = listIdentifier;
        }
        public DocumentationCommentList(DocumentationCommentListAssociation documentationCommentListAssociations, bool includeNewLine = false, AsciiStringReference listIdentifier = null)
            : this(new List<DocumentationCommentListAssociation> { documentationCommentListAssociations }, includeNewLine, listIdentifier)
        {
        }
        public DocumentationCommentList(DocumentationComment documentationComment, bool includeNewLine = false, AsciiStringReference listIdentifier = null)
            : this(new DocumentationCommentListAssociation(documentationComment), includeNewLine, listIdentifier)
        {
        }
        public DocumentationCommentList(Identifier identifier, AsciiMaxStringReference text, bool includeNewLine = false, byte indentLevel = byte.MinValue, bool includeNewLineAtListLevel = false, AsciiStringReference listIdentifier = null)
            : this(new DocumentationComment(identifier, text, includeNewLine, indentLevel), includeNewLineAtListLevel, listIdentifier)
        {
        }
        public DocumentationCommentList(string identifier, string text, bool includeNewLine = false, byte indentLevel = byte.MinValue, bool includeNewLineAtListLevel = false, AsciiStringReference listIdentifier = null)
            : this(new Identifier(identifier), new AsciiMaxStringReference(text), includeNewLine, indentLevel, includeNewLineAtListLevel, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentListId { get; set; }

        [ProtoMember(2)]
        public bool IncludeNewLine { get; set; }

        [ProtoMember(3)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(4)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(5)]
        public List<DocumentationCommentListAssociation> DocumentationComments { get; set; }

        public List<DocumentationCommentListAssociation> GetAssociations() => DocumentationComments;

        public void SetRecords(List<DocumentationComment> records)
        {
            DocumentationComments = UniqueListAssociationsFactory<DocumentationComment, DocumentationCommentListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<DocumentationComment>.Create(records, r => r.DocumentationCommentId));
        }
    }
}
