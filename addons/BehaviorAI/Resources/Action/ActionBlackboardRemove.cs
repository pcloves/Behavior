using Godot;

namespace BehaviorAI;

[GlobalClass]
[Tool]
public partial class ActionBlackboardRemove : BehaviorAction
{
    [Export] public string Key;

    public override void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.GetFirstChild<Blackboard>().RemoveData(Key);
    }
}