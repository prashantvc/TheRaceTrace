internal static class EnumerableExtensions
{

    public static IEnumerable<double> CumulativeSum(this IEnumerable<double> source)
    {
        double sum = 0;
        foreach (var item in source)
        {
            sum += item;
            yield return sum;
        }
    }

    public static IEnumerable<double> CumulativeSum(this IEnumerable<LapTime> source)
    {
        double sum = 0;
        foreach (var item in source)
        {
            sum += item.Time;
            yield return sum;
        }
    }   
}

