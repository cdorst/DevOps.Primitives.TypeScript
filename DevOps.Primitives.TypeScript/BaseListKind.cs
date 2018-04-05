using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum BaseListKind : byte
    {
        [EnumStringValue("extends")]
        Extends,
        [EnumStringValue("implements")]
        Implements
    }
}
