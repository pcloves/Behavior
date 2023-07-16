using Godot;

namespace Behavior.addons.Behavior.StateMachine;

public interface IState
{
    public void OnStateEnter(Node entity);
    public void OnStateExit(Node entity);
    public void OnSignal(StringName signal, params Variant[] args);
}