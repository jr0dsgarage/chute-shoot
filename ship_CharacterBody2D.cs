using Godot;
using System;

public partial class ship_CharacterBody2D: CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public float RotationSpeed { get; set; } = 1.5f;


	public void GetInput()
	{
		if (0 < Position.X && Position.X < 480)
		{
			Velocity = Transform.X * Input.GetAxis("up", "down") * Speed;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		
		MoveAndSlide();
	}
}
