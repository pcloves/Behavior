using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorUnit : MarginContainer
{
    private HBoxContainer _checkers;
    private Button _addChecker;


    private HBoxContainer _actions;
    private Button _addAction;

    private CheckButton _active;

    private TextureButton _remove;

    public BehaviorUnit BehaviorUnit { get; set; }

    public override void _Ready()
    {
        _active = GetNodeOrNull<CheckButton>("%Active");
        _active.ButtonPressed = BehaviorUnit?.Active ?? false;

        _checkers = GetNodeOrNull<HBoxContainer>("%Checkers");
        _addChecker = GetNodeOrNull<Button>("%AddChecker");

        _actions = GetNodeOrNull<HBoxContainer>("%Actions");
        _addAction = GetNodeOrNull<Button>("%AddAction");

        _remove = GetNodeOrNull<TextureButton>("%Remove");
        _remove.Pressed += OnRemovePressed;
    }

    private void OnRemovePressed()
    {
        var behaviorState = GetOwner<UiBehaviorState>();
        behaviorState.BehaviorState.Units.Remove(BehaviorUnit);
        
        GetParent().RemoveChild(this);
        
        QueueFree();
    }
}