using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class InterfaceDeclaration : TypeDeclaration
    {
        public InterfaceDeclaration() { }
        public InterfaceDeclaration(
            string identifier,
            string @namespace,
            string comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            DecoratorList decoratorList = null,
            TypeParameterList typeParameterList = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            BaseList extendsList = null)
            : base(
                  identifier,
                  @namespace,
                  comment,
                  export,
                  importStatementList,
                  decoratorList,
                  typeParameterList,
                  null,
                  methodList,
                  propertyList,
                  extendsList)
        {
        }

        protected override string GetTypeDeclaration()
        {
            var export = Export ? "export " : string.Empty;
            var extends = $" {ExtendsList?.GetBaseListSyntax(BaseListKind.Extends)}".TrimEnd();
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine($"{export}interface {Identifier}{TypeParameterList?.GetTypeParameterListSyntax()}{extends} {{");
            // Add properties
            if (PropertyList != null)
            {
                var properties = PropertyList.GetRecords().Select(property => property.GetPropertySyntax());
                foreach (var property in properties)
                    foreach (var line in property.SplitLines())
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
