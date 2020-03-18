namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.TravellingSalesman;

    public class HillClimbingStochasticTS : Algorithm<Town, Town>
    {
        private readonly Random Random = new Random();

        public HillClimbingStochasticTS(Problem problem)
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

        public override string Name { get; protected set; } = "Hill Climbing Stochastic";

        public override void DoOneIteration()
        {
            foreach (Town town in this.Solution)
            {
                var q = this.GetNextSolution(town, this.GetNextTown(town));

                if (this.Solution.CalculateFitness() > q.CalculateFitness())
                {
                    this.Solution = q;
                }
            }
        }

        private Town GetNextTown(Town town, int epsilon = 10)
        {
            if (this.Solution.Count > 1)
            {
                return this.Solution
                    .OrderBy(x => Math.Pow(x.First() - town.First(), 2) + Math.Pow(x.Last() - town.Last(), 2))
                    .Skip(1)
                    .ElementAt(this.Random.Next(0, epsilon > this.Solution.Count ? this.Solution.Count : epsilon));
            }
            else
            {
                return town;
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
    }
}
