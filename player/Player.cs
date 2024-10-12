using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	Area2D hitbox;

	const float Speed = 140;

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move((float)delta);
	}

	void Move(float delta)
	{
		var vec = Input.GetVector("left", "right", "up", "down");
		Position += vec * Speed * delta;
	}
}
