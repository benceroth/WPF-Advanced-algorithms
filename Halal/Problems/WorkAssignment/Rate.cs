namespace Halal.Problems.WorkAssignment
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Rate : List<double>, ISolutionElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rate"/> class.
        /// </summary>
        public Rate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rate"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Rate(IEnumerable<double> items)
            : base(items)
        {
        }

        /// <summary>
        /// Gets value.
        /// </summary>
        public double Value => this[0];

        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == 1;
        }
    }
}
