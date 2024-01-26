using System.Runtime.CompilerServices;
using System.Text.Json;
using ErgastLib;
using Moq;
using Refit;

namespace ErgastLibTests
{
    [TestClass]
    public class ErgastServiceTest
    {

        [TestMethod]
        public async Task GetDrivers()
        {
            var apiMock = new Mock<IErgastApi>();
            var service = new ErgastService(apiMock.Object);

            var constructors = await ParseTestDataAsync("Constructors.json");
            apiMock.Setup(x => x.GetConstructorsAsync(CurrentSeason, LastRound))
                .ReturnsAsync(constructors!);
            
            var driverData = await ParseTestDataAsync("Drivers.json");
            apiMock.Setup(x => 
                    x.GetDriversAsync(CurrentSeason, LastRound, It.IsAny<string>()))
                .ReturnsAsync(driverData!);

            var drivers = await service.GetDriversAsync();
            
            apiMock.Verify(x=>
                x.GetDriversAsync(CurrentSeason, LastRound, It.IsAny<string>()), 
                Times.Exactly(10));
            Assert.AreEqual(20, drivers.Count);
        }
        
        [TestMethod]
        public async Task GetConstructorData()
        {
            var testData = await ParseTestDataAsync("Constructors.json");
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
            var testLapData = await ParseTestDataAsync("Laps.json");
            var apiMock = new Mock<IErgastApi>();
            apiMock.Setup(x => x.GetLapsAsync(CurrentSeason, LastRound, 2000))
                .ReturnsAsync(testLapData!);
            
            var service = new ErgastService(apiMock.Object);
            var lapTimes = await service.GetLapTimeAsync();
            
            apiMock.Verify(x=>x.GetLapsAsync(CurrentSeason, LastRound, 2000), Times.Once);
            Assert.AreEqual(1157, lapTimes.GetLapTimes().Count(), "Lap times count do not match");
        }

        [TestMethod]
        public async Task RaceOverview()
        {
            var testLapData = await ParseTestDataAsync("Laps.json");
            var apiMock = new Mock<IErgastApi>();
            
            apiMock.Setup(x => x.GetLapsAsync(CurrentSeason, LastRound, 2000))
                .ReturnsAsync(testLapData!);
            
            var constructors = await ParseTestDataAsync("Constructors.json");
            apiMock.Setup(x => x.GetConstructorsAsync(CurrentSeason, LastRound))
                .ReturnsAsync(constructors!);
            
            var driverData = await ParseTestDataAsync("Drivers.json");
            apiMock.Setup(x => 
                    x.GetDriversAsync(CurrentSeason, LastRound, It.IsAny<string>()))
                .ReturnsAsync(driverData!);
            
            var service = new ErgastService(apiMock.Object);
            var raceSummary = await service.RaceSummaryAsync();
            
            apiMock.Verify(x=>x.GetLapsAsync(CurrentSeason, LastRound, 2000), Times.Once);
            Assert.AreEqual(2023, raceSummary.Season, "Race season do not match");
            Assert.AreEqual(22, raceSummary.Round, "round do not match");
        }
        
        static async Task<RaceData?> ParseTestDataAsync(string fileName)
        {
            var data = await File.ReadAllTextAsync(Path.Combine("TestData", fileName));
            return JsonSerializer.Deserialize<RaceData>(data);
        }
        
        const string CurrentSeason = "current";
        const string LastRound = "last";
    }
}