namespace Halal.IO
{
    using System.Linq;
    using Halal.Problems.FunctionApproximation;

    /// <inheritdoc/>
    public class ImportFunctionApproximation : Import<Problem>
    {
        /// <inheritdoc/>
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
