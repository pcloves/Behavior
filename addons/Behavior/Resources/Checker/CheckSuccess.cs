using Behavior.Resources.Define;
using Godot;

namespace Behavior.Resources.Checker;

[Tool]
[GlobalClass]
public partial class CheckSuccess : BehaviorChecker
{
    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return true;
    }
}