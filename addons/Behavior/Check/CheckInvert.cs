using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckInvert))]
[Tool]
public partial class CheckInvert : BehaviorChecker
{
    [Export] public BehaviorChecker Checker { get; set; }

    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return !Checker.Check(entity, signalArgs);
    }
}