using System;
using System.Linq;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// Represents a comparison of two lamps by the filed <see cref="FiledName"/>.
    /// </summary>
    public class ComparisonPair
    {
        public int Id { get; }

        public string FiledName { get; }

        public string FirstValue { get; }

        public string SecondValue { get; }


        public ComparisonPair(int id, string filedName, string firstValue, string secondValue)
        {
            if(!lampProps.Contains(filedName))
                throw new ArgumentOutOfRangeException(nameof(filedName));


            Id = id;
            FiledName = filedName;
            FirstValue = firstValue;
            SecondValue = secondValue;
        }


        private static readonly string[] lampProps = 
            typeof(Lamp).GetProperties().Select(o => o.Name).ToArray();
    }
}
