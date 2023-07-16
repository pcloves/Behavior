using System.Linq;
using Behavior.Define;
using Godot;
using Godot.Collections;

namespace Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckAndOr : CheckerResource
{
    public bool Or { get; set; } = true;
    [Export] public Array<CheckerResource> Checkers { get; set; } = new();

    public override bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        bool Predicate(CheckerResource checker) => checker.Check(behaviorAi, signal, signalArgs);
        return Or ? Checkers.Any(Predicate) : Checkers.All(Predicate);
    }
}