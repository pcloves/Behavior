using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorUnit : MarginContainer
{
    private HBoxContainer _checkers;
    private TextureButton _addChecker;

    private HBoxContainer _actions;
    private TextureButton _addAction;

    private CheckButton _active;

    private TextureButton _remove;

    public BehaviorUnit BehaviorUnit { get; set; }

    public override void _Ready()
    {
        _active = GetNodeOrNull<CheckButton>("%Active");
        _active.ButtonPressed = BehaviorUnit?.Active ?? false;

        _checkers = GetNodeOrNull<HBoxContainer>("%Checkers");
        _addChecker = GetNodeOrNull<TextureButton>("%AddChecker");

        _actions = GetNodeOrNull<HBoxContainer>("%Actions");
        _addAction = GetNodeOrNull<TextureButton>("%AddAction");

        _remove = GetNodeOrNull<TextureButton>("%Remove");
        _remove.Pressed += OnRemovePressed;
    }

    private void OnRemovePressed()
    {
        var uiBehaviorState = GetParent().GetParent<UiBehaviorState>();
        uiBehaviorState.BehaviorState.Units.Remove(BehaviorUnit);

        GetParent().RemoveChild(this);

        QueueFree();
    }
}