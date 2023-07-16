using System;
using Godot;
using Godot.Collections;
using CheckerResource = Behavior.Resources.Define.CheckerResource;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckRandom : CheckerResource
{
    private static readonly Random Random = new();

    [Export] public Array<CheckerResource> Checkers { get; set; } = new();

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return false;

        return Checkers[Random.Next(Checkers.Count)].Check(behavior, signal, signalArgs);
    }
}