namespace PierreMilo.Domain.Services
{
    public interface IJwtService
    {
        string GenerateToken(dynamic usuario);
        int? ValidateToken(string token);
    }
}
