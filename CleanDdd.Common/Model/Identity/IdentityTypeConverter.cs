using AutoMapper;

namespace CleanDdd.Common.Model.Identity
{
    public class IdentityTypeConverter: ITypeConverter<long,LongIdentity>
    {
        public LongIdentity Convert(long source, LongIdentity destination, ResolutionContext context)
        {
            return new LongIdentity(source);
        }
    }
}