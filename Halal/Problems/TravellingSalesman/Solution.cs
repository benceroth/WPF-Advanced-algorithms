namespace Halal.Problems.TravellingSalesman
{
    using System;
    using System.Linq;

    /// <inheritdoc/>
    public sealed class Solution : Solution<Town>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        /// <param name="dimensionCount">Indicates dimensions of an element.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Solution(int dimensionCount = 2, int capacity = 1000)
            : base(dimensionCount, capacity)
        {
        }

        /// <inheritdoc/>
        public override double CalculateFitness()
        {
            double fitness = 0;
            if (this.Count > 1)
            {
                Town previous = this.First();
                foreach (Town town in this.Skip(1))
                {
                    fitness += this.CalculateFitnessBetween(previous, town);
                    previous = town;
                }
            }

            return fitness;
        }

        /// <summary>
        /// Calculates fitness between two towns.
        /// </summary>
        /// <param name="first">First town.</param>
        /// <param name="second">Second town.</param>
        /// <returns>Fitness.</returns>
        internal double CalculateFitnessBetween(Town first, Town second)
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
