/// <summary>
/// Provides extension methods for the <see cref="IEnumerable{double}"/> interface.
/// </summary>
static class EnumerableExtensions
{
    /// <summary>
    /// Computes the cumulative sum of a sequence of double values.
    /// </summary>
    /// <param name="source">A sequence of double values to calculate the cumulative sum of.</param>
    /// <returns>An <see cref="IEnumerable{double}"/> that contains the cumulative sum of the input sequence.</returns>
    public static IEnumerable<double> CumulativeSum(this IEnumerable<double> source)
    {
        double sum = 0;
        foreach (var item in source)
        {
            sum += item;
            yield return sum;
        }
    }
}