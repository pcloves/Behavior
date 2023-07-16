using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Godot;

namespace Behavior;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public partial class Behavior
{
    private void OnSignal(params Variant[] args)
    {
        _stateCurrent.OnSignal(this, args[0].AsString(), args.Skip(1).ToArray());
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