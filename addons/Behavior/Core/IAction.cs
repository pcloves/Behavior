using Godot;

namespace Behavior.Core;

public interface IAction
{
    /// <summary>
    /// 行为执行操作
    /// </summary>
    /// <param name="behavior"></param>
    /// <param name="signal"></param>
    /// <param name="signalArgs">Signal附带的参数</param>
    public void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs);
}