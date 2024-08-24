using System;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int number)
        {
            if (number < 0) throw new ArgumentException("Number must be non-negative.");
            return number == 0 ? 1 : number * GetFactorial(number - 1);
        }

        public static string FormatSeparators(params string[] items)
        {
            if (items == null || items.Length == 0) return string.Empty;

            if (items.Length == 1) return items[0];
            if (items.Length == 2) return $"{items[0]} and {items[1]}";

            var allButLast = string.Join(", ", items.Take(items.Length - 1));
            var last = items.Last();
            return $"{allButLast} and {last}";
        }
    }
}
