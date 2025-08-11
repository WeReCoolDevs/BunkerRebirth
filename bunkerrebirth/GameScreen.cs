using Godot;
using System;
using System.Text;

public partial class GameScreen : Node2D
{
	private PacketPeerUdp udpPeer = new PacketPeerUdp();
	private Player player = new Player();

	
	public override void _Ready()
	{
		GD.Print("GameScreen loaded.");

		// Example: passing NetworkManager to Player instead of using GetNode inside Player
		var player = GetNode<Player>("Player");
		//var networkManager = GetNode<NetworkManager>("NetworkManager");
	}

}
