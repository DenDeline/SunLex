using Autofac;
using SunLex.ApplicationCore.Interfaces;
using SunLex.ApplicationCore.Services;

namespace SunLex.ApplicationCore
{
    public class DefaultCoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LearningResourcesService>()
                .As<ILearningResourcesService>()
                .InstancePerLifetimeScope();
        }
    }
}