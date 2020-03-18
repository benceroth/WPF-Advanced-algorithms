namespace Halal.Algorithms
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    public class HillClimbingStochasticFA : Algorithm<Value, Coefficient>
    {
        private readonly Random Random = new Random();

        public HillClimbingStochasticFA(Problem problem)
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

        public override string Name { get; protected set; } = "Hill Climbing Stochastic";

        public override void DoOneIteration()
        {
            foreach (Coefficient coefficient in this.Solution)
            {
                var q = this.GetNextSolution(coefficient, this.GetNextCoefficient(coefficient));

                if (this.Solution.CalculateFitness() > q.CalculateFitness())
                {
                    this.Solution = q;
                }
            }
        }

        private double GetRandomDouble(double limit = 10) => (this.Random.NextDouble() - 0.5) * limit;

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.GetRandomDouble() });

        private double GetNextValue(Coefficient coefficient, double epsilon = 0.0001) => this.GetRandomDouble() >= 0 ? coefficient.Value + epsilon : coefficient.Value - epsilon;

        private Coefficient GetNextCoefficient(Coefficient coefficient, int multiplier = 1) => new Coefficient(new[] { this.GetNextValue(coefficient) * multiplier });

        private Solution GetNextSolution(Coefficient oldCoefficient, Coefficient newCoefficient)
        {
            var solution = new Solution(this.Solution.problem);
            solution.AddRange(this.Solution);
            solution.Replace(oldCoefficient, newCoefficient);
            return solution;
        }
    }
}
