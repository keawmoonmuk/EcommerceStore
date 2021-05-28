using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtenstion
    {
        public static IServiceCollection AddSwaggerDocumention(this IServiceCollection services)
        {
          // add swagger 
          services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name ="Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securitRequirement = new OpenApiSecurityRequirement {{securitySchema, new[]
                {"Bearer"}}};
                c.AddSecurityRequirement(securitRequirement);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumention(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","API v1"));

            return app;
        }
    }
}