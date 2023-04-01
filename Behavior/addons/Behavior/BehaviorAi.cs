using System.Linq;
using Godot;

namespace Behavior.addons.Behavior;

public partial class BehaviorAi : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public Define.BehaviorDefine BehaviorDefine { get; set; }

    private Define.BehaviorState CurrentSate { get; set; }

    public override void _Ready()
    {
        ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }
    
    public void ChangeState(string stateId)
    {
        if (CurrentSate != null)
        {
            var errorStateExit = EmitSignal("StateExit", CurrentSate.Id);
            if (errorStateExit != Error.Ok)
            {
                GD.PrintErr($"{nameof(EmitSignal)} failed, signal:StateExit, {nameof(CurrentSate)}:{CurrentSate.Id}");
            }

            var signalsOld = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsOld)
            {
                Disconnect(signal, new Callable(this, nameof(OnSignal)));
            }
        }

        CurrentSate = BehaviorDefine.BehaviorStates.First(state => state.Id.Equals(stateId));

        if (CurrentSate == null) return;
        
        var signalsNew = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
        foreach (var signal in signalsNew)
        {
            if (!HasSignal(signal))
            {
                AddUserSignal(signal);
            }

            Connect(signal, new Callable(this, nameof(OnSignal)));
        }

        var errorStateEnter = EmitSignal("StateEnter", CurrentSate.Id);
        if (errorStateEnter != Error.Ok)
        {
            GD.PrintErr($"{nameof(EmitSignal)} failed, signal:StateEnter, {nameof(CurrentSate)}:{CurrentSate.Id}");
        }
    }


    public bool HasState(string stateId)
    {
        return BehaviorDefine?.BehaviorStates.Select(state => state.Id.Equals(stateId)).Any() ?? false;
    }

}