using Common.EntityFrameworkServices;
using Common.EnumStringValues;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Methods", Schema = nameof(TypeScript))]
    public class Method : IUniqueListRecord
    {
        public Method() { }
        public Method(
            in Identifier identifier,
            in DocumentationComment comment,
            in Block block = default,
            in Identifier type = default,
            in AccessModifiers? accessModifier = default,
            in bool isAsync = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default)
        {
            AccessModifier = accessModifier;
            Block = block;
            DecoratorList = decoratorList;
            DocumentationComment = comment;
            Identifier = identifier;
            IsAsync = isAsync;
            ParameterList = parameterList;
            Type = type;
            TypeParameterList = typeParameterList;
        }
        public Method(
            in string identifier,
            in string comment,
            in Block block = default,
            in string type = default,
            in AccessModifiers? accessModifier = default,
            in bool isAsync = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default)
            : this(
                  new Identifier(in identifier),
                  new DocumentationComment(in comment),
                  in block,
                  string.IsNullOrWhiteSpace(type) ? null : new Identifier(in type),
                  in accessModifier,
                  in isAsync,
                  in parameterList,
                  in decoratorList,
                  in typeParameterList)
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
            var modifier = AccessModifier == null ? Empty : Concat(AccessModifier.GetStringValue(), " ");
            var type = Type == null ? Empty : Concat(": ", Type);
            var block = Block == null ? ";" : Concat(" ",Block.GetBlockSyntax());
            return Concat(GetDocumentation(DocumentationComment, ParameterList), NewLine, modifier, Identifier.ToString(), "(", ParameterList?.GetParameterListSyntax() ?? Empty, ")", type, block);
        }

        private static string GetDocumentation(in DocumentationComment comment, in ParameterList parameters)
        {
            var documentationBuilder = new StringBuilder().Append(OpenJsDoc).AppendLine(Concat(" * ", comment));
            foreach (var parameter in parameters?.GetRecords() ?? new Parameter[] { })
                documentationBuilder.AppendLine(Concat(" * @param ", parameter.Identifier, " - ", parameter.DocumentationComment));
            return documentationBuilder.AppendLine(CloseJsDoc).ToString();
        }
    }
}
