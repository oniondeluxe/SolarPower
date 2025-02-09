using OnionDlx.SolPwr.Services;

namespace OnionDlx.SolPwr.Application.Services
{
    public class PlantOperationSpinner : IHostedService
    {
        private readonly ILogger<PlantOperationSpinner> _logger;
        private Timer _timer;
        private readonly IIntegrationProxy _integrationProxy;

        public PlantOperationSpinner(ILogger<PlantOperationSpinner> logger, IIntegrationProxy integrationProxy)
        {
            _logger = logger;
            _integrationProxy = integrationProxy;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _integrationProxy.Initialize(cancellationToken);
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(6));
            }
            catch (Exception ex)
            {
                // The initialize went to sh*t, so no point trying further
                _logger.LogError(ex.Message);
                return Task.FromException(ex);
            }

            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            _logger.LogInformation("Background task is running.");

            // By now, we have figured out which plugin to go to
            _integrationProxy.GetEndpoint()?.ExecuteAsync().Wait();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _integrationProxy?.Cleanup();
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
