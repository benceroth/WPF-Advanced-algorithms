namespace Halal.Problems.PathFinding
{
    using System;
    using System.Linq;

    public sealed class Solution : Solution<Move>
    {
        public Solution(Move start, int dimensionCount = 2, int capacity = 1000)
            : base(dimensionCount, capacity)
        {
            this.Add(start ?? throw new ArgumentNullException(nameof(start)));
        }

        public override double CalculateFitness()
        {
            double fitness = 0;
            if (this.Count > 1)
            {
                Move previous = this.First();
                foreach (Move town in this.Skip(1))
                {
                    fitness += this.CalculateFitnessBetween(previous, town);
                    previous = town;
                }
            }

            return fitness;
        }

        private double CalculateFitnessBetween(Move first, Move second)
        {
            if (first.Count != second.Count)
            {
                throw new InvalidOperationException("Not matching dimensions!");
            }
            else
            {
                double fitness = 0;
                for (int i = 0; i < first.Count; i++)
                {
                    fitness += Math.Pow(first[i] - second[i], 2);
                }

                return Math.Sqrt(fitness);
            }
        }
    }
}