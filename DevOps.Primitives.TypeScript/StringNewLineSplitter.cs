using System;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    internal static class StringNewLineSplitter
    {
        private static readonly string[] _separator = new[] { NewLine };

        public static string[] SplitLines(this string instance)
            => instance.Split(_separator, StringSplitOptions.None);
    }
}
