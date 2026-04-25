using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class LevelManager : Node
{
	public float lightLevel = 2.0f;
	
	public void AddLight(float amount){
		lightLevel+=amount;
	}

	public void Reset(){
		lightLevel = 2.5f;
	}
}
