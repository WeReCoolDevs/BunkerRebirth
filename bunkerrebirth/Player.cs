using Godot;
using System;

public partial class Player : Node2D
{
	private NetworkManager _networkManager;

	public void SetNetworkManager(NetworkManager manager)
	{
		_networkManager = manager;
	}
	   public override void _Ready()
	{
		_networkManager = GetNode<NetworkManager>("/root/GameScreen/NetworkManager");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey)
		{
			InputEventKey keyEvent = (InputEventKey)@event;
			if (keyEvent.Pressed || keyEvent.IsReleased())
			{
				Vector2 inputVector = Vector2.Zero;

				if (Input.IsActionPressed("move_up"))
					inputVector.Y -= 1;
				if (Input.IsActionPressed("move_down"))
					inputVector.Y += 1;
				if (Input.IsActionPressed("move_left"))
					inputVector.X -= 1;
				if (Input.IsActionPressed("move_right"))
					inputVector.X += 1;

				if (inputVector != Vector2.Zero)
					inputVector = inputVector.Normalized();

				_networkManager?.QueuePlayerInput(inputVector);
			}
		}
	}
}
