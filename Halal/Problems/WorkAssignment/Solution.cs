namespace Halal.Problems.WorkAssignment
{
    using System;
    using System.Linq;

    /// <inheritdoc/>
    public sealed class Solution : Solution<Rate>
    {
        private readonly Problem problem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Solution(Problem problem, int capacity = 1000)
            : base(1, capacity)
        {
            this.problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        /// <inheritdoc/>
        public override double CalculateFitness()
        {
            return this.CalculateSalary() / this.CalculateQuality();
        }

        /// <summary>
        /// Calculates salary.
        /// </summary>
        /// <returns>Salary.</returns>
        public double CalculateSalary()
        {
            double salary = 0;
            for (int i = 0; i < this.problem.Count; i++)
            {
                salary += this.problem.ElementAt(i).Salary * this.ElementAt(i).Value;
            }

            return salary;
        }

        /// <summary>
        /// Calculates quality.
        /// </summary>
        /// <returns>Quality.</returns>
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
