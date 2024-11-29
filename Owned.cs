namespace OwnedRepro;

public sealed class Owned<T>(IServiceScopeFactory serviceScopeFactory) where T : class
{
    public IOwnedScope GetScope()
    {
        var scope = serviceScopeFactory.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();
        return new OwnedScope(service, scope);
    }

    private sealed class OwnedScope(T value, IAsyncDisposable disposable) : IOwnedScope
    {
        // NoHandler issue can be resolved by making this nullable
        // and having the caller null
        public T Value { get; } = value;

        public async ValueTask DisposeAsync() => 
            await disposable.DisposeAsync();
    }

    public interface IOwnedScope : IAsyncDisposable
    {
        public T Value { get; }
    }
}
