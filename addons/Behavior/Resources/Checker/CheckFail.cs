using Behavior.Resources.Define;
using Godot;

namespace Behavior.Resources.Checker;

[Tool]
[GlobalClass]
public partial class CheckFail : BehaviorChecker
{
    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return false;
    }
}