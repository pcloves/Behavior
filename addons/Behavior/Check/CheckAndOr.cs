using System;
using Godot;
using Godot.Collections;
using System.Linq;

namespace Behavior.addons.Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckAndOr : BehaviorChecker
{
    public bool Or { get; set; } = true;
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        bool Predicate(BehaviorChecker checker) => checker.Check(behaviorAi, signal, signalArgs);
        return Or ? Checkers.Any(Predicate) : Checkers.All(Predicate);
    }
}