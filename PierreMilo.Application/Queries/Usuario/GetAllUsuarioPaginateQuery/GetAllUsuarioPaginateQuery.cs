using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Responses.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginateQuery
{
    public class GetAllUsuarioPaginateQuery : PaginateRequest, IRequest<PaginateResponse<UsuariosResponse>>
    {
    }
}
