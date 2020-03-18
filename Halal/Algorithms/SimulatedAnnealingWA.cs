namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.WorkAssignment;

    public class SimulatedAnnealingWA : Algorithm<Person, Rate>
    {
        private readonly Random Random = new Random();
        private Solution p;
        private double t = 1;

        public SimulatedAnnealingWA(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution(problem);
            this.Solution.AddRange(this.GetRandomRates());
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
            foreach (Rate rate in this.p)
            {
                var q = this.GetNextSolution(rate);
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
                else if(diff > 5)
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

        private double GetNextValue(Rate rate, double epsilon = 0.0001) => this.Random.NextDouble() >= 0.5 ? rate.Value + epsilon : ((rate.Value - epsilon) > 0 ? rate.Value - epsilon : 0);

        private Rate GetNextRate(Rate rate) => new Rate(new[] { this.GetNextValue(rate) });

        private Solution GetNextSolution(Rate oldRate, Rate newRate)
        {
            var solution = new Solution(this.Solution.problem);
            solution.AddRange(this.Solution);
            solution.Replace(oldRate, newRate);
            return solution;
        }

        private Solution GetNextSolution(Rate rate)
        {
            var newRate = this.GetNextRate(rate);
            double diff = rate.Value - newRate.Value + this.Solution.problem.RequestedTime - this.Solution.Sum(x => x.Value);
            double sum = this.Solution.problem.RequestedTime - rate.Value;
            var solution = new Solution(this.Solution.problem);
            solution.AddRange(this.Solution);
            solution.Replace(rate, newRate);

            for (int i = 0; i < this.Solution.problem.Count; i++)
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
            double sum = this.Solution.problem.RequestedTime;
            for (int i = 0; i < this.Solution.problem.Count; i++)
            {
                if (i + 1 == this.Solution.problem.Count)
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
    }
}
