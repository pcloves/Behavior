using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior;

[RegisteredType(nameof(BehaviorState))]
[Tool]
public partial class BehaviorState : Resource
{
    [Export] public string Id { get; set; } = "New Behavior State";
    [Export] public Array<BehaviorUnit> Units { get; private set; } = new();
}