using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class InterfaceDeclaration : TypeDeclaration
    {
        public InterfaceDeclaration() { }
        public InterfaceDeclaration(
            in string identifier,
            in string @namespace,
            in string comment,
            in bool export = true,
            in ImportStatementList importStatementList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default,
            in MethodList methodList = default,
            in PropertyList propertyList = default,
            in BaseList extendsList = default)
            : base(
                  in identifier,
                  in @namespace,
                  in comment,
                  in export,
                  in importStatementList,
                  in decoratorList,
                  in typeParameterList,
                  null,
                  in methodList,
                  in propertyList,
                  in extendsList)
        {
        }

        protected override string GetTypeDeclaration()
        {
            var export = Export ? "export " : Empty;
            var extends = Concat(" ", ExtendsList?.GetBaseListSyntax(BaseListKind.Extends) ?? Empty).TrimEnd();
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine(Concat(export, "interface ", Identifier.ToString(), TypeParameterList?.GetTypeParameterListSyntax() ?? Empty, extends, " {"));
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
