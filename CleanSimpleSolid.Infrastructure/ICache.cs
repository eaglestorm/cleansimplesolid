using CleanDdd.Common.Model.Identity;

namespace ServiceBase.Infrastructure
{
    public interface ICache<T, TId>
    {
        
        void Add(T t);
        
        T TryGet(IDbIdentity<TId> identity);

        void Clear();

        void Remove(IDbIdentity<TId> identity);
    }
}