namespace Subscriptions.Features.Common;

public interface IHandler<in TRequest, TResult>
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IHandler<TResult>
{
    Task<TResult> HandleAsync(CancellationToken cancellationToken = default);
}