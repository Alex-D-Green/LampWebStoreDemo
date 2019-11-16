using System;
using System.Collections.Generic;
using System.Linq;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// Represents the comparison result of the <see cref="FirstLamp"/> and the <see cref="SecondLamp"/>.
    /// </summary>
    public class Comparison: Entity<int>
    {
        public Lamp FirstLamp { get; }

        public Lamp SecondLamp { get; }

        public IReadOnlyCollection<ComparisonPair> Comparisons { get; }


        public Comparison(Lamp firstLamp, Lamp secondLamp, IEnumerable<ComparisonPair> comparisons, int id = 0)
            : base(id)
        {
            if(firstLamp is null)
                throw new ArgumentNullException(nameof(firstLamp));

            if(secondLamp is null)
                throw new ArgumentNullException(nameof(secondLamp));


            FirstLamp = firstLamp;
            SecondLamp = secondLamp;
            Comparisons = (comparisons ?? Enumerable.Empty<ComparisonPair>()).ToArray();
        }
    }
}
