using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Check;

/// <summary>
/// Signal检查器
/// </summary>
[RegisteredType(nameof(BehaviorChecker))]
[Tool]
public partial class BehaviorChecker : Resource
{
    /// <summary>
    /// 对实体和信号进行检查
    /// </summary>
    /// <param name="entity">要进行检查的实体</param>
    /// <param name="signalArgs">Signal的参数</param>
    /// <returns></returns>
    public virtual bool Check(Node entity, params Variant[] signalArgs)
    {
        return false;
    }
}