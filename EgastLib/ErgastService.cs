using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgastLib
{
    internal class ErgastService
    {

        public void GetLaps()
        {
            ergastApi.GetLaps();
        }

        public static ErgastService Instance => _instance ??= new ErgastService();
        private ErgastService()
        {
            ergastApi = RestService.For<IErgastApi>("http://ergast.com/api/f1");
        }

        private static ErgastService _instance;
        readonly IErgastApi ergastApi;

    }
}
