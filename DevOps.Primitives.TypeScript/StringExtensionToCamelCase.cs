using static System.String;

namespace DevOps.Primitives.TypeScript
{
    internal static class StringExtensionToCamelCase
    {
        public static string ToCamelCase(this string instance)
            => Concat(instance[0].ToString().ToLower(), instance.Substring(1));
    }
}
