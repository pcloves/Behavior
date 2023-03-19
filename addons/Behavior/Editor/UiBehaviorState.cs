using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorState : MarginContainer
{
    private const string UiBehaviorUnitPath = "res://addons/Behavior/Editor/UiBehaviorUnit.tscn";
    private static readonly PackedScene UiBehaviorUnitPackedScene = ResourceLoader.Load<PackedScene>(UiBehaviorUnitPath);
    
    public BehaviorState BehaviorState { get; set; }

    private VBoxContainer _vBoxContainer;
    private TextureButton _removeButton;
    private Button _showButton;
    private Button _newButton;
    private Label _label;
    private bool _isExpand = true;

    public override void _Ready()
    {
        _vBoxContainer = GetNode<VBoxContainer>("%VBoxContainer");
        
        _removeButton= GetNode<TextureButton>("%Remove");
        _removeButton.Pressed += OnRemoveButtonPressed;
        
        _showButton = GetNode<Button>("%Show");
        _showButton.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _newButton = GetNode<Button>("%New");
        _newButton.Connect(BaseButton.SignalName.Pressed, Callable.From(NewBehaviorUnit));
        
        _label = GetNode<Label>("%Label");
        _label.Text = BehaviorState?.Id;
    }

    private void NewBehaviorUnit()
    {
        var uiBehaviorUnit = UiBehaviorUnitPackedScene.Instantiate<UiBehaviorUnit>();
        var behaviorUnit = new BehaviorUnit();

        uiBehaviorUnit.BehaviorUnit = behaviorUnit;
        BehaviorState.Units.Add(behaviorUnit);
        
        _vBoxContainer.AddChildBefore(uiBehaviorUnit, _newButton);
    }

    private void OnRemoveButtonPressed()
    {
        var owner = GetOwnerOrNull<UiBehaviorDefine>();
        
        owner.BehaviorDefine.BehaviorStates.Remove(BehaviorState);
        
        GetParent().RemoveChild(this);

        QueueFree();
    }

    private void Expand()
    {
        _isExpand = !_isExpand;

        foreach (Control child in GetChildren())
        {
            if (!_isExpand && child.HasMeta("no_hide") && child.GetMeta("no_hide").AsBool()) continue;

            child.Visible = _isExpand;
        }
    }
}