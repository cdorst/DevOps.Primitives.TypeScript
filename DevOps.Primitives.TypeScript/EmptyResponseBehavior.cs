using System;
using static DevOps.Primitives.TypeScript.EnumFlagPositionConstants;

namespace DevOps.Primitives.TypeScript
{
    /// <summary>Describes behavior taken when responding with no data</summary>
    [Flags]
    public enum EmptyResponseBehavior : byte
    {
        None,
        ExcludeEmptyProperties  = _0,
        ReturnEmptyArray        = _1,
        ReturnEmptyObject       = _2,
        ReturnEmptyString       = _3,
        ReturnNull              = _4,
        Default = ExcludeEmptyProperties | ReturnNull
    }
}
