using Godot;
using Godot.Collections;

namespace Behavior.addons.Behavior.Define;

[Tool]
public partial class BehaviorState : Resource
{
    [Export] public string Id { get; set; } = "New Behavior State";
    [Export] public bool Active { get; set; } = true;
    [Export] public Array<BehaviorUnit> Units { get; private set; } = new();
}