using System.Net;
using System.Net.Sockets;
using System.Numerics;

public class GameUdpService : IHostedService, IDisposable
{
    private readonly ILogger<GameUdpService> _logger;
    private UdpClient _udp;
    private int _port;
    private bool _running;

    public GameUdpService(ILogger<GameUdpService> logger)
    {
        _logger = logger;
        _port = 9000;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _udp = new UdpClient(_port);
        _logger.LogInformation($"Listening on UDP port {_port}");

        _running = true;
        _udp.BeginReceive(OnReceive, null);

        return Task.CompletedTask;
    }

    private void OnReceive(IAsyncResult ar)
    {
        if (!_running) return;

        try
        {
            IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = _udp.EndReceive(ar, ref clientEP);

            Vector2 movement = BytesToVector2(data);
            _logger.LogInformation($"Received from {clientEP}: {movement}");

            // TODO: Process movement, update positions, send response


            if (_running)
                _udp.BeginReceive(OnReceive, null);
        }
        catch (ObjectDisposedException)
        {
            // Socket closed, ignore
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UDP receive");
            if (_running)
                _udp.BeginReceive(OnReceive, null);
        }
    }

    private Vector2 BytesToVector2(byte[] bytes)
    {
        float x = BitConverter.ToSingle(bytes, 0);
        float y = BitConverter.ToSingle(bytes, 4);
        return new Vector2(x, y);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping UDP listener");
        _running = false;
        _udp?.Close();
        //_udp = null;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _udp?.Dispose();
    }
}
