using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PierreMilo.Domain.Common
{
    public class PaginateRequest
    {
        [BindRequired]
        public int Limit { get; set; }
        [BindRequired]
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
