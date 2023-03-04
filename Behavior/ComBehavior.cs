using Godot;
using Godot.Collections;

namespace Game.Behavior;

public partial class ComBehavior : Node
{
    [Export(PropertyHint.ResourceType, hintString: nameof(Behavior.BehaviorDefine))]
    public BehaviorDefine BehaviorDefine { get; set; }

    public override void _Ready()
    {
        base._Ready();

        GD.Print("define:", BehaviorDefine?._behaviorUnits.Count ?? 0);

        foreach (var unit in BehaviorDefine?._behaviorUnits ?? new Array<BehaviorUnit>())
        {
            var signal = unit._signal;
            var checkers = unit._checkers;
            var actions = unit._actions;

            GD.Print("signal:", signal);

            foreach (var checker in checkers)
            {
                GD.Print("check:", checker.GetType().Name);
            }

            foreach (var action in actions)
            {
                GD.Print("action:", action.GetType().Name);
            }
        }
    }
}