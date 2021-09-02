using CleanDdd.Common.Model.Identity;

namespace ServiceBase.Infrastructure.Caches
{
    /// <summary>
    /// Repository cache.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IIdentityCache<T, TId>
    {
        
        void Add(T t);
        
        T TryGet(IDbIdentity<TId> identity);

        void Clear();

        void Remove(IDbIdentity<TId> identity);
    }
}