namespace Halal.Problems.WorkAssignment
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public sealed class Person : List<double>, IProblemElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="items">Initial items.</param>
        public Person(IEnumerable<double> items)
            : base(items)
        {
        }

        /// <summary>
        /// Gets salary.
        /// </summary>
        public double Salary => this[0];

        /// <summary>
        /// Gets quality.
        /// </summary>
        public double Quality => this[1];

        /// <inheritdoc/>
        public bool IsValid(int dimensionCount)
        {
            return this.Count == 2;
        }
    }
}
