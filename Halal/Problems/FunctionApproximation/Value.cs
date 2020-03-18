namespace Halal.Problems.FunctionApproximation
{
    using System.Collections.Generic;

    public sealed class Value : List<double>, IProblemElement
    {
        public Value()
        {
        }

        public Value(IEnumerable<double> items)
            : base(items)
        {
        }

        public bool IsValid(int dimensionCount)
        {
            return this.Count == dimensionCount + 1;
        }
    }
}