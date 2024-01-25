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
            var laps = await service.GetLastRaceLaps();

            apiMock.Verify(x => x.GetLapsAsync(It.Is<string>(s => s == "current"), It.Is<string>(s => s == "last"), It.Is<int>(i => i == 2000)), Times.Once);
            Assert.IsNotNull(laps);
            Assert.AreEqual(1157, laps.MrData.TotalLaps);
            

        }

        async Task<Laps?> GetTestDataAsync()
        {
            string data = await File.ReadAllTextAsync(Path.Combine("TestData","Laps.json"));
            return JsonSerializer.Deserialize<Laps>(data);
        }

        Laps? _testData = null;
    }
}