namespace DevOps.Primitives.TypeScript
{
    internal static class StringExtensionToCamelCase
    {
        public static string ToCamelCase(this string instance)
            => $"{instance[0].ToString().ToLower()}{instance.Substring(1)}";
    }
}
