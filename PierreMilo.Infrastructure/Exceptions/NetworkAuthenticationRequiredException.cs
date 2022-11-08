namespace PierreMilo.Infrastructure.Exceptions
{
    public class NetworkAuthenticationRequiredException : Exception
    {
        public NetworkAuthenticationRequiredException()
        {
        }

        public NetworkAuthenticationRequiredException(string message)
            : base(message)
        {
        }

        public NetworkAuthenticationRequiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
