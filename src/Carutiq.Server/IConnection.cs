namespace Carutiq.Server;

public interface IConnection : IDisposable
{
    public TcpClient Socket { get; }
    public int MaxIdleTime { get; }
    public bool HasData => Socket.Available > 0;
    public string? ClientAddress => Socket.Client.RemoteEndPoint?.ToString();
}