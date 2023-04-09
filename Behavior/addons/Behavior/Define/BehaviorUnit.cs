using Behavior.addons.Behavior.Action;
using Behavior.addons.Behavior.Check;
using Godot;
using Godot.Collections;

namespace Behavior.addons.Behavior.Define;

[Tool]
public partial class BehaviorUnit : Resource
{
    [Export] public string Signal { get; set; }
    [Export] public bool Active { get; set; } = true;
    [Export] public CheckAndOr Checker { get; set; } = new();
    [Export] public Array<BehaviorAction> Actions { get; set; } = new();
}