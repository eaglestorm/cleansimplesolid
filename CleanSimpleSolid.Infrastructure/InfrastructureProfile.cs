using AutoMapper;
using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Model.Tasks;
using CleanSimpleSolid.Core.Model.User;
using ServiceBase.Infrastructure.Records;

namespace ServiceBase.Infrastructure
{
    public class InfrastructureProfile: Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<CssTaskRecord, CssTask>();
            CreateMap<long, LongIdentity>().ConstructUsing(x=> new LongIdentity(x));
            CreateMap<CssUserRecord, CssUser>();
        }
    }
}