using Autofac;
using SunLex.Infrastructure.Data;
using SunLex.SharedKernel.Interfaces;


namespace SunLex.Infrastructure
{
    public class DefaultInfrastructureModule: Module
    {
        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterCommonDependencies(builder);
        }
    }
}
