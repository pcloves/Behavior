using Godot;
using ActionResource = Behavior.Define.ActionResource;

namespace Behavior.Action;

[Tool]
[GlobalClass]
public partial class ActionCreateTimer : ActionResource
{
    /// <summary>
    /// 计算器的名称
    /// </summary>
    [Export]
    public string TimerName { get; set; }

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

    public override void Execute(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        var min = Mathf.Min(TimeSecondMin, TimeSecondMax);
        var max = Mathf.Max(TimeSecondMin, TimeSecondMax);
        var randomSecond = GD.RandRange(min, max);

        var timer = behaviorAi.GetTree().CreateTimer(randomSecond);

        timer.Timeout += () => behaviorAi.EmitSignal(BehaviorAi.SignalName.Timeout, TimerName);
    }
}