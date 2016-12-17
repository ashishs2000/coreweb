using System;
using System.Collections.Generic;

namespace Neogov.Core.Common.Exceptions
{
    public class CoreException : Exception
    {
        public List<string> Errors { get; } = new List<string>();

        public CoreException()
        {
        }

        public CoreException(params string[] messages) : base(messages[0])
        {
            Errors.AddRange(messages);
        }

        public CoreException(string message, Exception ex) : base(message, ex)
        {
            Errors.Add(message);
        }
    }
}