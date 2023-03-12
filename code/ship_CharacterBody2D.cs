using Godot;

public partial class ship_CharacterBody2D : CharacterBody2D
{
    [Export]
    public int Speed { get => _speed; set => _speed = value; }
    public float ShipSize { get => _shipSize; set => _shipSize = value; }

    private float _bottomClamp;
    private int _speed = 200;
    private float _shipSize;

    public void GetInput()
    {
        if (_bottomClamp < Position.Y || Position.Y < _shipSize / 2) // topClamp = 0 + shipSize/2;
        {
            // TODO - Screen Edge Bouncing should use Collision objects in prep for chutes
            Velocity = Velocity with { Y = -Velocity.Y }; 
        }
        else
        {
            Velocity = Transform.X * Input.GetAxis("up", "down") * _speed;
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        _shipSize = GetNode<CollisionShape2D>("ship_CollisionShape2D").Shape.GetRect().Size.X;
        _bottomClamp = 480 - _shipSize / 2;
        GetInput();
        MoveAndSlide();
    }
}
