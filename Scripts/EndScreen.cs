using Godot;
using System;

public partial class EndScreen : Control
{
	[Export(PropertyHint.File)]
	public String BackToMenu = "";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void PlayAgain(){
		GetTree().ChangeSceneToFile(BackToMenu);
	}

}
