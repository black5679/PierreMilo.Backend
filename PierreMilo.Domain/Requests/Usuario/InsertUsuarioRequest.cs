using PierreMilo.Domain.Models;

namespace PierreMilo.Domain.Requests.Usuario
{
    public class InsertUsuarioRequest
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Password { get; set; }
        public string? Foto { get; set; }
        public int IdRol { get; set; }
        public List<PermisoRequest> Permisos { get; set; }
    }
}
