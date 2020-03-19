namespace Halal.Problems.WorkAssignment
{
    using System;
    using System.Linq;

    public sealed class Solution : Solution<Rate>
    {
        private readonly Problem problem;

        public Solution(Problem problem, int capacity = 1000)
            : base(1, capacity)
        {
            this.problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        public override double CalculateFitness()
        {
            return this.CalculateSalary() / this.CalculateQuality();
        }

        public double CalculateSalary()
        {
            double salary = 0;
            for (int i = 0; i < this.problem.Count; i++)
            {
                salary += this.problem.ElementAt(i).Salary * this.ElementAt(i).Value;
            }

            return salary;
        }

        public double CalculateQuality()
        {
            double quality = 0;
            for (int i = 0; i < this.problem.Count; i++)
            {
                quality += this.problem.ElementAt(i).Quality * this.ElementAt(i).Value;
            }

            return quality / this.problem.RequestedTime;
        }
    }
}
