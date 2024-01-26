using ErgastLib;
using Moq;
using System.Text.Json;

namespace TheRaceTraceViewModelTest
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public async Task LoadRaceSummary()
        {
            var serviceMock = new Mock<IErgastService>();
            serviceMock.Setup(s => s.RaceSummaryAsync(0, 0, 2000))
                .ReturnsAsync(await ParseTestRaceSummaryDataAsync());
            var vm = new MainViewModel(serviceMock.Object);

            vm.LoadConstructorsCommand.Execute(null);

            serviceMock.Verify(serviceMock => serviceMock.RaceSummaryAsync(0, 0, 2000), Times.Once());
            Assert.IsNotNull(vm.RaceSummary);
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