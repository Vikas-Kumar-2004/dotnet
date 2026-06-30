using static CQRSMediatorPattern.Abstractions.ICommand;

namespace CQRSMediatorPattern.Abstractions
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;

        Task<TResult> SendAsync<TCommand, TResult>(
            TCommand command,
            CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResult>;

        Task<TResult> QueryAsync<TQuery, TResult>(
            TQuery query,
            CancellationToken ccancellationTokent = default)
            where TQuery : IQuery<TResult>;
    }
}
