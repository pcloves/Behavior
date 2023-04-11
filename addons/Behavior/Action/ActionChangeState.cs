using Behavior.addons.Behavior.Extensions;
using Godot;

namespace Behavior.addons.Behavior.Action;

[Tool]
public partial class ActionChangeState : BehaviorAction
{
    [Export]
    public string NewStateId { get; set; }

    public override void Execute(Node entity, params Variant[] signalArgs)
    {
        var comBehavior = entity.GetFirstChild<BehaviorAi>();

        if (!comBehavior.HasState(NewStateId))
        {
            GD.PrintErr($"The {nameof(NewStateId)} doesn't exist.");
            return;
        }

        comBehavior.ChangeState(NewStateId);
    }
}