using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavior.addons.Behavior.Define;
using Behavior.addons.Behavior.StateMachine;
using Godot;

namespace Behavior.addons.Behavior;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public partial class BehaviorAi : Node, IStateMachine
{
    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    public Node Parent { get; private set; }

    private BehaviorState _stateCurrent;

    public override void _EnterTree()
    {
        Parent = GetParent<Node>();
        ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    public void ChangeState(string stateId)
    {
        var stateNew = BehaviorDefine?.BehaviorStates.FirstOrDefault(state => state.Id.Equals(stateId), null);

        if (_stateCurrent != null)
        {
            _stateCurrent.OnStateExit(this);

            var signalsOld = _stateCurrent.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsOld)
            {
                DisconnectSignal(signal);
            }
        }

        GD.Print($"{nameof(ChangeState)} from {_stateCurrent?.Id ?? "null"} to {stateNew?.Id ?? "null"}");

        _stateCurrent = stateNew;

        if (_stateCurrent != null)
        {
            var signalsNew = _stateCurrent.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsNew)
            {
                ConnectSignal(signal);
            }

            _stateCurrent.OnStateEnter(this);
        }
    }
}