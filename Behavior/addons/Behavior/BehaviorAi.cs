using Godot;
using System.Linq;

namespace Behavior.addons.Behavior;

public partial class BehaviorAi : Node
{
    [Signal]
    public delegate void TimeoutEventHandler(string timerName);
    [Signal]
    public delegate void StateEnterEventHandler(string stateId);

    [Signal]
    public delegate void StateExitEventHandler(string stateId);

    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public Define.BehaviorDefine BehaviorDefine { get; set; }

    private Define.BehaviorState CurrentSate { get; set; }

    public override void _EnterTree()
    {
        ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }

    public override void _Ready()
    {
    }

    public void ChangeState(string stateId)
    {
        if (CurrentSate != null)
        {
            var errorStateExit = EmitSignal(SignalName.StateExit, CurrentSate.Id);
            if (errorStateExit != Error.Ok)
            {
                GD.PrintErr($"{nameof(EmitSignal)} failed, signal:StateExit, {nameof(CurrentSate)}:{CurrentSate.Id}");
            }

            var signalsOld = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsOld)
            {
                DisconnectSignal(signal);
            }
        }

        CurrentSate = BehaviorDefine.BehaviorStates.First(state => state.Id.Equals(stateId));

        if (CurrentSate == null) return;

        var signalsNew = CurrentSate.Units.Select(unit => unit.Signal).Distinct();
        foreach (var signal in signalsNew)
        {
            ConnectSignal(signal);
        }

        var errorStateEnter = EmitSignal(SignalName.StateEnter, CurrentSate.Id);
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