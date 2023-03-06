using Godot;
using System;

public partial class ship_CharacterBody2D : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 600;

    public void GetInput()
    {
        if (480 < Position.Y || Position.Y < 0)
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
