using Godot;
using System;

public partial class LanternGlass : Area3D
{
	[Signal]

	public delegate void LanternCollectedEventHandler();
	
	LevelManager levelmanager;

	//Node3D player;

	[Export]

	public float spin_speed = 2.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		levelmanager = GetTree().Root.GetNode<LevelManager>("LevelManager");
		//player = GetTree().Root.GetNode<Node3D>("character-female-a");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		RotateY(spin_speed *(float)(delta));
	}

	public void Collected(Node3D body){
		EmitSignal("LanternCollected");
		levelmanager.AddLight(0.5f);
		QueueFree();

	}
}
