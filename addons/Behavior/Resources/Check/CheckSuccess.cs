﻿using Behavior.Resources.Define;
using Godot;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckSuccess : Checker
{
    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return true;
    }
}