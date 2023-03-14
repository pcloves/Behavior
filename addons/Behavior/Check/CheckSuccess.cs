using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

[RegisteredType(nameof(CheckSuccess))]
[Tool]
public partial class CheckSuccess : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return true;
    }
}