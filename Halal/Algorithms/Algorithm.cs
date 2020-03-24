namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Halal.Problems;
    using MathNet.Numerics.Distributions;

    /// <summary>
    /// Defines an algorithm.
    /// </summary>
    /// <typeparam name="TProblemElement">Problem type to be solved.</typeparam>
    /// <typeparam name="TSolutionElement">Solution type for the problem.</typeparam>
    public abstract class Algorithm<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        /// <summary>
        /// List of solutions.
        /// </summary>
        protected readonly List<Solution<TSolutionElement>> solutions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm{TProblemElement, TSolutionElement}"/> class.
        /// </summary>
        /// <param name="problem">Problem to be solved.</param>
        public Algorithm(Problem<TProblemElement> problem)
        {
            this.Problem = problem;
            this.solutions = new List<Solution<TSolutionElement>>();
        }

        /// <summary>
        /// Gets or sets name of the algorithm.
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// Gets problem of the algorithm.
        /// </summary>
        public Problem<TProblemElement> Problem { get; private set; }

        /// <summary>
        /// Gets or sets best solution.
        /// </summary>
        public Solution<TSolutionElement> Solution
        {
            get => this.solutions[0];
            protected set => this.solutions[0] = value;
        }

        /// <summary>
        /// Gets a list of solutions.
        /// </summary>
        public IReadOnlyList<Solution<TSolutionElement>> Solutions => this.solutions;

        /// <summary>
        /// Gets random generator.
        /// </summary>
        protected Random Random { get; private set; } = new Random();

        /// <summary>
        /// Gets normal distributed random generator.
        /// </summary>
        protected Normal NormalDistRandom { get; private set; } = new Normal(0, 1);

        /// <summary>
        /// Calculates one iteration.
        /// </summary>
        public abstract void DoOneIteration();
    }
}