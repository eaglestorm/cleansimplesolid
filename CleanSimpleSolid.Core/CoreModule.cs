using Autofac;
using CleanSimpleSolid.Core.Services;

namespace CleanSimpleSolid.Core
{
    public class CoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().AsSelf();
        }
    }
}