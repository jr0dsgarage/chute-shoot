using Godot;

public partial class ship_CharacterBody2D : CharacterBody2D
{
    [Export]
    public int Speed { get => speed; set => speed = value; }
    public float ShipSize { get => shipSize; set => shipSize = value; }

    private float bottomClamp;
    private int speed = 200;
    private float shipSize;

    public void GetInput()
    {
        if (bottomClamp < Position.Y || Position.Y < shipSize / 2) // topClamp = 0 + shipSize/2;
        {
            // TODO - Screen Edge Bouncing should use Collision objects in prep for chutes
            Velocity = Velocity with { Y = -Velocity.Y }; 
        }
        else
        {
            Velocity = Transform.X * Input.GetAxis("up", "down") * speed;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        shipSize = GetNode<CollisionShape2D>("ship_CollisionShape2D").Shape.GetRect().Size.X;
        bottomClamp = 480 - shipSize / 2;
        GetInput();
        MoveAndSlide();
    }
}
