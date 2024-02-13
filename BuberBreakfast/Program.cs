using BuberBreakfast.Services.Breakfasts;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfeastService, BreakfastService>();
}
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    // Endpoint-ul pentru Swagger UI
});
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

