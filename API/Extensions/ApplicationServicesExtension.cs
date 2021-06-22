using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>(); // add service cacheservice
            services.AddScoped<ITokenService, TokenService>();              // add service token
            services.AddScoped<IOrderService, OrderService>();              //add service order
            services.AddScoped<IPaymentService, PaymentService>();          // add payment interface service and
            services.AddScoped<IUnitOfWork, UnitOfWork>();                  //add service uinit work
            services.AddScoped<IProductRepository, ProductRepository>();    //add repository patten
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>))); // add generic repo
            services.AddScoped<IBasketRepository, BasketRepository>();      // add server interface basket and class basket
            
            // add serivce  api error
            services.Configure<ApiBehaviorOptions>(option => 
            {
                option.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x  => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                    
                };
            });

            return services;
        }
    }
}