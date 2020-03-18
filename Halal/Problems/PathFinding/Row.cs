namespace Halal.Problems.PathFinding
{
    using System.Collections.Generic;

    public sealed class Row : List<int>, IProblemElement
    {
        public Row()
        {
        }

        public Row(IEnumerable<int> items)
            : base(items)
        {
        }

        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
