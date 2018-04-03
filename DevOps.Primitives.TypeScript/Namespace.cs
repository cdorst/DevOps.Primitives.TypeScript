using ProtoBuf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Namespaces", Schema = nameof(TypeScript))]
    public class Namespace
    {
        public Namespace() { }
        public Namespace(Identifier identifier) { Identifier = identifier; }
        public Namespace(string identifier) : this(new Identifier(identifier)) { }

        [Key]
        [ProtoMember(1)]
        public int NamespaceId { get; set; }

        [ProtoMember(2)]
        public Identifier Identifier { get; set; }
        [ProtoMember(3)]
        public int IdentifierId { get; set; }

        public string GetNamespaceSyntax(string declarations)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"namespace {Identifier.GetValue()} {{");
            if (!string.IsNullOrWhiteSpace(declarations))
            {
                var lines = declarations.Split(new[] { NewLine }, StringSplitOptions.None);
                foreach (var line in lines)
                    stringBuilder.AppendLine($"{Indent}{line}");
            }
            return stringBuilder.AppendLine(CloseCurlyBrace).ToString();
        }
    }
}
