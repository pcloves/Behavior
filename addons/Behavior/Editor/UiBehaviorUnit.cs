using System;
using Game.addons.Behavior.Action;
using Game.addons.Behavior.Check;
using Game.addons.Behavior.Extensions;
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
        _active.Pressed += () => BehaviorUnit.Active = _active.ButtonPressed;

        _checkers = GetNodeOrNull<HBoxContainer>("%Checkers");
        _addChecker = GetNodeOrNull<TextureButton>("%AddChecker");
        _addChecker.Pressed += () => NewOptionButton(_checkers, _addChecker, typeof(BehaviorChecker));
        
        NewOptionButton(_checkers, _addChecker, typeof(BehaviorChecker));

        _actions = GetNodeOrNull<HBoxContainer>("%Actions");
        _addAction = GetNodeOrNull<TextureButton>("%AddAction");
        _addAction.Pressed += () => NewOptionButton(_actions, _addAction, typeof(BehaviorAction));
        
        NewOptionButton(_actions, _addAction, typeof(BehaviorAction));

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

    private void NewOptionButton(Node parent, Node brother, Type type)
    {
        var optionButton = new OptionButton();

        foreach (var action in BehaviorPlugin.GetBehaviorTypes(type).Keys)
        {
            optionButton.AddItem(action);
        }
        
        parent.AddChildBefore(optionButton, brother);
    }
}