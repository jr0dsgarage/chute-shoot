using Godot;
using System;

public partial class ship_Position : Label
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
		Text = $"XPos: {shipRef.Position.X}\nYpos: {shipRef.Position.Y}";
	}
}
