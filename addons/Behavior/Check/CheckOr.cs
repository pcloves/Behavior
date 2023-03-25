using System.Linq;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckOr))]
[Tool]
public partial class CheckOr : BehaviorChecker
{
    [Export] public Array<BehaviorChecker> Checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        return Checkers.Any(checker => checker.Check(entity, signalArgs));
    }
}