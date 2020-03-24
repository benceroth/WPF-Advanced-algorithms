namespace Halal.Problems.TravellingSalesman
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Town : List<double>, ISolutionElement, IProblemElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Town"/> class.
        /// </summary>
        public Town()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Town"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Town(IEnumerable<double> items)
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
