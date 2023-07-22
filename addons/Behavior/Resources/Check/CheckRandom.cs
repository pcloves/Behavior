﻿using System;
using Behavior.Resources.Define;
using Godot;
using Godot.Collections;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckRandom : Checker
{
    private static readonly Random Random = new();

    [Export] public Array<Checker> Checkers { get; set; } = new();

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return false;

        return Checkers[Random.Next(Checkers.Count)].Check(behavior, signal, signalArgs);
    }
}