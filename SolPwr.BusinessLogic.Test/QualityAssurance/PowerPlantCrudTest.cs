using Microsoft.Extensions.Logging;
using Moq;
using OnionDlx.SolPwr.BusinessLogic;
using OnionDlx.SolPwr.BusinessObjects;
using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Services;
using System.Collections;
using System.Linq.Expressions;

namespace OnionDlx.SolPwr.QualityAssurance
{
    public class CrudTests
    {
        #region Boilerplate

        private Mock<IMeteoLookupService> _endpointMock;
        private Mock<ILogger<IPlantManagementService>> _loggerMock;
        private Mock<IMeteoLookupServiceCallback> _meteoCallbackMock;


        [SetUp]
        public void Setup()
        {
            // Trivial parts are always the same here
            _loggerMock = new Mock<ILogger<IPlantManagementService>>();
            _endpointMock = new Mock<IMeteoLookupService>();
            _meteoCallbackMock = new Mock<IMeteoLookupServiceCallback>();
            _meteoCallbackMock.Setup(ep => ep.GetEndpoint()).Returns(_endpointMock.Object);
        }


        private PlantManagementService CreateInstanceWithData(IList<PowerPlant> source)
        {
            // EF part
            var repoFactoryMock = new Mock<IUtilitiesRepositoryFactory>();
            var repoMock = new Mock<IUtilitiesRepository>();
            repoFactoryMock.Setup(mock => mock.NewCommand()).Returns(repoMock.Object);
            repoFactoryMock.Setup(mock => mock.NewQuery()).Returns(repoMock.Object);

            var powerPlantCollectionMock = new Mock<IBusinessObjectCollection<PowerPlant>>().WithAsyncData(source);
            repoMock.Setup(repo => repo.PowerPlants).Returns(powerPlantCollectionMock.Object);

            // Service ready to be used
            return new PlantManagementService(repoFactoryMock.Object, _loggerMock.Object, _meteoCallbackMock.Object);
        }

        #endregion

        [Test]
        public async Task PrePopulated_SelectAll_Success()
        {
            // Arrange
            var source = new List<PowerPlant>
            {
                new PowerPlant
                {
                    Id = Guid.NewGuid(),
                    PlantName = "Three Mile island",
                    UtcInstallDate = DateTime.UtcNow,
                    Location = new GeoCoordinate(5, 10),
                    PowerCapacity = 100
                },
                new PowerPlant
                {
                    Id = Guid.NewGuid(),
                    PlantName = "福島第",
                    UtcInstallDate = DateTime.UtcNow,
                    Location = new GeoCoordinate(10, 20),
                    PowerCapacity = 200
                },
                new PowerPlant
                {
                    Id = Guid.NewGuid(),
                    PlantName = "Чорнобиль",
                    UtcInstallDate = DateTime.UtcNow,
                    Location = new GeoCoordinate(20, 30),
                    PowerCapacity = 300
                }
            };

            // This is the real service, but with mocked surroundings
            var testee = CreateInstanceWithData(source);

            // Act
            var result = await testee.GetAllPlantsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(source.Count));
            //_loggerMock.VerifyFor("OK");
        }


    }
}
