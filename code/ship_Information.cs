using Godot;
using System;

// Prints currently relevant info about the player controlled ship_CharacterBody2D onto the game screen
public partial class ship_Information : Label
{
    private static ship_CharacterBody2D _shipRef;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _shipRef = GetNode<ship_CharacterBody2D>(path: "/root/main/ship_CharacterBody2D");
}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
		bool hotkeyToggle = Input.IsActionJustPressed(action: "debugInfoToggle");
		if (Visible)
		{
			Text = $@"Ship Information:
					XPos: {_shipRef.Position.X}
					Ypos: {_shipRef.Position.Y}
					XVel: {_shipRef.Velocity.X}
					YVel: {_shipRef.Velocity.Y}
					Speed:{_shipRef.Speed}
					CanSpeed: {_shipRef.CanSpeedBoost}
					Speeding: {_shipRef.IsSpeedBoosting}
					TBoosting: {_shipRef.IsTurboBoosting}
					Returning: {_shipRef.IsReturning}
					";
			if (hotkeyToggle)
			{
				Hide();
			}
		}
		else if (hotkeyToggle)
		{
				Show();
		}        
    }
}
