using Godot;
using Godot.Collections;

namespace Game.Behavior.Check;

public partial class CheckFail : AbstractChecker
{
    public override bool Check(Node entity, params Variant[] signalParam)
    {
        return false;
    }
}