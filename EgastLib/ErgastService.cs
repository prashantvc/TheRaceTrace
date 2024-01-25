using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EgastLib
{
    public class ErgastService
    {
        public async Task<Laps> GetLapsAsync(int season = 0, int race = 0, int limit = 2000)
        {
            string year = season == 0 ? "current" : season.ToString();
            string raceNumber = race == 0 ? "last" : race.ToString();
            

            return await ergastApi.GetLapsAsync(year, raceNumber, limit);
        }
        public ErgastService(IErgastApi ergastApi)
        {
            this.ergastApi = ergastApi;
        }

        readonly IErgastApi ergastApi;
    }
}
