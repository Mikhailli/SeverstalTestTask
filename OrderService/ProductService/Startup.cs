using System.Text.Json.Serialization;
using Autofac;
using Common.DataAccess.Implementations;
using ProductService.Modules;

namespace ProductService;

/// <summary>
/// Класс содержит методы для внедрения зависимостей
/// </summary>
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>();
        services.AddHttpContextAccessor();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    }

    public void ConfigureContainer(HostBuilderContext hostBuilderContext, ContainerBuilder builder)
    {
        builder.RegisterModule(new RepositoriesModule());
        builder.RegisterModule(new ServicesModule());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}
