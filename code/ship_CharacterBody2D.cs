using System;
using Godot;

/// <summary>
/// Class <c>ship_CharacterBody2D</c> is the player controlled ship.
/// </summary>
/// <remarks>
/// <para>It is a KinematicBody2D with a CollisionShape2D and a Sprite.</para>
/// <para>It has a <c>GetInput</c> function that is called every frame and makes the ship take actions based on the player's input.</para>
/// <para>It has a <c>_PhysicsProcess</c> function that is called every frame and makes the ship move and bounce off the screen edges.</para>
/// </remarks>
/// <seealso cref="Godot.CharacterBody2D"/>
/// <seealso cref="Godot.KinematicBody2D"/>
/// </summary>
public partial class ship_CharacterBody2D : CharacterBody2D
{

    [Export]
    /// <value>Property <c>Speed</c> represents the Speed Stat of the ship, and is used to limit the speed of the ship in the vertical axis, as well as limit the ship's Boosting distance.</value>
    public int Speed { get => _currentSpeed; set => _defaultSpeed = value; }
    [Export]
    /// <value>
    /// Property <c>BoostSpeed</c> represents the Boost Speed Stat of the ship, and is used to limit the speed of the ship in the horizontal axis, as well as limit the ship's Boosting distance.
    /// BoostSpeeds can be between 1 and 10, but moves the ship 10 to 100 pixels per second
    /// </value>
    public int BoostSpeed { get => _boostPixelsToMove / 10; set => _boostPixelsToMove = value * 10; }
    public bool IsTurboBoosting { get => _isTurboBoosting; set => _isTurboBoosting = value; }
    public bool IsReturning { get => _isReturningFromTurboBoost; set => _isReturningFromTurboBoost = value; }
    public float BoostTimeToReturn { get => _boostDelta; set => _boostDelta = value; }
    public bool IsSpeedBoosting { get => _isSpeedBoosting; set => _isSpeedBoosting = value; }
    public bool CanSpeedBoost { get => _canSpeedBoost; set => _canSpeedBoost = value; }
    public float ShipSize { get => _shipSize; set => _shipSize = value; }

    private float _bottomClamp;
    private float _shipSize;
    private float _defaultPositionX = 100;
    private int _defaultSpeed = 100;
    private int _currentSpeed;
    private int _boostPixelsToMove;
    private float _boostDelta;
    private bool _isTurboBoosting = false;
    private bool _isReturningFromTurboBoost = true;
    private bool _isSpeedBoosting = false;
    private bool _canSpeedBoost = true; // set to false by SpeedBoostBar.cs when the bar's Value is 0

    /// <summary>
    /// Function <c>GetInput</c> is called every frame and makes the ship take actions based on the player's input.
    /// </summary>
    public void GetInput()
    {
        // Boost Handling
        if (!_isTurboBoosting)
        {
            if (_canSpeedBoost && Input.IsActionPressed("speed-boost") && !_isSpeedBoosting)
            {
                _isSpeedBoosting = true;
            }
            else if (!Input.IsActionPressed("speed-boost"))  // seems weird but it doesn't work if I just use 'else'
            {
                _isSpeedBoosting = false;
            }
            if (Input.IsActionJustPressed("turbo-boost"))
            {
                _isTurboBoosting = true;
            }
        }

        // Set ship's horizontal Velocity using the player's Up/Down input
        if (_bottomClamp < Position.Y || Position.Y < _shipSize / 2) // topClamp = 0 + _shipSize/2;
        {
            // TODO - Screen Edge Bouncing should use Collision objects in prep for chutes
            Velocity = Velocity with { Y = -Velocity.Y };
        }
        else
        {
            Velocity = Transform.X * Input.GetAxis("up", "down") * _currentSpeed;
        }
    }

    private void BoostAndReturn()
    {
        if (_isTurboBoosting)
        {
            if (Position.X < _defaultPositionX + _boostPixelsToMove)
            {
                Velocity = -Transform.Y * _currentSpeed * _boostDelta * _boostPixelsToMove;
            }
            if (Position.X >= _defaultPositionX + _boostPixelsToMove)
            {
                _isTurboBoosting = false;
                _isReturningFromTurboBoost = true;
            }
        }
        else if (Position.X > _defaultPositionX && _isReturningFromTurboBoost)
        {
            Velocity = Velocity with { X = 0 }; // small X velocity is introduced during GetInput, so we set it to 0 to avoid bugs
            Position = Position with { X = Position.X - (((_boostDelta * _currentSpeed) + BoostSpeed) * (1.0f / BoostSpeed)) };
        }
        else if (Position.X <= _defaultPositionX)
        {
            Position = Position with { X = _defaultPositionX };
            _isReturningFromTurboBoost = false;
        }
    }

    public override void _Ready()
    {
        // can be moved back into _PhysicsProcess if the shipsize is going to change during TurboBoost
        _shipSize = GetNode<CollisionShape2D>("ship_CollisionShape2D").Shape.GetRect().Size.X;
        _bottomClamp = 480 - _shipSize / 2; // TODO - 480 should be a variable set by the screen size from the main scene
    }

    public override void _PhysicsProcess(double delta)
    {        
        _boostDelta = BoostSpeed * (float)delta;
        int doubleSpeed = _defaultSpeed * 2;
        if (_isSpeedBoosting)
            _currentSpeed = doubleSpeed;
        else
            _currentSpeed = _defaultSpeed;

        if (!_isTurboBoosting && !_isReturningFromTurboBoost)
        {
            GetInput();
        }
        BoostAndReturn();
        MoveAndSlide();
    }
}
