using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ModifierListAssociations", Schema = nameof(TypeScript))]
    public class ModifierListAssociation : IUniqueListAssociation<SyntaxToken>
    {
        public ModifierListAssociation() { }
        public ModifierListAssociation(SyntaxToken syntaxToken, ModifierList modifierList = null)
        {
            SyntaxToken = syntaxToken;
            ModifierList = modifierList;
        }

        [Key]
        [ProtoMember(1)]
        public short ModifierListAssociationId { get; set; }

        [ProtoMember(2)]
        public ModifierList ModifierList { get; set; }
        [ProtoMember(3)]
        public byte ModifierListId { get; set; }

        [ProtoMember(4)]
        public SyntaxToken SyntaxToken { get; set; }
        [ProtoMember(5)]
        public short SyntaxTokenId { get; set; }

        public SyntaxToken GetRecord() => SyntaxToken;

        public void SetRecord(SyntaxToken record)
        {
            SyntaxToken = record;
            SyntaxTokenId = SyntaxToken.SyntaxTokenId;
        }
    }
}
