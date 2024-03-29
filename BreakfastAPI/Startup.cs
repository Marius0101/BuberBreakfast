using BreakfastAPI.Services.Breakfasts;
using Microsoft.OpenApi.Models;

namespace BreakfastAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BreakfastAPI", Version = "v1" });
            });

            services.AddControllers();
            services.AddScoped<IBreakfastControllerService, BreakfastControllerService>();
            services.AddScoped<IBreakfastService, BreakfastService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error");

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreakfastAPI V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
