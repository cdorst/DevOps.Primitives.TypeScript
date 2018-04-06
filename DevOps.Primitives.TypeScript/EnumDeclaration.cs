using Common.EntityFrameworkServices;
using ProtoBuf;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class EnumDeclaration : TypeDeclaration
    {
        public EnumDeclaration() { }
        public EnumDeclaration(
            Identifier identifier,
            Namespace @namespace,
            DocumentationComment comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            EnumMemberList enumMemberList = null)
            : base(
                  identifier,
                  @namespace,
                  comment,
                  export,
                  importStatementList)
        {
            EnumMemberList = enumMemberList;
        }
        public EnumDeclaration(
            string identifier,
            string @namespace,
            string comment,
            bool export = true,
            ImportStatementList importStatementList = null,
            EnumMemberList enumMemberList = null)
            : this(
                  new Identifier(identifier),
                  new Namespace(@namespace),
                  new DocumentationComment(comment),
                  export,
                  importStatementList,
                  enumMemberList)
        {
        }

        [ProtoMember(23)]
        public EnumMemberList EnumMemberList { get; set; }
        [ProtoMember(24)]
        public int EnumMemberListId { get; set; }

        protected override string GetTypeDeclaration()
        {
            var export = Export ? "export " : string.Empty;
            var stringBuilder = new StringBuilder()
                .Append(DocumentationComment.ToSelfClosingJsDoc())
                .AppendLine($"{export}enum {Identifier} {{");
            // Add enum members
            var members = EnumMemberList.GetRecords().Select(member => member.GetEnumMemberSyntax());
            foreach (var member in members)
                foreach (var line in member.SplitLines())
                    stringBuilder.AppendIndentedLine(line);
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
