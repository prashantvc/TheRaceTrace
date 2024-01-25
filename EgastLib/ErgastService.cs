namespace ErgastLib;

public class ErgastService(IErgastApi ergastApi)
{

    async Task<RaceData> GetRaceDataAsync(int season = 0, int race = 0, int limit = 2000)
    {
        string year = season == 0 ? "current" : season.ToString();
        string raceNumber = race == 0 ? "last" : race.ToString();
        return await ergastApi.GetLapsAsync(year, raceNumber, limit);
    }

    public async Task GetLapTimeAsync()
    {
        var racData = await GetRaceDataAsync();
        racData.GetLapTimes();
    }
}

