namespace ErgastLib;

/// <summary>
/// Interface for the Ergast API service.
/// </summary>
public interface IErgastService
{
    /// <summary>
    /// Asynchronously retrieves a summary of a race for a given season and round.
    /// </summary>
    /// <param name="season">The year of the season. Default is `current.`</param>
    /// <param name="round">The number of the race in the season. Default is `last`.</param>
    /// <returns>The race summary with lap times and race information</returns>
    Task<RaceSummary> RaceSummaryAsync(int season = 0, int round = 0);

    /// <summary>
    /// Asynchronously retrieves lap time data for a given season and round.
    /// </summary>
    /// <param name="season">The year of the season. Default is `current.`</param>
    /// <param name="round">The number of the race in the season. Default is `last`.</param>
    /// <returns>Lap times</returns>
    Task<RaceData> GetLapTimeAsync(int season = 0, int round = 0);

    /// <summary>
    /// Asynchronously retrieves a list of drivers for a given season and round.
    /// </summary>
    /// <param name="season">The year of the season. Default is `current.`</param>
    /// <param name="round">The number of the race in the season. Default is `last`.</param>
    /// <returns>List of <see cref="Driver"/></returns>
    Task<IEnumerable<Driver>> GetDriversAsync(int season = 0, int round = 0);

    /// <summary>
    /// Asynchronously retrieves a list of races for a given season.
    /// </summary>
    /// <param name="season">The year of the season. Default is `current.`</param>
    /// <returns>List of <see cref="Race"/> for the season</returns>
    Task<IEnumerable<Race>> RaceListAsync(int selectedSeason);
}