using Godot;
using System;
using System.Text;

public partial class GameScreen : Node2D
{
	private PacketPeerUdp udpPeer = new PacketPeerUdp();

	public override void _Ready()
	{
		// Connect to the UDP server
		udpPeer.ConnectToHost("127.0.0.1", 9000);
		GD.Print("Connected to UDP server at 127.0.0.1:9000");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_up"))
		{
			byte[] data = Encoding.UTF8.GetBytes("MOVE:UP");
			udpPeer.PutPacket(data);
			GD.Print("Sent MOVE:UP to server");
		}

		if (@event.IsActionPressed("ui_down"))
		{
			byte[] data = Encoding.UTF8.GetBytes("MOVE:DOWN");
			udpPeer.PutPacket(data);
			GD.Print("Sent MOVE:DOWN to server");
		}

		if (@event.IsActionPressed("ui_right"))
		{
			byte[] data = Encoding.UTF8.GetBytes("MOVE:RIGHT");
			udpPeer.PutPacket(data);
			GD.Print("Sent MOVE:DOWN to server");
		}

		if (@event.IsActionPressed("ui_left"))
		{
			byte[] data = Encoding.UTF8.GetBytes("MOVE:LEFT");
			udpPeer.PutPacket(data);
			GD.Print("Sent MOVE:LEFT to server");
		}
	}

	public override void _Process(double delta)
	{
		while (udpPeer.GetAvailablePacketCount() > 0)
		{
			byte[] packet = udpPeer.GetPacket();
			string msg = Encoding.UTF8.GetString(packet);
			GD.Print("Server says: " + msg);
		}
	}

}
