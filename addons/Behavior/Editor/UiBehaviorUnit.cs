using Game.addons.Behavior.Action;
using Game.addons.Behavior.Check;
using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorUnit : MarginContainer
{
    private HBoxContainer _actions;
    private Button _addAction;

    private CheckButton _active;

    private Button _remove;

    public Define.BehaviorUnit BehaviorUnit { get; set; }

    public override void _Ready()
    {
        _active = GetNodeOrNull<CheckButton>("%Active");
        _active.ButtonPressed = BehaviorUnit?.Active ?? false;
        _active.Pressed += () => BehaviorUnit.Active = _active.ButtonPressed;

        _actions = GetNodeOrNull<HBoxContainer>("%Actions");
        _addAction = GetNodeOrNull<Button>("%AddAction");
        _addAction.Pressed += OnAddActionPressed;
        
        OnAddActionPressed();

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;
    }

    private void OnRemovePressed()
    {
        var uiBehaviorState = GetParent().GetParent<UiBehaviorState>();
        uiBehaviorState.BehaviorState.Units.Remove(BehaviorUnit);

        GetParent().RemoveChild(this);

        QueueFree();
    }

    private void OnAddActionPressed()
    {
        var optionButton = new OptionButton();

        foreach (var behavior in BehaviorPlugin.GetBehaviorTypes(typeof(BehaviorAction)).Keys)
        {
            optionButton.AddItem(behavior);
        }

        _addAction.Disabled = true;
        optionButton.Selected = -1;
        _actions.AddChildBefore(optionButton, _addAction);
    }
}