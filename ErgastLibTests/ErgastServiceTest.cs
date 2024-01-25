using System.Text.Json;
using ErgastLib;
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
        public async Task GetFlatLapTimes()
        {
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetLapsAsync("current", "last", 2000)).ReturnsAsync(_testData!);
            
            var service = new ErgastService(apiMock.Object);
            var lapTimes = await service.GetLapTimeAsync();
            
            apiMock.Verify(x=>x.GetLapsAsync("current", "last", 2000), Times.Once);
            Assert.AreEqual(1157, lapTimes.Count(), "Lap times count do not match");
        }

        async Task<RaceData?> GetTestDataAsync()
        {
            string data = await File.ReadAllTextAsync(Path.Combine("TestData","Laps.json"));
            return JsonSerializer.Deserialize<RaceData>(data);
        }

        RaceData? _testData = null;
    }
}