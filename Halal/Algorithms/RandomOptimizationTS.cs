namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.TravellingSalesman;

    /// <inheritdoc/>
    public sealed class RandomOptimizationTS : Algorithm<Town, Town>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomOptimizationTS"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public RandomOptimizationTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem.OrderBy(x => this.Random.NextDouble()));
        }

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Random Optimization";

        /// <inheritdoc/>
        public override void DoOneIteration()
        {
            var next = Enumerable.Range(0, Environment.ProcessorCount)
                .AsParallel()
                .Select(x => this.GetNextSolution())
                .OrderBy(x => x.CalculateFitness()).First();
            if (this.Solution.CalculateFitness() > next.CalculateFitness())
            {
                this.Solution = next;
            }
        }

        private Solution GetNextSolution()
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
                    .OrderBy(x => (this.Solution as Solution).CalculateFitnessBetween(town, x))
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
