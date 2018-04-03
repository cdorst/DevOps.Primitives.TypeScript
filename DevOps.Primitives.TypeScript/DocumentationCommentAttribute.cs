using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("DocumentationCommentAttributes", Schema = nameof(TypeScript))]
    public class DocumentationCommentAttribute : IUniqueListRecord
    {
        public DocumentationCommentAttribute() { }
        public DocumentationCommentAttribute(Identifier identifier, Identifier value)
        {
            Identifier = identifier;
            Value = value;
        }
        public DocumentationCommentAttribute(string identifier, string value)
            : this(new Identifier(identifier), new Identifier(value))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int DocumentationCommentAttributeId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        [ProtoMember(4)]
        public Identifier Value { get; set; }
        [ProtoMember(5)]
        public int ValueId { get; set; }
    }
}
