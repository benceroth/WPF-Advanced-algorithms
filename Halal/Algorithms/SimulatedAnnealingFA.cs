namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.FunctionApproximation;

    public class SimulatedAnnealingFA : Algorithm<Value, Coefficient>
    {
        private readonly Random Random = new Random();
        private Solution p;
        private double t = 1;

        public SimulatedAnnealingFA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
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
            foreach (Coefficient coefficient in this.p)
            {
                var q = this.GetNextSolution(coefficient, this.GetNextCoefficient(coefficient));
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

        private double GetProbability(double diff, double k = 1) => Math.Pow(Math.E, diff / (k * this.t));

        private double GetTemperature(double maxTemp = int.MaxValue, double alfa = 2) => maxTemp * Math.Pow(1 - (this.t / maxTemp), alfa);

        private double GetRandomDouble(double limit = 10) => (this.Random.NextDouble() - 0.5) * limit;

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.GetRandomDouble() });

        private double GetNextValue(Coefficient coefficient, double epsilon = 0.0001) => this.GetRandomDouble() >= 0 ? coefficient.First() + epsilon : coefficient.First() - epsilon;

        private Coefficient GetNextCoefficient(Coefficient coefficient, int multiplier = 1) => new Coefficient(new[] { this.GetNextValue(coefficient) * multiplier });

        private Solution GetNextSolution(Coefficient oldCoefficient, Coefficient newCoefficient)
        {
            var solution = new Solution(this.Solution.problem);
            solution.AddRange(this.Solution);
            solution.Replace(oldCoefficient, newCoefficient);
            return solution;
        }
    }
}
