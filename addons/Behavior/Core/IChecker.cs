using Godot;

namespace Behavior.Core;

public interface IChecker
{
    /// <summary>
    /// 对实体和信号进行检查，返回true表示检查通过
    /// </summary>
    /// <param name="behavior">触发检查器的Behavior实例</param>
    /// <param name="signal">Godot信号的字符表达形式</param>
    /// <param name="signalArgs">Signal的参数</param>
    /// <returns></returns>
    public bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs);
}