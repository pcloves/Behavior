using System.Linq;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckAnd))]
[Tool]
public partial class CheckAnd : BehaviorChecker
{
    [Export] public Array<BehaviorChecker> _checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        if (_checkers.Count == 0)
        {
            return false;
        }

        return _checkers.All(checker => checker.Check(entity, signalArgs));
    }
}