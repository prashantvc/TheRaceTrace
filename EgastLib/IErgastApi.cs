using Refit;

public interface IErgastApi
{
    //http://ergast.com/api/f1/current/last/laps.json?limit=2000


    /// <summary>
    /// Returns the last lap times for the last race is current season
    /// </summary>
    /// <param name="season">Season in years, default current</param>
    /// <param name="race">Race number, default last</param>
    /// <param name="limit">Number of laps</param>
    /// <returns>Task of <see cref="RaceData"/></returns>
    [Get("/{season}/{race}/laps.json")]
    Task<RaceData> GetLapsAsync(string season, string race, [Query] int limit);
}