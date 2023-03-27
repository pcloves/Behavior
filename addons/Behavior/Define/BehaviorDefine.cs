using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Define;

[Tool]
public partial class BehaviorDefine : Resource
{
    [Export] public string Name;
    [Export] public Array<BehaviorState> BehaviorStates { get; set; } = new();
}