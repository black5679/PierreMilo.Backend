using MediatR;
using PierreMilo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Application.Queries.Rol.GetAllRol
{
    public class GetAllRolQuery : IRequest<List<CommonResponse>>
    {

    }
}
