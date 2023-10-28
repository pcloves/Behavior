using Godot;

namespace BehaviorAI;

public abstract partial class BehaviorAction : Resource
{
    public abstract void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs);

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}