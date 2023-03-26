using System.Linq;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckAndOr))]
[Tool]
public partial class CheckAndOr : BehaviorChecker 
{
    public bool Or { get; set; } = true;
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        return Or
            ? Checkers.Any(checker => checker.Check(entity, signalArgs))
            : Checkers.All(checker => checker.Check(entity, signalArgs));
    }
}