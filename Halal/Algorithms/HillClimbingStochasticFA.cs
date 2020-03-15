namespace Halal.Algorithms
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    public class HillClimbingStochasticFA : Algorithm<Value, Coefficient>
    {
        private const int Limit = 10;
        private const double Epsilon = 0.00001;
        private readonly Random Random = new Random();

        public HillClimbingStochasticFA(Problem problem)
            : base(problem)
        {
            var solution = new Solution(problem);
            solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
            this.solutions.Add(solution);
        }

        public Solution Solution => (Solution)this.solutions[0];

        public override string Name { get; protected set; } = "Hill Climbing Algorithm";

        public override void DoOneIteration()
        {
            foreach (Coefficient coefficient in this.Solution)
            {
                var next = this.GetNextCoefficient(coefficient);
                var solution = new Solution(this.Solution.problem);
                solution.AddRange(this.Solution);
                solution.Replace(coefficient, next);

                if (this.Solution.CalculateFitness() > solution.CalculateFitness())
                {
                    coefficient[0] = next[0];
                }
            }
        }

        public ConcurrentDictionary<Solution, double> dict = new ConcurrentDictionary<Solution, double>();

        public void DoOneIterationParallel(int i)
        {
            double fitness = this.Solution.CalculateFitness();
            foreach (Coefficient coefficient in this.Solution)
            {
                var solution = new Solution(this.Solution.problem);
                solution.AddRange(this.Solution);
                solution.Replace(coefficient, this.GetNextCoefficient(coefficient, i));
                double newFitness = solution.CalculateFitness();
                if (newFitness < fitness)
                {
                    this.dict.TryAdd(solution, newFitness);
                }
            }
        }

        private double GetRandomDouble() => (Random.NextDouble() - 0.5) * Limit;

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.GetRandomDouble() });

        private double GetNextValue(Coefficient coefficient) => this.GetRandomDouble() >= 0 ? coefficient.First() + Epsilon : coefficient.First() - Epsilon;

        private Coefficient GetNextCoefficient(Coefficient coefficient, int multiplier = 1) => new Coefficient(new[] { this.GetNextValue(coefficient) * multiplier });
    }
}
