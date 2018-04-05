using System;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    internal static class StringNewLineSplitter
    {
        public static string[] SplitLines(this string instance)
            => instance.Split(new[] { NewLine }, StringSplitOptions.None);
    }
}
