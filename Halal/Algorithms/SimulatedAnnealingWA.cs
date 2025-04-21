namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.WorkAssignment;

    /// <inheritdoc/>
    public sealed class SimulatedAnnealingWA : Algorithm<Person, Rate>
    {
        private const double K = 1.0 / 100000;
        private const double Epsilon = 0.001;
        private const double TEpsilon = 0.001;

        private Solution temporary;
        private double temperature = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatedAnnealingWA"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public SimulatedAnnealingWA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(this.GetRandomRates());
            this.temporary = this.GetNextSolution(null, null);
        }

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Simulated Annealing";

        private new Problem Problem => base.Problem as Problem;

        /// <inheritdoc/>
        public override void DoOneIteration()
        {
            foreach (Rate rate in this.temporary)
            {
                var next = this.GetNextSolution(rate);
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
                    break;
                }
                else if(diff > 1) // Beragadás miatt.
                {
                    this.temperature = this.GetTemperature();
                    if (this.Random.NextDouble() < this.GetProbability(diff))
                    {
                        this.temporary = next;
                        break;
                    }
                }
            }
        }

        private Solution GetNextSolution(Rate rate)
        {
            var newRate = this.GetNextRate(rate);
            double diff = rate.Value - newRate.Value + this.Problem.RequestedTime - this.Solution.Sum(x => x.Value);
            double sum = this.Problem.RequestedTime - rate.Value;
            var solution = new Solution(this.Problem);
            solution.AddRange(this.Solution);
            solution.Replace(rate, newRate);

            for (int i = 0; i < this.Problem.Count; i++)
            {
                var element = solution.ElementAt(i);
                if (element != newRate)
                {
                    var value = element.Value + (diff * element.Value / sum);
                    solution.Replace(element, new Rate(new[] { value }));
                }
            }

            return solution;
        }

        private Solution GetNextSolution(Rate oldRate, Rate newRate)
        {
            var solution = new Solution(this.Problem);
            solution.AddRange(this.Solution);
            solution.Replace(oldRate, newRate);
            return solution;
        }

        private IEnumerable<Rate> GetRandomRates()
        {
            double sum = this.Problem.RequestedTime;
            for (int i = 0; i < this.Problem.Count; i++)
            {
                if (i + 1 == this.Problem.Count)
                {
                    yield return new Rate(new[] { sum });
                }
                else
                {
                    double value = this.Random.NextDouble() * sum / 2;
                    sum -= value;
                    yield return new Rate(new[] { value });
                }
            }
        }

        private double GetProbability(double diff) => Math.Pow(Math.E, - diff / (K * this.temperature));

        private double GetTemperature() => this.temperature * (1 - TEpsilon);

        private double GetNextValue(Rate rate) => this.Random.NextDouble() >= 0.5 ? 
            (rate.Value + Epsilon <= this.Problem.RequestedTime ? rate.Value + Epsilon : this.Problem.RequestedTime) : 
            ((rate.Value - Epsilon) > 0 ? rate.Value - Epsilon : 0);

        private Rate GetNextRate(Rate rate) => new Rate(new[] { this.GetNextValue(rate) });
    }
}
