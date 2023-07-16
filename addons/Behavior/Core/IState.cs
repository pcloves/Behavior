using Godot;

namespace Behavior.Core;

public interface IState
{
    public void OnStateEnter(Behavior behavior);
    public void OnStateExit(Behavior behavior);
    public void OnSignal(Behavior behavior, StringName signal, params Variant[] args);
}