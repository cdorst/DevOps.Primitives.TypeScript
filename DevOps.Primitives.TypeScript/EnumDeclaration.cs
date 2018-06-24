using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class EnumDeclaration : TypeDeclaration
    {
        public EnumDeclaration() { }
        public EnumDeclaration(
            in Identifier identifier,
            in Namespace @namespace,
            in DocumentationComment comment,
            in bool export = true,
            in ImportStatementList importStatementList = default,
            in EnumMemberList enumMemberList = default)
            : base(
                  in identifier,
                  in @namespace,
                  in comment,
                  in export,
                  in importStatementList)
        {
            EnumMemberList = enumMemberList;
        }
        public EnumDeclaration(
            in string identifier,
            in string @namespace,
            in string comment,
            in bool export = true,
            in ImportStatementList importStatementList = default,
            in EnumMemberList enumMemberList = default)
            : this(
                  new Identifier(in identifier),
                  new Namespace(in @namespace),
                  new DocumentationComment(in comment),
                  in export,
                  in importStatementList,
                  in enumMemberList)
        {
        }

        [ProtoMember(23)]
        public EnumMemberList EnumMemberList { get; set; }
        [ProtoMember(24)]
        public int EnumMemberListId { get; set; }

        protected override string GetTypeDeclaration()
        {
            var export = Export ? "export " : Empty;
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine(Concat(export, "enum ", Identifier.ToString(), " {"));
            // Add enum members
            var members = EnumMemberList.GetRecords().Select(member => member.GetEnumMemberSyntax());
            foreach (var member in members)
                foreach (var line in member.SplitLines())
                    stringBuilder.AppendIndentedLine(line);
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
