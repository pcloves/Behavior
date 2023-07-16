using Behavior.Define;
using Godot;

namespace Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckSuccess : CheckerResource
{
    public override bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        return true;
    }
}