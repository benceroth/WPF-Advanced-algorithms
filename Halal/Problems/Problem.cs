namespace Halal.Problems
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a problem.
    /// </summary>
    /// <typeparam name="TElement">Problem element type.</typeparam>
    public abstract class Problem<TElement> : IEnumerable<TElement>
        where TElement : IProblemElement
    {
        private readonly int dimensionCount;
        private readonly List<TElement> elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="Problem{TElement}"/> class.
        /// </summary>
        /// <param name="dimensionCount">Indicating dimensions of an element.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Problem(int dimensionCount = 2, int capacity = 1000)
        {
            this.dimensionCount = dimensionCount;
            this.elements = new List<TElement>(capacity);
        }

        /// <summary>
        /// Gets elements count.
        /// </summary>
        public int Count => this.elements.Count;

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