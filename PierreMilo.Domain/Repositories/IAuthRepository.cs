using PierreMilo.Domain.Models;
using PierreMilo.Domain.Requests.Auth;

namespace PierreMilo.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<UsuarioModel> LoginAdmin(LoginAdminRequest request);
    }
}
