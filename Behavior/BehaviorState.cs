using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.Behavior;

[RegisteredType(nameof(BehaviorState))]
public partial class BehaviorState : Resource
{
    [Export] public string Id { get; private set; }
    [Export] public Array<BehaviorUnit> Units { get; private set; }
}