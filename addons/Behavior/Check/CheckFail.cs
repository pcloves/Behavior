using Godot;

namespace Behavior.addons.Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckFail : BehaviorChecker
{
    public override bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        return false;
    }
}