using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class ClassDeclaration : TypeDeclaration
    {
        public ClassDeclaration() { }
        public ClassDeclaration(
            Identifier identifier,
            Namespace @namespace,
            DocumentationComment comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            DecoratorList decoratorList = null,
            TypeParameterList typeParameterList = null,
            Constructor constructor = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            BaseList extendsList = null,
            BaseList implementsList = null,
            bool isAbstract = false)
            : base(
                  identifier,
                  @namespace,
                  comment,
                  export,
                  importStatementList,
                  decoratorList,
                  typeParameterList,
                  constructor,
                  methodList,
                  propertyList,
                  extendsList)
        {
            ImplementsList = implementsList;
            IsAbstract = isAbstract;
        }
        public ClassDeclaration(
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
            BaseList extendsList = null,
            BaseList implementsList = null,
            bool isAbstract = false)
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
                  extendsList,
                  implementsList,
                  isAbstract)
        {
        }

        [ProtoMember(23)]
        public BaseList ImplementsList { get; set; }
        [ProtoMember(24)]
        public int? ImplementsListId { get; set; }

        [ProtoMember(25)]
        public bool IsAbstract { get; set; }

        protected override string GetTypeDeclaration()
        {
            var export = Export ? "export " : string.Empty;
            var @abstract = IsAbstract ? "abstract " : string.Empty;
            var extends = $" {ExtendsList?.GetBaseListSyntax(BaseListKind.Extends)}".TrimEnd();
            var implements = $" {ImplementsList?.GetBaseListSyntax(BaseListKind.Implements)}".TrimEnd();
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine($"{export}{@abstract}class {Identifier}{TypeParameterList?.GetTypeParameterListSyntax()}{extends}{implements} {{");
            // Add properties
            if (PropertyList != null)
            {
                foreach (var property in PropertyList.GetRecords().Select(property => property.GetPropertySyntax()))
                    foreach (var line in property.SplitLines())
                        stringBuilder.AppendIndentedLine(line);
                if (Constructor != null || MethodList != null)
                    stringBuilder.AppendLine();
            }
            // Add constructor
            if (Constructor != null)
            {
                foreach (var line in Constructor.GetConstructorSyntax(Identifier.ToString()).SplitLines())
                    stringBuilder.AppendIndentedLine(line);
                if (MethodList != null) stringBuilder.AppendLine();
            }
            // Add methods
            if (MethodList != null)
            {
                var methods = MethodList.GetRecords().Select(method => method.GetMethodSyntax());
                foreach (var method in methods)
                    foreach (var line in method.SplitLines())
                        stringBuilder.AppendIndentedLine(line);
            }
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
