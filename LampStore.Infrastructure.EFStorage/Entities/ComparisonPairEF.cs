using System.ComponentModel.DataAnnotations.Schema;


namespace LampStore.Infrastructure.EFStorage.Entities
{
    [Table("ComparisonPairs")]
    internal class ComparisonPairEF
    {
        public int Id { get; set; }

        public string FiledName { get; set; }

        public string FirstValue { get; set; }

        public string SecondValue { get; set; }


        public int ComparisonEFId { get; set; }
    }
}
