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
    [Table("Constructors", Schema = nameof(TypeScript))]
    public class Constructor : IUniqueListRecord
    {
        public Constructor() { }
        public Constructor(
            Identifier identifier,
            Block block,
            AccessModifiers? accessModifier = null,
            ParameterList parameterList = null,
            DecoratorList decoratorList = null)
        {
            Identifier = identifier;
            Block = block;
            AccessModifier = accessModifier;
            ParameterList = parameterList;
            DecoratorList = decoratorList;
        }
        public Constructor(
            string identifier,
            Block block,
            AccessModifiers? accessModifier = null,
            ParameterList parameterList = null,
            DecoratorList decoratorList = null)
            : this(new Identifier(identifier), block, accessModifier, parameterList, decoratorList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstructorId { get; set; }

        [ProtoMember(2)]
        public AccessModifiers? AccessModifier { get; set; }

        [ProtoMember(3)]
        public Block Block { get; set; }
        [ProtoMember(4)]
        public int BlockId { get; set; }

        [ProtoMember(5)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(6)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(9)]
        public Identifier Identifier { get; set; }
        [ProtoMember(10)]
        public int IdentifierId { get; set; }

        [ProtoMember(11)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(12)]
        public int? ParameterListId { get; set; }

        public string GetConstructorSyntax(string typeName)
        {
            var modifier = AccessModifier == null ? string.Empty : $"{AccessModifier.GetStringValue()} ";
            return $"{GetDocumentation(typeName, ParameterList)}{NewLine}{modifier}constructor({ParameterList?.GetParameterListSyntax()}) {Block.GetBlockSyntax()}";
        }

        private static string GetDocumentation(string typeName, ParameterList parameters)
        {
            var documentationBuilder = new StringBuilder().Append(OpenJsDoc).AppendLine($" * Create a new {typeName} instance");
            foreach (var parameter in parameters?.GetRecords() ?? new Parameter[] { })
                documentationBuilder.AppendLine($" * @param {parameter.Identifier} - {parameter.DocumentationComment}");
            return documentationBuilder.AppendLine(CloseJsDoc).ToString();
        }
    }
}
