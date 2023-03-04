using Godot;
using System;

public partial class Main : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var characterBody2D = GetNodeOrNull<CharacterBody2D>("Name");
        
        GD.Print("CharacterBody2D:", characterBody2D?.Name);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}