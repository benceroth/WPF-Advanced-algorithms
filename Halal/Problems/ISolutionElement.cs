namespace Halal.Problems
{
    using System.Collections;

    /// <summary>
    /// Defines a solution element.
    /// </summary>
    public interface ISolutionElement : IEnumerable
    {
        /// <summary>
        /// Determines if solution element is valid.
        /// </summary>
        /// <param name="dimensionCount">Indicating dimensions of an element.</param>
        /// <returns>Value indicating whether solution element is valid.</returns>
        bool IsValid(int dimensionCount);
    }
}
