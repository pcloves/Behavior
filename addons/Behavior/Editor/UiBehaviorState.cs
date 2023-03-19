using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorState : VBoxContainer
{
    public BehaviorState BehaviorState { get; set; }
    private TextureButton _removeButton;
    private Button _showButton;
    private Label _label;
    private bool _isExpand = true;

    public override void _Ready()
    {
        _removeButton= GetNode<TextureButton>("%Remove");
        _removeButton.Pressed += OnRemoveButtonPressed;
        
        _showButton = GetNode<Button>("%Show");
        _showButton.Connect(BaseButton.SignalName.Pressed, Callable.From(Expand));

        _label = GetNode<Label>("%Label");
        _label.Text = BehaviorState?.Id;
    }

    private void OnRemoveButtonPressed()
    {
        var parent = GetParent<UiBehaviorDefine>();
        parent.BehaviorDefine.BehaviorStates.Remove(BehaviorState);
        parent.RemoveChild(this);

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