using System.Runtime.Serialization;

namespace PierreMilo.Infrastructure.Exceptions
{
    [Serializable]
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
        protected NetworkAuthenticationRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
