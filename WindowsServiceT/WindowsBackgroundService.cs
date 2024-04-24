using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WindowsServiceT
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly JokeService _jokeService;
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly IOptions<MyParameter> options;

        public WindowsBackgroundService(
            JokeService jokeService,
            ILogger<WindowsBackgroundService> logger,
            IOptions<MyParameter> options)
        {
            _jokeService = jokeService;
            _logger = logger;
            this.options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    string joke = _jokeService.GetJoke();
                    _logger.LogWarning($"New ))))))))) with Pouya {joke} --- {options.Value.Text}");

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
