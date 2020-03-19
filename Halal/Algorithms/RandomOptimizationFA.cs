namespace Halal.Algorithms
{
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    public sealed class RandomOptimizationFA : Algorithm<Value, Coefficient>
    {
        public RandomOptimizationFA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
        }

        public override string Name { get; protected set; } = "Random Optimization";

        public override void DoOneIteration()
        {
            var next = this.GetNextSolution();
            if (this.Solution.CalculateFitness() > next.CalculateFitness())
            {
                this.Solution = next;
            }
        }

        private Solution GetNextSolution()
        {
            var solution = new Solution(this.Problem);
            foreach (var coefficient in this.Solution)
            {
                solution.Add(this.GetNextCoefficient(coefficient));
            }

            return solution;
        }

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.NormalDistRandom.Sample() });

        private Coefficient GetNextCoefficient(Coefficient coefficient) => new Coefficient(new[] { coefficient.Value + this.NormalDistRandom.Sample() });
    }
}
