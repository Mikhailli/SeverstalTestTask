using Autofac;

namespace OrderService.Modules;

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
        var servicesAssembly = typeof(Services.Implementations.OrderService).Assembly;

        builder.RegisterAssemblyTypes(servicesAssembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
