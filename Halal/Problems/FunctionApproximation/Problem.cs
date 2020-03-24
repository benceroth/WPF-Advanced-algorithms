namespace Halal.Problems.FunctionApproximation
{
    /// <inheritdoc/>
    public sealed class Problem : Problem<Value>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        /// <param name="capacity">Initial capacity.</param>
        public Problem(int capacity = 1000)
            : base(1, capacity)
        {
        }
    }
}