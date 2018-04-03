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
    [ProtoInclude(96, typeof(ClassDeclaration))]
    [ProtoInclude(97, typeof(EnumDeclaration))]
    [ProtoInclude(98, typeof(InterfaceDeclaration))]
    [Table("TypeDeclarations", Schema = nameof(TypeScript))]
    public class TypeDeclaration
    {
        public TypeDeclaration() { }
        public TypeDeclaration(
            Identifier identifier,
            Namespace _namespace,
            bool export = true,
            ModifierList modifierList = null,
            ImportStatementList importStatementList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null)
        {
            Identifier = identifier;
            Namespace = _namespace;
            Export = export;
            ModifierList = modifierList;
            ImportStatementList = importStatementList;
            DocumentationCommentList = documentationCommentList;
            AttributeListCollection = attributeListCollection;
            TypeParameterList = typeParameterList;
            ConstraintClauseList = constraintClauseList;
            BaseList = baseList;
            ConstructorList = constructorList;
            FieldList = fieldList;
            MethodList = methodList;
            PropertyList = propertyList;
        }
        public TypeDeclaration(
            string identifier,
            string @namespace,
            bool export = true,
            ModifierList modifierList = null,
            ImportStatementList importStatementList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null)
            : this(
                  new Identifier(identifier),
                  new Namespace(@namespace),
                  export,
                  modifierList,
                  importStatementList,
                  documentationCommentList,
                  attributeListCollection,
                  typeParameterList,
                  constraintClauseList,
                  baseList,
                  constructorList,
                  fieldList,
                  methodList,
                  propertyList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int TypeDeclarationId { get; set; }

        [ProtoMember(2)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(3)]
        public int? AttributeListCollectionId { get; set; }

        [ProtoMember(4)]
        public BaseList BaseList { get; set; }
        [ProtoMember(5)]
        public int? BaseListId { get; set; }

        [ProtoMember(6)]
        public ConstraintClauseList ConstraintClauseList { get; set; }
        [ProtoMember(7)]
        public int? ConstraintClauseListId { get; set; }

        [ProtoMember(8)]
        public ConstructorList ConstructorList { get; set; }
        [ProtoMember(9)]
        public int? ConstructorListId { get; set; }

        [ProtoMember(10)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(11)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(12)]
        public bool Export { get; set; }

        [ProtoMember(13)]
        public FieldList FieldList { get; set; }
        [ProtoMember(14)]
        public int? FieldListId { get; set; }

        [ProtoMember(15)]
        public Identifier Identifier { get; set; }
        [ProtoMember(16)]
        public int IdentifierId { get; set; }

        [ProtoMember(17)]
        public ImportStatementList ImportStatementList { get; set; }
        [ProtoMember(18)]
        public int? ImportStatementListId { get; set; }

        [ProtoMember(19)]
        public MethodList MethodList { get; set; }
        [ProtoMember(20)]
        public int? MethodListId { get; set; }

        [ProtoMember(21)]
        public ModifierList ModifierList { get; set; }
        [ProtoMember(22)]
        public byte? ModifierListId { get; set; }

        [ProtoMember(23)]
        public Namespace Namespace { get; set; }
        [ProtoMember(24)]
        public int NamespaceId { get; set; }

        [ProtoMember(25)]
        public PropertyList PropertyList { get; set; }
        [ProtoMember(26)]
        public int? PropertyListId { get; set; }

        [ProtoMember(27)]
        public TypeParameterList TypeParameterList { get; set; }
        [ProtoMember(28)]
        public int? TypeParameterListId { get; set; }

        public string GetNamespace()
            => Namespace.Identifier.Name.Value;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var imports = ImportStatementList?.GetRecords();
            if (Any(imports))
                foreach (var import in imports)
                    stringBuilder.AppendLine(import.GetImportStatementSyntax());

            var declaration = GetTypeDeclaration();
            if (Export) declaration = $"export {declaration}";
            if (Namespace != null) declaration = Namespace.GetNamespaceSyntax(declaration);

            return stringBuilder
                .AppendLine()
                .AppendLine(declaration)
                .AppendLine()
                .ToString();
        }

        protected virtual string GetTypeDeclaration() => string.Empty;
    }
}
