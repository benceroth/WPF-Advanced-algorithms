namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Halal.Problems;
    using MathNet.Numerics.Distributions;

    public abstract class Algorithm<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        protected readonly List<Solution<TSolutionElement>> solutions;

        public Algorithm(Problem<TProblemElement> problem)
        {
            this.Problem = problem;
            this.solutions = new List<Solution<TSolutionElement>>();
        }

        public abstract string Name { get; protected set; }

        public Problem<TProblemElement> Problem { get; private set; }

        public Solution<TSolutionElement> Solution
        {
            get => this.solutions[0];
            protected set => this.solutions[0] = value;
        }

        public IReadOnlyList<Solution<TSolutionElement>> Solutions => this.solutions;

        protected Random Random { get; private set; } = new Random();

        protected Normal NormalDistRandom { get; private set; } = new Normal(0, 1);

        public abstract void DoOneIteration();
    }
}