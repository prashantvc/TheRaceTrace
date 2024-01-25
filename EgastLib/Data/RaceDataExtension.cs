public partial class RaceData
{
    public IEnumerable<Lap> GetLaps()
    {
        var firstRace = MrData.RaceTable.Races.FirstOrDefault();
        return firstRace != null ? firstRace.Laps : Array.Empty<Lap>();
    }

    public void GetLapTimes()
    {
        var laps = GetLaps();
        
        var lapTimes = laps.SelectMany(
            lap => lap.Timings.Select(
                timing => new LapTime(lap.Number,timing.DriverId, timing.Position,  timing.Time)
            ));
    }
}