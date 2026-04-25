using Godot;
using System;

public partial class GameCam : Node3D
{
    public Node3D target;

    public override void _Ready()
    {
        target = GetParent().GetNode<Node3D>("character-female-a");
    }

    public override void _Process(double delta)
    {
        Position = target.Position;
    }
}

