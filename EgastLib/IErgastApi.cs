using Refit;
namespace ErgastLib;

public interface IErgastApi
{
    /// <summary>
    /// Returns the lap times for the race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="race">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{race}/laps.json?limit=2000")]
    Task<RaceData> GetLapsAsync(string season, string race);

    /// <summary>
    /// Returns the constructors for the specified race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="race">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{race}/constructors.json")]
    Task<RaceData> GetConstructorsAsync(string season, string race);

    /// <summary>
    /// Returns the drivers for the specified constructor in the specified race in the specified season.
    /// </summary>
    /// <param name="season">The year of the season.</param>
    /// <param name="race">The number of the race in the season.</param>
    /// <param name="constructor">The name of the constructor.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{race}/constructors/{constructor}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string race, string constructor);

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
    /// <param name="race">The number of the race in the season.</param>
    /// <returns>The race data <see cref="RaceData"/></returns>
    [Get("/{season}/{race}/drivers.json")]
    Task<RaceData> GetDriversAsync(string season, string race);
}