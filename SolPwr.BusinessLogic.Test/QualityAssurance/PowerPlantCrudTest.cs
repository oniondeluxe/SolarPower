using Microsoft.Extensions.Logging;
using Moq;
using OnionDlx.SolPwr.BusinessLogic;
using OnionDlx.SolPwr.BusinessObjects;
using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Services;
using System.Linq.Expressions;

namespace OnionDlx.SolPwr.QualityAssurance
{
    public class CrudTests
    {
        // private Mock<ILogger<IPlantManagementService>> _loggerMock;
        // private Mock<IUtilitiesRepository> _utilitiesRepositoryMock;
        // private Mock<IUtilitiesRepositoryFactory> _utilitiesRepositoryFactoryMock;
        //// private Mock<IMeteoLookupServiceCallback> _meteoLookupServiceCallbackMock;
        // private PlantManagementService _plantManagementService;


        private Mock<IUtilitiesRepositoryFactory> _repoFactoryMock;
        private Mock<IUtilitiesRepository> _repoMock;
        private Mock<ILogger<IPlantManagementService>> _loggerMock;
        private Mock<IMeteoLookupServiceCallback> _meteoCallbackMock;
        private PlantManagementService _plantManagementService;


        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<IPlantManagementService>>();
            _repoFactoryMock = new Mock<IUtilitiesRepositoryFactory>();
            _meteoCallbackMock = new Mock<IMeteoLookupServiceCallback>();
            _repoMock = new Mock<IUtilitiesRepository>();

            var powerPlantsMock = new Mock<IBusinessObjectCollection<PowerPlant>>();
            powerPlantsMock.Setup(plants => plants.FirstOrDefaultAsync()).ReturnsAsync((PowerPlant)null);
            powerPlantsMock.Setup(plants => plants.FirstOrDefaultAsync(It.IsAny<Expression<Func<PowerPlant, bool>>>())).ReturnsAsync((PowerPlant)null);

            _repoMock.Setup(repo => repo.PowerPlants).Returns(powerPlantsMock.Object);

            _plantManagementService = new PlantManagementService(_repoFactoryMock.Object, _loggerMock.Object, _meteoCallbackMock.Object);


            _repoFactoryMock = new Mock<IUtilitiesRepositoryFactory>();
            _repoMock = new Mock<IUtilitiesRepository>();
            _loggerMock = new Mock<ILogger<IPlantManagementService>>();
            _meteoCallbackMock = new Mock<IMeteoLookupServiceCallback>();

            _repoFactoryMock.Setup(factory => factory.NewQuery()).Returns(_repoMock.Object);

            _plantManagementService = new PlantManagementService(_repoFactoryMock.Object, _loggerMock.Object, _meteoCallbackMock.Object);
        }


        [Test]
        public async Task BlankRepo_CreatePlant_Success()
        {
            // Arrange
            //_utilitiesRepositoryFactoryMock.Setup(repo => repo.GetAllPlantsAsync()).ReturnsAsync(new List<PowerPlantImmutable>());
            _repoFactoryMock.Setup(repo => repo.NewQuery()).Returns(_repoMock.Object);
            _repoFactoryMock.Setup(repo => repo.NewCommand()).Returns(_repoMock.Object);



            // Arrange
            var powerPlants = new List<PowerPlant>
            {
                new PowerPlant { Id = Guid.NewGuid(), PlantName = "Plant1", UtcInstallDate = DateTime.UtcNow, Location = new GeoCoordinate(0, 0), PowerCapacity = 100 },
                new PowerPlant { Id = Guid.NewGuid(), PlantName = "Plant2", UtcInstallDate = DateTime.UtcNow, Location = new GeoCoordinate(0, 0), PowerCapacity = 200 }
            };

            var powerPlantCollectionMock = new Mock<IBusinessObjectCollection<PowerPlant>>();
            powerPlantCollectionMock.Setup(p => p.GetEnumerator()).Returns(powerPlants.GetEnumerator());

            _repoMock.Setup(repo => repo.PowerPlants).Returns(powerPlantCollectionMock.Object);



            // Act
            var result = await _plantManagementService.GetAllPlantsAsync();

            // Assert
            //Assert.That().IsNotNull(result);
            //Assert.IsEmpty(result);
        }
    }
}
