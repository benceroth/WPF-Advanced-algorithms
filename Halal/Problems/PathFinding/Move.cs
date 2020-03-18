namespace Halal.Problems.PathFinding
{
    using System.Collections.Generic;

    public sealed class Move : List<int>, ISolutionElement
    {
        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
