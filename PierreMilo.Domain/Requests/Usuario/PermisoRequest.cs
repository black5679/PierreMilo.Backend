using PierreMilo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Requests.Usuario
{
    public class PermisoRequest
    {
        public int IdVista { get; set; }
        public bool Visualizar { get; set; }
        public bool Registrar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public static PermisoModel ToModel(int idUsuario, PermisoRequest request)
        {
            PermisoModel model = new()
            {
                IdUsuario = idUsuario,
                IdVista = request.IdVista,
                Visualizar = request.Visualizar,
                Registrar = request.Registrar,
                Editar = request.Editar,
                Eliminar = request.Eliminar
            };
            return model;
        }
    }
}
