namespace Halal.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.WorkAssignment;

    /// <inheritdoc/>
    public sealed class HillClimbingStochasticWA : Algorithm<Person, Rate>
    {
        private const double Epsilon = 0.001;

        /// <summary>
        /// Initializes a new instance of the <see cref="HillClimbingStochasticWA"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public HillClimbingStochasticWA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(this.GetRandomRates());
        }

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Hill Climbing Stochastic";

        private new Problem Problem => base.Problem as Problem;

        /// <inheritdoc/>
        public override void DoOneIteration()
        {
            foreach (Rate rate in this.Solution)
            {
                var next = this.GetNextSolution(rate);
                if (this.Solution.CalculateFitness() > next.CalculateFitness())
                {
                    this.Solution = next;
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

        private double GetNextValue(Rate rate) => this.Random.NextDouble() >= 0.5 ? rate.Value + Epsilon : ((rate.Value - Epsilon) > 0 ? rate.Value - Epsilon : 0);

        private Rate GetNextRate(Rate rate) => new Rate(new[] { this.GetNextValue(rate) });
    }
}
