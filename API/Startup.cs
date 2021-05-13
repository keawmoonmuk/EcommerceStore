
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public readonly IConfiguration _config;  //configuration

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();  //add repository patten
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>))); // add generic repo
            services.AddAutoMapper(typeof(MappingProfiles));  // add service automapper
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => 
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            // services.AddSwaggerGen(c => 
            // {
            //     c.SwaggerDoc("v1", new OpenApifo {Title = "API",EnvironmentVariablesExtensions = "v1"});
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndPoint("/swagger/v1/swagger.json","API v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();  // add file

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
