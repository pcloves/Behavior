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

    public override void _Ready()
    {
        base._Ready();

        // ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }

    public void ChangeState(string stateName)
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

        CurrentSate = BehaviorDefine.BehaviorStates.First(state => state.Id.Equals(stateName));

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
}