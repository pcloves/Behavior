using Game.addons.Behavior;
using Game.addons.Behavior.Action;
using Game.addons.Behavior.Extensions;
using Godot;

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