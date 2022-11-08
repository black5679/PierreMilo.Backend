using System.Collections.Generic;

namespace PierreMilo.Domain.Common
{
    public class PaginateResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
        public decimal Total { get; set; }
    }
}
