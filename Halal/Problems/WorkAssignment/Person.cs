namespace Halal.Problems.WorkAssignment
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public sealed class Person : List<double>, IProblemElement
    {
        public Person()
        {
        }

        public Person(IEnumerable<double> items)
            : base(items)
        {
        }

        public double Salary => this[0];

        public double Quality => this[1];

        public bool IsValid(int dimensionCount)
        {
            return this.Count == 2;
        }
    }
}
