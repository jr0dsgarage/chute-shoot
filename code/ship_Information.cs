using Godot;
using System;

// Prints currently relevant info about the player controlled ship_CharacterBody2D onto the game screen
public partial class ship_Information : Label
{
	private ship_CharacterBody2D _shipRef;
	private puff_GPUParticles2D _puffRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 _shipRef = GetNode<ship_CharacterBody2D>("/root/main/ship_CharacterBody2D");
		 _puffRef = GetNode<puff_GPUParticles2D>("/root/main/ship_CharacterBody2D/puff_GPUParticles2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $@"XPos: {_shipRef.Position.X}
				Ypos: {_shipRef.Position.Y}
				XVel: {_shipRef.Velocity.X}
				YVel: {_shipRef.Velocity.Y}
				Speed:{_shipRef.Speed}
				VPM: {_puffRef.VelocityPuffModifier} (not-used)";
	}
}
