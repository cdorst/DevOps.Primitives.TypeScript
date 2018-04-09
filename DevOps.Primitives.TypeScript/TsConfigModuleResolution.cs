using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum TsConfigModuleResolution : byte
    {
        [EnumStringValue(nameof(node))]
        node,
        [EnumStringValue(nameof(classic))]
        classic
    }
}
