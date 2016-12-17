using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.Exceptions
{
    public class AuthorizationException : CoreException
    {
        public override string Message { get; }

        public AuthorizationException(string message = null)
        {
            this.Message = message ?? "You don't have necessary rights to perform that operation.";
        }

        public AuthorizationException(SecurityObject securityObject, Crud crud)
        {
            this.Message = $"You don't have {crud} right to perform on {securityObject}";
        }
    }
}