using Godot;

namespace Behavior.addons.Behavior.Action;

/// <summary>
/// 行为执行器
/// </summary>
[Tool]
public abstract partial class BehaviorAction : Resource
{
    /// <summary>
    /// 行为执行操作
    /// </summary>
    /// <param name="behaviorAi"></param>
    /// <param name="signal"></param>
    /// <param name="signalArgs">Signal附带的参数</param>
    public virtual void Execute(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
    }

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}