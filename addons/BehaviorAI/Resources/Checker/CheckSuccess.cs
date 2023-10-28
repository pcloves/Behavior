using Godot;

namespace BehaviorAI;

[Tool]
[GlobalClass]
public partial class CheckSuccess : BehaviorChecker
{
    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return true;
    }
}