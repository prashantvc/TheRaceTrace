namespace ErgastLib;

/// <summary>
/// Ergast API service
/// </summary>
public interface IErgastService
{
    Task<RaceSummary> RaceSummaryAsync(int season = 0, int round = 0);
    Task<RaceData> GetLapTimeAsync(int season = 0, int round = 0);
    Task<IEnumerable<Driver>> GetDriversAsync(int season = 0, int round = 0);
    Task<IEnumerable<Race>> RaceListAsync(int selectedSeason);
}

