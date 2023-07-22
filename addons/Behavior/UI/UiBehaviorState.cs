using System.Linq;
using Behavior.Resources.Define;
using Godot;
using Godot.Collections;
using BehaviorDefine = Behavior.Resources.Define.BehaviorDefine;
using BehaviorUnit = Behavior.Resources.Define.BehaviorUnit;

namespace Behavior.UI;

[Tool]
public partial class UiBehaviorState : PanelContainer
{
    private const string UiBehaviorUnitPath = "res://addons/Behavior/UI/UiBehaviorUnit.tscn";

    private static readonly PackedScene
        UiBehaviorUnitPackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorUnitPath);

    public BehaviorDefine BehaviorDefine { get; set; }
    public State BehaviorState { get; set; }

    private VBoxContainer _vBoxContainer;

    private Button _isInitState;

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

        _isInitState = GetNodeOrNull<Button>("%IsInitState");
        _isInitState.Visible = BehaviorDefine?.BehaviorStates.FirstOrDefault() == BehaviorState;

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;
        _remove.Size = new Vector2(Mathf.Max(_remove.Size.X, _remove.Size.Y),
            Mathf.Max(_remove.Size.X, _remove.Size.Y));

        _show = GetNodeOrNull<Button>("%Show");
        _show.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _newBehaviorUnit = GetNodeOrNull<Button>("%NewBehaviorUnit");
        _newBehaviorUnit.Connect(BaseButton.SignalName.Pressed,
            Callable.From(() => AddUiBehaviorUnit(UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>())));

        _changeContainer = GetNodeOrNull<HBoxContainer>("%ChangeContainer");
        _changeContainer.Visible = false;

        _nameLineEdit = GetNodeOrNull<LineEdit>("%NameLineEdit");
        _nameLineEdit.TextSubmitted += _ => OnFinishChange(true);
        _nameLineEdit.FocusExited += () => OnFinishChange(true);

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

        foreach (var unit in BehaviorState?.Units ?? new Array<BehaviorUnit>())
        {
            var uiBehaviorUnit = UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>();
            uiBehaviorUnit.BehaviorUnit = unit;

            AddUiBehaviorUnit(uiBehaviorUnit);
        }

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

    public void AddUiBehaviorUnit(UiBehaviorUnit uiBehaviorUnit)
    {
        uiBehaviorUnit.UiBehaviorStateBelong = this;
        uiBehaviorUnit.BehaviorUnit ??= new BehaviorUnit();

        if (!BehaviorState!.Units.Contains(uiBehaviorUnit.BehaviorUnit))
        {
            BehaviorState.Units.Add(uiBehaviorUnit.BehaviorUnit);
        }

        _vBoxContainer.AddChild(uiBehaviorUnit);
    }

    public void RemoveUiBehaviorUnit(UiBehaviorUnit uiBehaviorUnit, bool queueFree = true)
    {
        BehaviorState.Units.Remove(uiBehaviorUnit.BehaviorUnit);

        _vBoxContainer.RemoveChild(uiBehaviorUnit);

        if (queueFree)
        {
            uiBehaviorUnit.QueueFree();
        }
    }

    public void MoveUiBehaviorUnit(UiBehaviorUnit uiBehaviorUnit, int toIndex)
    {
        if (uiBehaviorUnit.UiBehaviorStateBelong != this)
        {
            GD.PrintErr(
                $"doesn't belong to the same {nameof(UiBehaviorState)}, this:{this}, target:{uiBehaviorUnit.UiBehaviorStateBelong}");
            return;
        }

        var units = BehaviorState.Units;
        if (!units.Contains(uiBehaviorUnit.BehaviorUnit))
        {
            GD.PrintErr($"{BehaviorState.Id} doesn't contains {nameof(BehaviorUnit)}:{uiBehaviorUnit.BehaviorUnit}");
            return;
        }

        var fromIndex = units.IndexOf(uiBehaviorUnit.BehaviorUnit);

        if (fromIndex == toIndex)
        {
            return;
        }

        (units[fromIndex], units[toIndex]) = (units[toIndex], units[fromIndex]);

        _vBoxContainer.MoveChild(uiBehaviorUnit, toIndex);
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

    public override string ToString()
    {
        return $"{nameof(UiBehaviorState)}:{BehaviorState}";
    }
}