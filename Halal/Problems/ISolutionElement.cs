namespace Halal.Problems
{
    using System.Collections;

    public interface IProblemElement : IEnumerable
    {
        bool IsValid(int dimensionCount);
    }
}
