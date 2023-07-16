using Godot;

namespace Behavior.Define;

/// <summary>
/// 行为执行器
/// </summary>
[Tool]
public abstract partial class ActionResource : Resource
{
    /// <summary>
    /// 行为执行操作
    /// </summary>
    /// <param name="behavior"></param>
    /// <param name="signal"></param>
    /// <param name="signalArgs">Signal附带的参数</param>
    public virtual void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
    }

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}