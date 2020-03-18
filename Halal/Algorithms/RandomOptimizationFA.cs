namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.FunctionApproximation;
    using MathNet.Numerics.Distributions;

    public sealed class RandomOptimizationFA : Algorithm<Value, Coefficient>
    {
        private readonly Random Random = new Random();

        private readonly Normal NormalDistRandom = new Normal(0, 1);

        public RandomOptimizationFA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
        }

        public Solution Solution
        {
            get => (Solution)this.solutions[0];
            private set => this.solutions[0] = value;
        }

        public override string Name { get; protected set; } = "Random Optimization";

        public override void DoOneIteration()
        {
            var q = this.GetNextSolution();
            if (this.Solution.CalculateFitness() > q.CalculateFitness())
            {
                this.Solution = q;
            }
        }

        private double GetRandomDouble(double limit = 10) => (this.Random.NextDouble() - 0.5) * limit;

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.GetRandomDouble() });

        private Coefficient GetNextCoefficient(Coefficient coefficient) => new Coefficient(new[] { coefficient.Value + this.NormalDistRandom.Sample() });

        private Solution GetNextSolution()
        {
            var solution = new Solution(this.Solution.problem);
            foreach (var coefficient in this.Solution)
            {
                solution.Add(this.GetNextCoefficient(coefficient));
            }

            return solution;
        }
    }
}
