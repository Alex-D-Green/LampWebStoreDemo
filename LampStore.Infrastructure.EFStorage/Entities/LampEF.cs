using System.ComponentModel.DataAnnotations.Schema;
using LampStore.AppCore.Core.Entities;


namespace LampStore.Infrastructure.EFStorage.Entities
{
    [Table("Lamps")]
    internal class LampEF
    {
        public int Id { get; set; }

        public LampType LampType { get; set; }

        public string Manufacturer { get; set; }

        public double Cost { get; set; }

        public string ImageRef { get; set; }
    }
}
