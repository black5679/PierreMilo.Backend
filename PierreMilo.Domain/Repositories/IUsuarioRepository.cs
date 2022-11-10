using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;
using PierreMilo.Domain.Responses.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<PaginateResponse<UsuariosResponse>> GetAllPaginate(PaginateRequest paginate);
        Task<Response> Insert(InsertUsuarioRequest request);
        Task<Response> Update(UpdateUsuarioRequest request);
    }
}
