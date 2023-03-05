using Game.Extensions;
using Godot;
using MonoCustomResourceRegistry;

namespace Game.Behavior.Action;

[RegisteredType(nameof(ActionCreateTimer))]
public partial class ActionCreateTimer : BehaviorAction
{
    [Export]
    private string timerName;

    [Export]
    private double timeSecond;
    
    public override async void Execute(Node entity, params Variant[] signalArgs)
    {
        var comBehavior = entity.GetFirstChild<ComBehavior>();

        await comBehavior.ToSignal(entity.GetTree().CreateTimer(timeSecond), Timer.SignalName.Timeout);
        
        GD.Print("timeout, id:", timerName);

        comBehavior.EmitSignal(ComBehavior.SignalName.Timeout, timerName);
    }
}