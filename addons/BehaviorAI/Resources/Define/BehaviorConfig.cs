using Godot;
using Godot.Collections;

namespace BehaviorAI;

[Tool]
[GlobalClass]
public partial class BehaviorConfig : Resource
{
    [Export] public string Name;
    [Export] public Array<BehaviorState> BehaviorStates { get; set; } = new();

    public override string ToString()
    {
        return $"{nameof(Name)}:{Name}, {nameof(BehaviorStates)}:{BehaviorStates.Count}";
    }
}