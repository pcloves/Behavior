using Game.addons.Behavior.Check;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using BehaviorAction = Game.addons.Behavior.Action.BehaviorAction;

namespace Game.addons.Behavior;

[RegisteredType(nameof(BehaviorUnit))]
[Tool]
public partial class BehaviorUnit : Resource
{
    [Export] public string Signal { get; set; }
    [Export] public Array<BehaviorChecker> Checkers { get; set; } = new();
    [Export] public Array<BehaviorAction> Actions { get; set; } = new();
}