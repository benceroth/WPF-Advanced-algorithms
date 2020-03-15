namespace Halal.Problems
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Solution<TElement> : IEnumerable<TElement>
        where TElement : ISolutionElement
    {
        protected readonly int dimensionCount;
        protected readonly List<TElement> elements;

        public Solution(int dimensionCount = 2, int capacity = 1000)
        {
            this.dimensionCount = dimensionCount;
            this.elements = new List<TElement>(capacity);
        }

        public int Count => this.elements.Count;

        public abstract double CalculateFitness();

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

        public void Remove(TElement element)
        {
            this.elements.Remove(element);
        }

        public void RemoveRange(IEnumerable<TElement> elements)
        {
            foreach (TElement element in elements)
            {
                this.Remove(element);
            }
        }

        public void Replace(TElement oldItem, TElement newItem)
        {
            int index = this.elements.IndexOf(oldItem);
            if (index > -1)
            {
                this.Remove(oldItem);
                this.elements.Insert(index, newItem);
            }
        }

        public void Clear()
        {
            this.elements.Clear();
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
