using Godot;

namespace Behavior.addons.Behavior.Check;

/// <summary>
/// Signal检查器
/// </summary>
[Tool]
public abstract partial class BehaviorChecker : Resource
{
    /// <summary>
    /// 对实体和信号进行检查
    /// </summary>
    /// <param name="parent">BehaviorAi的parent</param>
    /// <param name="signalArgs">Signal的参数</param>
    /// <returns></returns>
    public virtual bool Check(Node parent, params Variant[] signalArgs)
    {
        return false;
    }

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}