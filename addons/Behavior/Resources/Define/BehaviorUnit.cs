using Behavior.Resources.Checker;
using Godot;
using Godot.Collections;

namespace Behavior.Resources.Define;

[Tool]
public partial class BehaviorUnit : Resource
{
    [Export] public string Signal { get; set; } = "";
    [Export] public bool Active { get; set; } = true;
    [Export] public CheckAndOr Checker { get; set; } = new();
    [Export] public Array<BehaviorAction> Actions { get; set; } = new();
    
    public override string ToString()
    {
        return $"{nameof(Signal)}:{Signal}, {nameof(Active)}:{Active}, {nameof(CheckAndOr)}:{Checker?.GetType().Name}, {nameof(Actions)}:{Actions.Count}";
    }
}