public partial class RaceData
{
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
                timing => new LapTime(lap.Number,timing.DriverId, timing.Position,  timing.Time)
            ));

        return lapTimes;
    }

    internal IEnumerable<Constructor> GetConstructors()
    {
        return MrData.ConstructorTable.Constructors;
    }
}