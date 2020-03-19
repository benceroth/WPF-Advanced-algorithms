namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Halal.Problems.TravellingSalesman;
    using MathNet.Numerics.Distributions;

    public sealed class RandomOptimizationTS : Algorithm<Town, Town>
    {
        private readonly Random Random = new Random();

        private readonly Normal NormalDistRandom = new Normal(0, 1);

        public RandomOptimizationTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem.OrderBy(x => this.Random.NextDouble()));
        }

        public Solution Solution
        {
            get => (Solution)this.solutions[0];
            private set => this.solutions[0] = value;
        }

        public override string Name { get; protected set; } = "Random Optimization";

        public override void DoOneIteration()
        {
            var randoms = Enumerable.Range(0, Environment.ProcessorCount).AsParallel().Select(x => this.GetNextSolution().Result).ToList();
            var q = randoms.OrderBy(x => x.CalculateFitness()).First();
            if (this.Solution.CalculateFitness() > q.CalculateFitness())
            {
                this.Solution = q;
            }
        }

        private async Task<Solution> GetNextSolution()
        {
            var added = new HashSet<Town>();
            var solution = new Solution();
            foreach (Town town in this.Solution.OrderBy(x => this.Random.NextDouble()))
            {
                var remainder = this.Solution.Count - solution.Count;
                remainder = remainder <= 0 ? 1 : remainder;
                int random = (int)Math.Abs(this.NormalDistRandom.Sample()) % remainder;

                if (!added.Contains(town))
                {
                    added.Add(town);
                    solution.Add(town);
                }

                var next = this.Solution
                    .Except(solution)
                    .OrderBy(x => this.Solution.CalculateFitnessBetween(town, x))
                    .Skip(random)
                    .FirstOrDefault();

                if (next != null)
                {
                    added.Add(next);
                    solution.Add(next);
                }
            }

            return solution;
        }
    }
}
