using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Neogov.Core.Common.CustomAttributes;
using Neogov.Core.Common.Enums;
using Neogov.Core.Common.Events;
using Neogov.Core.Common.Exceptions;
using Neogov.Core.Common.Security;
using Neogov.Core.Common.Utilities;
using Neogov.Core.Common.Wrappers;
using Neogov.Core.Resources;

namespace Neogov.Core.Common.Contracts.BaseContracts
{
    public abstract class ApplicationServiceBase : MarshalByRefObject
    {
        public IDomainEventDispatcher Dispatcher { get; set; }
        protected int EmployerId => UserContext.Current.EmployerId;

        public virtual void BeforeMethodExecuting(MethodInfo methodInfo, IMethodCallMessage message)
        {
            CheckReadonlyMode(methodInfo);
            CheckAuthorization(methodInfo);

            if (this.Dispatcher == null)
                throw new ApplicationException("Event dispatcher should be initialized before calling Application service methods!");

            DomainEvent.CreateContext(this.Dispatcher);
        }

        public virtual void AfterMethodExecuted(MethodInfo methodInfo, IMethodCallMessage message, object methodResult)
        {
            var result = (methodResult as Result);
            if (result == null || result.IsSuccess)
            {
                DomainEvent.Context.Dispatch();
            }

            DomainEvent.Context.Dispose();
        }

        public virtual void AfterExceptionOccurred(Exception exception, MethodInfo methodInfo)
        {
            if (DomainEvent.Context != null)
                DomainEvent.Context.Dispose();

            Log.Exception(exception);
        }

        private static void CheckAuthorization(MemberInfo methodInfo)
        {
            var methodRules = methodInfo.GetCustomAttributes<AuthorizationAttribute>(true);
            var serviceSecurityObject = methodInfo.DeclaringType.GetCustomAttribute<WithSecurityObjectAttribute>(true);

            if (methodRules == null)
                return;

            foreach (var methodRule in methodRules)
            {
                var securityObject = methodRule.SecurityObject;
                var excludedProfileTypes = methodRule.ExcludedProfileTypes;

                if (serviceSecurityObject != null && securityObject == SecurityObject.None)
                    securityObject = serviceSecurityObject.SecurityObject;

                CheckAuthorization(securityObject, methodRule.OperationType, excludedProfileTypes);
            }
        }

        private static void CheckReadonlyMode(MemberInfo methodInfo)
        {
            var methodRules = methodInfo.GetCustomAttributes<AllowInReadOnlyModeAttribute>(true);

            if (methodRules != null && methodRules.Any())
                return;
            
            if (UserContext.Original != null)
                throw new AuthorizationException(ResExceptions.NotAllowedAsImpersonatedUser);
        }

        protected static void CheckAuthorization(SecurityObject securityObject, Crud crud, ProfileType[] excludedProfileTypes = null)
        {
            if (excludedProfileTypes != null && excludedProfileTypes.Contains(UserContext.Current.ProfileType))
                return;

            if (!UserContext.Current.HasAccess(securityObject, crud))
                throw new AuthorizationException(securityObject, crud);
        }
    }
}