namespace Halal.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.FunctionApproximation;

    public class ImportFunctionApproximation : Import<Problem>
    {
        protected override Problem FromText(string[][] text)
        {
            int rows = text.Length;
            int dimensionCount = text.FirstOrDefault()?.Length ?? 2;
            var result = new Problem(rows);
            result.AddRange(text.Select(x => new Value(x.Select(y => double.Parse(y)))));

            return result;
        }
    }
}
