using Godot;
using System;

public partial class MainMenu : Control
{
	[Export(PropertyHint.File)] 
	
	public String LevelOnePath = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void Start(){
		GetTree().ChangeSceneToFile(LevelOnePath);
	}
}
