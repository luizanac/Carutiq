namespace Carutiq.Server;

public class Connection : IConnection
{
    public TcpClient Socket { get; }
    public int MaxIdleTime { get; }

    public Connection(TcpClient socket)
    {
        var config = BrokerConfiguration.GetConfig();
        MaxIdleTime = config.ConnectionMaxIdleTime;
        Socket = socket;
    }

    public void Dispose()
    {
        Socket.Dispose();
    }
}