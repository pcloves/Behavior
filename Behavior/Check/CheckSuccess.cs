﻿using Godot;
using Godot.Collections;

namespace Game.Behavior.Check;

public partial class CheckSuccess : AbstractChecker
{
    public override bool Check(Node entity, params Variant[] signalParam)
    {
        return true;
    }
}