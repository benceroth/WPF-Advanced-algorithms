namespace Halal.IO
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines an import operation.
    /// </summary>
    /// <typeparam name="T">Imported type.</typeparam>
    public abstract class Import<T>
    {
        /// <summary>
        /// Imports an element from a file.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <returns>Element.</returns>
        public T FromFile(string path)
        {
            return this.FromText(this.ReadFile(path));
        }

        /// <summary>
        /// Imports an element from text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Element.</returns>
        protected abstract T FromText(string[][] text);

        /// <summary>
        /// Reads content of file.
        /// </summary>
        /// <param name="path">Path of file.</param>
        /// <returns>Strings based on lines and commas.</returns>
        protected string[][] ReadFile(string path)
        {
            return File.ReadAllLines(path)
                .Select(x => x.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length > 0)
                .ToArray();
        }
    }
}
