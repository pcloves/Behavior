using Godot;

namespace BehaviorAI;

[Tool]
[GlobalClass]
public partial class CheckInvert : BehaviorChecker
{
    [Export] public BehaviorChecker Checker { get; set; }

    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return Checker?.Check(behavior, signal, signalArgs) ?? false;
    }
}