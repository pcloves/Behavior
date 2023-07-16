using System.Linq;
using Godot;
using Godot.Collections;
using CheckerResource = Behavior.Resources.Define.CheckerResource;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckAndOr : CheckerResource
{
    public bool Or { get; set; } = true;
    [Export] public Array<CheckerResource> Checkers { get; set; } = new();

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        bool Predicate(CheckerResource checker) => checker.Check(behavior, signal, signalArgs);
        return Or ? Checkers.Any(Predicate) : Checkers.All(Predicate);
    }
}