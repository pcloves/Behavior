using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior;

[RegisteredType(nameof(BehaviorDefine))]
public partial class BehaviorDefine : Resource
{
    [Export] public Array<BehaviorState> BehaviorStates { get; set; } = new();
}