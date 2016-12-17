using System.Collections.Generic;

namespace Neogov.Core.Common.Wrappers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; }

        public List<string> DebugInfo { get; set; } = new List<string>();

        private Result() {}
        public Result(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public void AddDebugInfo(params string[] messages)
        {
            DebugInfo.AddRange(messages);
        }

        public void Error(string message,params object[] args)
        {
            IsSuccess = false;
            Message = string.Format(message, args);
        }

        public static Result<T> Ok<T>(T data, string message = null)
        {
            return new Result<T>(data, true, message);
        }

        public static Result<T> Fail<T>(string message, params object[] parameters)
        {
            return new Result<T>(default(T), false, string.Format(message, message));
        }

        public static Result<T> Fail<T>(T data, string message, params object[] parameters)
        {
            return new Result<T>(data, false, string.Format(message, message));
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
        
        public Result(T data, bool isSuccess, string message = null) : base(isSuccess, message)
        {
            Value = data;
        }

        public Result(bool isSuccess, string message = null) : base(isSuccess, message)
        {
        }
    }
}