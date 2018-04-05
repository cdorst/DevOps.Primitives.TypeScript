using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Parameters", Schema = nameof(TypeScript))]
    public class Parameter : IUniqueListRecord
    {
        public Parameter() { }
        public Parameter(Identifier identifier, Identifier type, DocumentationComment comment, Expression defaultValue = null, DecoratorList decoratorList = null)
        {
            DecoratorList = decoratorList;
            DefaultValue = defaultValue;
            DocumentationComment = comment;
            Identifier = identifier;
            Type = type;
        }
        public Parameter(string identifier, string type, string comment, Expression defaultValue = null, DecoratorList decoratorList = null)
            : this(new Identifier(identifier), new Identifier(type), new DocumentationComment(comment), defaultValue, decoratorList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ParameterId { get; set; }

        [ProtoMember(2)]
        public bool IsReadonly { get; set; }

        [ProtoMember(3)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(4)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(5)]
        public Expression DefaultValue { get; set; }
        [ProtoMember(6)]
        public int? DefaultValueId { get; set; }

        [ProtoMember(7)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(8)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(9)]
        public Identifier Identifier { get; set; }
        [ProtoMember(10)]
        public int IdentifierId { get; set; }

        [ProtoMember(11)]
        public Identifier Type { get; set; }
        [ProtoMember(12)]
        public int TypeId { get; set; }

        public string GetParameterSyntax()
        {
            var decorators = DecoratorList?.GetInlineDecoratorListSyntax();
            if (!string.IsNullOrEmpty(decorators)) decorators = $"{decorators} ";
            var modifier = IsReadonly ? "readonly " : string.Empty;
            var assignment = DefaultValue == null ? string.Empty : $" = {DefaultValue}";
            return $"{decorators}{modifier}{Identifier}: {Type}{assignment}";
        }
    }
}
