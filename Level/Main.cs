using Game.Extensions;
using Godot;

namespace Game.Level;

public partial class Main : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var characterBody2D = this.GetFirstChild<CharacterBody2D>();

        GD.Print("CharacterBody2D:", characterBody2D?.Name);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}