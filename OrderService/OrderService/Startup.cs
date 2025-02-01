using Autofac;
using Common.DataAccess.Implementations;
using OrderService.Modules;

namespace OrderService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>();
        services.AddHttpContextAccessor();

        services.AddControllers();
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
