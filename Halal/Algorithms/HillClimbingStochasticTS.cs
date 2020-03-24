namespace Halal.Algorithms
{
    using System;
    using System.Linq;
    using Halal.Problems.TravellingSalesman;

    /// <inheritdoc/>
    public sealed class HillClimbingStochasticTS : Algorithm<Town, Town>
    {
        private const int Epsilon = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="HillClimbingStochasticTS"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public HillClimbingStochasticTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem.OrderBy(x => this.Random.NextDouble()));
        }

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Hill Climbing Stochastic";

        /// <inheritdoc/>
        public override void DoOneIteration()
        {
            foreach (Town town in this.Solution)
            {
                var next = this.GetNextSolution(town, this.GetNextTown(town));
                if (this.Solution.CalculateFitness() > next.CalculateFitness())
                {
                    this.Solution = next;
                }
            }
        }

        private Solution GetNextSolution(Town oldTown, Town newTown)
        {
            var solution = new Solution();
            solution.AddRange(this.Solution);
            solution.Replace(newTown, null);
            solution.Replace(oldTown, newTown);
            solution.Replace(null, oldTown);
            return solution;
        }

        private Town GetNextTown(Town town)
        {
            if (this.Solution.Count > 1)
            {
                return this.Solution
                    .OrderBy(x => Math.Pow(x.First() - town.First(), 2) + Math.Pow(x.Last() - town.Last(), 2))
                    .ElementAt(this.Random.Next(1, this.Solution.Count < Epsilon ? this.Solution.Count : Epsilon));
            }
            else
            {
                return town;
            }
        }
    }
}
