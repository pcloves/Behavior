using Godot;

namespace Game.addons.Behavior.Check;

[Tool]
public partial class CheckSuccess : BehaviorChecker
{
    public override bool Check(Node entity, params Variant[] signalArgs)
    {
        return true;
    }
}