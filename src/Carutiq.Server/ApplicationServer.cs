namespace Carutiq.Server;

public sealed class ApplicationServer : IApplicationServer
{
    private readonly TcpListener _listener;
    private readonly IConnectionHandler _connectionHandler;

    public ApplicationServer(BrokerConfiguration config, IConnectionHandler connectionHandler)
    {
        var endPoint = config.GetEndpoint();
        _listener = new TcpListener(endPoint);
        _connectionHandler = connectionHandler;
    }

    public async Task Listen(Action? onListen = default, CancellationToken token = default)
    {
        _listener.Start();
        onListen?.Invoke();
        while (_listener.Server.IsBound)
        {
            if (token.IsCancellationRequested) break;
            var socket = await _listener.AcceptTcpClientAsync(token);
            var connection = new Connection(socket);
            _ = _connectionHandler.Handle(connection, token);
        }
    }
}