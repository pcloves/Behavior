using Godot;
using CheckerResource = Behavior.Define.CheckerResource;

namespace Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckFail : CheckerResource
{
    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return false;
    }
}