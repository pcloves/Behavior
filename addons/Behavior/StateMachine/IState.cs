using Godot;

namespace Behavior.StateMachine;

public interface IState
{
    public void OnStateEnter(BehaviorAi behaviorAi);
    public void OnStateExit(BehaviorAi behaviorAi);
    public void OnSignal(BehaviorAi behaviorAi, StringName signal, params Variant[] args);
}