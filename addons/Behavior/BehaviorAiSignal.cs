﻿using System;
using System.Linq;
using Godot;

namespace Game.addons.Behavior;

public partial class BehaviorAi
{
    private string _signal;
    
    public new Error EmitSignal(StringName signal, params Variant[] args)
    {
        _signal = signal;
        Error error;
        try
        {
            error = base.EmitSignal(signal, args);
        }
        catch (Exception e)
        {
            GD.PrintErr("exception caught, message:", e.Message);
            error = Error.Failed;
        }
        finally
        {
            _signal = null;
        }

        return error;
    }

    private void OnSignal(params Variant[] args)
    {
        if (CurrentSate == null)
        {
            GD.PrintErr($"{nameof(CurrentSate)} is null!");
            return;
        }

        var units = CurrentSate.Units.Where(unit => unit.Signal.Equals(_signal));
        foreach (var unit in units)
        {
            var checkers = unit.Checker;
            var actions = unit.Actions;

            var node = GetParentOrNull<Node>();
            if (!checkers.Check(node, args)) continue;

            foreach (var action in actions)
            {
                action.Execute(node, args);
            }
        }
    }

    private void OnSignal()
    {
        OnSignal(new Variant[] { });
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