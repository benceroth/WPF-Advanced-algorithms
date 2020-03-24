namespace Halal.Problems.PathFinding
{
    /// <inheritdoc/>
    public sealed class Problem : Problem<Row>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        /// <param name="dimensionCount">Indicating dimensions of an element.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Problem(int dimensionCount = 2, int capacity = 1000)
            : base(dimensionCount, capacity)
        {
        }
    }
}
