using Godot;
using Godot.Collections;
using System;

public partial class Ui : CanvasLayer
{
	public Label lightLevel_lbl;
	public float lightLevel = 2.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lightLevel_lbl = GetNode<Label>("hb/LightLevel");
		Array<Node> lanterns = GetTree().GetNodesInGroup("lanterns");
		for (int i = 0; i < lanterns.Count; i++){
			LanternGlass n = (LanternGlass)lanterns[i];
			n.LanternCollected += () => AddLight();

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddLight(){
		lightLevel = lightLevel +1;
		lightLevel_lbl.Text = "Light Level: " +lightLevel.ToString();
	}
}
