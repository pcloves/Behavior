using Godot;
using ActionResource = Behavior.Define.ActionResource;

namespace Behavior.Action;

[Tool]
[GlobalClass]
public partial class ActionChangeState : ActionResource
{
    [Export] public string NewStateId { get; set; }

    public override void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.ChangeState(NewStateId);
    }
}