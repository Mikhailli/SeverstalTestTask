using Common.DataAccess.Implementations;
using Common.DataAccess.Interfaces;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace OrderService.Modules;

/// <summary>
/// Модуль для внедрения зависимостей связанных с репозиториями
/// </summary>
public class RepositoriesModule : Module
{
    /// <summary>
    /// Внедрение зависимостей
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EFGenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        var repositoryAssembly = typeof(EFGenericRepository<>).GetTypeInfo().Assembly;

        builder.RegisterAssemblyTypes(repositoryAssembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
