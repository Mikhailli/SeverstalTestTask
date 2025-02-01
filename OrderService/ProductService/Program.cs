using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProductService;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API для сервиса хранения",
        Version = "v1",
        Description = "Предоставляет доступ к данным только для чтения",
        Contact = new OpenApiContact { Name = "Михаил Бабичев", Email = "babichev.mikhail.s@gmail.com" }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var startup = new Startup();
startup.ConfigureServices(builder.Services);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

var app = builder.Build();
startup.Configure(app, app.Environment);


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API для сервиса хранения v1");
});

app.Run();