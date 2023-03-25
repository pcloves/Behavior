#nullable enable
using Game.addons.Behavior.Extensions;
using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorState : MarginContainer
{
    private const string UiBehaviorUnitPath = "res://addons/Behavior/Editor/UiBehaviorUnit.tscn";

    private static readonly PackedScene
        UiBehaviorUnitPackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorUnitPath);

    public BehaviorState? BehaviorState { get; set; }

    private VBoxContainer? _vBoxContainer;
    private Button? _remove;
    private Button? _show;
    private Button? _new;
    private Label? _label;
    private UiBehaviorUnit? _demo;
    private bool _isExpand = true;

    public override void _Ready()
    {
        _vBoxContainer = GetNodeOrNull<VBoxContainer>("%VBoxContainer");

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;

        _show = GetNodeOrNull<Button>("%Show");
        _show.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _new = GetNodeOrNull<Button>("%New");
        _new.Connect(BaseButton.SignalName.Pressed, Callable.From(() => NewBehaviorUnit()));

        _label = GetNodeOrNull<Label>("%Label");
        _label.Text = BehaviorState?.Id ?? "Error";

        _demo = GetNodeOrNull<UiBehaviorUnit>("%Demo");
        _demo.Visible = false;

        foreach (var unit in BehaviorState?.Units ?? new Array<Define.BehaviorUnit>()) NewBehaviorUnit(unit);
    }

    private void NewBehaviorUnit(Define.BehaviorUnit? behaviorUnit = default)
    {
        behaviorUnit ??= new Define.BehaviorUnit();

        var uiBehaviorUnit = UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>();
        uiBehaviorUnit.BehaviorUnit = behaviorUnit;

        if (!BehaviorState!.Units.Contains(behaviorUnit))
        {
            BehaviorState.Units.Add(behaviorUnit);
        }

        _vBoxContainer.AddChildBefore(uiBehaviorUnit, _new);
    }

    private void OnRemovePressed()
    {
        var uiBehaviorDefine = GetParent().GetParent<UiBehaviorDefine>();
        uiBehaviorDefine.BehaviorDefine.BehaviorStates.Remove(BehaviorState);

        GetParent().RemoveChild(this);

        QueueFree();
    }

    private void Expand()
    {
        _isExpand = !_isExpand;

        foreach (var node in _vBoxContainer!.GetChildren())
        {
            var child = (Control)node;
            if (!_isExpand && child.HasMeta("no_hide") && child.GetMeta("no_hide").AsBool()) continue;

            child.Visible = _isExpand;
        }
    }
}