namespace Halal.Problems.FunctionApproximation
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Value : List<double>, IProblemElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        public Value()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Value(IEnumerable<double> items)
            : base(items)
        {
        }

        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount + 1;
        }
    }
}