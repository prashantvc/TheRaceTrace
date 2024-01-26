namespace ErgastLib;

public class ErgastService(IErgastApi ergastApi)
{
    
    //TODO: Think of the way to cache response
    public async Task<IEnumerable<LapTime>> GetLapTimeAsync(int season = 0, int round = 0, int limit = 2000)
    {
        var raceParams = ParseRaceParameters(season, round);
        var racData = await ergastApi.GetLapsAsync(raceParams.season, raceParams.round, limit);
        return racData.GetLapTimes();
    }
    
    public async Task<IEnumerable<Constructor>> GetConstructorsAsync(int season = 0, int round = 0)
    {
        var raceParams = ParseRaceParameters(season, round);
        var raceData = await ergastApi.GetConstructorsAsync(raceParams.season, raceParams.round);

        return raceData.GetConstructors();
    }

    public async Task<List<Driver>> GetDriversAsync(int season = 0, int round = 0)
    {
        var constructors = await GetConstructorsAsync(season, round);
        var raceParams = ParseRaceParameters(season, round);

        var tasks = new List<Task>();
        var drivers = new List<Driver>();
        Parallel.ForEach(constructors, ctr =>
        {
            tasks.Add(Task.Run(async () =>
            {
                var raceData = await ergastApi.GetDriversAsync(raceParams.season, raceParams.round, ctr.Id);
                lock (drivers)
                {
                    foreach (var drv in raceData.GetDrivers())     
                    {
                        drv.Constructor = ctr;
                        drivers.Add(drv);
                    }
                }
            }));
        });
        await Task.WhenAll(tasks);
        return drivers.ToList();
    }

    static (string season, string round) ParseRaceParameters(int season, int round)
    {
        string localSeason = season == 0 ? CurrentSeason : season.ToString();
        string localRound = round == 0 ? LastRound : round.ToString();

        return (localSeason, localRound);
    }

    const string CurrentSeason = "current";
    const string LastRound = "last";
}

