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

namespace PierreMilo.Application.Queries.Rol.GetAllRol
{
    public class GetAllRolQueryHandler : IRequestHandler<GetAllRolQuery, List<CommonResponse>>
    {
        private readonly IRolRepository rolRepository;
        public GetAllRolQueryHandler(IRolRepository rolRepository)
        {
            this.rolRepository = rolRepository;
        }

        public async Task<List<CommonResponse>> Handle(GetAllRolQuery request, CancellationToken cancellationToken)
        {
            var response = await rolRepository.GetAll();
            return response;
        }
    }
}
