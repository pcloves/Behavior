using Godot;

namespace Game.addons.Behavior.Check;

[Tool]
public partial class CheckFail : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return false;
    }
}