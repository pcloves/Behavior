using System;
using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Check;

[Tool]
public partial class CheckRandom : BehaviorChecker 
{
    private static readonly Random Random = new();

    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();
    
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return false;

        return Checkers[Random.Next(Checkers.Count)].Check(entity, signalArgs);
    }
}