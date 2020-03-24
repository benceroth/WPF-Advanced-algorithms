namespace Halal.Problems.WorkAssignment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <inheritdoc/>
    public sealed class Problem : Problem<Person>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        /// <param name="requestedTime">Time to be divided.</param>
        /// <param name="capacity">Initial capacity.</param>
        public Problem(double requestedTime, int capacity = 1000)
            : base(2, capacity)
        {
            this.RequestedTime = requestedTime <= 0 ? 1 : requestedTime;
        }

        /// <summary>
        /// Gets requested time.
        /// </summary>
        public double RequestedTime { get; private set; }
    }
}
