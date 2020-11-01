using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using InventoryAPI.Services;
using InventoryAPI.Repositories;
using InventoryAPI.Data;

namespace InventoryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemService, ItemsService>();
            services.AddControllers();
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("InventoryDatabase"))
                       .UseSnakeCaseNamingConvention());

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
