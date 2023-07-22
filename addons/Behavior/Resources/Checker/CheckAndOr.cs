using System.Linq;
using Behavior.Resources.Define;
using Godot;
using Godot.Collections;

namespace Behavior.Resources.Checker;

[Tool]
[GlobalClass]
public partial class CheckAndOr : BehaviorChecker
{
    public bool Or { get; set; } = true;
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        bool Predicate(BehaviorChecker checker) => checker.Check(behavior, signal, signalArgs);
        return Or ? Checkers.Any(Predicate) : Checkers.All(Predicate);
    }
}