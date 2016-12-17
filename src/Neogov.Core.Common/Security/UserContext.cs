using System.Runtime.Remoting.Messaging;

namespace Neogov.Core.Common.Security
{
    public static class UserContext
    {
        private const string USER_KEY = "CURRENT_USER_INFO";
        private const string ORIGINAL_USER_KEY = "ORIGINAL_USER_INFO";

        public static IUserContext Current
        {
            get { return (IUserContext)CallContext.GetData(USER_KEY); }
            set { CallContext.SetData(USER_KEY, value); }
        }

        public static IUserContext Original
        {
            get { return (IUserContext)CallContext.GetData(ORIGINAL_USER_KEY); }
            set { CallContext.SetData(ORIGINAL_USER_KEY, value); }
        }
    }
}