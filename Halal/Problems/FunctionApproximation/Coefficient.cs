namespace Halal.Problems.FunctionApproximation
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Coefficient : List<double>, ISolutionElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coefficient"/> class.
        /// </summary>
        public Coefficient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coefficient"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Coefficient(IEnumerable<double> items)
            : base(items)
        {
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public double Value
        {
            get => this[0];
            set => this[0] = value;
        }

        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == 1;
        }
    }
}
