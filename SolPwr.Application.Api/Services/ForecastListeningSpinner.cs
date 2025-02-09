namespace OnionDlx.SolPwr.Application.Services
{
    public class ForecastListeningSpinner : IHostedService
    {
        private readonly ILogger<ForecastListeningSpinner> _logger;
        private Timer _timer;

        public ForecastListeningSpinner(ILogger<ForecastListeningSpinner> logger)
        {
            _logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(6));
            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            _logger.LogInformation("Background task is running.");
            
            // Add your task logic here
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
