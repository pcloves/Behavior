using Game.Behavior;
using Game.Extensions;
using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Action;

[RegisteredType(nameof(ActionCreateTimer))]
public partial class ActionCreateTimer : BehaviorAction
{
    public ActionCreateTimer()
    {
        GD.Print("!!!!!!!!!!!!");
    }

    [Export] public string TimerName { get; set; }

    [Export] public double TimeSecond { get; set; }

    public override async void Execute(Node entity, params Variant[] signalArgs)
    {
        var comBehavior = entity.GetFirstChild<ComBehavior>();

        await comBehavior.ToSignal(entity.GetTree().CreateTimer(TimeSecond), Timer.SignalName.Timeout);

        GD.Print("timeout, id:", TimerName);

        comBehavior.EmitSignal(ComBehavior.SignalName.Timeout, TimerName);
    }
}