using Common.EntityFrameworkServices;
using Common.EnumStringValues;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Properties", Schema = nameof(TypeScript))]
    public class Property : IUniqueListRecord
    {
        public Property() { }
        public Property(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment documentationComment,
            in AccessModifiers? accessModifier = default,
            in bool isStatic = default,
            in bool isReadonly = default,
            in DecoratorList decoratorList = default,
            in Expression defaultValue = default)
        {
            AccessModifier = accessModifier;
            DecoratorList = decoratorList;
            DefaultValue = defaultValue;
            DocumentationComment = documentationComment;
            Identifier = identifier;
            IsReadonly = isReadonly;
            IsStatic = isStatic;
            Type = type;
        }
        public Property(
            in string identifier,
            in string type,
            in string comment,
            in AccessModifiers? accessModifier = default,
            in bool isStatic = default,
            in bool isReadonly = default,
            in DecoratorList decoratorList = default,
            in Expression defaultValue = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in accessModifier,
                  in isStatic,
                  in isReadonly,
                  in decoratorList,
                  in defaultValue)
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
                .AppendLine(Concat(GetModifiers(), Identifier.ToString(), ": ", Type, DefaultValue?.GetDefaultValueAssignmentSyntax() ?? Empty, ";"))
                .ToString();
        }

        private string GetModifiers()
        {
            var modifiers = AccessModifier == null ? Empty : Concat(AccessModifier.GetStringValue(), " ");
            if (IsStatic) modifiers = Concat(modifiers, "static ");
            if (IsReadonly) modifiers = Concat(modifiers, "readonly ");
            return modifiers;
        }
    }
}
