using Godot;
using System;

// Prints currently relevant info about the player controlled ship_CharacterBody2D onto the game screen
public partial class ship_Information : Label
{
	ship_CharacterBody2D shipRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 shipRef = GetNode<ship_CharacterBody2D>("/root/main/ship_CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(shipRef.Position.X);
		Text = $"XPos: {shipRef.Position.X}\nYpos: {shipRef.Position.Y}\nXVel: {shipRef.Velocity.X}\nYVel: {shipRef.Velocity.Y}\nSpeed:{shipRef.Speed}";
	}
}
