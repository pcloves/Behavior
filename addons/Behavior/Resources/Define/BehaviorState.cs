using System.Linq;
using Behavior.Define;
using Behavior.StateMachine;
using Godot;
using Godot.Collections;

namespace Behavior.addons.Behavior.Define;

[Tool]
public partial class BehaviorState : Resource, IState
{
    [Export] public string Id { get; set; } = "New Behavior State";
    [Export] public bool Active { get; set; } = true;
    [Export] public Array<BehaviorUnit> Units { get; private set; } = new();

    public override string ToString()
    {
        return $"{nameof(Id)}:{Id}, {nameof(Active)}:{Active}, {nameof(Units)}:{Units.Count}";
    }

    public void OnStateEnter(global::Behavior.Behavior behavior)
    {
        behavior.EmitSignal(global::Behavior.Behavior.SignalName.StateEnter, Id);
    }

    public void OnStateExit(global::Behavior.Behavior behavior)
    {
        behavior.EmitSignal(global::Behavior.Behavior.SignalName.StateExit, Id);
    }

    public void OnSignal(global::Behavior.Behavior behavior, StringName signal, params Variant[] args)
    {
        var units = Units.Where(unit => unit.Signal.Equals(signal));
        
        foreach (var unit in units)
        {
            var checkers = unit.Checker;
            var actions = unit.Actions;

            if (!checkers.Check(behavior, signal, args)) continue;

            foreach (var action in actions)
            {
                action.Execute(behavior, signal, args);
            }
        }
    }
}