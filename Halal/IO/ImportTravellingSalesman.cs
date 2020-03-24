namespace Halal.IO
{
    using System.Linq;
    using Halal.Problems.TravellingSalesman;

    /// <inheritdoc/>
    public class ImportTravellingSalesman : Import<Problem>
    {
        /// <inheritdoc/>
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
