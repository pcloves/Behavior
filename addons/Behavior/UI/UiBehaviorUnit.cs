using Behavior.Define;
using Behavior.Extensions;
using Godot;
using Godot.Collections;

namespace Behavior.UI;

[Tool]
public partial class UiBehaviorUnit : PanelContainer
{
    private const string UiBehaviorCheckersScenePath = "res://addons/Behavior/UI/UiBehaviorCheckers.tscn";
    private const string UiBehaviorActionScenePath = "res://addons/Behavior/UI/UiBehaviorAction.tscn";

    private static readonly PackedScene UiBehaviorCheckersPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckersScenePath);

    private static readonly PackedScene UiBehaviorActionPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorActionScenePath);

    private Button _reorder;

    private OptionButton _signalOption;

    private UiBehaviorCheckers _checkers;
    private HBoxContainer _hBoxContainer;
    private VSeparator _checkersActionsSeparator;

    private HBoxContainer _actions;
    private Button _addAction;

    private CheckButton _active;

    private Button _remove;

    public UiBehaviorState UiBehaviorStateBelong { get; set; }
    public BehaviorUnit BehaviorUnit { get; set; }

    public override void _Ready()
    {
        _reorder = GetNodeOrNull<Button>("%Reorder");

        _signalOption = GetNodeOrNull<OptionButton>("%Signal");
        _signalOption.ItemSelected += OnSignalOptionItemSelected;

        _signalOption.Clear();
        foreach (var (signal, id) in BehaviorAi.SignalName2Id)
        {
            _signalOption.AddItem(signal, id);
        }

        _signalOption.Select(string.IsNullOrEmpty(BehaviorUnit?.Signal) ? -1 : _signalOption.GetItemIndex(BehaviorAi.SignalName2Id[BehaviorUnit.Signal]));

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

    private void OnSignalOptionItemSelected(long index)
    {
        var id = _signalOption.GetItemId((int)index);
        BehaviorUnit.Signal = BehaviorAi.SignalId2Name[id];
    }

    private void InitUiBehaviorAction()
    {
        foreach (var action in BehaviorUnit?.Actions ?? new Array<ActionResource>())
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
        UiBehaviorStateBelong.BehaviorState?.Units.Remove(BehaviorUnit);

        QueueFree();
    }

    private void OnAddActionPressed(ActionResource actionResource = null)
    {
        var uiBehaviorAction = UiBehaviorActionPackedScene.Instantiate<UiBehaviorAction>();

        uiBehaviorAction.BehaviorUnitBelong = BehaviorUnit;
        uiBehaviorAction.ActionResource = actionResource;

        _actions.AddChildBefore(uiBehaviorAction, _addAction);
    }

    public override string ToString()
    {
        return $"{nameof(UiBehaviorUnit)}:{BehaviorUnit}";
    }
}