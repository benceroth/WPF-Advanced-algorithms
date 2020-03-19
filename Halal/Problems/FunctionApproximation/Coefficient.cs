namespace Halal.Problems.FunctionApproximation
{
    using System.Collections.Generic;

    public sealed class Coefficient : List<double>, ISolutionElement
    {
        public Coefficient()
        {
        }

        public Coefficient(IEnumerable<double> items)
            : base(items)
        {
        }

        public double Value
        {
            get => this[0];
            set => this[0] = value;
        }

        public bool IsValid(int dimensionCount)
        {
            return this.Count == 1;
        }
    }
}
