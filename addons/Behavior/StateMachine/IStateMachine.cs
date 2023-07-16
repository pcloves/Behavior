using Godot;

namespace Behavior.addons.Behavior.StateMachine;

public interface IStateMachine
{
    public void ChangeState(string stateId);
}