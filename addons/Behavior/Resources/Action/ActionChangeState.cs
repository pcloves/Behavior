using Godot;
using ActionResource = Behavior.Resources.Define.ActionResource;

namespace Behavior.Resources.Action;

[Tool]
[GlobalClass]
public partial class ActionChangeState : ActionResource
{
    [Export] public string NewStateId { get; set; }

    public override void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.ChangeState(NewStateId);
    }
}