namespace ErgastLib;

public class ErgastService(IErgastApi ergastApi) : IErgastService
{
    public async Task<RaceSummary> RaceSummaryAsync(int season = 0, int round = 0)
    {
        var laps = await GetLapTimeAsync(season, round);
        var drivers = await GetDriversAsync(season, round);

        var lapTimes = laps.GetLapTimes().GroupBy(l => l.DriverId)
            .ToDictionary(g => g.Key, g => g.OrderBy(l => l.LapNumber))
            .Select(p => new DriverLapTime(p.Key, drivers.FirstOrDefault(d => d.Id == p.Key), [.. p.Value]));

        return new RaceSummary(laps, lapTimes.ToList());
    }

    public async Task<RaceData> GetLapTimeAsync(int season = 0, int round = 0)
    {
        var raceParams = ParseRaceParameters(season, round);
        return await ergastApi.GetLapsAsync(raceParams.season, raceParams.round);
    }

    public async Task<IEnumerable<Driver>> GetDriversAsync(int season = 0, int round = 0)
    {
        var raceParams = ParseRaceParameters(season, round);
        var data = await ergastApi.GetDriversAsync(raceParams.season, raceParams.round);
        return data.GetDrivers();
    }

    public async Task<IEnumerable<Race>> RaceListAsync(int selectedSeason)
    {
        var raceParams = ParseRaceParameters(selectedSeason);
        var data = await ergastApi.GetRaceListAsync(raceParams.season);
        return data.GetRaces();
    }

    static (string season, string round) ParseRaceParameters(int season, int round = 0)
    {
        string localSeason = season == 0 ? CurrentSeason : season.ToString();
        string localRound = round == 0 ? LastRace : round.ToString();

        return (localSeason, localRound);
    }

    const string CurrentSeason = "current";
    const string LastRace = "last";
}

