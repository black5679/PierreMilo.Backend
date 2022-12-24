using MediatR;
using PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginate;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Application.Queries.Vista.GetAllVista
{
    public class GetAllVistaQueryHandler : IRequestHandler<GetAllVistaQuery, List<CommonResponse>>
    {
        private readonly IVistaRepository vistaRepository;
        public GetAllVistaQueryHandler(IVistaRepository vistaRepository)
        {
            this.vistaRepository = vistaRepository;
        }

        public async Task<List<CommonResponse>> Handle(GetAllVistaQuery request, CancellationToken cancellationToken)
        {
            var response = await vistaRepository.GetAll();
            return response;
        }
    }
}
