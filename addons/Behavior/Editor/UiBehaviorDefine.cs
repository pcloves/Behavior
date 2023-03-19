using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorDefine : VBoxContainer
{
    private const string UiBehaviorStateScenePath = "res://addons/Behavior/Editor/UiBehaviorState.tscn";
    private static readonly PackedScene UiBehaviorStatePackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorStateScenePath);

    public BehaviorDefine BehaviorDefine { get; set; }
    private Button _new;

    public override void _Ready()
    {
        base._Ready();

        _new = GetNodeOrNull<Button>("%New");
        _new.Pressed += OnNewPressed;
        _new.Disabled = false;

        // this.RemoveFirstChild<UiBehaviorState>();

        if (BehaviorDefine == null) return;

        foreach (var state in BehaviorDefine.BehaviorStates)
        {
            AddNewBehaviorState(state);
        }
    }

    private void AddNewBehaviorState(BehaviorState state)
    {
        var uiBehaviorState = UiBehaviorStatePackedScene.Instantiate<UiBehaviorState>();
        uiBehaviorState.BehaviorState = state;

        this.AddChildBefore(uiBehaviorState, _new);
    }

    private void OnNewPressed()
    {
        var behaviorState = new BehaviorState();
        BehaviorDefine.BehaviorStates.Add(behaviorState);
        
        AddNewBehaviorState(behaviorState);
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        BehaviorDefine = null;
    }
}