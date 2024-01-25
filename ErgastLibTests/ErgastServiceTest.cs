using EgastLib;
using System.Text.Json;
using Moq;

namespace ErgastLibTests
{
    [TestClass]
    public class ErgastServiceTest
    {
        [TestInitialize]
        public async Task Setup()
        {
            _testData = await GetTestDataAsync();
        }

        [TestMethod]
        public async Task GivenErgastServiceWhenCalledLastRaceLapsThenReturnLastRaceLapResults()
        {
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetLapsAsync("current", "last", 2000)).ReturnsAsync(_testData!);

            var service = new ErgastService(apiMock.Object);
            var laps = await service.GetLapsAsync();

            apiMock.Verify(x=>x.GetLapsAsync("current", "last", 2000), Times.Once);
            Assert.IsNotNull(laps);
            Assert.AreEqual(1157, laps.MrData.TotalLaps);
        }

        [TestMethod]
        public async Task GivenSeasonAndRoundWhenAskedLapsReturnLaps()
        {
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetLapsAsync("2021", "2", 2000)).ReturnsAsync(_testData!);
            
            var service = new ErgastService(apiMock.Object);
            var laps = await service.GetLapsAsync(2021, 2);
            
            Assert.IsNotNull(laps);
            apiMock.Verify(x=>x.GetLapsAsync("2021", "2", 2000), Times.Once);
            
        }

        async Task<Laps?> GetTestDataAsync()
        {
            string data = await File.ReadAllTextAsync(Path.Combine("TestData","Laps.json"));
            return JsonSerializer.Deserialize<Laps>(data);
        }

        Laps? _testData = null;
    }
}