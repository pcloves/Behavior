namespace Behavior.Core;

public interface IStateMachine
{
    public void ChangeState(string stateId);
}