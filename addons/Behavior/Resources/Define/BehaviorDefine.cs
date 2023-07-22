using Godot;
using Godot.Collections;

namespace Behavior.Resources.Define;

[Tool]
public partial class BehaviorDefine : Resource
{
    [Export] public string Name;
    [Export] public Array<State> BehaviorStates { get; set; } = new();

    public override string ToString()
    {
        return $"{nameof(Name)}:{Name}, {nameof(BehaviorStates)}:{BehaviorStates.Count}";
    }
}