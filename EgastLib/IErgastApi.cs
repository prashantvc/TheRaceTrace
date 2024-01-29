using Refit;
namespace ErgastLib;

public interface IErgastApi
{
    /// <summary>
    /// Returns the last lap times for the last race is current season
    /// </summary>
    /// <param name="season">Season in years, default current</param>
    /// <param name="round">Race number, default last</param>
    /// <returns>Task of <see cref="RaceData"/></returns>
    [Get("/{season}/{round}/laps.json?limit=2000")]
    Task<RaceData> GetLapsAsync(string season, string round);

    [Get("/{season}/{round}/constructors.json")]
    Task<RaceData> GetConstructorsAsync(string season, string round);

    [Get("/{season}/{round}/constructors/{constructor}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string round, string constructor);

    [Get("/{season}.json")]
    Task<RaceData> GetRaceListAsync(string season);

    [Get("/{season}/{round}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string round);
}