using PierreMilo.Domain.Common;
using PierreMilo.Domain.Models;
using PierreMilo.Domain.Requests.Auth;

namespace PierreMilo.Domain.Repositories
{
    public interface IVistaRepository
    {
        Task<List<CommonResponse>> GetAll();
    }
}
