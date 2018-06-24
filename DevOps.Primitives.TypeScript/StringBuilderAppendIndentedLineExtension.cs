using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    internal static class StringBuilderAppendIndentedLineExtension
    {
        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, in byte indent = 1)
            => stringBuilder
                .AppendLine()
                .AppendIndent(indent);

        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, in byte indent, in bool appendComma)
            => appendComma
                ? stringBuilder.Append(Comma).AppendIndentedLine(indent)
                : stringBuilder.AppendIndentedLine(indent);

        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, in string line)
            => stringBuilder
                .AppendLine()
                .Append(Indent)
                .Append(line);

        public static StringBuilder AppendLine(this StringBuilder stringBuilder, in byte indent = 0, in string line = default)
            => stringBuilder
                .AppendLine()
                .AppendIndent(indent)
                .Append(line);

        public static StringBuilder AppendIndent(this StringBuilder stringBuilder, in byte indent = 1)
        {
            for (byte i = 0; i < indent; i++)
                stringBuilder.Append(Indent);
            return stringBuilder;
        }
    }
}
