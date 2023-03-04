using Game.Behavior.Action;
using Game.Behavior.Check;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Game.Behavior;

[RegisteredType(nameof(BehaviorUnit))]
public partial class BehaviorUnit : Resource
{
    [Export] public string _signal { get; set; }
    [Export] public Array<BehaviorChecker> _checkers { get; set; }
    [Export] public Array<BehaviorAction> _actions { get; set; }
}