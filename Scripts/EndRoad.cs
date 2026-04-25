using Godot;
using System;
using System.IO;

public partial class EndRoad : Area3D
{
	[Export(PropertyHint.File)]

	public String NextLevelPath = "";
	public void Complete(Player body){
		if(NextLevelPath !=""){
			GetTree().ChangeSceneToFile(NextLevelPath);
		}
		
	}
}
