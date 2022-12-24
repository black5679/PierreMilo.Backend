using MediatR;
using PierreMilo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Application.Queries.Vista.GetAllVista
{
    public class GetAllVistaQuery : IRequest<List<CommonResponse>>
    {

    }
}
