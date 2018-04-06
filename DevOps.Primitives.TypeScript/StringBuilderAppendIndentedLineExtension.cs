using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    internal static class StringBuilderAppendIndentedLineExtension
    {
        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder, string line)
            => stringBuilder.AppendLine($"{Indent}{line}");
    }
}
