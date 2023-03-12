using Godot;
using System;

public partial class bgLayer : TextureRect
{
	[Export]
	public float ScrollSpeed { get => _scrollSpeedMultiplier ; set => _scrollSpeedMultiplier = value; }

	private float _scrollSpeedMultiplier = 1.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// bg_layers are twice the screen width and tiled 
		if (Position.X >= -640)
		{
			Position = Position with { X = Position.X - (1.0f * _scrollSpeedMultiplier) };
		}
		if (Position.X <= -640)
		{
			Position = Position with { X = 0.0f };
		}
	}
}
