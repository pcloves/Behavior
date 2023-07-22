using Behavior.Resources.Define;
using Godot;

namespace Behavior.Resources.Checker;

[Tool]
[GlobalClass]
public partial class CheckInvert : BehaviorChecker
{
    [Export] public BehaviorChecker Checker { get; set; }

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return Checker?.Check(behavior, signal, signalArgs) ?? false;
    }
}