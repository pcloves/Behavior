using Game.addons.Behavior.Extensions;
using Game.Behavior;
using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Action;

[RegisteredType(nameof(ActionAwaitTime))]
[Tool]
public partial class ActionAwaitTime : BehaviorAction
{

    /// <summary>
    /// 计算时间时间范围下限（包括）
    /// </summary>
    [Export(PropertyHint.Range, "0,10000,or_greater")]
    public double TimeSecondMin { get; set; }

    /// <summary>
    /// 计算时间时间范围上限（包括）
    /// </summary>
    [Export(PropertyHint.Range, "0,10000")]
    public double TimeSecondMax { get; set; }

    public override async void Execute(Node entity, params Variant[] signalArgs)
    {
        var comBehavior = entity.GetFirstChild<ComBehavior>();
        var randomSecond =
            GD.RandRange(Mathf.Min(TimeSecondMin, TimeSecondMax), Mathf.Max(TimeSecondMin, TimeSecondMax));

        await comBehavior.ToSignal(entity.GetTree().CreateTimer(randomSecond), Timer.SignalName.Timeout);
    }
}