using Immediate.Handlers.Shared;

namespace OwnedRepro;

public class Worker(
    IServiceProvider serviceProvider, 
    IHostApplicationLifetime lifetime, 
    ILogger<Worker> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IHostApplicationLifetime _lifetime = lifetime;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Publishing {EventType}", typeof(NoHandlerEvent));
        Publish(new NoHandlerEvent());
        await Task.Delay(100);

        _logger.LogInformation("Publishing {EventType}", typeof(OneHandlerEvent));
        Publish(new OneHandlerEvent());
        await Task.Delay(100);

        _logger.LogInformation("Publishing {EventType}", typeof(TwoHandlerEvent));
        Publish(new TwoHandlerEvent());
        await Task.Delay(100);

        _lifetime.StopApplication();
    }

    private void Publish<TEvent>(TEvent @event)
    {
        var handlers = _serviceProvider.GetService<IEnumerable<Owned<IHandler<TEvent, ValueTuple>>>>();

        foreach (var handler in handlers ?? [])
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await using var scope = handler.GetScope();
                    _ = await scope.Value.HandleAsync(@event);
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Exception when creating scope or calling handler {Handler}", handler.GetType());
                }
            });
        }
    }
}
