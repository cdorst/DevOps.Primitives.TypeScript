using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    public static class StringBuilderAppendIndentedLineExtension
    {
        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, byte indent = 1)
            => stringBuilder
                .AppendLine()
                .AppendIndent(indent);

        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, byte indent, bool appendComma)
            => appendComma
                ? stringBuilder.Append(Comma).AppendIndentedLine(indent)
                : stringBuilder.AppendIndentedLine(indent);

        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, string line)
            => stringBuilder
                .AppendLine()
                .Append(Indent)
                .Append(line);

        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, byte indent, string line)
            => stringBuilder
                .AppendLine()
                .AppendIndent(indent)
                .Append(line);

        public static StringBuilder AppendCommaNewLine(this StringBuilder stringBuilder, byte indent = 0, string line = null)
            => stringBuilder
                .Append(Comma)
                .AppendLine()
                .AppendIndent(indent)
                .Append(line);

        public static StringBuilder AppendLine(this StringBuilder stringBuilder, byte indent = 0, string line = null)
            => stringBuilder
                .AppendLine()
                .AppendIndent(indent)
                .Append(line);

        public static StringBuilder AppendIndent(this StringBuilder stringBuilder, byte indent = 1)
        {
            for (byte i = 0; i < indent; i++)
                stringBuilder.Append(Indent);
            return stringBuilder;
        }
    }
}
