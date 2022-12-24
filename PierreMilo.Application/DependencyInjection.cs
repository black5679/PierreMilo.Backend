using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using PierreMilo.Domain.Services;
using PierreMilo.Application.Services;

namespace PierreMilo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IJwtService, JwtService>();
            return services;
        }
    }
}
