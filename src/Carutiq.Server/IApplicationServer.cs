namespace Carutiq.Server;

public interface IApplicationServer
{
    public Task Listen(Action? onListen = default, CancellationToken token = default);
}