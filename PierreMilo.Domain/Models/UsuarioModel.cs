using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
    }
}
