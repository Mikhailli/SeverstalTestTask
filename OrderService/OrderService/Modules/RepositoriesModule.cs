using Common.DataAccess.Implementations;
using Common.DataAccess.Interfaces;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace OrderService.Modules;

public class RepositoriesModule : Module
{
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
