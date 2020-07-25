using System;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model
{
    public class Base<T,TU>
    where T: IDbIdentity<TU>
    {
        public Base()
        {
            CreatedDate = DateTimeOffset.UtcNow;
            ModifiedDate = CreatedDate;
        }
        public Base(T t, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            Id = t;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }    
        
        /// <summary>
        /// Identifier for the model.
        /// </summary>
        public T Id { get; protected set; }
        
        /// <summary>
        /// When the model was created.
        /// </summary>
        /// <remarks>
        /// If you don't care when it was created or modified then it's likely not an entity and just a value object.
        /// </remarks>
        public DateTimeOffset CreatedDate { get; }
        
        /// <summary>
        /// The last time the model was modified.
        /// </summary>
        public DateTimeOffset ModifiedDate { get; private set; }

        protected void SetModified()
        {
            ModifiedDate = DateTimeOffset.UtcNow;
        }
    }
}