namespace Halal.Problems.PathFinding
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Row : List<int>, IProblemElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        public Row()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Row(IEnumerable<int> items)
            : base(items)
        {
        }

        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
