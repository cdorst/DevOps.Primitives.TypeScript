using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum AccessModifiers : byte
    {
        [EnumStringValue("private")]
        Private,
        [EnumStringValue("protected")]
        Protected,
        [EnumStringValue("public")]
        Public
    }
}
