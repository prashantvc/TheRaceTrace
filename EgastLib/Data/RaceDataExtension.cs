public partial class RaceData
{
    public int Season => MrData.RaceTable.Season;
    public int Round => MrData.RaceTable.Round;
    public string RaceName => MrData.RaceTable.Races.FirstOrDefault()?.RaceName ?? string.Empty;
    public string RaceDate => MrData.RaceTable.Races.FirstOrDefault()?.Date ?? string.Empty;
    
    IEnumerable<Lap> GetLaps()
    {
        var firstRace = MrData.RaceTable.Races.FirstOrDefault();
        return firstRace != null ? firstRace.Laps : Array.Empty<Lap>();
    }

    internal IEnumerable<LapTime> GetLapTimes()
    {
        var laps = GetLaps();

        var lapTimes = laps.SelectMany(
            lap => lap.Timings.Select(
                timing => new LapTime(lap.Number, timing.DriverId, timing.Position, timing.Time)
            ));

        return lapTimes;
    }

    internal IEnumerable<Race> GetRaces()
    {
        return MrData.RaceTable.Races;
    }

    internal IEnumerable<Constructor> GetConstructors()
    {
        return MrData.ConstructorTable.Constructors;
    }

    internal IEnumerable<Driver> GetDrivers()
    {
        return MrData.DriverTable.Drivers;
    }
}