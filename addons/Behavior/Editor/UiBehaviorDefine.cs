using Game.addons.Behavior.Extensions;
using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorDefine : ScrollContainer 
{
    private const string UiBehaviorStateScenePath = "res://addons/Behavior/Editor/UiBehaviorState.tscn";

    private static readonly PackedScene UiBehaviorStatePackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorStateScenePath);

    public BehaviorDefine BehaviorDefine { get; set; }

    private VBoxContainer _vBoxContainer;
    private Button _new;

    private UiBehaviorState _demo;

    public override void _Ready()
    {
        base._Ready();

        _vBoxContainer = GetNodeOrNull<VBoxContainer>("%VBoxContainer");
        
        _new = GetNodeOrNull<Button>("%New");
        _new.Pressed += OnNewPressed;
        _new.Disabled = false;

        _demo = GetNodeOrNull<UiBehaviorState>("%Demo");
        _demo.Visible = false;
        
        
        foreach (var state in BehaviorDefine?.BehaviorStates ?? new Array<BehaviorState>())
        {
            AddNewBehaviorState(state);
        }
    }
    
    public override void _ExitTree()
    {
        base._ExitTree();

        BehaviorDefine = null;
    }
    
    private void AddNewBehaviorState(BehaviorState state)
    {
        var uiBehaviorState = UiBehaviorStatePackedScene.Instantiate<UiBehaviorState>();
        uiBehaviorState.BehaviorState = state;

        _vBoxContainer.AddChildBefore(uiBehaviorState, _new);
    }

    private void OnNewPressed()
    {
        var behaviorState = new BehaviorState();
        BehaviorDefine.BehaviorStates.Add(behaviorState);

        AddNewBehaviorState(behaviorState);
    }
}