using FlexisourceIT.Models;
using FlexisourceIT.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Add configuration for ApiSettings
    services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

    services.AddControllers();
    services.AddHttpClient<IRainfallService, RainfallService>();
    services.AddTransient<IRainfallService, RainfallService>(); // Add the interface as well

    // Add Swagger services
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rainfall API", Version = "v1" });
    });
}

void Configure(WebApplication app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API V1");
        });
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
}