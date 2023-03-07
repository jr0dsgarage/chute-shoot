using Godot;
using System;

public partial class ship_CharacterBody2D : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 600;

    private float shipSize;
    private float bottomClamp;

    public void GetInput()
    {
        shipSize = GetNode<CollisionShape2D>("ship_CollisionShape2D").Shape.GetRect().Size.X;
        bottomClamp = 480 - shipSize/2;

        if (bottomClamp < Position.Y || Position.Y < shipSize/2) // topClamp = 0 + shipSize/2;
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
