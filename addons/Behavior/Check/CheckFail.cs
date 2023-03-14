﻿using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckFail))]
[Tool]
public partial class CheckFail : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return false;
    }
}