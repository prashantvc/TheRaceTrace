/// <summary>
/// Represents a series of lap data for a driver.
/// </summary>
/// <param name="Driver">The driver of the lap series.</param>
/// <param name="DataPoint">A list of tuples representing lap number and time.</param>
record LapSeries(Driver? Driver, List<(int Lap, double Time)> DataPoint);

/// <summary>
/// Provides methods to create race traces.
/// </summary>
class RaceTrace
{
    /// <summary>
    /// Creates a list of lap series for each driver.
    /// </summary>
    /// <param name="driverLapTimes">A list of lap times for each driver.</param>
    /// <returns>A list of lap series for each driver.</returns>
    internal List<LapSeries> CreateTraces(List<DriverLapTime> driverLapTimes)
    {
        var result = driverLapTimes
            .OrderByDescending(Classification)
            .ToList();
        var winner = result.First();
        var lapCount = winner.LapTimes.Count;
        var referenceLapTime = winner.LapTimes.Average(p => p.Time);
        var referenceRaceTime = Enumerable.Repeat(referenceLapTime, lapCount).CumulativeSum().ToList();

        var series = result.Select(p => CreateTrace(p, referenceRaceTime)).ToList();
        return series;
    }
    static LapSeries CreateTrace(DriverLapTime driverLapTime, List<double> referenceRaceTime)
    {
        var lapCount = driverLapTime.LapTimes.Count;
        var lapNumbers = Enumerable.Range(1, lapCount).ToList();
        var raceTime = driverLapTime.LapTimes.Select(p => p.Time).CumulativeSum();

        if (lapCount != referenceRaceTime.Count)
        {
            referenceRaceTime = referenceRaceTime.Take(lapCount).ToList();
        }

        var relativeRaceTime = raceTime.Zip(referenceRaceTime, (x, y) => y - x).ToList();

        LapSeries driverSeries = new(driverLapTime.Driver, []);

        for (int i = 0; i < lapNumbers.Count; i++)
        {
            driverSeries.DataPoint.Add((lapNumbers[i], relativeRaceTime[i]));
        }

        return driverSeries;

    }
    static Tuple<int, double> Classification(DriverLapTime driverLaps)
    {
        return new(driverLaps.LapTimes.Count, -driverLaps.LapTimes.Sum(p => p.Time));
    }
}

