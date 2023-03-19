using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorDefine : VBoxContainer
{
    private static readonly string UiBehaviorStateScenePath = "res://addons/Behavior/Editor/UiBehaviorState.tscn";
    private static readonly PackedScene PackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorStateScenePath);

    public BehaviorDefine BehaviorDefine { get; set; }
    private TextureButton _new;

    public override void _Ready()
    {
        base._Ready();

        _new = GetNodeOrNull<TextureButton>("%New");
        _new.Pressed += OnNewPressed;

        if (BehaviorDefine == null) return;

        foreach (var state in BehaviorDefine.BehaviorStates)
        {
            AddNewBehaviorState(state);
        }
    }

    private void AddNewBehaviorState(BehaviorState state)
    {
        var uiBehaviorState = PackedScene.Instantiate<UiBehaviorState>();
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