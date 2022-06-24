using HealthChekExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Api version  1", Version = "v1", Description = "Test Description", });
    options.SwaggerDoc("v2", new OpenApiInfo() { Title = "Api version  2", Version = "v2", Description = "Test Description", });




});


//healthChek con clase generica de una libreria
builder.Services.AddHealthChecks().AddGenericoHealthCheck(builder.Configuration);

//Versionamiento de apis
builder.Services.AddApiVersioning(op =>
{
    op.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    op.AssumeDefaultVersionWhenUnspecified = true;
    op.ReportApiVersions = true;
});


builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});



var app = builder.Build();

app.UseApiVersioning();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"health Check v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", $"health Check v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
