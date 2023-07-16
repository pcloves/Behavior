using Godot;
using CheckerResource = Behavior.Resources.Define.CheckerResource;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckInvert : CheckerResource
{
    [Export] public CheckerResource CheckerResource { get; set; }

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return CheckerResource?.Check(behavior, signal, signalArgs) ?? false;
    }
}