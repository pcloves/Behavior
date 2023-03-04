using System;
using Game.Behavior.Action;
using Game.Behavior.Check;
using Godot;
using Godot.Collections;

namespace Game.Behavior;

public partial class BehaviorUnit : Resource
{
    [Export] public string _signal { get; set; }
    [Export] public Array<AbstractChecker> _checkers { get; set; }
    [Export] public Array<AbstractAction> _actions { get; set; }
}