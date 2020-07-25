using System;

namespace CleanDdd.Common.Model.Identity
{
    public class LongIdentity: IDbIdentity<long>
    {
       

        public LongIdentity(long id)
        {
            Id = id;
        }

        public long Id { get; private set;  }

        public void SetIdentity(long t)
        {
            if (Id > 0)
            {
                throw new ArgumentException("Identity has already been set.");
            }
            Id = t;
        }

        public bool HasValue()
        {
            return Id != 0;
        }
    }
}