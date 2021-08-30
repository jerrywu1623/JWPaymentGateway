using System.Reflection;
using FluentValidation;
using JWPaymentGateway.Application.Common.Behaviours;
using JWPaymentGateway.Application.Mappings;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JWPaymentGateway.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(PaymentMapping)));
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            
            return services;
        }
    }
}