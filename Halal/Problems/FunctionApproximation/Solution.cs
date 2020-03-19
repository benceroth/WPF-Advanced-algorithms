namespace Halal.Problems.FunctionApproximation
{
    using System;
    using System.Linq;

    public sealed class Solution : Solution<Coefficient>
    {
        private readonly Problem<Value> problem;

        public Solution(Problem<Value> problem, int capacity = 1000)
            : base(1, capacity)
        {
            this.problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        public override double CalculateFitness()
        {
            double fitness = 0;
            if (this.Count != 5)
            {
                throw new InvalidOperationException("Not matching coefficient count (5)");
            }
            else if (this.Any(x => !x.IsValid(this.dimensionCount)))
            {
                throw new InvalidOperationException("Not matching dimensions!");
            }
            else if (this.problem.Count > 0)
            {
                double[] coefficients = this.Select(x => x.Value).ToArray();
                foreach (Value value in this.problem)
                {
                    fitness += Math.Pow(this.CalculateY(value.First(), coefficients) - value.Last(), 2);
                }
            }

            return fitness;
        }

        private double CalculateY(double x, double[] coefficients) => (coefficients[0] * Math.Pow(x - coefficients[1], 3)) + (coefficients[2] * Math.Pow(x - coefficients[3], 2)) + coefficients[3];
    }
}