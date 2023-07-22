using Godot;

namespace Behavior.Resources.Define;

public abstract partial class BehaviorChecker : Resource
{
    public abstract bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs);

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}