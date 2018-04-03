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
    [Table("DocumentationCommentAttributeLists", Schema = nameof(TypeScript))]
    public class DocumentationCommentAttributeList : IUniqueList<DocumentationCommentAttribute, DocumentationCommentAttributeListAssociation>
    {
        public DocumentationCommentAttributeList() { }
        public DocumentationCommentAttributeList(List<DocumentationCommentAttributeListAssociation> documentationCommentAttributeListAssociations, AsciiStringReference listIdentifier = null)
        {
            DocumentationCommentAttributeListAssociations = documentationCommentAttributeListAssociations;
            ListIdentifier = listIdentifier;
        }
        public DocumentationCommentAttributeList(DocumentationCommentAttributeListAssociation argumentListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<DocumentationCommentAttributeListAssociation> { argumentListAssociation }, listIdentifier)
        {
        }
        public DocumentationCommentAttributeList(DocumentationCommentAttribute attribute, AsciiStringReference listIdentifier = null)
            : this(new DocumentationCommentAttributeListAssociation(attribute), listIdentifier)
        {
        }
        public DocumentationCommentAttributeList(Identifier attribute, Identifier value, AsciiStringReference listIdentifier = null)
            : this(new DocumentationCommentAttribute(attribute, value), listIdentifier)
        {
        }
        public DocumentationCommentAttributeList(string attribute, string value, AsciiStringReference listIdentifier = null)
            : this(new Identifier(attribute), new Identifier(value), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentAttributeListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<DocumentationCommentAttributeListAssociation> DocumentationCommentAttributeListAssociations { get; set; }

        public List<DocumentationCommentAttributeListAssociation> GetAssociations() => DocumentationCommentAttributeListAssociations;

        public void SetRecords(List<DocumentationCommentAttribute> records)
        {
            DocumentationCommentAttributeListAssociations = UniqueListAssociationsFactory<DocumentationCommentAttribute, DocumentationCommentAttributeListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<DocumentationCommentAttribute>.Create(records, r => r.DocumentationCommentAttributeId));
        }
    }
}
