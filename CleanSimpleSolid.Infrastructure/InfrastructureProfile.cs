using AutoMapper;
using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Model.Tasks;
using ServiceBase.Infrastructure.Records;

namespace ServiceBase.Infrastructure
{
    public class InfrastructureProfile: Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<ExampleRecord, Todo>();
            CreateMap<long, LongIdentity>().ConstructUsing(x=> new LongIdentity(x));
        }
    }
}