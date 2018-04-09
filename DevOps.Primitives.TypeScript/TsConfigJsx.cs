using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum TsConfigJsx : byte
    {
        [EnumStringValue(nameof(preserve))]
        preserve,
        [EnumStringValue("react-native")]
        reactNative,
        [EnumStringValue(nameof(react))]
        react
    }
}
