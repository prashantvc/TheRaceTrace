namespace ErgastLib;

public class ErgastService(IErgastApi ergastApi)
{
    public async Task<IEnumerable<LapTime>> GetLapTimeAsync(int season = 0, int round = 0, int limit = 2000)
    {
        string year = season == 0 ? CurrentSeason : season.ToString();
        string lastRound = round == 0 ? LastRound : round.ToString();
        
        var racData = await ergastApi.GetLapsAsync(year, lastRound, limit);
        return racData.GetLapTimes();
    }
    
    public async Task<IEnumerable<Constructor>> GetConstructorsAsync(int season = 0, int round = 0)
    {
        string year = season == 0 ? CurrentSeason : season.ToString();
        string lastRound = round == 0 ? LastRound : round.ToString();

        var raceData = await ergastApi.GetConstructorsAsync(year, lastRound);

        return raceData.GetConstructors();
    }

    const string CurrentSeason = "current";
    const string LastRound = "last";
}

