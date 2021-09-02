namespace ServiceBase.Infrastructure.Caches
{
    /// <summary>
    /// Caches objects based on the given key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyValueCache<T>
    {
        void Add(string key, T t);
        
        T TryGet(string key);

        void Clear();

        void Remove(string key);
    }
}