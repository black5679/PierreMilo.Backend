using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Domain.Common
{
    public class PaginateRequest
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        public Order? Order { get; set; }
        public string? OrderBy { get; set; }
    }

    public enum Order
    {
        Descendente = 0,
        Ascendente = 1
    }
}
