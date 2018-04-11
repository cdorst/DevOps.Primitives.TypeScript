﻿using System;
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
            ZeroPropertyBehavior zeroPropertyBehavior = ZeroPropertyBehavior.ReturnNull,
            byte indent = IndentZero,
            StringBuilder stringBuilder = null)
            => JsonObject(JsonProperties(), zeroPropertyBehavior, indent, stringBuilder);

        private IEnumerable<Func<StringBuilder, byte, StringBuilder>> JsonProperties()
        {
            if (NotNull(Email)) yield return JsonString(nameof(Email), Email);
            if (NotNull(Name)) yield return JsonString(nameof(Name), Name);
            if (NotNull(Url)) yield return JsonString(nameof(Url), Url);
        }
    }
}