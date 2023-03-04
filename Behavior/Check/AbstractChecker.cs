using Godot;
using Godot.Collections;

namespace Game.Behavior.Check;

/// <summary>
/// Signal检查器
/// </summary>
public abstract partial class AbstractChecker : Resource
{
    /// <summary>
    /// 对实体和信号进行检查
    /// </summary>
    /// <param name="entity">要进行检查的实体</param>
    /// <param name="signalParam">Signal的参数</param>
    /// <returns></returns>
    public abstract bool Check(Node entity, params Variant[] signalParam);
}