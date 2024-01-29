using ErgastLib;
using Moq;
using System.Text.Json;

namespace TheRaceTraceViewModelTest
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public async Task GivenARaceSummaryBuildRaceTracePlot()
        {
            var serviceMock = new Mock<IErgastService>();

            var summary = await ParseTestRaceSummaryDataAsync();

            serviceMock.Setup(s => s.RaceSummaryAsync(2023, 1)).ReturnsAsync(summary);
            var vm = new MainViewModel(serviceMock.Object);
            vm.SelectedSeason = 2023;
            vm.SelectedRace = new Race { Round = 1, RaceName = "Test Race Name" };

            vm.LoadRaceDataCommand.Execute(null);

            serviceMock.Verify(serviceMock => serviceMock.RaceSummaryAsync(2023, 1), Times.Once());
            Assert.AreEqual(20, vm.PlotModel.Series.Count);
        }


        [TestMethod]
        public async Task GivenASeasonReturnListOfRaces()
        {
            var serviceMock = new Mock<IErgastService>();
            serviceMock.Setup(p=>p.RaceListAsync(2023)).ReturnsAsync(await ParseTestRaceListDataAsync());

            var vm = new MainViewModel(serviceMock.Object)
            {
                SelectedSeason = 2023
            };
            vm.LoadRaceListCommand.Execute(null);

            serviceMock.Verify(serviceMock => serviceMock.RaceListAsync(2023), Times.Once());
            Assert.AreEqual(22, vm.RaceList.Count());
        }

        private async Task<IEnumerable<Race>> ParseTestRaceListDataAsync()
        {
            var data = await File.ReadAllTextAsync(Path.Combine("TestData", "Season.json"));
            var raceData = JsonSerializer.Deserialize<RaceData>(data);

            return raceData!.GetRaces();
        }

        static async Task<RaceSummary?> ParseTestRaceSummaryDataAsync()
        {
            var data = await File.ReadAllTextAsync(Path.Combine("TestData", "RaceSummary.json"));
            var trs = JsonSerializer.Deserialize<TestRaceSummary>(data);

            var lapTimes = trs!.DriverLapTimes.Select(
                    p => new DriverLapTime(p.Id, p.Driver,
                        p.LapTimes.Select(l => new LapTime(l.LapNumber, l.DriverId, l.Position, l.Time)).ToList()
                    )
                );

            return new(trs.Season, trs.Round, trs.RaceName, trs.Date, lapTimes.ToList());
        }
    }
}