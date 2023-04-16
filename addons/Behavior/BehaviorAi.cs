using System.Collections.Generic;
using System.Linq;
using Behavior.addons.Behavior.Define;
using Godot;

namespace Behavior.addons.Behavior;

public partial class BehaviorAi : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    private BehaviorState _state;

    private readonly IDictionary<string, IEnumerable<BehaviorUnit>> _signal2Units =
        new Dictionary<string, IEnumerable<BehaviorUnit>>();

    public override void _EnterTree()
    {
        ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }

    public void ChangeState(string stateId)
    {
        var state = BehaviorDefine?.BehaviorStates.First(state => state.Id.Equals(stateId));
        if (state == null)
        {
            GD.PrintErr($"{nameof(ChangeState)} failed due to null stateId:{stateId}");
            return;
        }

        if (_state != null)
        {
            EmitSignal(SignalName.StateExit, _state.Id);

            var signalsOld = _state.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsOld)
            {
                DisconnectSignal(signal);
                _signal2Units.Remove(signal);
            }
        }

#if DEBUG
        if (_signal2Units.Count != 0)
        {
            GD.PrintErr(
                $"signal remains while {nameof(ChangeState)} from {_state?.Id ?? "null"} to {stateId}, remain signal:{_signal2Units.Keys}");
        }
#endif

        GD.Print($"{nameof(ChangeState)} from {_state?.Id ?? "null"} to {state.Id}");

        _state = state;

        var signalsNew = _state.Units.Select(unit => unit.Signal).Distinct();
        foreach (var signal in signalsNew)
        {
            ConnectSignal(signal);
            _signal2Units.Add(signal, _state.Units.Where(unit => unit.Signal.Equals(signal)));
        }

        EmitSignal(SignalName.StateEnter, _state.Id);
    }
}