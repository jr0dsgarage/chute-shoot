using Godot;
using System;

public partial class ship_CharacterBody2D : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 600;

	public float shipSize;
	public float bottomClamp;

    public void GetInput()
    {
		shipSize = (GetNode<CollisionShape2D>("ship_CollisionShape2D").Shape.GetRect().Size.X);
		bottomClamp = 480 - shipSize;
		// topClamp = shipSize;
        if (bottomClamp < Position.Y || Position.Y < shipSize)
        {
            Velocity = Velocity with { Y = -Velocity.Y };
        }
        else
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
