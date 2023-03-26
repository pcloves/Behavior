using Game.addons.Behavior.Extensions;
using Game.Behavior;
using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Action;

[RegisteredType(nameof(ActionChangeState))]
[Tool]
public partial class ActionChangeState : BehaviorAction
{
    [Export]
    public string NewStateId { get; set; }

    public override void Execute(Node entity, params Variant[] signalArgs)
    {
        var comBehavior = entity.GetFirstChild<ComBehavior>();
        
        if (!comBehavior.hasState(NewStateId))
        {
            GD.PrintErr($"The {nameof(NewStateId)} doesn't exist.");
            return;
        }
        
        comBehavior.ChangeState(NewStateId);
    }
}