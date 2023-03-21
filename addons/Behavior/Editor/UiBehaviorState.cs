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
    private TextureButton? _removeButton;
    private Button? _showButton;
    private Button? _newButton;
    private Label? _label;
    private UiBehaviorUnit? _demo;
    private bool _isExpand = true;

    public override void _Ready()
    {
        _vBoxContainer = GetNodeOrNull<VBoxContainer>("%VBoxContainer");

        _removeButton = GetNodeOrNull<TextureButton>("%Remove");
        _removeButton.Pressed += OnRemoveButtonPressed;

        _showButton = GetNodeOrNull<Button>("%Show");
        _showButton.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _newButton = GetNodeOrNull<Button>("%New");
        _newButton.Connect(BaseButton.SignalName.Pressed, Callable.From(() => NewBehaviorUnit()));

        _label = GetNodeOrNull<Label>("%Label");
        _label.Text = BehaviorState?.Id ?? "Error";

        _demo = GetNodeOrNull<UiBehaviorUnit>("%Demo");
        _demo.Visible = false;
        
        foreach (var unit in BehaviorState?.Units ?? new Array<BehaviorUnit>()) NewBehaviorUnit(unit);
    }

    private void NewBehaviorUnit(BehaviorUnit? behaviorUnit = default)
    {
        behaviorUnit ??= new BehaviorUnit();

        var uiBehaviorUnit = UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>();
        uiBehaviorUnit.BehaviorUnit = behaviorUnit;

        BehaviorState?.Units.Add(behaviorUnit);

        _vBoxContainer.AddChildBefore(uiBehaviorUnit, _newButton);
    }

    private void OnRemoveButtonPressed()
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