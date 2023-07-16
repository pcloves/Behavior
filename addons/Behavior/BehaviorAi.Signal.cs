using System.Diagnostics.CodeAnalysis;
using Godot;

namespace Behavior.addons.Behavior;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public partial class BehaviorAi
{
    private void OnSignal(params Variant[] args)
    {
        var signal = args[0].AsString();
        if (!_signal2Units.ContainsKey(signal))
        {
            GD.PrintErr($"the signal:{signal} is not connected!");
            return;
        }

        var units = _signal2Units[signal];
        foreach (var unit in units)
        {
            var checkers = unit.Checker;
            var actions = unit.Actions;

            if (!checkers.Check(this, signal, args)) continue;

            foreach (var action in actions)
            {
                action.Execute(this, signal, args);
            }
        }
    }

    private void OnSignal()
    {
        OnSignal(System.Array.Empty<Variant>());
    }

    private void OnSignal(Variant arg1)
    {
        OnSignal(new[] { arg1 });
    }

    private void OnSignal(Variant arg1, Variant arg2)
    {
        OnSignal(new[] { arg1, arg2 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3)
    {
        OnSignal(new[] { arg1, arg2, arg3 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3, Variant arg4)
    {
        OnSignal(new[] { arg1, arg2, arg3, arg4 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3, Variant arg4, Variant arg5)
    {
        OnSignal(new[] { arg1, arg2, arg3, arg4, arg5 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3, Variant arg4, Variant arg5, Variant arg6)
    {
        OnSignal(new[] { arg1, arg2, arg3, arg4, arg5, arg6 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3, Variant arg4, Variant arg5, Variant arg6,
        Variant arg7)
    {
        OnSignal(new[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
    }

    private void OnSignal(Variant arg1, Variant arg2, Variant arg3, Variant arg4, Variant arg5, Variant arg6,
        Variant arg7, Variant arg8)
    {
        OnSignal(new[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
    }
}