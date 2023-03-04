using Godot;
using Godot.Collections;

namespace Game.Behavior.Action;

/// <summary>
/// 行为执行器
/// </summary>
public abstract partial class AbstractAction : Resource
{
    /// <summary>
    /// 行为执行操作
    /// </summary>
    /// <param name="signalParam">Signal附带的参数</param>
    public abstract void Execute(params Variant[] signalParam);
}