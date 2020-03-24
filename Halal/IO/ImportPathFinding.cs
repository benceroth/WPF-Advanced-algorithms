namespace Halal.IO
{
    using System.Linq;
    using Halal.Problems.PathFinding;

    /// <inheritdoc/>
    public class ImportPathFinding : Import<Problem>
    {
        /// <inheritdoc/>
        protected override Problem FromText(string[][] text)
        {
            int rows = text.Length;
            int dimensionCount = text.FirstOrDefault()?.Length ?? 2;
            var result = new Problem(rows);
            result.AddRange(text.Select(x => new Row(x.Select(y => int.Parse(y)))));

            return result;
        }
    }
}
