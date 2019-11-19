using System;


namespace LampStore.AppCore.Core.Exceptions
{
    public class ItemNotFoundStorageException: StorageException
    {
        public ItemNotFoundStorageException()
        {
        }

        public ItemNotFoundStorageException(string message)
            : base(message)
        {
        }

        public ItemNotFoundStorageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
