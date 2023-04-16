using Godot;
using Godot.Collections;
using System;

namespace Behavior.addons.Behavior.Check;

[Tool]
public partial class CheckRandom : BehaviorChecker
{
    private static readonly Random Random = new();

    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(Node parent, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return false;

        return Checkers[Random.Next(Checkers.Count)].Check(parent, signalArgs);
    }
}