using Autofac;

namespace OrderService.Modules;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var servicesAssembly = typeof(Services.Implementations.OrderService).Assembly;

        builder.RegisterAssemblyTypes(servicesAssembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
