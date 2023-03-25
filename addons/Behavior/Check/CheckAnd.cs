using System.Linq;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckAnd))]
[Tool]
public partial class CheckAnd : BehaviorChecker
{
    [Export] public Array<BehaviorChecker> Checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        return Checkers.All(checker => checker.Check(entity, signalArgs));
    }
}