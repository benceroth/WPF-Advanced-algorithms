namespace Halal.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    public sealed class GeneticAlgorithmFA : Algorithm<Value, Coefficient>
    {
        private const int PopulationCount = 50;
        private const double MutationPropability = 0.4;

        public GeneticAlgorithmFA(Problem problem)
            : base(problem)
        {
            for (int i = 0; i < PopulationCount; i++)
            {
                var solution = new Solution(problem);
                solution.AddRange(Enumerable.Range(0, 5).Select(x => this.GetRandomCoefficient()));
                this.solutions.Add(solution);
            }
        }

        public override string Name { get; protected set; } = "Genetic Algorithm";

        public override void DoOneIteration()
        {
            var population = new List<Solution>();
            var parents = this.solutions.Select(x => x as Solution).OrderBy(x => x.CalculateFitness()).ToList();
            while (population.Count != PopulationCount)
            {
                var selectedParents = this.SelectMatings(parents).ToList();
                var firstParent = selectedParents.First();
                var secondParent = selectedParents.Last();
                population.Add(this.Mutate(this.CrossOver(firstParent, secondParent)));
            }

            this.Solution = population.OrderBy(x => x.CalculateFitness()).First();
            this.solutions.RemoveRange(1, PopulationCount - 1);
            this.solutions.AddRange(population.Except(this.solutions));
        }

        private IEnumerable<Solution> SelectMatings(List<Solution> parents)
        {
            int n = parents.Count;
            int sum = n * (n + 1) * ((2 * n) + 1) / 6;

            for (int i = 0; i < 2; i++)
            {
                int pSum = 0;
                int fitness = parents.Count;
                double random = this.Random.NextDouble() * sum;
                yield return parents.SkipWhile(solution =>
                {
                    pSum += fitness * fitness--;
                    return pSum < random;
                }).First();
            }
        }

        private Solution CrossOver(Solution first, Solution second)
        {
            var solution = new Solution(this.Problem);
            for (int i = 0; i < first.Count; i++)
            {
                var rate = this.Random.NextDouble();
                solution.Add(new Coefficient() { (first[i].Value * rate) + ((1 - rate) * second[i].Value) });
            }

            return solution;
        }

        private Solution Mutate(Solution solution)
        {
            if (this.Random.NextDouble() < MutationPropability)
            {
                foreach (var coefficient in solution)
                {
                    if (this.Random.NextDouble() > 0.5)
                    {
                        coefficient.Value += this.NormalDistRandom.Sample();
                    }
                }
            }

            return solution;
        }

        private Coefficient GetRandomCoefficient() => new Coefficient(new[] { this.NormalDistRandom.Sample() });
    }
}