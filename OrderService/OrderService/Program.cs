using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using OrderService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API дл€ сервиса управлени€ заказами",
        Version = "v1",
        Description = "ќбрабатывает создание и отмену заказа, изменение статуса заказа, редактирование заказа (удалить/добавить товар)",
        Contact = new OpenApiContact { Name = "ћихаил Ѕабичев", Email = "babichev.mikhail.s@gmail.com" }
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API дл€ сервиса управлени€ заказами v1");
});

app.Run();