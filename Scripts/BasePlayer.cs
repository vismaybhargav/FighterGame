using Godot;
using System;

public partial class BasePlayer : CharacterBody3D
{
	[Export] public const float Speed = 5.0f;
	[Export] public const float JumpVelocity = 9.8f; // Why this is gravity idk
	[Export] public const float DashVelocity = 10f;
	
	// Internal Vars
	private bool _isDashing;
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		// Add the gravity. 
		if (!IsOnFloor()) velocity.Y -= _gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Handle dash
		if(Input.IsActionJustPressed("")) {
		}

		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
