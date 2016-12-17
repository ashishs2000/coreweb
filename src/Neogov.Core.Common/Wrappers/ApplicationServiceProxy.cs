using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Neogov.Core.Common.Contracts.BaseContracts;
using Neogov.Core.Common.Exceptions;
using Neogov.Core.Common.Extensions;

namespace Neogov.Core.Common.Wrappers
{
    public class ApplicationServiceProxy<T> : RealProxy where T : ApplicationServiceBase
    {
        private readonly T _baseService;
        public ApplicationServiceProxy(T baseService) : base(typeof(T))
        {
            this._baseService = baseService;
        }

        public T Resolve()
        {
            return GetTransparentProxy() as T;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCallMessage = msg as IMethodCallMessage;
            if (methodCallMessage != null)
            {
                var methodCall = new MethodCallMessageWrapper(methodCallMessage);
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (methodInfo == null)
                    throw new ApplicationException("Method " + methodCall.MethodName + " is invalid!");

                try
                {
                    _baseService.BeforeMethodExecuting(methodInfo, methodCall);

                    var methodResult = methodInfo.Invoke(_baseService, methodCall.InArgs);

                    _baseService.AfterMethodExecuted(methodInfo, methodCall, methodResult);

                    return new ReturnMessage(methodResult, null, 0, methodCall.LogicalCallContext, methodCall);
                }
                catch (CoreException ex)
                {
                    return HandleCoreException(ex, methodCall, methodInfo);
                }
                catch (Exception ex)
                {
                    var coreException = ex.FindInner<CoreException>();
                    if (coreException != null)
                        return HandleCoreException(coreException, methodCall, methodInfo);

                    _baseService.AfterExceptionOccurred(ex, methodInfo);
                    throw;
                }
            }

            return msg;
        }

        private ReturnMessage HandleCoreException(CoreException ex, IMethodCallMessage methodCall, MethodInfo methodInfo)
        {
            _baseService.AfterExceptionOccurred(ex, methodInfo);

            var result = ConstructResultFromPerformException(ex, methodInfo);

            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
        }

        private static Result ConstructResultFromPerformException(CoreException exception, MethodInfo method)
        {
            Debug.Assert(method.DeclaringType != null);

            var result = Activator.CreateInstance(method.ReturnType) as Result;
            if (result == null)
                throw new ApplicationException(method.DeclaringType.Name + "." + method.Name + " return type should inherit from CommandResult!");

            result.Error(exception.Message);
            return result;
        }
    }
}