using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PierreMilo.Domain.Repositories;
using PierreMilo.Infrastructure.Repositories;

namespace PierreMilo.Infrastructure
{
    public static class DependencyInyection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<DataContext>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVistaRepository, VistaRepository>();
            services.AddTransient<IRolRepository, RolRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
        }
    }
}
