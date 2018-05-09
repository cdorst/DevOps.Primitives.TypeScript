using Common.EnumStringValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.StringConstants;
using Behavior = DevOps.Primitives.TypeScript.EmptyResponseBehavior;
using When = Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;

namespace DevOps.Primitives.TypeScript
{
    public static class JsonStringBuilderHelper
    {
        public const byte IndentOne = One;
        public const byte IndentTwo = 2;
        public const byte IndentZero = Zero;
        private const int One = 1;
        private const int Zero = 0;

        public static bool Any<TKey, TValue>(IEnumerable<KeyValuePair<string, string>> dictionary)
            => dictionary?.Any() ?? false;

        public static StringBuilder AppendArrayProperty(this StringBuilder stringBuilder,
            string name, IEnumerable<string> items, byte indent = IndentOne, bool appendComma = true, bool formatNameAsCamelCase = true)
            => GetArrayProperty(name, items, indent, formatNameAsCamelCase, stringBuilder.AppendIndentedLine(indent, appendComma));

        public static StringBuilder AppendBoolProperty(this StringBuilder stringBuilder,
            string name, byte indent = IndentOne, bool value = true, bool appendComma = true, bool formatNameAsCamelCase = true)
            => GetBooleanProperty(name, value, formatNameAsCamelCase, stringBuilder.AppendIndentedLine(indent, appendComma));

        public static StringBuilder AppendEmptyResponse(this StringBuilder stringBuilder, Behavior behavior)
            => behavior.HasFlag(Behavior.ReturnEmptyArray) ? stringBuilder.Append(EmptyArray)
            : behavior.HasFlag(Behavior.ReturnEmptyObject) ? stringBuilder.Append(EmptyObject)
            : behavior.HasFlag(Behavior.ReturnEmptyString) ? stringBuilder.Append(EmptyString)
            : stringBuilder.Append(Null);

        public static StringBuilder AppendObjectProperty(this StringBuilder stringBuilder,
            string name, IDictionary<string, IEnumerable<string>> members, byte indent = IndentOne, bool appendComma = true, bool formatNameAsCamelCase = true)
            => GetObjectProperty(name, members, indent, formatNameAsCamelCase, stringBuilder.AppendIndentedLine(indent, appendComma));

        public static StringBuilder AppendProperty(this StringBuilder stringBuilder, string name, bool formatNameAsCamelCase = true)
            => Property(name, formatNameAsCamelCase, stringBuilder);

        public static StringBuilder AppendPropertyAssignment(this StringBuilder stringBuilder, string name, bool formatNameAsCamelCase, string valueLiteral)
            => PropertyAssignment(name, formatNameAsCamelCase, valueLiteral, stringBuilder);

        public static StringBuilder AppendPropertyAssignment(this StringBuilder stringBuilder, string name, bool formatNameAsCamelCase, StringBuilder valueLiteral)
            => PropertyAssignment(name, formatNameAsCamelCase, valueLiteral, stringBuilder);

        public static StringBuilder AppendStringProperty(this StringBuilder stringBuilder,
            string name, string text, byte indent = IndentOne, bool appendComma = true, bool formatNameAsCamelCase = true)
            => GetStringProperty(name, text, formatNameAsCamelCase, stringBuilder.AppendIndentedLine(indent, appendComma));

        public static StringBuilder AppendWrapQuotes(this StringBuilder stringBuilder, string text)
            => WrapQoutes(text, stringBuilder);

        public static StringBuilder ConditionallyAppendArray(this StringBuilder stringBuilder, string name, IEnumerable<string> items, byte indent = IndentOne)
            => When.Any(items)
                ? stringBuilder.AppendArrayProperty(name, items, indent)
                : stringBuilder;

        public static StringBuilder ConditionallyAppendBool(this StringBuilder stringBuilder, string name, bool addWhenTrue, byte indent = IndentOne)
            => addWhenTrue
                ? stringBuilder.AppendBoolProperty(name, indent)
                : stringBuilder;

        public static StringBuilder ConditionallyAppendObject(this StringBuilder stringBuilder, string name, IDictionary<string, IEnumerable<string>> items, byte indent = IndentOne)
            => When.Any(items)
                ? stringBuilder.AppendObjectProperty(name, items, indent)
                : stringBuilder;

        public static StringBuilder ConditionallyAppendString(this StringBuilder stringBuilder, string name, string text, byte indent = IndentOne)
            => string.IsNullOrEmpty(text) || text == Null
                ? stringBuilder
                : stringBuilder.AppendStringProperty(name, text, indent);

        public static Func<KeyValuePair<T, string>, KeyValuePair<string, string>> EnumKeyedDictionary<T>()
            => dict => new KeyValuePair<string, string>((dict.Key as Enum).GetStringValue(), dict.Value);

        public static StringBuilder GetArrayProperty(string name, IEnumerable<IJsonSerializable> items, byte indent = IndentOne, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            stringBuilder.AppendProperty(name, formatNameAsCamelCase).Append(OpenSquareBracket);
            var count = items.Count();
            for (int i = Zero; i < count; i++)
            {
                var nextIndent = NextIndent(indent);
                stringBuilder = items.ElementAt(i).GetJsonStringBuilder(indent: nextIndent, stringBuilder: stringBuilder.AppendLine().AppendIndent(nextIndent));
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder.AppendLine(indent, CloseSquareBracket);
        }

        public static StringBuilder GetArrayProperty(string name, IEnumerable<string> items, byte indent = IndentOne, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            stringBuilder.AppendProperty(name, formatNameAsCamelCase).Append(OpenSquareBracket);
            var count = items.Count();
            for (int i = Zero; i < count; i++)
            {
                stringBuilder.AppendLine().AppendIndent(NextIndent(indent)).AppendWrapQuotes(items.ElementAt(i));
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder.AppendLine(indent, CloseSquareBracket);
        }

        public static StringBuilder GetBooleanProperty(string name, bool value = true, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, BoolString(value), stringBuilder);

        public static StringBuilder GetNumberProperty(string name, long value, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, value.ToString(), stringBuilder);

        public static StringBuilder GetNumberProperty(string name, int value, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, value.ToString(), stringBuilder);

        public static StringBuilder GetNumberProperty(string name, short value, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, value.ToString(), stringBuilder);

        public static StringBuilder GetNumberProperty(string name, byte value, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, value.ToString(), stringBuilder);

        public static StringBuilder GetNullProperty(string name, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => NullPropertyAssignment(name, formatNameAsCamelCase, stringBuilder);

        public static StringBuilder GetObjectProperty(string name, IJsonSerializable @object, byte indent = byte.MinValue, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
            => stringBuilder == null
                ? @object.GetJsonStringBuilder(indent: indent, stringBuilder: new StringBuilder().AppendProperty(name, formatNameAsCamelCase))
                : @object.GetJsonStringBuilder(indent: indent, stringBuilder: stringBuilder.AppendProperty(name, formatNameAsCamelCase));

        public static StringBuilder GetObjectProperty(string name, IEnumerable<KeyValuePair<string, IEnumerable<string>>> members, byte indent = byte.MinValue, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            stringBuilder.AppendProperty(name, formatNameAsCamelCase).Append(OpenCurlyBrace);
            var count = members.Count();
            for (int i = Zero; i < count; i++)
            {
                var entry = members.ElementAt(i);
                stringBuilder.AppendArrayProperty(entry.Key, entry.Value, NextIndent(indent), appendComma: false);
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder.AppendLine(indent, CloseCurlyBrace);
        }

        public static StringBuilder GetObjectProperty(string name, IEnumerable<KeyValuePair<string, bool>> members, byte indent = byte.MinValue, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            stringBuilder.AppendProperty(name, formatNameAsCamelCase).Append(OpenCurlyBrace);
            var count = members.Count();
            for (int i = Zero; i < count; i++)
            {
                var entry = members.ElementAt(i);
                stringBuilder.AppendBoolProperty(entry.Key, value: entry.Value, indent: NextIndent(indent), appendComma: false);
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder.AppendLine(indent, CloseCurlyBrace);
        }

        public static StringBuilder GetObjectProperty(string name, IEnumerable<KeyValuePair<string, string>> members, byte indent = byte.MinValue, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            stringBuilder.AppendProperty(name, formatNameAsCamelCase).Append(OpenCurlyBrace);
            var count = members.Count();
            for (int i = Zero; i < count; i++)
            {
                var entry = members.ElementAt(i);
                stringBuilder.AppendStringProperty(entry.Key, entry.Value, NextIndent(indent), appendComma: false);
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder.AppendLine(indent, CloseCurlyBrace);
        }

        public static StringBuilder GetStringProperty(string name, string text, bool formatNameAsCamelCase = true, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            return string.IsNullOrEmpty(text)
                ? stringBuilder.AppendPropertyAssignment(name, formatNameAsCamelCase, Null)
                : stringBuilder.AppendPropertyAssignment(name, formatNameAsCamelCase, WrapQoutes(text));
        }

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IEnumerable<string> items, bool formatNameAsCamelCase = true)
            => JsonArray(name, items, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IEnumerable<IJsonSerializable> items, bool formatNameAsCamelCase = true)
            => JsonArray(name, items, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, bool? value = true, bool formatNameAsCamelCase = true)
            => value == null
                ? JsonNull(name, formatNameAsCamelCase)
                : JsonBoolean(name, value.Value, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, long? value, bool formatNameAsCamelCase = true)
            => value == null
                ? JsonNull(name, formatNameAsCamelCase)
                : JsonNumber(name, value.Value, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, int? value, bool formatNameAsCamelCase = true)
            => value == null
                ? JsonNull(name, formatNameAsCamelCase)
                : JsonNumber(name, value.Value, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, short? value, bool formatNameAsCamelCase = true)
            => value == null
                ? JsonNull(name, formatNameAsCamelCase)
                : JsonNumber(name, value.Value, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, byte? value, bool formatNameAsCamelCase = true)
            => value == null
                ? JsonNull(name, formatNameAsCamelCase)
                : JsonNumber(name, value.Value, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> JsonEnum<TEnum>(string name, TEnum value, bool formatNameAsCamelCase = true)
            where TEnum : struct, IComparable
            => JsonNumber(name, Convert.ToInt64(value), formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IJsonSerializable @object, bool formatNameAsCamelCase = true)
            => JsonObject(name, @object, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IEnumerable<KeyValuePair<string, IEnumerable<string>>> members, bool formatNameAsCamelCase = true)
            => JsonObject(name, members, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IEnumerable<KeyValuePair<string, bool>> members, bool formatNameAsCamelCase = true)
            => JsonObject(name, members, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, IEnumerable<KeyValuePair<string, string>> members, bool formatNameAsCamelCase = true)
            => JsonObject(name, members, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json<TEnum>(string name, IEnumerable<KeyValuePair<TEnum, string>> members, bool formatNameAsCamelCase = true) where TEnum : struct, IConvertible
            => JsonObject(name, members, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> Json(string name, string text, bool formatNameAsCamelCase = true)
            => JsonString(name, text, formatNameAsCamelCase);

        public static Func<StringBuilder, byte, StringBuilder> JsonArray(string name, IEnumerable<string> items, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetArrayProperty(name, items, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonArray(string name, IEnumerable<IJsonSerializable> items, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetArrayProperty(name, items, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonBoolean(string name, bool value = true, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetBooleanProperty(name, value, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonNumber(string name, long value, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetNumberProperty(name, value, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonNumber(string name, int value, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetNumberProperty(name, value, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonNumber(string name, short value, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetNumberProperty(name, value, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonNumber(string name, byte value, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetNumberProperty(name, value, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonNull(string name, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetNullProperty(name, formatNameAsCamelCase, stringBuilder);

        public static StringBuilder JsonObject(IEnumerable<Func<StringBuilder, byte, StringBuilder>> properties, Behavior behavior = Behavior.Default, byte indent = IndentZero, StringBuilder stringBuilder = null)
        {
            if (stringBuilder == null) stringBuilder = new StringBuilder();
            var count = properties.Count();
            if (count == 0) return stringBuilder.AppendEmptyResponse(behavior);
            stringBuilder.Append(OpenCurlyBrace);
            for (int i = 0; i < count; i++)
            {
                var nextIndent = NextIndent(indent);
                stringBuilder.AppendLine().AppendIndent(nextIndent);
                stringBuilder = properties.ElementAt(i)(stringBuilder, nextIndent);
                if (NeedsComma(count, i)) stringBuilder.Append(Comma);
            }
            return stringBuilder
                .AppendIndent(indent)
                .AppendLine(CloseCurlyBrace)
                .AppendLine();
        }

        public static Func<StringBuilder, byte, StringBuilder> JsonObject(string name, IJsonSerializable @object, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetObjectProperty(name, @object, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonObject(string name, IEnumerable<KeyValuePair<string, IEnumerable<string>>> members, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetObjectProperty(name, members, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonObject(string name, IEnumerable<KeyValuePair<string, bool>> members, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetObjectProperty(name, members, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonObject(string name, IEnumerable<KeyValuePair<string, string>> members, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetObjectProperty(name, members, indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonObject<TEnum>(string name, IEnumerable<KeyValuePair<TEnum, string>> members, bool formatNameAsCamelCase = true) where TEnum : struct, IConvertible
            => (stringBuilder, indent)
                => GetObjectProperty(name, members.MapEnumDictionary(), indent, formatNameAsCamelCase, stringBuilder);

        public static Func<StringBuilder, byte, StringBuilder> JsonString(string name, string text, bool formatNameAsCamelCase = true)
            => (stringBuilder, indent)
                => GetStringProperty(name, text, formatNameAsCamelCase, stringBuilder);

        public static IEnumerable<KeyValuePair<string, string>> MapEnumDictionary<TEnum>(this IEnumerable<KeyValuePair<TEnum, string>> dictionary)
            => dictionary.Select(EnumKeyedDictionary<TEnum>());

        public static bool NeedsComma(int count, int i)
            => count > One && i < count - One;

        public static byte NextIndent(byte indent)
            => (byte)(indent + One);

        public static bool NotNull(object instance)
            => instance != null;

        public static bool NotNull(string instance)
            => !string.IsNullOrEmpty(instance);

        public static bool NotZero(long instance)
            => instance != Zero;

        public static StringBuilder NullPropertyAssignment(string name, bool formatNameAsCamelCase, StringBuilder stringBuilder = null)
            => PropertyAssignment(name, formatNameAsCamelCase, Null, stringBuilder);

        public static StringBuilder Property(string name, bool formatNameAsCamelCase, StringBuilder stringBuilder = null)
            => (stringBuilder ?? new StringBuilder())
                .AppendWrapQuotes(formatNameAsCamelCase ? name.ToCamelCase() : name)
                .Append(ColonSpace);

        public static StringBuilder PropertyAssignment(string name, bool formatNameAsCamelCase, string valueLiteral, StringBuilder stringBuilder = null)
            => (stringBuilder ?? new StringBuilder())
                .AppendProperty(name, formatNameAsCamelCase)
                .Append(valueLiteral);

        public static StringBuilder PropertyAssignment(string name, bool formatNameAsCamelCase, StringBuilder valueLiteral, StringBuilder stringBuilder = null)
            => (stringBuilder ?? new StringBuilder())
                .AppendProperty(name, formatNameAsCamelCase)
                .Append(valueLiteral);

        public static StringBuilder WrapQoutes(string text, StringBuilder stringBuilder = null)
            => (stringBuilder ?? new StringBuilder())
                .Append(DoubleQuote)
                .Append(text)
                .Append(DoubleQuote);

        private static string BoolString(bool value)
            => value ? BoolTrue : BoolFalse;
    }
}
