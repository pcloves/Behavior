using Godot;

namespace Game.Behavior;

public partial class ComBehavior : Node
{
    [Export(PropertyHint.ResourceType, hintString: "BehaviorDef")]
    public BehaviorDefine _behaviorDefine { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        GD.Print("define:", _behaviorDefine?._behaviorUnits.Count ?? 0);

        foreach (var unit in _behaviorDefine._behaviorUnits)
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


    private void OnSignal(params Variant[] param)
    {
        
    }
    
}