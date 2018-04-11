namespace DevOps.Primitives.TypeScript
{
    /// <summary>Describes behavior taken when provided with zero properties</summary>
    public enum ZeroPropertyBehavior : byte
    {
        ReturnNull,
        ReturnEmptyObject,
        ReturnStringDotEmpty
    }
}
