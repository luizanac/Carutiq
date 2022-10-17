namespace Carutiq.Server;

public class ConnectionHandler : IConnectionHandler
{
    public Task Handle(IConnection connection, CancellationToken token = default) =>
        Task.Run(() =>
        {
            var addressHandle = LogContext.PushProperty("ClientAddress", connection.ClientAddress);
            Log.Information("Connection received");

            while (connection.Socket.Connected || !token.IsCancellationRequested)
            {
                if (!connection.HasData)
                    continue;

                var buffer = new byte[connection.Socket.Available];
                _ = connection.Socket.Client.Receive(buffer, SocketFlags.None);
                
                Log.Information("{Data}", Encoding.UTF8.GetString(buffer));
            }

            Log.Information("Connection closed");

            addressHandle.Dispose();
            connection.Dispose();
        }, token);
}