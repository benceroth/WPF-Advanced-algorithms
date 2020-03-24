namespace Halal.Problems.PathFinding
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Move : List<int>, ISolutionElement
    {
        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
