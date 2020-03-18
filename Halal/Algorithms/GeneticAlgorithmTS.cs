namespace Halal.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.TravellingSalesman;

    public sealed class GeneticAlgorithmTS : Algorithm<Town, Town>
    {
        private readonly Random Random = new Random();

        public GeneticAlgorithmTS(Problem problem)
            : base(problem)
        {
            this.solutions.Add(null);
            this.Solution = new Solution();
            this.Solution.AddRange(problem.OrderBy(x => this.Random.NextDouble()));
        }

        public Solution Solution
        {
            get => (Solution)this.solutions[0];
            private set => this.solutions[0] = value;
        }

        public override string Name { get; protected set; } = "Genetic Algorithm";

        public override void DoOneIteration()
        {
            var solution = new Solution();
            this.GetChilds(solution, this.SelectParents());

            if (this.Solution.CalculateFitness() > solution.CalculateFitness())
            {
                this.Solution = solution;
            }
        }

        private List<List<Town>> SelectParents()
        {
            var previous = this.Solution.First();
            var elits = new Dictionary<List<Town>, double>();
            for (int i = 1; i < this.Solution.Count; i++)
            {
                var actual = this.Solution[i];
                elits.Add(new List<Town> { previous, actual }, this.Solution.CalculateFitnessBetween(previous, actual));
                previous = actual;
            }

            return elits.OrderBy(x => x.Value).Select(x => x.Key).ToList();
        }

        private void GetChilds(Solution solution, List<List<Town>> parents)
        {
            var added = new List<Town>();
            float sum = (1 + parents.Count) / 2 * parents.Count;
            for (int i = parents.Count - 1; i > 0; i--)
            {
                int n = parents.Count - i;
                if (n / sum > this.Random.NextDouble() * parents.Count / sum * 0.7)
                {
                    int index = this.Random.Next(0, solution.Count);
                    solution.InsertRange(index, parents[i].Except(added));
                    added.AddRange(parents[i]);
                }
                else
                {
                    foreach (var parent in parents[i].Except(added))
                    {
                        int index = this.Random.Next(0, solution.Count);
                        solution.Insert(index, parent);
                    }

                    added.AddRange(parents[i]);
                }
            }
        }
    }
}