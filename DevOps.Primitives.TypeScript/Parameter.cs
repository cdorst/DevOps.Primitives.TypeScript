using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Parameters", Schema = nameof(TypeScript))]
    public class Parameter : IUniqueListRecord
    {
        public Parameter() { }
        public Parameter(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default)
        {
            DecoratorList = decoratorList;
            DefaultValue = defaultValue;
            DocumentationComment = comment;
            Identifier = identifier;
            Type = type;
        }
        public Parameter(
            in string identifier,
            in string type,
            in string comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in defaultValue,
                  in decoratorList)
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
            if (!IsNullOrEmpty(decorators)) decorators = Concat(decorators, " ");
            var modifier = IsReadonly ? "readonly " : Empty;
            var assignment = DefaultValue == null ? Empty : Concat(" = ", DefaultValue);
            return Concat(decorators, modifier, Identifier.ToString(), ": ", Type, assignment);
        }
    }
}
