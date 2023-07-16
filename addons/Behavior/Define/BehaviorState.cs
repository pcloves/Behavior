using System.Linq;
using Behavior.StateMachine;
using Godot;
using Godot.Collections;

namespace Behavior.Define;

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

    public void OnStateEnter(global::Behavior.BehaviorAi behaviorAi)
    {
        behaviorAi.EmitSignal(global::Behavior.BehaviorAi.SignalName.StateEnter, Id);
    }

    public void OnStateExit(global::Behavior.BehaviorAi behaviorAi)
    {
        behaviorAi.EmitSignal(global::Behavior.BehaviorAi.SignalName.StateExit, Id);
    }

    public void OnSignal(global::Behavior.BehaviorAi behaviorAi, StringName signal, params Variant[] args)
    {
        var units = Units.Where(unit => unit.Signal.Equals(signal));
        
        foreach (var unit in units)
        {
            var checkers = unit.Checker;
            var actions = unit.Actions;

            if (!checkers.Check(behaviorAi, signal, args)) continue;

            foreach (var action in actions)
            {
                action.Execute(behaviorAi, signal, args);
            }
        }
    }
}