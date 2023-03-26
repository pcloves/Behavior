using System.Linq;
using Game.addons.Behavior;
using Godot;

namespace Game.Behavior;

public partial class ComBehavior : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    private BehaviorState CurrentSate { get; set; }

    private string _signal;

    public void ChangeState(string stateId)
    {
        if (CurrentSate != null)
        {
            EmitSignal("StateExit", CurrentSate.Id);

            var signals = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signals)
            {
                Disconnect(signal, new Callable(this, nameof(OnSignal)));
            }
        }

        CurrentSate = BehaviorDefine.BehaviorStates.First(state => state.Id.Equals(stateId));

        if (CurrentSate != null)
        {
            var signals = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signals)
            {
                if (!HasSignal(signal))
                {
                    AddUserSignal(signal);
                }

                Connect(signal, new Callable(this, nameof(OnSignal)));
            }

            EmitSignal("StateEnter", CurrentSate.Id);
        }
    }


    public bool hasState(string stateId)
    {
        return (BehaviorDefine?.BehaviorStates.Select(state => state.Id.Equals(stateId))).Any();
    }
}