namespace CleanDdd.Common.Model.Identity
{
    /// <summary>
    /// database identity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbIdentity<T>
    {        
        /// <summary>
        /// The value.
        /// </summary>
        T Id { get;  }

        /// <summary>
        /// set the identity.
        /// </summary>
        /// <param name="t"></param>
        void SetIdentity(T t);

        bool HasValue();
    }
}