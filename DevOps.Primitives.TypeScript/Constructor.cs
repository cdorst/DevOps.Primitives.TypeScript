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
    [Table("Constructors", Schema = nameof(TypeScript))]
    public class Constructor : IUniqueListRecord
    {
        public Constructor() { }
        public Constructor(
            in Identifier identifier,
            in Block block,
            in AccessModifiers? accessModifier = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default)
        {
            Identifier = identifier;
            Block = block;
            AccessModifier = accessModifier;
            ParameterList = parameterList;
            DecoratorList = decoratorList;
        }
        public Constructor(
            in string identifier,
            in Block block,
            in AccessModifiers? accessModifier = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default)
            : this(new Identifier(in identifier), in block, in accessModifier, in parameterList, in decoratorList)
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

        [ProtoMember(7)]
        public Identifier Identifier { get; set; }
        [ProtoMember(8)]
        public int IdentifierId { get; set; }

        [ProtoMember(9)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(10)]
        public int? ParameterListId { get; set; }

        public string GetConstructorSyntax(in string typeName)
        {
            var modifier = AccessModifier == null ? Empty : Concat(AccessModifier.GetStringValue(), " ");
            return Concat(GetDocumentation(typeName, ParameterList), NewLine, modifier, "constructor(", ParameterList?.GetParameterListSyntax() ?? Empty, ") ", Block.GetBlockSyntax());
        }

        private static string GetDocumentation(in string typeName, in ParameterList parameters)
        {
            var documentationBuilder = new StringBuilder().Append(OpenJsDoc).AppendLine(Concat(" * Create a new ", typeName, " instance"));
            foreach (var parameter in parameters?.GetRecords() ?? new Parameter[] { })
                documentationBuilder.AppendLine(Concat(" * @param ", parameter.Identifier, " - ", parameter.DocumentationComment));
            return documentationBuilder.AppendLine(CloseJsDoc).ToString();
        }
    }
}
