namespace Halal.Problems
{
    using System.Collections;

    /// <summary>
    /// Defines a problem element.
    /// </summary>
    public interface IProblemElement : IEnumerable
    {
        /// <summary>
        /// Determines if problem element is valid.
        /// </summary>
        /// <param name="dimensionCount">Indicating dimensions of an element.</param>
        /// <returns>Value indicating whether problem element is valid.</returns>
        bool IsValid(int dimensionCount);
    }
}
