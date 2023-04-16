using System.Threading.Tasks;
using Behavior.addons.Behavior.Extensions;
using Godot;

namespace Behavior.addons.Behavior.Action;

[Tool]
public partial class ActionCreateTimer : BehaviorAction
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

    public override async Task Execute(Node parent, params Variant[] signalArgs)
    {
        var comBehavior = parent.GetFirstChild<BehaviorAi>();
        var randomSecond =
            GD.RandRange(Mathf.Min(TimeSecondMin, TimeSecondMax), Mathf.Max(TimeSecondMin, TimeSecondMax));

        await comBehavior.ToSignal(parent.GetTree().CreateTimer(randomSecond), Timer.SignalName.Timeout);

        comBehavior.EmitSignal(BehaviorAi.SignalName.Timeout, TimerName);
    }
}