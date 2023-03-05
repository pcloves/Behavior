using System.Linq;
using Godot;

namespace Game.Behavior;

public partial class ComBehavior : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(Behavior.BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    private BehaviorState CurrentSate { get; set; }

    private string _signal;

    public override void _Ready()
    {
        base._Ready();
        
        if (BehaviorDefine != null)
        {
            var units = BehaviorDefine._BehaviorStates;

            var unit = units[0];
            
            ChangeState(unit.Id);
        }

    }

    public void ChangeState(string stateName)
    {
        if (CurrentSate != null)
        {
            EmitSignal("StateExit", CurrentSate.Id);

            var signals = CurrentSate.Units.Select(unit => unit._signal).Distinct();
            foreach (var signal in signals)
            {
                Disconnect(signal, new Callable(this, nameof(OnSignal)));
            }
        }

        CurrentSate = BehaviorDefine._BehaviorStates.First(state => state.Id.Equals(stateName));

        if (CurrentSate != null)
        {
            var signals = CurrentSate.Units.Select(unit => unit._signal).Distinct();
            foreach (var signal in signals)
            {
                AddUserSignal(signal);
                Connect(signal, new Callable(this, nameof(OnSignal)));
            }

            EmitSignal("StateEnter", CurrentSate.Id);
        }
    }
}