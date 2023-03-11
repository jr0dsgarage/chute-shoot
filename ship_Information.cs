using Godot;
using System;

// Prints currently relevant info about the player controlled ship_CharacterBody2D onto the game screen
public partial class ship_Information : Label
{
	private ship_CharacterBody2D shipRef;
	private puff_GPUParticles2D puffRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 shipRef = GetNode<ship_CharacterBody2D>("/root/main/ship_CharacterBody2D");
		 puffRef = GetNode<puff_GPUParticles2D>("/root/main/ship_CharacterBody2D/puff_GPUParticles2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $@"XPos: {shipRef.Position.X}
				Ypos: {shipRef.Position.Y}
				XVel: {shipRef.Velocity.X}
				YVel: {shipRef.Velocity.Y}
				Speed:{shipRef.Speed}
				VPM: {puffRef.velocityPuffModifier}";
	}
}
