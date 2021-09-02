using Autofac;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Interfaces.Repository;
using CleanSimpleSolid.Core.Model.User;
using ServiceBase.Infrastructure.Caches;
using ServiceBase.Infrastructure.Caches.Impl;
using ServiceBase.Infrastructure.Repository;

namespace ServiceBase.Infrastructure
{
    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryKeyValueCache<CssUser>>().As<IKeyValueCache<CssUser>>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}