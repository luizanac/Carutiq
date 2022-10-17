using System.Net;

namespace Carutiq.Core;

public class BrokerConfiguration
{
    private static BrokerConfiguration? _instance;
    public int MaxBufferSize { get; private set; }
    public string? Host { get; private set; }
    public int Port { get; private set; }
    public int ConnectionMaxIdleTime { get; private set; }

    public static BrokerConfiguration GetConfig()
    {
        if (_instance is not null)
            return _instance;

        _instance = new BrokerConfiguration
        {
            Host = "0.0.0.0",
            Port = 11200,
            MaxBufferSize = 1024 * 1024,
            ConnectionMaxIdleTime = 30000
        };

        return _instance;
    }

    public IPEndPoint GetEndpoint()
    {
        if (!IPEndPoint.TryParse($"{Host}:{Port}", out var endpoint))
            throw new InvalidConfigurationException("Host and port is not valid");

        return endpoint!;
    }
}