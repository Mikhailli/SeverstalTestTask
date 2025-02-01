using Autofac.Core;
using Autofac;
using System.Net.Http;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models.Settings;
using WpfOrderManagementSystem.ViewModels;
using Microsoft.Extensions.Configuration;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;
using WpfOrderManagementSystem.Services.Implementations;
using WpfOrderManagementSystem.Services.Interfaces;
using WpfOrderManagementSystem.ViewModels.OrdersEditor;

namespace WpfOrderManagementSystem;

internal class BootstrapperAutofac
{
    private const string _windowTitle = "Система управления заказами";

    public IContainer Container { get; } = null!;

    public NavigationViewModel MainWindowViewModel
    {
        get => Container.Resolve<NavigationViewModel>(
            new NamedParameter("initialViewModel", Container.Resolve<MainViewModel>()), new NamedParameter("windowTitle", _windowTitle));
    }

    public BootstrapperAutofac(IConfiguration configuration)
    {
        AppSettings appSettings = configuration.Get<AppSettings>()!;

        var builder = new ContainerBuilder();

        builder.Register(_ => new ServiceForInformationApiSettings { BaseUrl = appSettings!.InformationServiceApiBaseAddress })
        .As<ServiceForInformationApiSettings>()
        .InstancePerLifetimeScope();

        builder.Register(_ => new ServiceForOrderManagementApiSettings { BaseUrl = appSettings!.OrderManagementServiceApiBaseAddress })
        .As<ServiceForOrderManagementApiSettings>()
        .InstancePerLifetimeScope();

        builder.Register(_ => appSettings)
            .As<AppSettings>()
            .SingleInstance();

        builder.RegisterType<ApiHttpClientFactory>()
            .As<IApiHttpClientFactory>()
            .SingleInstance();
        builder.RegisterType<ViewModelLocator>()
            .SingleInstance();
        builder.RegisterType<NavigationManager>()
            .SingleInstance();
        builder.RegisterType<NavigationViewModel>()
            .SingleInstance();
        builder.RegisterType<MainViewModel>();
        builder.RegisterType<ProductEditorViewModel>();
        builder.RegisterType<AddOrderViewModel>();
        builder.RegisterType<DeleteOrderViewModel>();
        builder.RegisterType<EditOrderViewModel>();
        builder.RegisterType<OrderEditorViewModel>();

        RegisterServiceWithHttpClient<ServiceForOrderManagement, IServiceForOrderManagement>(builder);
        RegisterServiceWithHttpClient<ProductServiceForInformation, IProductServiceForInformation>(builder);
        RegisterServiceWithHttpClient<OrderServiceForInformation, IOrderServiceForInformation>(builder);

        Container = builder.Build();
    }

    private void RegisterServiceWithHttpClient<TImplementation, TInterface>(ContainerBuilder builder) where TInterface : notnull where TImplementation : notnull
    {
        builder.RegisterType<TImplementation>()
            .As<TInterface>()
            .WithParameter(new ResolvedParameter((pi, _) => pi.ParameterType == typeof(HttpClient),
            (_, context) => context.Resolve<IApiHttpClientFactory>().GetUnauthorizedClient()))
            .InstancePerLifetimeScope();
    }
}
