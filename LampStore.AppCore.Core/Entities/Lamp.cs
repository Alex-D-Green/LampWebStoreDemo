using System;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// The lamp domain entity.
    /// </summary>
    public class Lamp: Entity<int>
    {
        public LampType LampType { get; }

        public string Manufacturer { get; }

        public double Cost { get; }

        public string ImageRef { get; }


        public Lamp(LampType lampType, string manufacturer, double cost, string imageRef, int id = 0)
            : base(id)
        {
            if(String.IsNullOrEmpty(manufacturer))
                throw new ArgumentNullException(nameof(manufacturer));

            if(cost <= 0)
                throw new ArgumentOutOfRangeException(nameof(cost));


            LampType = lampType;
            Manufacturer = manufacturer;
            Cost = cost;
            ImageRef = imageRef;
        }
    }
}
