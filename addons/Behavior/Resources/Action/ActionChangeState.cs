using Godot;

namespace Behavior.Resources.Action;

[Tool]
[GlobalClass]
public partial class ActionChangeState : Define.BehaviorAction
{
    [Export] public string NewStateId { get; set; }

    public override void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.ChangeState(NewStateId);
    }
}