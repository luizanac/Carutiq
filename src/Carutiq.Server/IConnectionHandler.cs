namespace Carutiq.Server;

public interface IConnectionHandler
{
    Task Handle(IConnection connection, CancellationToken token = default);
}