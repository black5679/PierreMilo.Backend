using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Responses.Usuario
{
    public class UsuarioResponse
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Foto { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
        public List<PermisoResponse> Permisos { get; set; }
    }

    public class PermisoResponse
    {
        public int IdVista { get; set; }
        public bool Visualizar { get; set; }
        public bool Registrar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
    }
}
