using Refit;

namespace EgastLib
{
    public interface IErgastApi
    {
        //http://ergast.com/api/f1/current/last/laps.json?limit=2000


        /// <summary>
        /// Returns the last lap times for the last race is current season
        /// </summary>
        /// <param name="season">Season in years, default current</param>
        /// <param name="race">Race number, default last</param>
        /// <param name="limit">Number of laps</param>
        /// <returns>Empty Task<see cref="Task"/></returns>
        [Get("/{season}/{race}/laps.json")]
        Task<string> GetLaps(string season = "current", string race = "last", [Query] int limit = 2000);
    }
}
