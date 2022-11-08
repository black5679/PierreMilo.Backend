using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Requests.Auth
{
    public class LoginAdminRequest
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}
