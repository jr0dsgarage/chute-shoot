using Godot;
using System;

public partial class SpeedBoostBar : ProgressBar
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
		// _shipRef.Speed gets multiplied by 10 during a get call, and Speed is doubled during SpeedBoosting, so we divide by 20 to get the actual speed.
		double rateOfDecrease = 0.5f + (_shipRef.Speed * delta) / 20;
		// Decrease the bar's value while the ship is boosting, and slowly increase it back to 100% when not boosting.
		if (_shipRef.IsSpeedBoosting)
            Value -= rateOfDecrease;
		else
            Value += rateOfDecrease * 0.75f;
		
		// If the bar's value is 0, the ship can't speed boost anymore, until it reaches 100% again.
		if (Value <= 0)
		{
			_shipRef.IsSpeedBoosting = false;
			_shipRef.CanSpeedBoost = false;
		}
		else if (Value >= 100)
			_shipRef.CanSpeedBoost = true;
	}
}
