using Godot;

namespace Behavior.addons.Behavior.Check;

[Tool]
public partial class CheckInvert : BehaviorChecker
{
    [Export] public BehaviorChecker Checker { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return !Checker.Check(entity, signalArgs);
    }
}