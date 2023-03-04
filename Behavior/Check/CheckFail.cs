using Godot;
using MonoCustomResourceRegistry;

namespace Game.Behavior.Check;

[RegisteredType(nameof(CheckFail))]
public partial class CheckFail : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalParam)
    {
        return false;
    }
}