namespace DevOps.Primitives.TypeScript
{
    public interface IByteArrayDeserializable<TEntity> : IByteArraySerializable
        where TEntity : class, IByteArrayDeserializable<TEntity>, new()
    {
        TEntity SetProperties(byte[] bytes);
    }
}
