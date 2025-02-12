namespace OnionDlx.SolPwr.QualityAssurance
{
    public class CrudTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void BlankRepo_CreatePlant_Success()
        {
            // TODO
            // The following needs to be mocked:
            // IUtilitiesRepositoryFactory - emulating EntityFramework layer
            // IMeteoLookupServiceCallback - emulating the meteo integrator layer

            Assert.Inconclusive("Implementation is needed");
        }
    }
}
