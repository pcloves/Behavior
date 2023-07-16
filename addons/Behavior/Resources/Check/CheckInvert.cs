using Godot;
using CheckerResource = Behavior.Define.CheckerResource;

namespace Behavior.Check;

[Tool]
[GlobalClass]
public partial class CheckInvert : CheckerResource
{
    [Export] public CheckerResource CheckerResource { get; set; }

    public override bool Check(Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return CheckerResource?.Check(behavior, signal, signalArgs) ?? false;
    }
}