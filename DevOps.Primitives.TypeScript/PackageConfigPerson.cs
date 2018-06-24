using System;
using System.Collections.Generic;
using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    public class PackageConfigPerson : IJsonSerializable
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public StringBuilder GetJsonStringBuilder(
            in EmptyResponseBehavior emptyResponseBehavior = EmptyResponseBehavior.Default,
            in byte indent = IndentZero,
            StringBuilder stringBuilder = default)
            => JsonObject(JsonProperties(), emptyResponseBehavior, indent, stringBuilder);

        private IEnumerable<Func<StringBuilder, byte, StringBuilder>> JsonProperties()
        {
            if (NotNull(Email)) yield return JsonString(nameof(Email), Email);
            if (NotNull(Name)) yield return JsonString(nameof(Name), Name);
            if (NotNull(Url)) yield return JsonString(nameof(Url), Url);
        }
    }
}
