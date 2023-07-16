using Behavior.Core;
using Godot;

namespace Behavior.Resources.Define;

public abstract partial class ActionResource : Resource, IAction
{
    public abstract void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs);

    public override string ToString()
    {
        return $"Type:{GetType().Name}";
    }
}