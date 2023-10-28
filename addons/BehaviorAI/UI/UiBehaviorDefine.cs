using Godot;
using Godot.Collections;

namespace BehaviorAI;

[Tool]
public partial class UiBehaviorDefine : ScrollContainer
{
    private const string UiBehaviorStateScenePath = "res://addons/BehaviorAI/UI/UiBehaviorState.tscn";

    private static readonly PackedScene UiBehaviorStatePackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorStateScenePath);

    public BehaviorDefine BehaviorDefine { get; set; }

    private VBoxContainer _vBoxContainer;
    private Button _new;

    public override void _Ready()
    {
        base._Ready();

        _vBoxContainer = GetNodeOrNull<VBoxContainer>("%VBoxContainer");

        _new = GetNodeOrNull<Button>("%New");
        _new.Pressed += OnNewPressed;
        _new.Disabled = false;

        foreach (var behaviorState in BehaviorDefine?.BehaviorStates ?? new Array<BehaviorState>())
        {
            AddNewBehaviorState(behaviorState);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        BehaviorDefine = null;
    }

    private void AddNewBehaviorState(BehaviorState state, bool editName = false)
    {
        var uiBehaviorState = UiBehaviorStatePackedScene.Instantiate<UiBehaviorState>();
        uiBehaviorState.BehaviorDefine = BehaviorDefine;
        uiBehaviorState.BehaviorState = state;
        if (editName)
        {
            uiBehaviorState.SetMeta("editName", true);
        }

        _vBoxContainer.AddChildBefore(uiBehaviorState, _new);
    }

    private void OnNewPressed()
    {
        var behaviorState = new BehaviorState();
        BehaviorDefine.BehaviorStates.Add(behaviorState);

        AddNewBehaviorState(behaviorState, true);
    }
}