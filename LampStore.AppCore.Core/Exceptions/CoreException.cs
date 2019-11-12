using System;

namespace LampStore.AppCore.Core.Exceptions
{
    /// <summary>
    /// The common exception. 
    /// It is used as base exceptions to wrap up specific exceptions' types.
    /// </summary>
    public class CoreException: Exception
    {
        public CoreException()
        {
        }

        public CoreException(string message)
            : base(message)
        {
        }

        public CoreException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
