using Godot;
using System;

public partial class puff_GPUParticles2D : GpuParticles2D
{
	[Export]
	public int BasePuffsAmount { get; set; } = 20;

	public int VelocityPuffModifier;

	private ship_CharacterBody2D _shipRef;

 	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		_shipRef = GetNode<ship_CharacterBody2D>("/root/main/ship_CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//currently unused
		VelocityPuffModifier = (int) Math.Clamp(MathF.Abs(_shipRef.Velocity.Y)/10,1,20);
		
		// TODO Using `Amount = basePuffsAmount + VelocityPuffModifier;` causes the emitter to restart, killing all of the previously generated particles 😔
		// Found that this is a bug
	}
}
