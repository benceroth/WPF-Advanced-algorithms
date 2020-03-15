namespace Halal.IO
{
    using System;
    using System.IO;
    using System.Linq;

    public abstract class Import<T>
    {
        public T FromFile(string path)
        {
            return this.FromText(this.ReadFile(path));
        }

        protected abstract T FromText(string[][] text);

        protected string[][] ReadFile(string path)
        {
            return File.ReadAllLines(path)
                .Select(x => x.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length > 0)
                .ToArray();
        }
    }
}
