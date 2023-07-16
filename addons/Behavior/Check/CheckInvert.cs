using Godot;

namespace Behavior.addons.Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckInvert : BehaviorChecker
{
    [Export] public BehaviorChecker Checker { get; set; }

    public override bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        return !Checker.Check(behaviorAi, signal, signalArgs);
    }
}