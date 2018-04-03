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
    [Table("ModifierLists", Schema = nameof(TypeScript))]
    public class ModifierList : IUniqueList<SyntaxToken, ModifierListAssociation>
    {
        public ModifierList() { }
        public ModifierList(List<ModifierListAssociation> modifierListAssociations, AsciiStringReference listIdentifier = null)
        {
            ModifierListAssociations = modifierListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ModifierList(ModifierListAssociation modifierListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ModifierListAssociation> { modifierListAssociation }, listIdentifier)
        {
        }
        public ModifierList(SyntaxToken syntaxToken, AsciiStringReference listIdentifier = null)
            : this(new ModifierListAssociation(syntaxToken), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public byte ModifierListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ModifierListAssociation> ModifierListAssociations { get; set; }

        public List<ModifierListAssociation> GetAssociations() => ModifierListAssociations;

        public void SetRecords(List<SyntaxToken> records)
        {
            ModifierListAssociations = UniqueListAssociationsFactory<SyntaxToken, ModifierListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SyntaxToken>.Create(records, r => r.SyntaxTokenId));
        }
    }
}
