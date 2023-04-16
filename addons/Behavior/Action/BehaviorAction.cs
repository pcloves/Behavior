using System;
using System.Threading.Tasks;
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
    /// <param name="parent">BehaviorAi的parent</param>
    /// <param name="signalArgs">Signal附带的参数</param>
    public virtual async Task Execute(Node parent, params Variant[] signalArgs)
    {
    }

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}