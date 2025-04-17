namespace Alexdric.Sample.Tests.Infrastructure.Repositories;

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;
    public TestAsyncEnumerator(IEnumerator<T> inner) => _inner = inner;

    public T Current => _inner.Current;

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync(CancellationToken cancellationToken) =>
        ValueTask.FromResult(_inner.MoveNext());

    public ValueTask<bool> MoveNextAsync() =>
        ValueTask.FromResult(_inner.MoveNext());
}