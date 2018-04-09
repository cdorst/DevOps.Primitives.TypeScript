using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum TsConfigTarget : byte
    {
        [EnumStringValue(nameof(ES3))]
        ES3,
        [EnumStringValue(nameof(ES5))]
        ES5,
        [EnumStringValue(nameof(ES2015))]
        ES2015,
        [EnumStringValue(nameof(ES2016))]
        ES2016,
        [EnumStringValue(nameof(ES2017))]
        ES2017,
        [EnumStringValue(nameof(ES2018))]
        ES2018,
        [EnumStringValue(nameof(ESNEXT))]
        ESNEXT
    }
}
