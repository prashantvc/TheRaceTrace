public class RaceSummary
{
    public RaceSummary(RaceData raceData, List<DriverLapTime> driverLapTimes)
    {
        Season = raceData.Season;
        Round = raceData.Round;
        RaceName = raceData.RaceName;
        Date = raceData.RaceDate;
        DriverLapTimes = driverLapTimes;
    }

    public RaceSummary(int season, int round, string raceName, string date, List<DriverLapTime> driverLapTimes)
    {
        Season = season;        
        Round = round;
        RaceName = raceName;
        Date = date;
        DriverLapTimes = driverLapTimes;
    }

    public int Season { get; private set; }
    public int Round { get; private set; }
    public string RaceName { get; private set; }
    public string Date { get; private set; }

    public List<DriverLapTime> DriverLapTimes { get; private set; }

    public override string ToString()
    {
        return $"Season: {Season}, Round: {Round}, RaceName: {RaceName}";
    }
}

public record DriverLapTime(string Id, Driver? Driver, List<LapTime> LapTimes);