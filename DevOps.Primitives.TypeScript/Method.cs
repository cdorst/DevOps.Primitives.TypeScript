using Common.EntityFrameworkServices;
using Common.EnumStringValues;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Methods", Schema = nameof(TypeScript))]
    public class Method : IUniqueListRecord
    {
        public Method() { }
        public Method(Identifier identifier, DocumentationComment comment, Block block = null, Identifier type = null, AccessModifiers? accessModifier = null, bool isAsync = false, ParameterList parameterList = null, DecoratorList decoratorList = null, TypeParameterList typeParameterList = null)
        {
            AccessModifier = accessModifier;
            IsAsync = isAsync;
            Block = block;
            DecoratorList = decoratorList;
            DocumentationComment = comment;
            Identifier = identifier;
            ParameterList = parameterList;
            Type = type;
            TypeParameterList = typeParameterList;
        }
        public Method(string identifier, string comment, Block block = null, string type = null, AccessModifiers? accessModifier = null, bool isAsync = false, ParameterList parameterList = null, DecoratorList decoratorList = null, TypeParameterList typeParameterList = null)
            : this(new Identifier(identifier), new DocumentationComment(comment), block, string.IsNullOrWhiteSpace(type) ? null : new Identifier(type), accessModifier, isAsync, parameterList, decoratorList, typeParameterList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MethodId { get; set; }

        [ProtoMember(2)]
        public AccessModifiers? AccessModifier { get; set; }

        [ProtoMember(3)]
        public bool IsAsync { get; set; }

        [ProtoMember(4)]
        public Block Block { get; set; }
        [ProtoMember(5)]
        public int? BlockId { get; set; }

        [ProtoMember(6)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(7)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(8)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(9)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(10)]
        public Identifier Identifier { get; set; }
        [ProtoMember(11)]
        public int IdentifierId { get; set; }

        [ProtoMember(12)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(13)]
        public int? ParameterListId { get; set; }

        [ProtoMember(14)]
        public Identifier Type { get; set; }
        [ProtoMember(15)]
        public int? TypeId { get; set; }

        [ProtoMember(16)]
        public TypeParameterList TypeParameterList { get; set; }
        [ProtoMember(17)]
        public int? TypeParameterListId { get; set; }

        public string GetMethodSyntax()
        {
            var modifier = AccessModifier == null ? string.Empty : $"{AccessModifier.GetStringValue()} ";
            var type = Type == null ? string.Empty : $": {Type}";
            var block = Block == null ? ";" : $" {Block.GetBlockSyntax()}";
            return $"{GetDocumentation(DocumentationComment, ParameterList)}{NewLine}{modifier}{Identifier}({ParameterList?.GetParameterListSyntax()}){type}{block}";
        }

        private static string GetDocumentation(DocumentationComment comment, ParameterList parameters)
        {
            var documentationBuilder = new StringBuilder().Append(OpenJsDoc).AppendLine($" * {comment}");
            foreach (var parameter in parameters?.GetRecords() ?? new Parameter[] { })
                documentationBuilder.AppendLine($" * @param {parameter.Identifier} - {parameter.DocumentationComment}");
            return documentationBuilder.AppendLine(CloseJsDoc).ToString();
        }
    }
}
