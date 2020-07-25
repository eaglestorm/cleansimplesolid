using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Model;

namespace CleanSimpleSolid.Core.Events
{
    /// <summary>
    /// An event that writes an audit log
    /// </summary>
    /// <typeparam name="T">The domain model associated with the event.</typeparam>
    /// <typeparam name="TL">The Identity Wrapper object</typeparam>
    /// <typeparam name="TU">The actual type of the identity, i.e. long or Guid.</typeparam>
    public interface IAuditableEvent<T, TL, TU>
    where T: Base<TL, TU>
    where TL: IDbIdentity<TU>
    {
        /// <summary>
        /// The event that occured.
        /// </summary>
        Events Event { get; } 
        
        /// <summary>
        /// The user who caused the event.
        /// </summary>
        long UserId { get; }
        
        /// <summary>
        /// The domain model associated with the event
        /// </summary>
        T Model { get; }
        
        /// <summary>
        /// A description of the event.
        /// </summary>
        string Description { get; }
    }
}