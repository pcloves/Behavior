using Behavior.Resources.Define;
using Godot;

namespace Behavior.Resources.Check;

[Tool]
[GlobalClass]
public partial class CheckInvert : Checker
{
    [Export] public Checker Checker { get; set; }

    public override bool Check(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        return Checker?.Check(behavior, signal, signalArgs) ?? false;
    }
}