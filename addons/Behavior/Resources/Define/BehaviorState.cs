using System.Linq;
using Godot;
using Godot.Collections;

namespace Behavior.Resources.Define;

[Tool]
public partial class BehaviorState : Resource
{
    [Export] public string Id { get; set; } = "New Behavior State";
    [Export] public bool Active { get; set; } = true;
    [Export] public Array<BehaviorUnit> Units { get; private set; } = new();

    public override string ToString()
    {
        return $"{nameof(Id)}:{Id}, {nameof(Active)}:{Active}, {nameof(Units)}:{Units.Count}";
    }

    public void OnStateEnter(Core.Behavior behavior)
    {
        behavior.EmitSignal(Core.Behavior.SignalName.StateEnter, Id);
    }

    public void OnStateExit(Core.Behavior behavior)
    {
        behavior.EmitSignal(Core.Behavior.SignalName.StateExit, Id);
    }

    public void OnSignal(Core.Behavior behavior, StringName signal, params Variant[] args)
    {
        var units = Units.Where(unit => unit.Signal.Equals(signal)).ToArray();

        if (units.Length == 0) return;

        GD.Print($" signal:{signal}");
        foreach (var unit in units)
        {
            var checkers = unit.Checker;
            var actions = unit.Actions;

            if (!checkers.Check(behavior, signal, args)) continue;

            foreach (var action in actions)
            {
                GD.Print($"  action:{action}");
                action.Execute(behavior, signal, args);
            }
        }
    }
}
