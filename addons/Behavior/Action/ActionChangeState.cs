using Godot;

namespace Behavior.addons.Behavior.Action;

[Tool]
[GlobalClass]
public partial class ActionChangeState : BehaviorAction
{
    [Export] public string NewStateId { get; set; }

    public override void Execute(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        behaviorAi.ChangeState(NewStateId);
    }
}