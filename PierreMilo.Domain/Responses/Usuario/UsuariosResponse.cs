using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Responses.Usuario
{
    public class UsuariosResponse
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Foto { get; set; }
        public bool Estado { get; set; }
        public string Rol { get; set; }
    }
}
