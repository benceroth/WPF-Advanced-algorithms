namespace Halal.Problems.PathFinding
{
    using System.Collections.Generic;

    public class Move : List<int>, ISolutionElement
    {
        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
