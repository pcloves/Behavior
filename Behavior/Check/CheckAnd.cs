using System.Linq;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Game.Behavior.Check;

public partial class CheckAnd : AbstractChecker
{
    [Export] public Array<AbstractChecker> _checkers { get; set; }

    public override bool Check(Node entity, params Variant[] signalParam)
    {
        if (_checkers.Count == 0)
        {
            return false;
        }

        return _checkers.All(checker => checker.Check(entity, signalParam));
    }
}