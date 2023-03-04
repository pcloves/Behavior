using System.Linq;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.Behavior.Check;

[RegisteredType(nameof(CheckOr))]
public partial class CheckOr : BehaviorChecker
{
    [Export] public Array<BehaviorChecker> _checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalParam)
    {
        if (_checkers.Count == 0)
        {
            return false;
        }

        return _checkers.Any(checker => checker.Check(entity, signalParam));
    }
}