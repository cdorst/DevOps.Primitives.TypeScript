using Common.EntityFrameworkServices;
using Common.EnumStringValues;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

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
            DocumentationComment documentationComment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null)
        {
            Identifier = identifier;
            Type = type;
            DocumentationComment = documentationComment;
            AccessModifier = accessModifier;
            IsStatic = isStatic;
            IsReadonly = isReadonly;
            DecoratorList = decoratorList;
            DefaultValue = defaultValue;
        }
        public Property(
            string identifier,
            string type,
            string comment,
            AccessModifiers? accessModifier = null,
            bool isStatic = false,
            bool isReadonly = false,
            DecoratorList decoratorList = null,
            Expression defaultValue = null)
            : this(new Identifier(identifier), new Identifier(type), new DocumentationComment(comment), accessModifier, isStatic, isReadonly, decoratorList, defaultValue)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int PropertyId { get; set; }

        [ProtoMember(2)]
        public AccessModifiers? AccessModifier { get; set; }

        [ProtoMember(3)]
        public bool IsStatic { get; set; }

        [ProtoMember(4)]
        public bool IsReadonly { get; set; }

        [ProtoMember(5)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(6)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(7)]
        public Expression DefaultValue { get; set; }
        [ProtoMember(8)]
        public int? DefaultValueId { get; set; }

        [ProtoMember(9)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(10)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(11)]
        public Identifier Identifier { get; set; }
        [ProtoMember(12)]
        public int IdentifierId { get; set; }

        [ProtoMember(13)]
        public Identifier Type { get; set; }
        [ProtoMember(14)]
        public int TypeId { get; set; }

        public string GetPropertySyntax()
        {
            var stringBuilder = new StringBuilder().Append(DocumentationComment.ToSelfClosingJsDoc());
            var decorators = DecoratorList?.GetRecords().Select(decorator => decorator.GetDecoratorSyntax());
            foreach (var decorator in decorators ?? new string[] { })
                stringBuilder.AppendLine(decorator);
            return stringBuilder
                .AppendLine($"{GetModifiers()}{Identifier}: {Type}{DefaultValue?.GetDefaultValueAssignmentSyntax()};")
                .ToString();
        }

        private string GetModifiers()
        {
            var modifiers = AccessModifier == null ? string.Empty : $"{AccessModifier.GetStringValue()} ";
            if (IsStatic) modifiers = $"{modifiers}static ";
            if (IsReadonly) modifiers = $"{modifiers}readonly ";
            return modifiers;
        }
    }
}
