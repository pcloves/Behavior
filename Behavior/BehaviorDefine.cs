using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.Behavior;

[RegisteredType(nameof(BehaviorDefine))]
public partial class BehaviorDefine : Resource
{
    [Export] public Array<BehaviorUnit> _behaviorUnits { get; set; }
}