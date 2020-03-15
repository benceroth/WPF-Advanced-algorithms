namespace Halal.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Halal.Problems.TravellingSalesman;

    public class ImportTravellingSalesman : Import<Problem>
    {
        protected override Problem FromText(string[][] text)
        {
            int rows = text.Length;
            int dimensionCount = text.FirstOrDefault()?.Length ?? 2;
            var result = new Problem(dimensionCount, rows);
            result.AddRange(text.Select(x => new Town(x.Select(y => double.Parse(y)))));

            return result;
        }
    }
}
