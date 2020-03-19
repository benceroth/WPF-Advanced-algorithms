namespace Halal.Problems
{
    using System.Collections;

    public interface ISolutionElement : IEnumerable
    {
        bool IsValid(int dimensionCount);
    }
}
