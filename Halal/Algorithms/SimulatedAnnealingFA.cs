namespace Halal.Algorithms
{
    using System;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    /// <inheritdoc/>
    public sealed class SimulatedAnnealingFA : Algorithm<Value, Coefficient>
    {
        private const double K = 1;
        private const double Alfa = 2;
        private const double Epsilon = 0.0001;
        private const double MaxTemperature = 10000;

        private Solution temporary;
        private double temperature = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatedAnnealingFA"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public SimulatedAnnealingFA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
            this.temporary = this.GetNextSolution(null, null);
        }

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Simulated Annealing";

        /// <inheritdoc/>
        public override void DoOneIteration()
        {
            foreach (Coefficient coefficient in this.temporary)
            {
                var next = this.GetNextSolution(coefficient, this.GetNextCoefficient(coefficient));
                double tempFitness = next.CalculateFitness();
                double nextFitness = this.temporary.CalculateFitness();
                var diff = tempFitness - nextFitness;
                if (diff < 0)
                {
                    this.temporary = next;
                    if (tempFitness < this.Solution.CalculateFitness())
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

        private Solution GetNextSolution(Coefficient oldCoefficient, Coefficient newCoefficient)
        {
            var solution = new Solution(this.Problem);
            solution.AddRange(this.Solution);
            solution.Replace(oldCoefficient, newCoefficient);
            return solution;
        }

        private double GetProbability(double diff) => Math.Pow(Math.E, -Math.Abs(diff) / (K * this.temperature));

        private double GetTemperature() => MaxTemperature * Math.Pow(1 - (this.temperature / MaxTemperature), Alfa);

        private double GetNextValue(Coefficient coefficient) => this.Random.NextDouble() >= 0 ? coefficient.Value + Epsilon : coefficient.Value - Epsilon;

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.NormalDistRandom.Sample() });

        private Coefficient GetNextCoefficient(Coefficient coefficient, int multiplier = 1) => new Coefficient(new[] { this.GetNextValue(coefficient) * multiplier });
    }
}
