namespace Halal.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems;

    public abstract class Algorithm<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        protected readonly Problem<TProblemElement> problem;
        protected readonly List<Solution<TSolutionElement>> solutions;

        public Algorithm(Problem<TProblemElement> problem)
        {
            this.problem = problem;
            this.solutions = new List<Solution<TSolutionElement>>();
        }

        public Problem<TProblemElement> Problem => this.problem;

        public IReadOnlyList<Solution<TSolutionElement>> Solutions => this.solutions;

        public abstract string Name { get; protected set; }

        public abstract void DoOneIteration();

        public IOrderedEnumerable<Solution<TSolutionElement>> GetOrderedSolutions() => this.solutions.OrderBy(x => x.CalculateFitness());
    }
}