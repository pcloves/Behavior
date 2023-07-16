using System;
using Godot;
using Godot.Collections;
using CheckerResource = Behavior.Define.CheckerResource;

namespace Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckRandom : CheckerResource
{
    private static readonly Random Random = new();

    [Export] public Array<CheckerResource> Checkers { get; set; } = new();

    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return false;

        return Checkers[Random.Next(Checkers.Count)].Check(behavior, signal, signalArgs);
    }
}