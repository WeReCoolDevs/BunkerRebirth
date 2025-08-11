using Godot;
using System;
using System.Text;

public partial class NetworkManager : Node2D
{
	private PacketPeerUdp _udpPeer = new();

	public override void _Ready()
	{
		// Connect to the UDP server
		_udpPeer.ConnectToHost("127.0.0.1", 9000);
		GD.Print("Connected to UDP server at 127.0.0.1:9000");
	}

	private byte[] Vector2ToBytes(Vector2 v)
	{
		byte[] bytes = new byte[8]; 
		Buffer.BlockCopy(BitConverter.GetBytes(v.X), 0, bytes, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(v.Y), 0, bytes, 4, 4);
		return bytes;
	}

	public void QueuePlayerInput(Vector2 movement)
	{
		byte[] data = Vector2ToBytes(movement);
		_udpPeer.PutPacket(data);
	}
}
