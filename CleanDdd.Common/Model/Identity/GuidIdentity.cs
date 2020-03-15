using System;

namespace CleanDdd.Common.Model.Identity
{
    public class GuidIdentity: IDbIdentity<Guid?>
    {
        
        public Guid? Id { get; private set; }
        public void SetIdentity(Guid? t)
        {
            if (Id.HasValue)
            {
                //safety check.
                throw new ArgumentException("The Id already has a value.");
            }

            Id = t;
        }

        public bool HasValue()
        {
            return Id.HasValue;
        }
    }
}