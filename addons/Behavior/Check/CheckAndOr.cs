using Godot;
using Godot.Collections;
using System.Linq;

namespace Behavior.addons.Behavior.Check;

[Tool]
public partial class CheckAndOr : BehaviorChecker
{
    public bool Or { get; set; } = true;
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();

    public override bool Check(Node parent, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        return Or
            ? Checkers.Any(checker => checker.Check(parent, signalArgs))
            : Checkers.All(checker => checker.Check(parent, signalArgs));
    }
}