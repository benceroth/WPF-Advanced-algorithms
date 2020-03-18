namespace Halal.Problems.WorkAssignment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class Problem : Problem<Person>
    {
        public Problem(double requestedTime, int capacity = 1000)
            : base(2, capacity)
        {
            this.RequestedTime = requestedTime <= 0 ? 1 : requestedTime;
        }

        public double RequestedTime { get; private set; }
    }
}
