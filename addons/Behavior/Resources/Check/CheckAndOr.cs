using System.Linq;
using Behavior.Resources.Define;
using Godot;
using Godot.Collections;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckAndOr : Checker
{
    public bool Or { get; set; } = true;
    [Export] public Array<Checker> Checkers { get; set; } = new();

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        if (Checkers.Count == 0) return true;

        bool Predicate(Checker checker) => checker.Check(behavior, signal, signalArgs);
        return Or ? Checkers.Any(Predicate) : Checkers.All(Predicate);
    }
}