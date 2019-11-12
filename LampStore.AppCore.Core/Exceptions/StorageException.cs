using System;


namespace LampStore.AppCore.Core.Exceptions
{
    /// <summary>
    /// The common Data Storage exception. 
    /// It is used to wrap over DB exceptions (to uncouple from certain ORM types).
    /// </summary>
    public class StorageException: CoreException
    {
        public StorageException()
        {
        }

        public StorageException(string message)
            : base(message)
        {
        }

        public StorageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
