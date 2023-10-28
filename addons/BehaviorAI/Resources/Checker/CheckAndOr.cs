using System.Linq;
using Godot;
using Godot.Collections;

namespace BehaviorAI;

[Tool]
[GlobalClass]
public partial class CheckAndOr : BehaviorChecker
{
    public bool Or { get; set; } = true;
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        return Or
            ? Checkers.Any(checker => checker.Check(behavior, signal, signalArgs))
            : Checkers.All(checker => checker.Check(behavior, signal, signalArgs));
    }
}