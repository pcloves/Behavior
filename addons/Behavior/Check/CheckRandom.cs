using System;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckRandom))]
[Tool]
public partial class CheckRandom : BehaviorChecker
{
    private static readonly Random Random = new();

    [Export] public Array<BehaviorChecker> _checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (_checkers.Count == 0)
        {
            return false;
        }

        return _checkers[Random.Next(_checkers.Count)].Check(entity, signalArgs);
    }
}