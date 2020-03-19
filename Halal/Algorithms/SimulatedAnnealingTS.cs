namespace Halal.Algorithms
{
    using System;
    using System.Linq;
    using Halal.Problems.TravellingSalesman;

    public sealed class SimulatedAnnealingTS : Algorithm<Town, Town>
    {
        private const double K = 1;
        private const double Alfa = 2;
        private const int Epsilon = 10;
        private const double MaxTemperature = 10000;

        private Solution temporary;
        private double temperature = 1;

        public SimulatedAnnealingTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem);
            this.temporary = this.GetNextSolution(null, null);
        }

        public override string Name { get; protected set; } = "Simulated Annealing";

        public override void DoOneIteration()
        {
            foreach (Town town in this.temporary)
            {
                var next = this.GetNextSolution(town, this.GetNextTown(town));
                double nextFitness = next.CalculateFitness();
                double tempFitness = this.temporary.CalculateFitness();
                var diff = nextFitness - tempFitness;
                if (diff < 0)
                {
                    this.temporary = next;
                    if (nextFitness < this.Solution.CalculateFitness())
                    {
                        this.Solution = next;
                    }
                }
                else
                {
                    this.temperature = this.GetTemperature();
                    if (this.Random.NextDouble() < this.GetProbability(diff))
                    {
                        this.temporary = next;
                    }
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
                    .Skip(1)
                    .ElementAt(this.Random.Next(0, this.Solution.Count < Epsilon ? this.Solution.Count : Epsilon));
            }
            else
            {
                return town;
            }
        }

        private double GetProbability(double diff) => Math.Pow(Math.E, -Math.Abs(diff) / (K * this.temperature));

        private double GetTemperature() => MaxTemperature * Math.Pow(1 - (this.temperature / MaxTemperature), Alfa);
    }
}
