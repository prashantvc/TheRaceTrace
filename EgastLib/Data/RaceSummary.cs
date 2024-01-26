public class RaceSummary(RaceData raceData, List<DriverLapTime> driverLapTimes)
{
    public int Season => raceData.Season;
    public int Round => raceData.Round;
    public string RaceName => raceData.RaceName;
    public string Date => raceData.RaceDate;
    
    public List<DriverLapTime> DriverLapTimes { get; } = driverLapTimes;

    public override string ToString()
    {
        return $"Season: {Season}, Round: {Round}, RaceName: {RaceName}";
    }
}

public record DriverLapTime (string Id, Driver? Driver, List<LapTime> LapTimes);