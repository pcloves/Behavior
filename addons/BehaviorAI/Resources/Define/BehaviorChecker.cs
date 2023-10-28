using Godot;

namespace BehaviorAI;

public abstract partial class BehaviorChecker : Resource
{
    public abstract bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs);

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}