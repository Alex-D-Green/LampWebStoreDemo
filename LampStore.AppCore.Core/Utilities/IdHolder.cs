using System;
using System.Reflection;


namespace LampStore.AppCore.Core.Utilities
{
    //https://stackoverflow.com/a/52717501/2187280

    /// <summary>
    /// This is utility class that used to wrap persistence level entity and 
    /// get its id after it was saved in DB.
    /// </summary>
    public class IdHolder<TKey>
    {
        private readonly object item;
        private readonly string propertyName;

        public virtual TKey Id 
        { get => (TKey)item.GetType().GetProperty(propertyName).GetValue(item); }


        public IdHolder(object item, string propertyName)
        {
            this.item = item ?? throw new ArgumentNullException(nameof(item));
            this.propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo prop = item.GetType().GetProperty(propertyName) ?? 
                throw new InvalidOperationException($"The item has no property with name {propertyName}.");

            if(prop.PropertyType != typeof(TKey))
                throw new InvalidOperationException(
                    $"The property {propertyName} has different type ({prop.PropertyType.Name}).");
        }
    }
}
