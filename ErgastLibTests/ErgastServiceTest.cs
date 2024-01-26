using System.Text.Json;
using ErgastLib;
using Moq;

namespace ErgastLibTests
{
    [TestClass]
    public class ErgastServiceTest
    {
        [TestMethod]
        public async Task GetConstructorData()
        {
            var testData = await GetTestConstructorDataAsync();
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetConstructorsAsync(CurrentSeason, LastRound))
                .ReturnsAsync(testData!);

            var service = new ErgastService(apiMock.Object);
            var constructors = await service.GetConstructorsAsync();
            apiMock.Verify(x => x.GetConstructorsAsync(CurrentSeason, LastRound), Times.Once);
            
            Assert.IsNotNull(constructors);
            Assert.AreEqual(10, constructors.Count());
        }
        
        [TestMethod]
        public async Task GetFlatLapTimes()
        {
            var testLapData = await GetTestLapsDataAsync();
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetLapsAsync(CurrentSeason, LastRound, 2000))
                .ReturnsAsync(testLapData!);
            
            var service = new ErgastService(apiMock.Object);
            var lapTimes = await service.GetLapTimeAsync();
            
            apiMock.Verify(x=>x.GetLapsAsync(CurrentSeason, LastRound, 2000), Times.Once);
            Assert.AreEqual(1157, lapTimes.Count(), "Lap times count do not match");
        }

        static async Task<RaceData?> GetTestConstructorDataAsync()
        {
            string data = await File.ReadAllTextAsync(Path.Combine("TestData", "Constructors.json"));
            return JsonSerializer.Deserialize<RaceData>(data);
        }

        static async Task<RaceData?> GetTestLapsDataAsync()
        {
            string data = await File.ReadAllTextAsync(Path.Combine("TestData", "Laps.json"));
            return JsonSerializer.Deserialize<RaceData>(data);
        }
        
        const string CurrentSeason = "current";
        const string LastRound = "last";
    }
}