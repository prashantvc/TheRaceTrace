using Refit;
namespace ErgastLib;

public interface IErgastApi
{
    /// <summary>
    /// Returns the lap times for the race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="round">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{round}/laps.json?limit=2000")]
    Task<RaceData> GetLapsAsync(string season, string round);

    /// <summary>
    /// Returns the constructors for the specified race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="round">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{round}/constructors.json")]
    Task<RaceData> GetConstructorsAsync(string season, string round);

    /// <summary>
    /// Returns the drivers for the specified constructor in the specified race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="round">The number of the race in the season.</param>
    /// <param name="constructor">The name of the constructor.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{round}/constructors/{constructor}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string round, string constructor);

    /// <summary>
    /// Returns the list of races for the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}.json")]
    Task<RaceData> GetRaceListAsync(string season);

    /// <summary>
    /// Returns the drivers for the specified race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="round">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{round}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string round);
}