namespace Halal.Problems
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Problem<TElement> : IEnumerable<TElement>
        where TElement : IProblemElement
    {
        private readonly int dimensionCount;
        private readonly List<TElement> elements;

        public Problem(int dimensionCount = 2, int capacity = 1000)
        {
            this.dimensionCount = dimensionCount;
            this.elements = new List<TElement>(capacity);
        }

        public int Count => this.elements.Count;

        public void Add(TElement element)
        {
            if (element?.IsValid(this.dimensionCount) == true)
            {
                this.elements.Add(element);
            }
        }

        public void AddRange(IEnumerable<TElement> elements)
        {
            foreach (TElement element in elements)
            {
                this.Add(element);
            }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
    }
}