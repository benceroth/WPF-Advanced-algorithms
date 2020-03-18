namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.TravellingSalesman;

    public class SimulatedAnnealingTS : Algorithm<Town, Town>
    {
        private readonly Random Random = new Random();
        private Solution p;
        private double t = 1;

        public SimulatedAnnealingTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem);
            this.p = this.GetNextSolution(null, null);
        }

        public Solution Solution
        {
            get => (Solution)this.solutions[0];
            private set => this.solutions[0] = value;
        }

        public override string Name { get; protected set; } = "Simulated Annealing";

        public override void DoOneIteration()
        {
            foreach (Town town in this.p)
            {
                var q = this.GetNextSolution(town, this.GetNextTown(town));
                double qFitness = q.CalculateFitness();
                double pFitness = this.p.CalculateFitness();
                var diff = qFitness - pFitness;

                if (diff < 0)
                {
                    this.p = q;
                    if (qFitness < this.Solution.CalculateFitness())
                    {
                        this.Solution = q;
                    }
                }
                else
                {
                    this.t = this.GetTemperature();
                    if (this.Random.NextDouble() < this.GetProbability(diff))
                    {
                        this.p = q;
                    }
                }
            }
        }

        private double GetProbability(double diff, double k = 1) => Math.Pow(Math.E, -Math.Abs(diff) / (k * this.t));

        private double GetTemperature(double maxTemp = 10000, double alfa = 2) => maxTemp * Math.Pow(1 - (this.t / maxTemp), alfa);

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
