using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavior.Resources.Define;
using Godot;
using BehaviorDefine = Behavior.Resources.Define.BehaviorDefine;

namespace Behavior.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public partial class Behavior : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    private BehaviorState _stateCurrent;

    private readonly Blackboard _blackboard = new();

    public override void _EnterTree()
    {
        AddChild(_blackboard);
        ChangeState(BehaviorDefine?.BehaviorStates[0].Id);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _blackboard.ClearData();
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    public void ChangeState(string stateId)
    {
        var stateNew =
            BehaviorDefine?.BehaviorStates.FirstOrDefault(state => state.Id.Equals(stateId) && state.Active, null);

        if (_stateCurrent != null)
        {
            _stateCurrent.OnStateExit(this);

            var signalsOld = _stateCurrent.Units.Select(unit => unit.Signal).Distinct();
            foreach (var signal in signalsOld)
            {
                DisconnectSignal(signal);
            }
        }

        GD.Print($"{nameof(ChangeState)}: {_stateCurrent?.Id ?? "null"} -> {stateNew?.Id ?? "null"}");

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