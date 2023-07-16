using System.Diagnostics.CodeAnalysis;
using Godot;

namespace Behavior.Define;

/// <summary>
/// Signal检查器
/// </summary>
[Tool]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public abstract partial class CheckerResource : Resource
{
    /// <summary>
    /// 对实体和信号进行检查
    /// </summary>
    /// <param name="behaviorAi"></param>
    /// <param name="signal"></param>
    /// <param name="signalArgs">Signal的参数</param>
    /// <returns></returns>
    public virtual bool Check(BehaviorAi behaviorAi, StringName signal, params Variant[] signalArgs)
    {
        return false;
    }

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}