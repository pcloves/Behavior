using Game.addons.Behavior.Action;
using Game.addons.Behavior.Define;
using Game.addons.Behavior.Extensions;
using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorUnit : PanelContainer
{
    private const string UiBehaviorCheckersScenePath = "res://addons/Behavior/Editor/UiBehaviorCheckers.tscn";
    private const string UiBehaviorActionScenePath = "res://addons/Behavior/Editor/UiBehaviorAction.tscn";

    private static readonly PackedScene UiBehaviorCheckersPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckersScenePath);

    private static readonly PackedScene UiBehaviorActionPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorActionScenePath);


    private LineEdit _signal;

    private UiBehaviorCheckers _checkers;
    private HBoxContainer _hBoxContainer;
    private VSeparator _checkersActionsSeparator;

    private HBoxContainer _actions;
    private Button _addAction;

    private CheckButton _active;

    private Button _remove;

    public BehaviorState BehaviorStateBelong { get; set; }
    public BehaviorUnit BehaviorUnit { get; set; }

    public override void _Ready()
    {
        _signal = GetNodeOrNull<LineEdit>("%Signal");
        _signal.TextChanged +=  OnSignalTextChanged;
        _signal.Text = BehaviorUnit?.Signal;

        _hBoxContainer = GetNodeOrNull<HBoxContainer>("%HBoxContainer");
        _checkersActionsSeparator = GetNodeOrNull<VSeparator>("%CheckersActionsSeparator");

        _active = GetNodeOrNull<CheckButton>("%Active");
        _active.ButtonPressed = BehaviorUnit?.Active ?? false;
        _active.Pressed += () => BehaviorUnit.Active = _active.ButtonPressed;

        _actions = GetNodeOrNull<HBoxContainer>("%Actions");
        _addAction = GetNodeOrNull<Button>("%AddAction");
        _addAction.Pressed += () => OnAddActionPressed();

        InitUiBehaviorAction();

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;
        _remove.Size = new Vector2(Mathf.Max(_remove.Size.X, _remove.Size.Y),
            Mathf.Max(_remove.Size.X, _remove.Size.Y));

        InitUiBehaviorCheckers();
    }

    private void OnSignalTextChanged(string newText)
    {
        BehaviorUnit.Signal = newText;
    }

    private void InitUiBehaviorAction()
    {
        foreach (var action in BehaviorUnit?.Actions ?? new Array<BehaviorAction>())
        {
            OnAddActionPressed(action);
        }

        if (BehaviorUnit == null || BehaviorUnit.Actions.Count == 0)
        {
            OnAddActionPressed();
        }
    }

    private void InitUiBehaviorCheckers()
    {
        _checkers = UiBehaviorCheckersPackedScene.Instantiate<UiBehaviorCheckers>();
        _checkers.CheckerAndOr = BehaviorUnit?.Checker;
        _checkers.CheckerAndOrBelong = null;
        _checkers.SizeFlagsHorizontal = SizeFlags.Fill | SizeFlags.Expand;

        _hBoxContainer.AddChildBefore(_checkers, _checkersActionsSeparator);
    }

    private void OnRemovePressed()
    {
        BehaviorStateBelong?.Units.Remove(BehaviorUnit);

        QueueFree();
    }

    private void OnAddActionPressed(BehaviorAction behaviorAction = null)
    {
        var uiBehaviorAction = UiBehaviorActionPackedScene.Instantiate<UiBehaviorAction>();

        uiBehaviorAction.BehaviorUnitBelong = BehaviorUnit;
        uiBehaviorAction.Action = behaviorAction;

        _actions.AddChildBefore(uiBehaviorAction, _addAction);
    }
}