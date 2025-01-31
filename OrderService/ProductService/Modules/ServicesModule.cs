using Autofac;
using ProductService.Services.Implementations;

namespace ProductService.Modules;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var servicesAssembly = typeof(OrderService).Assembly;

        builder.RegisterAssemblyTypes(servicesAssembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
