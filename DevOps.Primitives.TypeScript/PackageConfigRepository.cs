using System;
using System.Collections.Generic;
using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    public class PackageConfigRepository : IJsonSerializable
    {
        public string Type { get; set; }
        public string Url { get; set; }

        public StringBuilder GetJsonStringBuilder(
            in EmptyResponseBehavior emptyResponseBehavior = EmptyResponseBehavior.Default,
            in byte indent = IndentZero,
            StringBuilder stringBuilder = default)
            => JsonObject(JsonProperties(), emptyResponseBehavior, indent, stringBuilder);

        private IEnumerable<Func<StringBuilder, byte, StringBuilder>> JsonProperties()
        {
            if (NotNull(Type)) yield return JsonString(nameof(Type), Type);
            if (NotNull(Url)) yield return JsonString(nameof(Url), Url);
        }
    }
}
