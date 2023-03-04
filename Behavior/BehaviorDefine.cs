using Godot;
using Godot.Collections;

namespace Game.Behavior;

public partial class BehaviorDefine : Resource
{
    [Export] public Array<BehaviorUnit> _behaviorUnits { get; set; }
}