using Godot;
using CheckerResource = Behavior.Resources.Define.CheckerResource;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckSuccess : CheckerResource
{
    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return true;
    }
}