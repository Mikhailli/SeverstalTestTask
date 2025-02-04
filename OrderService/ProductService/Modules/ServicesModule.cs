using Autofac;
using ProductService.Services.Implementations;

namespace ProductService.Modules;

/// <summary>
/// Модуль для внедрения зависимостей связанных с сервисами
/// </summary>
public class ServicesModule : Module
{
    /// <summary>
    /// Внедрение зависимостей
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        var servicesAssembly = typeof(OrderService).Assembly;

        builder.RegisterAssemblyTypes(servicesAssembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
