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

        public TElement this[int i]
        {
            get { return this.elements[i]; }
            set { this.elements[i] = value; }
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

        public void Swap(int oldIndex, int newIndex)
        {
            if (oldIndex != newIndex && oldIndex >= 0 && newIndex < this.Count)
            {
                TElement oldElement = this[oldIndex];
                TElement newElement = this[newIndex];
                if (newIndex > oldIndex)
                {
                    this.Remove(newElement);
                    this.Insert(newIndex, oldElement);
                    this.Remove(oldElement);
                    this.Insert(oldIndex, newElement);
                }
                else
                {
                    this.Remove(oldElement);
                    this.Insert(oldIndex, newElement);
                    this.Remove(newElement);
                    this.Insert(newIndex, oldElement);
                }
            }
        }

        public void Insert(int index, TElement element)
        {
            this.elements.Insert(index, element);
        }

        public void InsertRange(int index, IEnumerable<TElement> elements)
        {
            this.elements.InsertRange(index, elements);
        }

        public int IndexOf(TElement element)
        {
            return this.elements.IndexOf(element);
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
