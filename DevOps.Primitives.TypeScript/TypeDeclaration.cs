using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    // https://github.com/mgravell/protobuf-net#inheritance
    [ProtoInclude(97, typeof(ClassDeclaration))]
    [ProtoInclude(98, typeof(EnumDeclaration))]
    [ProtoInclude(99, typeof(InterfaceDeclaration))]
    [Table("TypeDeclarations", Schema = nameof(TypeScript))]
    public class TypeDeclaration
    {
        public TypeDeclaration() { }
        public TypeDeclaration(
            Identifier identifier,
            Namespace @namespace,
            DocumentationComment comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            Constructor constructor = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            BaseList extendsList = null)
        {
            Identifier = identifier;
            Namespace = @namespace;
            Export = export;
            ImportStatementList = importStatementList;
            DocumentationComment = comment;
            DecoratorList = attributeListCollection;
            TypeParameterList = typeParameterList;
            Constructor = constructor;
            MethodList = methodList;
            PropertyList = propertyList;
            ExtendsList = extendsList;
        }
        public TypeDeclaration(
            string identifier,
            string @namespace,
            string comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            DecoratorList decoratorList = null,
            TypeParameterList typeParameterList = null,
            Constructor constructor = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            BaseList extendsList = null)
            : this(
                  new Identifier(identifier),
                  new Namespace(@namespace),
                  new DocumentationComment(comment),
                  export,
                  importStatementList,
                  decoratorList,
                  typeParameterList,
                  constructor,
                  methodList,
                  propertyList,
                  extendsList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeDeclarationId { get; set; }

        [ProtoMember(2)]
        public Constructor Constructor { get; set; }
        [ProtoMember(3)]
        public int? ConstructorId { get; set; }

        [ProtoMember(4)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(5)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(6)]
        public DocumentationComment DocumentationComment { get; set; }
        [ProtoMember(7)]
        public int DocumentationCommentId { get; set; }

        [ProtoMember(8)]
        public bool Export { get; set; }

        [ProtoMember(9)]
        public BaseList ExtendsList { get; set; }
        [ProtoMember(10)]
        public int? ExtendsListId { get; set; }

        [ProtoMember(11)]
        public Identifier Identifier { get; set; }
        [ProtoMember(12)]
        public int IdentifierId { get; set; }

        [ProtoMember(13)]
        public ImportStatementList ImportStatementList { get; set; }
        [ProtoMember(14)]
        public int? ImportStatementListId { get; set; }

        [ProtoMember(15)]
        public MethodList MethodList { get; set; }
        [ProtoMember(16)]
        public int? MethodListId { get; set; }

        [ProtoMember(17)]
        public Namespace Namespace { get; set; }
        [ProtoMember(18)]
        public int NamespaceId { get; set; }

        [ProtoMember(19)]
        public PropertyList PropertyList { get; set; }
        [ProtoMember(20)]
        public int? PropertyListId { get; set; }

        [ProtoMember(21)]
        public TypeParameterList TypeParameterList { get; set; }
        [ProtoMember(22)]
        public int? TypeParameterListId { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var imports = ImportStatementList?.GetRecords();
            if (Any(imports))
            {
                foreach (var import in imports)
                    stringBuilder.AppendLine(import.GetImportStatementSyntax());
                stringBuilder.AppendLine();
            }

            var declaration = GetTypeDeclaration();
            if (Namespace != null) declaration = Namespace.GetNamespaceSyntax(declaration);

            return stringBuilder
                .AppendLine()
                .AppendLine(declaration)
                .AppendLine()
                .ToString()
                .TrimStart();
        }

        protected virtual string GetTypeDeclaration() => string.Empty;
    }
}
