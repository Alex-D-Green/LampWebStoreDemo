using System;
using System.Collections.Generic;
using System.Linq;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// Represents the comparison result of the <see cref="FirstLamp"/> and the <see cref="SecondLamp"/>.
    /// </summary>
    public class Comparison
    {
        public int Id { get; }

        public Lamp FirstLamp { get; }

        public Lamp SecondLamp { get; }

        public IReadOnlyCollection<ComparisonPair> Comparisons { get; }


        public Comparison(int id, Lamp firstLamp, Lamp secondLamp, IEnumerable<ComparisonPair> comparisons)
        {
            if(firstLamp is null)
                throw new ArgumentNullException(nameof(firstLamp));

            if(secondLamp is null)
                throw new ArgumentNullException(nameof(secondLamp));


            Id = id;
            FirstLamp = firstLamp;
            SecondLamp = secondLamp;
            Comparisons = (comparisons ?? Enumerable.Empty<ComparisonPair>()).ToArray();
        }
    }
}
