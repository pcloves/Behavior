using Godot;

namespace Behavior.addons.Behavior.Check;

[Tool]
public partial class CheckSuccess : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return true;
    }
}