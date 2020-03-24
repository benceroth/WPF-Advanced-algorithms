namespace Halal.Problems
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a solution.
    /// </summary>
    /// <typeparam name="TElement">Solution element type.</typeparam>
    public abstract class Solution<TElement> : IEnumerable<TElement>
        where TElement : ISolutionElement
    {
        private readonly int dimensionCount;
        private readonly List<TElement> elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{TElement}"/> class.
        /// </summary>
        /// <param name="dimensionCount">Indicating dimensions of an element.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Solution(int dimensionCount = 2, int capacity = 1000)
        {
            this.dimensionCount = dimensionCount;
            this.elements = new List<TElement>(capacity);
        }

        /// <summary>
        /// Gets elements count.
        /// </summary>
        public int Count => this.elements.Count;

        /// <summary>
        /// Gets dimensions of an element.
        /// </summary>
        public int DimensionCount => this.dimensionCount;

        /// <summary>
        /// Returns appropriate element specified by index.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>Element.</returns>
        public TElement this[int i]
        {
            get { return this.elements[i]; }
            set { this.elements[i] = value; }
        }

        /// <summary>
        /// Calculates fitness.
        /// </summary>
        /// <returns>Fitness.</returns>
        public abstract double CalculateFitness();

        /// <summary>
        /// Adds an element.
        /// </summary>
        /// <param name="element">Element.</param>
        public void Add(TElement element)
        {
            if (element?.IsValid(this.dimensionCount) == true)
            {
                this.elements.Add(element);
            }
        }

        /// <summary>
        /// Adds a range of elements.
        /// </summary>
        /// <param name="elements">Elements.</param>
        public void AddRange(IEnumerable<TElement> elements)
        {
            foreach (TElement element in elements)
            {
                this.Add(element);
            }
        }

        /// <summary>
        /// Removes an element.
        /// </summary>
        /// <param name="element">Element.</param>
        public void Remove(TElement element)
        {
            this.elements.Remove(element);
        }

        /// <summary>
        /// Replaces an old item with a new item keeping the index.
        /// </summary>
        /// <param name="oldItem">Item to be removed.</param>
        /// <param name="newItem">Item to be put.</param>
        public void Replace(TElement oldItem, TElement newItem)
        {
            int index = this.elements.IndexOf(oldItem);
            if (index > -1)
            {
                this.Remove(oldItem);
                this.elements.Insert(index, newItem);
            }
        }

        /// <inheritdoc/>
        public IEnumerator<TElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
    }
}
