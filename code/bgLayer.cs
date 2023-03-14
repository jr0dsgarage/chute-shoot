using Godot;
using System;

public partial class bgLayer : TextureRect
{
	[Export]
	public float ScrollSpeed { get => _scrollSpeedMultiplier ; set => _scrollSpeedMultiplier = value; }
	[Export]
	public bool UseVerticalParallax { get => _verticalParallaxEnabled ; set => _verticalParallaxEnabled = value;}
	private static ship_CharacterBody2D _shipRef;
	private float _scrollSpeedMultiplier = 1.0f;
	private bool _verticalParallaxEnabled;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 _shipRef = GetNode<ship_CharacterBody2D>("/root/main/ship_CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// bg_layers are twice the screen width and tiled, then scaled
		if (Position.X >= -640 * Scale.X)
		{
			Position = Position with { X = Position.X - (1.0f * _scrollSpeedMultiplier) };
		}
		else
		{
			Position = Position with { X = 0.0f };
		}
		if (_verticalParallaxEnabled)
		{
			Position = Position with {Y = Position.Y - (_shipRef.Velocity.Y / _shipRef.Speed / Scale.Y )};
		}
	}
}
