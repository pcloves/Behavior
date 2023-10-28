using Godot;

namespace BehaviorAI;

[Tool]
[GlobalClass]
public partial class ActionChangeState : BehaviorAction
{
    [Export] public string NewStateId { get; set; }

    public override void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.ChangeState(NewStateId);
    }
}