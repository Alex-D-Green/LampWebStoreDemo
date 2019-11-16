using System;
using System.Linq;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// Represents a comparison of two lamps by the filed <see cref="FiledName"/>.
    /// </summary>
    public class ComparisonPair: Entity<int>
    {
        #region Inner fields

        private static readonly string[] lampProps =
            typeof(Lamp).GetProperties().Select(o => o.Name).ToArray();

        #endregion


        public string FiledName { get; }

        public string FirstValue { get; }

        public string SecondValue { get; }


        public ComparisonPair(string filedName, string firstValue, string secondValue, int id = 0)
            : base(id)
        {
            if(!lampProps.Contains(filedName))
                throw new ArgumentOutOfRangeException(nameof(filedName));


            FiledName = filedName;
            FirstValue = firstValue;
            SecondValue = secondValue;
        }
    }
}
