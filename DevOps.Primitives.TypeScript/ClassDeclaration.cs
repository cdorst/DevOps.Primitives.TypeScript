using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class ClassDeclaration : TypeDeclaration
    {
        public ClassDeclaration() { }
        public ClassDeclaration(
            in Identifier identifier,
            in Namespace @namespace,
            in DocumentationComment comment,
            in bool export = true,
            in ImportStatementList importStatementList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default,
            in Constructor constructor = default,
            in MethodList methodList = default,
            in PropertyList propertyList = default,
            in BaseList extendsList = default,
            in BaseList implementsList = default,
            in bool isAbstract = default)
            : base(
                  in identifier,
                  in @namespace,
                  in comment,
                  in export,
                  in importStatementList,
                  in decoratorList,
                  in typeParameterList,
                  in constructor,
                  in methodList,
                  in propertyList,
                  in extendsList)
        {
            ImplementsList = implementsList;
            IsAbstract = isAbstract;
        }
        public ClassDeclaration(
            in string identifier,
            in string @namespace,
            in string comment,
            in bool export = true,
            in ImportStatementList importStatementList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default,
            in Constructor constructor = default,
            in MethodList methodList = default,
            in PropertyList propertyList = default,
            in BaseList extendsList = default,
            in BaseList implementsList = default,
            in bool isAbstract = default)
            : this(
                  new Identifier(in identifier),
                  new Namespace(in @namespace),
                  new DocumentationComment(in comment),
                  in export,
                  in importStatementList,
                  in decoratorList,
                  in typeParameterList,
                  in constructor,
                  in methodList,
                  in propertyList,
                  in extendsList,
                  in implementsList,
                  in isAbstract)
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
            var export = Export ? "export " : Empty;
            var @abstract = IsAbstract ? "abstract " : Empty;
            var extends = Concat(" ", ExtendsList?.GetBaseListSyntax(BaseListKind.Extends) ?? Empty).TrimEnd();
            var implements = Concat(" ", ImplementsList?.GetBaseListSyntax(BaseListKind.Implements) ?? Empty).TrimEnd();
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine(Concat(export, @abstract, "class ", Identifier.ToString(), TypeParameterList?.GetTypeParameterListSyntax() ?? Empty, extends, implements, " {"));
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
