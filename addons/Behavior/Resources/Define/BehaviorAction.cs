using Godot;

namespace Behavior.Resources.Define;

public abstract partial class BehaviorAction : Resource
{
    public abstract void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs);

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}