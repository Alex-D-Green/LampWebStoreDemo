using System;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// The lamp domain entity.
    /// </summary>
    public class Lamp
    {
        public int Id { get; }

        public LampType LampType { get; }

        public string Manufacturer { get; }

        public double Cost { get; }

        public string ImageRef { get; }


        public Lamp(int id, LampType lampType, string manufacturer, double cost, string imageRef)
        {
            if(String.IsNullOrEmpty(manufacturer))
                throw new ArgumentNullException(nameof(manufacturer));

            if(cost <= 0)
                throw new ArgumentOutOfRangeException(nameof(cost));


            Id = id;
            LampType = lampType;
            Manufacturer = manufacturer;
            Cost = cost;
            ImageRef = imageRef;
        }
    }
}
