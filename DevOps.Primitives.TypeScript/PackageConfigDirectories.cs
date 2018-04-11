using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum PackageConfigDirectories : byte
    {
        [EnumStringValue(nameof(lib))]
        lib,
        [EnumStringValue(nameof(bin))]
        bin,
        [EnumStringValue(nameof(man))]
        man,
        [EnumStringValue(nameof(doc))]
        doc,
        [EnumStringValue(nameof(example))]
        example,
        [EnumStringValue(nameof(test))]
        test
    }
}
