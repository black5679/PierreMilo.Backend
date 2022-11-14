namespace PierreMilo.Domain.Models
{
    public class PermisoModel
    {
        public int IdUsuario { get; set; }
        public int IdVista { get; set; }
        public bool Visualizar { get; set; }
        public bool Registrar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
    }
}
