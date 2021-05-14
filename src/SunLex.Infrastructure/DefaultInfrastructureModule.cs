using Autofac;
using SunLex.Infrastructure.Data;
using SunLex.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
