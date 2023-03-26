using Game.addons.Behavior.Define;
using Godot;
using Godot.Collections;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorState : PanelContainer
{
    private const string UiBehaviorUnitPath = "res://addons/Behavior/Editor/UiBehaviorUnit.tscn";

    private static readonly PackedScene
        UiBehaviorUnitPackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorUnitPath);

    public BehaviorDefine BehaviorDefine { get; set; }
    public BehaviorState BehaviorState { get; set; }

    private VBoxContainer _vBoxContainer;
    private Button _remove;
    private Button _show;
    private Button _newBehaviorUnit;

    private HBoxContainer _changeContainer;
    private LineEdit _nameLineEdit;
    private Button _accept;
    private Button _cancel;

    private HBoxContainer _nameContainer;
    private Label _nameLabel;
    private Button _edit;

    private bool _isExpand = true;

    public override void _Ready()
    {
        _vBoxContainer = GetNodeOrNull<VBoxContainer>("%VBoxContainer");

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;
        _remove.Size = new Vector2(Mathf.Max(_remove.Size.X, _remove.Size.Y),
            Mathf.Max(_remove.Size.X, _remove.Size.Y));

        _show = GetNodeOrNull<Button>("%Show");
        _show.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _newBehaviorUnit = GetNodeOrNull<Button>("%NewBehaviorUnit");
        _newBehaviorUnit.Connect(BaseButton.SignalName.Pressed, Callable.From(() => NewBehaviorUnit()));

        _changeContainer = GetNodeOrNull<HBoxContainer>("%ChangeContainer");
        _changeContainer.Visible = false;
        
        _nameLineEdit = GetNodeOrNull<LineEdit>("%NameLineEdit");
        
        _accept = GetNodeOrNull<Button>("%Accept");
        _accept.Pressed += () => OnFinishChange(true);
        
        _cancel = GetNodeOrNull<Button>("%Cancel");
        _cancel.Pressed += () => OnFinishChange(false);

        _nameContainer = GetNodeOrNull<HBoxContainer>("%NameContainer");
        _nameContainer.Visible = true;
        
        _nameLabel = GetNodeOrNull<Label>("%NameLabel");
        _nameLabel.Text = BehaviorState?.Id ?? "Error";
        
        _edit = GetNodeOrNull<Button>("%Edit");
        _edit.Pressed += OnEditPressed;

        foreach (var unit in BehaviorState?.Units ?? new Array<BehaviorUnit>()) NewBehaviorUnit(unit);
        
        if (HasMeta("editName"))
        {
            OnEditPressed();
        }
    }

    private void OnEditPressed()
    {
        _changeContainer.Visible = true;
        _nameContainer.Visible = false;

        _nameLineEdit.Text = _nameLabel.Text;
        _nameLineEdit.SelectAll();
        _nameLineEdit.GrabFocus();
        _nameLineEdit.CaretColumn = _nameLineEdit.GetSelectionToColumn();
    }

    private void OnFinishChange(bool changed)
    {
        _changeContainer.Visible = false;
        _nameContainer.Visible = true;
        if (changed)
        {
            _nameLabel.Text = _nameLineEdit.Text;
            BehaviorState.Id = _nameLineEdit.Text;
        }
    }

    private void NewBehaviorUnit(BehaviorUnit behaviorUnit = default)
    {
        behaviorUnit ??= new BehaviorUnit();

        var uiBehaviorUnit = UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>();

        uiBehaviorUnit.BehaviorStateBelong = BehaviorState;
        uiBehaviorUnit.BehaviorUnit = behaviorUnit;

        if (!BehaviorState!.Units.Contains(behaviorUnit))
        {
            BehaviorState.Units.Add(behaviorUnit);
        }

        _vBoxContainer.AddChild(uiBehaviorUnit);
    }

    private void OnRemovePressed()
    {
        BehaviorDefine?.BehaviorStates.Remove(BehaviorState);

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

        _newBehaviorUnit.Visible = _isExpand;
    }
}