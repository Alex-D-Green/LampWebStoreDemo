using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace LampStore.Infrastructure.EFStorage.Entities
{
    [Table("Comparisons")]
    internal class ComparisonEF
    {
        public int Id { get; set; }

        public LampEF FirstLamp { get; set; }

        public LampEF SecondLamp { get; set; }

        public IReadOnlyCollection<ComparisonPairEF> Comparisons { get; set; }
    }
}
