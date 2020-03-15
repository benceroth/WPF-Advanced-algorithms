namespace Halal.Problems.TravellingSalesman
{
    using System.Collections.Generic;

    public class Town : List<double>, ISolutionElement, IProblemElement
    {
        public Town()
        {

        }

        public Town(IEnumerable<double> items)
            : base(items)
        {
        }

        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount;
        }
    }
}
