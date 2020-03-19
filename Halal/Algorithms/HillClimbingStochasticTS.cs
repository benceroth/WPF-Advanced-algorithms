namespace Halal.Algorithms
{
    using System;
    using System.Linq;
    using Halal.Problems.TravellingSalesman;

    public sealed class HillClimbingStochasticTS : Algorithm<Town, Town>
    {
        private const int Epsilon = 10;

        public HillClimbingStochasticTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem.OrderBy(x => this.Random.NextDouble()));
        }

        public override string Name { get; protected set; } = "Hill Climbing Stochastic";

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
