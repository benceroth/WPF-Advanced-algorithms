namespace Halal.Problems.FunctionApproximation
{
    using System.Collections.Generic;

    public class Coefficient : List<double>, ISolutionElement
    {
        public Coefficient()
        {
        }

        public Coefficient(IEnumerable<double> items)
            : base(items)
        {
        }

        public bool IsValid(int dimensionCount)
        {
            return this.Count == 1;
        }
    }
}
